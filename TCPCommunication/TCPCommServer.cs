using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPCommunication
{
    using CommonReferences;
    // Delegat pentru evenimentul de primire a unui semnal nou
    public delegate void NewSignalReceived(SensorBase sensorValue);
    // Clasa pentru serverul TCP
    public class TCPCommServer : IDisposable
    {
        private int _port = 50000; // Portul de comunicare pentru serverul TCP/IP
        private string _thisServerIP = "192.168.196.105"; // Adresa IP a acestui server

        protected TcpListener server = null!; // Obiect pentru ascultarea conexiunilor TCP
        protected List<Thread> ServerThreadList = new List<Thread>(); // Listă pentru gestionarea firelor de execuție ale serverului

        private bool _isRunning = false; // Flag pentru a indica dacă serverul este în execuție
        public event NewSignalReceived? newSignalReceivedEvent; // Eveniment pentru semnal nou primit

        // Metodă pentru eliberarea resurselor utilizate de server
        public void Dispose()
        {
            try
            {
                foreach (Thread thread in ServerThreadList) // Parcurge lista de fire de execuție
                {
                    if (thread.IsAlive) // Verifică dacă firul este activ
                    {
                        thread.Join(); // Așteaptă finalizarea firului
                    }
                }
                ServerThreadList.Clear(); // Golește lista de fire
            }
            catch (Exception ex)
            {
                throw new Exception("TCP client error on disposing the threads ->" + ex.Message); // Aruncă o excepție în caz de eroare
            }
        }

        // Metodă pentru pornirea serverului TCP
        private void StartTCPServer()
        {
            if (_isRunning) return; // Dacă serverul este deja pornit, ieșim din metodă
            Thread newThread = new Thread(StartInNewThread); // Creează un nou fir de execuție pentru server
            ServerThreadList.Add(newThread); // Adaugă firul în lista de fire
            newThread.Start(); // Pornește firul
        }

        // Metodă pentru pornirea serverului într-un fir nou
        private void StartInNewThread()
        {
            server = new TcpListener(IPAddress.Parse(_thisServerIP), _port); // Inițializează serverul cu adresa IP și portul specificat
            server.Start(); // Pornește serverul
            Console.WriteLine($"Server is running: {_isRunning}"); // Afișează starea serverului
            Console.WriteLine($"Server started on {_thisServerIP}:{_port}"); // Afișează mesajul de pornire a serverului

            while (_isRunning) // Buclă principală a serverului
            {
                Console.WriteLine("Server is now listening..."); // Afișează mesajul de ascultare
                if (server.Pending()) // Verifică dacă există conexiuni în așteptare
                {
                    Console.WriteLine("Client connection detected."); // Afișează mesajul de detectare a unui client
                    TcpClient tempClient = server.AcceptTcpClient(); // Acceptă conexiunea clientului
                    Console.WriteLine($"Client connected from {tempClient.Client.RemoteEndPoint}"); // Afișează adresa clientului conectat

                    RemoveClosedThreadsFromList(); // Curăță lista de fire închise

                    Thread newThread = new Thread(new ParameterizedThreadStart(ClientThread)); // Creează un nou fir pentru client
                    ServerThreadList.Add(newThread); // Adaugă firul în lista de fire
                    newThread.Start(tempClient); // Pornește firul cu clientul ca parametru
                }

                Thread.Sleep(100); // Așteaptă 100 ms pentru a preveni utilizarea excesivă a procesorului
            }

            server.Stop(); // Oprește serverul
        }

        // Metodă pentru eliminarea firelor închise din listă
        private void RemoveClosedThreadsFromList()
        {
            List<Thread> myAliveThreadList = new List<Thread>(); // Creează o listă temporară pentru firele active
            foreach (Thread thread in ServerThreadList) // Parcurge lista de fire
            {
                if (thread.IsAlive) // Verifică dacă firul este activ
                {
                    myAliveThreadList.Add(thread); // Adaugă firul activ în lista temporară
                }
            }
            ServerThreadList = myAliveThreadList; // Actualizează lista originală cu firele active
        }

        // Metodă pentru oprirea serverului TCP
        public void CloseTCPServer()
        {
            if (server != null) // Verifică dacă serverul este inițializat
            {
                server.Stop(); // Oprește serverul
            }
            foreach (Thread thread in ServerThreadList) // Parcurge lista de fire
            {
                if (thread.IsAlive) // Verifică dacă firul este activ
                {
                    thread.Join(); // Așteaptă finalizarea firului
                }
            }
            server = null!; // Resetează serverul la null
        }

        // Metodă pentru gestionarea unui client într-un fir separat
        public void ClientThread(object? clientData)
        {
            if (clientData is null) return; // Dacă datele clientului sunt null, ieșim din metodă
            TcpClient client = (TcpClient)clientData; // Convertim datele la TcpClient
            NetworkStream stream = client.GetStream(); // Obținem fluxul de date al clientului
            stream.ReadTimeout = 60 * 1000; // Setăm timeout-ul pentru citire la 60 de secunde

            List<byte> signalValueInBytes = new List<byte>(); // Listă pentru stocarea datelor primite
            int bufData = 0; // Variabilă pentru stocarea datelor temporare

            try
            {
                while (client.Connected && stream.DataAvailable) // Cât timp clientul este conectat și există date disponibile
                {
                    bufData = stream.ReadByte(); // Citim un byte de la client
                    if (bufData == -1) break; // Dacă nu mai sunt date, ieșim din buclă
                    signalValueInBytes.Add((byte)bufData); // Adăugăm byte-ul în listă
                }

                Console.WriteLine($"Raw Data Received: {BitConverter.ToString(signalValueInBytes.ToArray())}"); // Afișăm datele brute primite

                ASCIIEncoding encoding = new ASCIIEncoding(); // Obiect pentru codificare ASCII
                string receivedText = encoding.GetString(signalValueInBytes.ToArray()); // Convertim datele brute în text

                Console.WriteLine($"Unpacked Text: {receivedText}"); // Afișăm textul decodificat

                UnpackSignalAndRaiseTheEvent(receivedText); // Decodificăm textul și declanșăm evenimentul
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}"); // Afișăm mesajul de eroare
                throw new Exception("TCP server error on receiving data from the client ->", ex); // Aruncăm o excepție
            }
        }

        // Metodă pentru decodificarea semnalului și declanșarea evenimentului
        private void UnpackSignalAndRaiseTheEvent(string packedSignalValues)
        {
            string strTimeStamp = string.Empty; // Variabilă pentru timestamp
            string StrSignalValue = string.Empty; // Variabilă pentru valoarea semnalului
            string strPatientCode = string.Empty; // Variabilă pentru codul pacientului
            string signalName = string.Empty; // Variabilă pentru numele semnalului

            string[] ValuesList = packedSignalValues.Split('#'); // Împărțim semnalele primite după delimitatorul '#'
            foreach (string packedSignalValue in ValuesList) // Parcurgem fiecare semnal
            {
                if (packedSignalValue.Length > 0) // Dacă semnalul nu este gol
                {
                    string[] valueFields = packedSignalValue.Split(','); // Împărțim semnalul în câmpuri
                    signalName = valueFields[0]; // Obținem numele semnalului
                    SensorType sensorType = (SensorType)Enum.Parse(typeof(SensorType), signalName); // Convertim numele în tip de senzor
                    strTimeStamp = valueFields[1]; // Obținem timestamp-ul
                    DateTime timeStamp;
                    DateTime.TryParse(strTimeStamp, out timeStamp); // Convertim timestamp-ul în DateTime
                    strPatientCode = valueFields[2]; // Obținem codul pacientului
                    StrSignalValue = valueFields[3]; // Obținem valoarea semnalului

                    string[] dataValuesList = valueFields[3].Split(';'); // Împărțim valorile semnalului
                    List<double> dataValueList = new List<double>(); // Listă pentru valorile semnalului
                    try
                    {
                        foreach (string currDataValue in dataValuesList) // Parcurgem fiecare valoare
                        {
                            string strDataValue = (currDataValue.TrimStart('[')).TrimEnd(']'); // Eliminăm caracterele '[' și ']'
                            double doubleValue;
                            Double.TryParse(strDataValue, out doubleValue); // Convertim valoarea în double
                            dataValueList.Add(doubleValue); // Adăugăm valoarea în listă
                        }

                        SendNewDataReceivedEvent(new SensorBase(strPatientCode, sensorType, dataValueList[0], timeStamp)); // Trimitem datele prin eveniment
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error when unpacking the text received from TCP client ", ex); // Aruncăm o excepție în caz de eroare
                    }
                }
            }
        }

        // Metodă pentru declanșarea evenimentului de semnal nou primit
        protected void SendNewDataReceivedEvent(SensorBase e)
        {
            if (this.newSignalReceivedEvent != null) // Verificăm dacă există abonați la eveniment
            {
                this.newSignalReceivedEvent(e); // Declanșăm evenimentul
            }
        }

        #region properties
        // Proprietate pentru verificarea stării serverului
        public bool IsRunning
        {
            get { return _isRunning; } // Returnează starea serverului
        }
        #endregion

        #region Constructors
        // Constructor pentru inițializarea serverului
        public TCPCommServer()
        {
            try
            {
                StartTCPServer(); // Pornește serverul
                _isRunning = true; // Setează flag-ul de execuție
            }
            catch (Exception ex)
            {
                throw new Exception("TCP server error on starting the server ->" + ex.Message); // Aruncă o excepție în caz de eroare
            }
        }
        #endregion
    }
}
