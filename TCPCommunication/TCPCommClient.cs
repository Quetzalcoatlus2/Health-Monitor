using System.Net.Sockets;
using System.Text;
using System.Globalization; // Ensure this is present
using System.Threading;

namespace TCPCommunication
{
    public class TCPCommClient : IDisposable // TCP client class, implements IDisposable for resource management
    {
        private string _serverIP = String.Empty; // Server IP address, initialized with empty string
        protected bool _running = false; // Indicates whether client is running
        protected int _port = 50000; // Port used for TCP connection
        protected List<Thread> myThreadList = new List<Thread>(); // List for managing threads

        // Method for releasing client resources
        public void Dispose()
        {
            try
            {
                foreach (Thread thread in myThreadList) // Iterates through thread list
                {
                    if (thread.IsAlive) // Checks whether thread is alive
                    {
                        thread.Join(); // Waits for thread completion
                    }
                }
                myThreadList.Clear(); // Clears thread list
            }
            catch (Exception ex)
            {
                throw new Exception("TCP client error on disposing the threads ->" + ex.Message); // Throws exception on error
            }
        }

        // Method for sending signal data to server
        public void SendSignalData(string patientCode, string key, DateTime timeStamp, double signalValue)
        {
            try
            {
                // Builds string containing signal data
                string signalValuePackedIntoString = BuildPacketStringSignalValue(patientCode, key, timeStamp, signalValue);
                this.SendSignalText(signalValuePackedIntoString); // Sends string to server
            }
            catch (Exception ex)
            {
                throw new Exception("TCP client error on sending signalValue to the server system ->" + ex.Message); // Throws exception on error
            }
        }

        // Method for building a packet string
        private string BuildPacketStringSignalValue(string patientCode, string key, DateTime timeStamp, double signalValue)
        {
            StringBuilder builder = new StringBuilder(); // Creates StringBuilder for string construction

            builder.Append("#"); // Adds start delimiter
            builder.Append(key); // Adds key
            builder.Append("," + timeStamp.ToString("o")); // Adds timestamp in ISO 8601 format
            builder.Append("," + patientCode); // Adds patient code
            builder.Append("," + signalValue.ToString("0.00", CultureInfo.InvariantCulture)); // Adds signal value formatted to 2 decimals
            builder.Append("#"); // Adds end delimiter

            return builder.ToString(); // Returns built string
        }

        // Method for sending signal text to server
        private void SendSignalText(String signalText)
        {
            try
            {
                RemoveClosedThreadsFromList(); // Cleans closed thread list
                Thread newThread = new Thread(new ParameterizedThreadStart(SendSignalTextNewThread)); // Creates a new thread
                myThreadList.Add(newThread); // Adds thread to list
                newThread.Start(signalText); // Starts thread cu textul de semnal ca parametru
            }
            catch (Exception ex)
            {
                throw new Exception("TCP client error on sending signalValue to the server system ->" + ex.Message); // Throws exception on error
            }
        }

        // Method for removing closed threads from list
        private void RemoveClosedThreadsFromList()
        {
            try
            {
                List<Thread> myAliveThreadList = new List<Thread>(); // Creates temporary list for active threads
                foreach (Thread thread in myThreadList) // Iterates through thread list
                {
                    if (thread.IsAlive) // Checks whether thread is alive
                    {
                        myAliveThreadList.Add(thread); // Adds active thread to temporary list
                    }
                }
                myThreadList = myAliveThreadList; // Updates original list with active threads
            }
            catch (Exception ex)
            {
                throw new Exception("TCP client error when trying to close the unused threads ->" + ex.Message); // Throws exception on error
            }
        }

        // Method for sending signal text on separate thread
        private void SendSignalTextNewThread(object? signalTextObject)
        {
            string? signalText = signalTextObject as string; // Converts object to string
            if (signalText == null) return; // If text is null, exits execution

            if (!IsServerReachable(_serverIP)) // Checks if server is reachable
            {
                Console.WriteLine($"Server {_serverIP} is not reachable."); // Displays an error message
                throw new Exception($"Server {_serverIP} is not reachable."); // Throws an exception
            }

            int retryCount = 3; // Maximum number of retries
            int delay = 2000; // Delay between retries (2 seconds)

            for (int attempt = 1; attempt <= retryCount; attempt++) // Loop for multiple attempts
            {
                try
                {
                    using TcpClient myTCPClient = new TcpClient(_serverIP, _port); // Creates a TCP client
                    NetworkStream stream = myTCPClient.GetStream(); // Gets client data stream
                    byte[] buffer = Encoding.ASCII.GetBytes(signalText); // Encodes text in ASCII format
                    stream.Write(buffer, 0, buffer.Length); // Sends data through stream
                    Console.WriteLine($"Data sent successfully: {signalText}"); // Displays success message
                    return; // Exits method on success
                }
                catch (SocketException ex)
                {
                    Console.WriteLine($"Attempt {attempt} failed: {ex.Message}"); // Displays an error message
                    if (attempt == retryCount) // If this is the last attempt
                    {
                        throw new Exception("Failed to send data after multiple attempts.", ex); // Throws an exception
                    }
                    Thread.Sleep(delay); // Waits before retrying
                }
            }
        }

        // Method for checking server reachability
        private bool IsServerReachable(string serverIP)
        {
            try
            {
                using var ping = new System.Net.NetworkInformation.Ping(); // Creates a Ping object
                var reply = ping.Send(serverIP); // Sends a ping to server
                return reply.Status == System.Net.NetworkInformation.IPStatus.Success; // Returns true if server responds
            }
            catch
            {
                return false; // Returns false on error
            }
        }

        #region Constructors

        // Private default constructor
        public TCPCommClient()
        {

        }

        // Constructor initializing server IP address
        public TCPCommClient(string serverIP)
        {
            _serverIP = "192.168.0.105"; // Sets server IP address
        }

        #endregion
    }




}
