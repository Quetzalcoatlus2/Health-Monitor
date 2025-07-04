using System.Net.Sockets;
using System.Text;
using System.Globalization; // Ensure this is present
using System.Threading;

namespace TCPCommunication
{
    public class TCPCommClient : IDisposable // Clasa pentru clientul TCP, implementează IDisposable pentru gestionarea resurselor
    {
        private string _serverIP = String.Empty; // Adresa IP a serverului, inițializată cu un șir gol
        protected bool _running = false; // Indică dacă clientul este în execuție
        protected int _port = 50000; // Portul utilizat pentru conexiunea TCP
        protected List<Thread> myThreadList = new List<Thread>(); // Listă pentru gestionarea firelor de execuție

        // Metodă pentru eliberarea resurselor utilizate de client
        public void Dispose()
        {
            try
            {
                foreach (Thread thread in myThreadList) // Parcurge lista de fire de execuție
                {
                    if (thread.IsAlive) // Verifică dacă firul este activ
                    {
                        thread.Join(); // Așteaptă finalizarea firului
                    }
                }
                myThreadList.Clear(); // Golește lista de fire
            }
            catch (Exception ex)
            {
                throw new Exception("TCP client error on disposing the threads ->" + ex.Message); // Aruncă o excepție în caz de eroare
            }
        }

        // Metodă pentru trimiterea datelor de semnal către server
        public void SendSignalData(string patientCode, string key, DateTime timeStamp, double signalValue)
        {
            try
            {
                // Construiește un șir de caractere care conține datele semnalului
                string signalValuePackedIntoString = BuildPacketStringSignalValue(patientCode, key, timeStamp, signalValue);
                this.SendSignalText(signalValuePackedIntoString); // Trimite șirul de caractere către server
            }
            catch (Exception ex)
            {
                throw new Exception("TCP client error on sending signalValue to the server system ->" + ex.Message); // Aruncă o excepție în caz de eroare
            }
        }

        // Metodă pentru construirea unui pachet de date sub formă de șir de caractere
        private string BuildPacketStringSignalValue(string patientCode, string key, DateTime timeStamp, double signalValue)
        {
            StringBuilder builder = new StringBuilder(); // Creează un StringBuilder pentru construirea șirului

            builder.Append("#"); // Adaugă un delimitator de început
            builder.Append(key); // Adaugă cheia
            builder.Append("," + timeStamp.ToString("o")); // Adaugă timestamp-ul în format ISO 8601
            builder.Append("," + patientCode); // Adaugă codul pacientului
            builder.Append("," + signalValue.ToString("0.00", CultureInfo.InvariantCulture)); // Adaugă valoarea semnalului formatată cu 2 zecimale
            builder.Append("#"); // Adaugă un delimitator de sfârșit

            return builder.ToString(); // Returnează șirul construit
        }

        // Metodă pentru trimiterea unui text de semnal către server
        private void SendSignalText(String signalText)
        {
            try
            {
                RemoveClosedThreadsFromList(); // Curăță lista de fire închise
                Thread newThread = new Thread(new ParameterizedThreadStart(SendSignalTextNewThread)); // Creează un nou fir de execuție
                myThreadList.Add(newThread); // Adaugă firul în listă
                newThread.Start(signalText); // Pornește firul cu textul de semnal ca parametru
            }
            catch (Exception ex)
            {
                throw new Exception("TCP client error on sending signalValue to the server system ->" + ex.Message); // Aruncă o excepție în caz de eroare
            }
        }

        // Metodă pentru eliminarea firelor închise din listă
        private void RemoveClosedThreadsFromList()
        {
            try
            {
                List<Thread> myAliveThreadList = new List<Thread>(); // Creează o listă temporară pentru firele active
                foreach (Thread thread in myThreadList) // Parcurge lista de fire
                {
                    if (thread.IsAlive) // Verifică dacă firul este activ
                    {
                        myAliveThreadList.Add(thread); // Adaugă firul activ în lista temporară
                    }
                }
                myThreadList = myAliveThreadList; // Actualizează lista originală cu firele active
            }
            catch (Exception ex)
            {
                throw new Exception("TCP client error when trying to close the unused threads ->" + ex.Message); // Aruncă o excepție în caz de eroare
            }
        }

        // Metodă pentru trimiterea textului de semnal pe un fir separat
        private void SendSignalTextNewThread(object? signalTextObject)
        {
            string? signalText = signalTextObject as string; // Convertirea obiectului la șir de caractere
            if (signalText == null) return; // Dacă textul este null, se oprește execuția

            if (!IsServerReachable(_serverIP)) // Verifică dacă serverul este accesibil
            {
                Console.WriteLine($"Server {_serverIP} is not reachable."); // Afișează un mesaj de eroare
                throw new Exception($"Server {_serverIP} is not reachable."); // Aruncă o excepție
            }

            int retryCount = 3; // Numărul maxim de încercări
            int delay = 2000; // Întârziere între încercări (2 secunde)

            for (int attempt = 1; attempt <= retryCount; attempt++) // Buclă pentru încercări multiple
            {
                try
                {
                    using TcpClient myTCPClient = new TcpClient(_serverIP, _port); // Creează un client TCP
                    NetworkStream stream = myTCPClient.GetStream(); // Obține fluxul de date al clientului
                    byte[] buffer = Encoding.ASCII.GetBytes(signalText); // Codifică textul în format ASCII
                    stream.Write(buffer, 0, buffer.Length); // Trimite datele pe flux
                    Console.WriteLine($"Data sent successfully: {signalText}"); // Afișează un mesaj de succes
                    return; // Iese din metodă la succes
                }
                catch (SocketException ex)
                {
                    Console.WriteLine($"Attempt {attempt} failed: {ex.Message}"); // Afișează un mesaj de eroare
                    if (attempt == retryCount) // Dacă este ultima încercare
                    {
                        throw new Exception("Failed to send data after multiple attempts.", ex); // Aruncă o excepție
                    }
                    Thread.Sleep(delay); // Așteaptă înainte de a încerca din nou
                }
            }
        }

        // Metodă pentru verificarea accesibilității serverului
        private bool IsServerReachable(string serverIP)
        {
            try
            {
                using var ping = new System.Net.NetworkInformation.Ping(); // Creează un obiect Ping
                var reply = ping.Send(serverIP); // Trimite un ping către server
                return reply.Status == System.Net.NetworkInformation.IPStatus.Success; // Returnează true dacă serverul răspunde
            }
            catch
            {
                return false; // Returnează false în caz de eroare
            }
        }

        #region Constructors

        // Constructor implicit privat
        public TCPCommClient()
        {

        }

        // Constructor care inițializează adresa IP a serverului
        public TCPCommClient(string serverIP)
        {
            _serverIP = "192.168.0.105"; // Setează adresa IP a serverului
        }

        #endregion
    }




}
