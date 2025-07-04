using System; // Recommended to be explicit
using CommonReferences; // Ensure this is present

// Clasa principală a aplicației
class Program
{
    // Punctul de intrare al aplicației
    static void Main(string[] args)
    {
        Console.WriteLine("Instantierea clasei SensorValue\n\r"); // Afișează un mesaj de inițializare

        // Instanțiere folosind constructorul implicit. Valorile de măsurare se setează folosind proprietățile
        SensorValue sensor1 = new SensorValue(); // Creează un obiect de tip SensorValue
        sensor1.Type = SensorType.BloodGlucose; // Setează tipul senzorului la "BloodGlucose"
        sensor1.TimeStamp = DateTime.Now; // Setează timestamp-ul la momentul curent
        sensor1.Value = 100; // Setează valoarea măsurată la 100 mg/dl
        DisplaySensorValues("Primul senzor initializat ", sensor1); // Afișează valorile senzorului

        // Instanțiere folosind constructorul cu parametri
        SensorValue sensor2 = new SensorValue(SensorType.SkinTemperature, 36.7, "23-Jan-10 14:56:00"); // Creează un obiect cu valori predefinite
        DisplaySensorValues("Al doilea senzor initializat ", sensor2); // Afișează valorile senzorului

        // Creează un obiect de tip PumpSensorValues pentru gestionarea pompei
        PumpSensorValues sensorValuesPump = new PumpSensorValues(3); // Creează o pompă cu 3 senzori
        sensorValuesPump.StartPumping(); // Pornește pompa
        Console.WriteLine("Pump running... Press Enter to stop."); // Afișează un mesaj pentru utilizator
        Console.ReadLine(); // Așteaptă ca utilizatorul să apese Enter
        sensorValuesPump.StopPumping(); // Oprește pompa
        Console.WriteLine("Pump stopped."); // Afișează un mesaj că pompa a fost oprită
    }

    // Metodă pentru afișarea valorilor unui senzor
    internal static void DisplaySensorValues(string headerText, SensorValue sensor)
    {
        Console.WriteLine("\t " + headerText); // Afișează un text de antet
        Console.WriteLine("\t\t Type = {0} ", sensor.Type.ToString()); // Afișează tipul senzorului
        Console.WriteLine("\t\t TimeStamp = {0} ", sensor.TimeStampString); // Afișează timestamp-ul senzorului
        Console.WriteLine("\t\t Value = {0} ", sensor.Value.ToString("0.00")); // Afișează valoarea senzorului formatată cu 2 zecimale
    }
}
