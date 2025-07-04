using System.Configuration;
using System.Data.SQLite;
using System.Data;
using System.Net; // Add this namespace
using CommonReferences; // Ensure this is present

namespace DataStore
{
    public class DAL_PatientData // Clasa pentru accesarea și manipularea datelor pacientului în baza de date
    {
        private string _thisServerIP = IPAddress.Any.ToString(); // Adresa IP a serverului, setată implicit la "localhost"

        public static void AddData(SensorBase sensorData) // Metodă pentru a adăuga datele unui senzor în baza de date
        {
            // Creează o conexiune la baza de date folosind string-ul de conexiune din fișierul de configurare
            SQLiteConnection conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["ConnStringSQLite"].ConnectionString);
            SQLiteCommand cmd = conn.CreateCommand(); // Creează o comandă SQL
            cmd.CommandType = CommandType.Text; // Setează tipul comenzii la text SQL
            cmd.CommandText = "insert into PatientData (id, patient_code, sensor_type, timestamp, value ) " +
                " values(:id, :patient_code, :sensor_type, :timestamp, :value) "; // Comandă SQL pentru inserarea datelor

            // Adaugă parametrii necesari pentru comanda SQL
            cmd.Parameters.Add(":id", DbType.String).Value = Guid.NewGuid().ToString(); // Generează un ID unic
            cmd.Parameters.Add(":patient_code", DbType.String).Value = sensorData.PatientCode; // Codul pacientului
            cmd.Parameters.Add(":sensor_type", DbType.String).Value = sensorData.SensorType; // Tipul senzorului
            cmd.Parameters.Add(":timestamp", DbType.DateTime).Value = sensorData.Timestamp.ToString("o"); // Timpul în format ISO 8601
            cmd.Parameters.Add(":value", DbType.Double).Value = sensorData.Value; // Valoarea măsurată de senzor

            try
            {
                cmd.Connection.Open(); // Deschide conexiunea la baza de date
                cmd.ExecuteNonQuery(); // Execută comanda SQL
            }
            catch (Exception ex)
            {
                // Aruncă o excepție cu un mesaj detaliat în caz de eroare
                throw new Exception(ex.Message + "Error adding PatientData");
            }
            finally
            {
                cmd.Connection.Close(); // Închide conexiunea la baza de date
            }
        }

        public enum PatCodeEnum // Enumerație pentru codurile pacienților
        {
            Patient_01, // Pacient 01
            Patient_02, // Pacient 02
            Patient_03, // Pacient 03
            Patient_04, // Pacient 04
            Patient_05, // Pacient 05
            Patient_06  // Pacient 06
        }

        public static List<SensorBase> GetData(PatCodeEnum patCode, DateTime currDay) // Metodă pentru a obține datele unui pacient pentru o anumită zi
        {
            List<SensorBase> sensorDataList = new List<SensorBase>(); // Listă pentru a stoca datele senzorilor
            SQLiteConnection conn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["ConnStringSQLite"].ConnectionString); // Creează conexiunea la baza de date
            SQLiteCommand cmd = conn.CreateCommand(); // Creează o comandă SQL
            cmd.CommandType = CommandType.Text; // Setează tipul comenzii la text SQL
            cmd.CommandText = "select id, patient_code, sensor_type, timestamp, value from PatientData " +
                "where patient_code = :patient_code and timestamp >= :minTime and timestamp < :maxTime"; // Comandă SQL pentru selectarea datelor

            // Adaugă parametrii necesari pentru comanda SQL
            cmd.Parameters.Add(":patient_code", DbType.String).Value = patCode.ToString(); // Codul pacientului (convertit la string)
            cmd.Parameters.Add(":minTime", DbType.DateTime).Value = currDay.Date; // Timpul minim (începutul zilei)
            cmd.Parameters.Add(":maxTime", DbType.DateTime).Value = currDay.Date.AddDays(1); // Timpul maxim (sfârșitul zilei)

            SQLiteDataReader? reader = null; // Cititor pentru a parcurge rezultatele interogării

            try
            {
                cmd.Connection.Open(); // Deschide conexiunea la baza de date
                reader = cmd.ExecuteReader(); // Execută comanda și obține rezultatele
                while (reader.Read()) // Parcurge fiecare rând din rezultatele interogării
                {
                    // Creează un obiect SensorBase cu datele din rândul curent
                    SensorBase pItem = new SensorBase(
                        (string)reader["patient_code"], // Codul pacientului
                        (SensorType)Enum.Parse(typeof(SensorType), (string)reader["sensor_type"]), // Tipul senzorului
                        Convert.ToDouble(reader["value"]), // Valoarea măsurată
                        (DateTime)reader["timestamp"] // Timpul măsurării
                    );
                    sensorDataList.Add(pItem); // Adaugă obiectul în listă
                }
            }
            catch (Exception ex)
            {
                // Aruncă o excepție cu un mesaj detaliat în caz de eroare
                throw new Exception("Error on reading from PatientData table " + ex.Message);
            }
            finally
            {
                if (reader != null) reader.Close(); // Închide cititorul dacă este deschis
                cmd.Connection.Close(); // Închide conexiunea la baza de date
            }
            return sensorDataList; // Returnează lista cu datele senzorilor
        }
    }
}
