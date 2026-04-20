using System.Configuration;
using System.Data.SQLite;
using System.Data;
using System.Net; // Add this namespace
using CommonReferences; // Ensure this is present

namespace DataStore
{
    public class DAL_PatientData // Class for accessing and handling patient data in the database
    {
        private string _thisServerIP = IPAddress.Any.ToString(); // Server IP address, defaulted to "localhost"

        public static void AddData(SensorBase sensorData) // Method to add sensor data into the database
        {
            // Creates a database connection using the connection string from config
            SQLiteConnection conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["ConnStringSQLite"].ConnectionString);
            SQLiteCommand cmd = conn.CreateCommand(); // Creates an SQL command
            cmd.CommandType = CommandType.Text; // Sets command type to SQL text
            cmd.CommandText = "insert into PatientData (id, patient_code, sensor_type, timestamp, value ) " +
                " values(:id, :patient_code, :sensor_type, :timestamp, :value) "; // SQL command for data insertion

            // Adds required parameters for SQL command
            cmd.Parameters.Add(":id", DbType.String).Value = Guid.NewGuid().ToString(); // Generates a unique ID
            cmd.Parameters.Add(":patient_code", DbType.String).Value = sensorData.PatientCode; // Patient code
            cmd.Parameters.Add(":sensor_type", DbType.String).Value = sensorData.SensorType; // Sensor type
            cmd.Parameters.Add(":timestamp", DbType.DateTime).Value = sensorData.Timestamp.ToString("o"); // Time in ISO 8601 format
            cmd.Parameters.Add(":value", DbType.Double).Value = sensorData.Value; // Measured sensor value

            try
            {
                cmd.Connection.Open(); // Opens database connection
                cmd.ExecuteNonQuery(); // Executes SQL command
            }
            catch (Exception ex)
            {
                // Throws exception with detailed message on error
                throw new Exception(ex.Message + "Error adding PatientData");
            }
            finally
            {
                cmd.Connection.Close(); // Closes database connection
            }
        }

        public enum PatCodeEnum // Enumeration for patient codes
        {
            Patient_01, // Patient 01
            Patient_02, // Patient 02
            Patient_03, // Patient 03
            Patient_04, // Patient 04
            Patient_05, // Patient 05
            Patient_06  // Patient 06
        }

        public static List<SensorBase> GetData(PatCodeEnum patCode, DateTime currDay) // Method to get patient data for a specific day
        {
            List<SensorBase> sensorDataList = new List<SensorBase>(); // List for storing sensor data
            SQLiteConnection conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["ConnStringSQLite"].ConnectionString); // Creates database connection
            SQLiteCommand cmd = conn.CreateCommand(); // Creates an SQL command
            cmd.CommandType = CommandType.Text; // Sets command type to SQL text
            cmd.CommandText = "select id, patient_code, sensor_type, timestamp, value from PatientData " +
                "where patient_code = :patient_code and timestamp >= :minTime and timestamp < :maxTime"; // SQL command for selecting data

            // Adds required parameters for SQL command
            cmd.Parameters.Add(":patient_code", DbType.String).Value = patCode.ToString(); // Patient code (converted to string)
            cmd.Parameters.Add(":minTime", DbType.DateTime).Value = currDay.Date; // Minimum time (start of day)
            cmd.Parameters.Add(":maxTime", DbType.DateTime).Value = currDay.Date.AddDays(1); // Maximum time (end of day)

            SQLiteDataReader? reader = null; // Reader for traversing query results

            try
            {
                cmd.Connection.Open(); // Opens database connection
                reader = cmd.ExecuteReader(); // Executes command and gets results
                while (reader.Read()) // Iterates each row in query results
                {
                    // Creates a SensorBase object from current row data
                    SensorBase pItem = new SensorBase(
                        (string)reader["patient_code"], // Patient code
                        (SensorType)Enum.Parse(typeof(SensorType), (string)reader["sensor_type"]), // Sensor type
                        Convert.ToDouble(reader["value"]), // Measured value
                        (DateTime)reader["timestamp"] // Measurement time
                    );
                    sensorDataList.Add(pItem); // Adds object to list
                }
            }
            catch (Exception ex)
            {
                // Throws exception with detailed message on error
                throw new Exception("Error on reading from PatientData table " + ex.Message);
            }
            finally
            {
                if (reader != null) reader.Close(); // Closes reader if open
                cmd.Connection.Close(); // Closes database connection
            }
            return sensorDataList; // Returns sensor data list
        }
    }
}
