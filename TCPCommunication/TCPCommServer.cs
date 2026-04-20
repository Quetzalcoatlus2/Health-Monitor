using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPCommunication
{
    using CommonReferences;
    // Delegate for new signal received event
    public delegate void NewSignalReceived(SensorBase sensorValue);
    // TCP server class
    public class TCPCommServer : IDisposable
    {
        private int _port = 50000; // Communication port for TCP/IP server
        private string _thisServerIP = "192.168.196.105"; // This server IP address

        protected TcpListener server = null!; // Object for listening to TCP connections
        protected List<Thread> ServerThreadList = new List<Thread>(); // List for managing server threads

        private bool _isRunning = false; // Flag indicating whether server is running
        public event NewSignalReceived? newSignalReceivedEvent; // Event for newly received signal

        // Method for releasing resources used by server
        public void Dispose()
        {
            try
            {
                foreach (Thread thread in ServerThreadList) // Iterates through thread list
                {
                    if (thread.IsAlive) // Checks whether thread is alive
                    {
                        thread.Join(); // Waits for thread completion
                    }
                }
                ServerThreadList.Clear(); // Clears thread list
            }
            catch (Exception ex)
            {
                throw new Exception("TCP client error on disposing the threads ->" + ex.Message); // Throws exception on error
            }
        }

        // Method for starting TCP server
        private void StartTCPServer()
        {
            if (_isRunning) return; // If server is already started, exit method
            Thread newThread = new Thread(StartInNewThread); // Creates a new server thread
            ServerThreadList.Add(newThread); // Adds thread to thread list
            newThread.Start(); // Starts thread
        }

        // Method for starting server in a new thread
        private void StartInNewThread()
        {
            server = new TcpListener(IPAddress.Parse(_thisServerIP), _port); // Initializes server with specific IP and port
            server.Start(); // Starts server
            Console.WriteLine($"Server is running: {_isRunning}"); // Displays server status
            Console.WriteLine($"Server started on {_thisServerIP}:{_port}"); // Displays server start message

            while (_isRunning) // Server main loop
            {
                Console.WriteLine("Server is now listening..."); // Displays listening message
                if (server.Pending()) // Checks for pending connections
                {
                    Console.WriteLine("Client connection detected."); // Displays client detection message
                    TcpClient tempClient = server.AcceptTcpClient(); // Accepts client connection
                    Console.WriteLine($"Client connected from {tempClient.Client.RemoteEndPoint}"); // Displays connected client address

                    RemoveClosedThreadsFromList(); // Cleans closed thread list

                    Thread newThread = new Thread(new ParameterizedThreadStart(ClientThread)); // Creates a new client thread
                    ServerThreadList.Add(newThread); // Adds thread to thread list
                    newThread.Start(tempClient); // Starts thread with client as parameter
                }

                Thread.Sleep(100); // Waits 100ms to prevent excessive CPU usage
            }

            server.Stop(); // Stops server
        }

        // Method for removing closed threads from list
        private void RemoveClosedThreadsFromList()
        {
            List<Thread> myAliveThreadList = new List<Thread>(); // Creates temporary list for active threads
            foreach (Thread thread in ServerThreadList) // Iterates through thread list
            {
                if (thread.IsAlive) // Checks whether thread is alive
                {
                    myAliveThreadList.Add(thread); // Adds active thread to temporary list
                }
            }
            ServerThreadList = myAliveThreadList; // Updates original list with active threads
        }

        // Method for stopping TCP server
        public void CloseTCPServer()
        {
            if (server != null) // Checks whether server is initialized
            {
                server.Stop(); // Stops server
            }
            foreach (Thread thread in ServerThreadList) // Iterates through thread list
            {
                if (thread.IsAlive) // Checks whether thread is alive
                {
                    thread.Join(); // Waits for thread completion
                }
            }
            server = null!; // Resets server to null
        }

        // Method for handling a client in a separate thread
        public void ClientThread(object? clientData)
        {
            if (clientData is null) return; // If client data is null, exit method
            TcpClient client = (TcpClient)clientData; // Converts data to TcpClient
            NetworkStream stream = client.GetStream(); // Gets the client network stream
            stream.ReadTimeout = 60 * 1000; // Sets read timeout to 60 seconds

            List<byte> signalValueInBytes = new List<byte>(); // List for storing received data
            int bufData = 0; // Variable for temporary data storage

            try
            {
                while (client.Connected && stream.DataAvailable) // While client is connected and data is available
                {
                    bufData = stream.ReadByte(); // Reads one byte from client
                    if (bufData == -1) break; // If no more data, exit loop
                    signalValueInBytes.Add((byte)bufData); // Adds byte to list
                }

                Console.WriteLine($"Raw Data Received: {BitConverter.ToString(signalValueInBytes.ToArray())}"); // Displays raw received data

                ASCIIEncoding encoding = new ASCIIEncoding(); // ASCII encoding object
                string receivedText = encoding.GetString(signalValueInBytes.ToArray()); // Converts raw data to text

                Console.WriteLine($"Unpacked Text: {receivedText}"); // Displays decoded text

                UnpackSignalAndRaiseTheEvent(receivedText); // Decodes text and raises event
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}"); // Displays error message
                throw new Exception("TCP server error on receiving data from the client ->", ex); // Throws an exception
            }
        }

        // Method for unpacking signal and raising event
        private void UnpackSignalAndRaiseTheEvent(string packedSignalValues)
        {
            string strTimeStamp = string.Empty; // Variable for timestamp
            string StrSignalValue = string.Empty; // Variable for signal value
            string strPatientCode = string.Empty; // Variable for patient code
            string signalName = string.Empty; // Variable for signal name

            string[] ValuesList = packedSignalValues.Split('#'); // Splits received signals by delimiter '#'
            foreach (string packedSignalValue in ValuesList) // Iterates each signal
            {
                if (packedSignalValue.Length > 0) // If signal is not empty
                {
                    string[] valueFields = packedSignalValue.Split(','); // Splits signal into fields
                    signalName = valueFields[0]; // Gets signal name
                    SensorType sensorType = (SensorType)Enum.Parse(typeof(SensorType), signalName); // Converts name to sensor type
                    strTimeStamp = valueFields[1]; // Gets timestamp
                    DateTime timeStamp;
                    DateTime.TryParse(strTimeStamp, out timeStamp); // Converts timestamp to DateTime
                    strPatientCode = valueFields[2]; // Gets patient code
                    StrSignalValue = valueFields[3]; // Gets signal value

                    string[] dataValuesList = valueFields[3].Split(';'); // Splits signal values
                    List<double> dataValueList = new List<double>(); // List for signal values
                    try
                    {
                        foreach (string currDataValue in dataValuesList) // Iterates each value
                        {
                            string strDataValue = (currDataValue.TrimStart('[')).TrimEnd(']'); // Removes '[' and ']' characters
                            double doubleValue;
                            Double.TryParse(strDataValue, out doubleValue); // Converts value to double
                            dataValueList.Add(doubleValue); // Adds value to list
                        }

                        SendNewDataReceivedEvent(new SensorBase(strPatientCode, sensorType, dataValueList[0], timeStamp)); // Sends data through event
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error when unpacking the text received from TCP client ", ex); // Throws an exception on error
                    }
                }
            }
        }

        // Method for raising new signal received event
        protected void SendNewDataReceivedEvent(SensorBase e)
        {
            if (this.newSignalReceivedEvent != null) // Checks whether there are event subscribers
            {
                this.newSignalReceivedEvent(e); // Raises event
            }
        }

        #region properties
        // Property for checking server state
        public bool IsRunning
        {
            get { return _isRunning; } // Returns server state
        }
        #endregion

        #region Constructors
        // Constructor for initializing server
        public TCPCommServer()
        {
            try
            {
                StartTCPServer(); // Starts server
                _isRunning = true; // Sets running flag
            }
            catch (Exception ex)
            {
                throw new Exception("TCP server error on starting the server ->" + ex.Message); // Throws exception on error
            }
        }
        #endregion
    }
}
