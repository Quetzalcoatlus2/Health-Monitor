using System; // For Random, DateTime, Enum
using System.Timers; // For Timer, ElapsedEventArgs
using CommonReferences; // Ensure this is present

// Definirea unui delegat pentru evenimentul de generare a unei valori noi de senzor
public delegate void OnNewSensorValue(SensorBase sensorBaseArg);
// Clasa pentru generarea valorilor senzorilor
public class PumpSensorValues
{
    // Eveniment care se declanșează la generarea unei valori noi de senzor
    public event OnNewSensorValue? newSensorValueEvent;

    System.Timers.Timer timerBase; // Timer pentru generarea periodică a valorilor
    Random myRandom; // Generator de numere aleatoare
    string patientCode = "DefaultPatientCode"; // Codul pacientului (valoare implicită)

    // Constructor care inițializează timer-ul și generatorul de numere aleatoare
    public PumpSensorValues(int periodSecondsBetweenValues)
    {
        myRandom = new Random(); // Inițializează generatorul de numere aleatoare
        patientCode = string.Empty; // Inițializează codul pacientului cu un șir gol
        timerBase = new System.Timers.Timer(); // Creează un nou timer
        timerBase.Interval = periodSecondsBetweenValues * 1000; // Setează intervalul timer-ului în milisecunde
        timerBase.Elapsed += new ElapsedEventHandler(timerBase_Elapsed); // Asociază metoda care se execută la fiecare tick al timer-ului
    }

    // Metodă pentru pornirea generării valorilor senzorilor
    public void StartPumping()
    {
        Console.WriteLine("Starting sensor value generation..."); // Afișează un mesaj de pornire
        timerBase.Start(); // Pornește timer-ul
    }

    // Metodă pentru oprirea generării valorilor senzorilor
    public void StopPumping()
    {
        Console.WriteLine("Stopping sensor value generation..."); // Afișează un mesaj de oprire
        timerBase.Stop(); // Oprește timer-ul
    }

    // Metodă care se execută la fiecare tick al timer-ului
    private void timerBase_Elapsed(object? sender, ElapsedEventArgs e)
    {
        int minNumber, maxNumber; // Variabile pentru limitele valorilor generate
        double valueRandom; // Variabilă pentru valoarea generată aleatoriu

        // Obține toate valorile definite în enum-ul SensorType
        SensorType[] sensorTypeValues = (SensorType[])Enum.GetValues(typeof(SensorType));
        int maxSensorIndex = sensorTypeValues.Length - 1; // Obține numărul maxim de tipuri de senzori

        // Generează un index aleatoriu pentru un tip de senzor (excluzând None)
        int typeRandomIndex = myRandom.Next(1, maxSensorIndex + 1); // Index între 1 și numărul maxim de tipuri
        SensorType sensorTypeRandom = sensorTypeValues[typeRandomIndex]; // Selectează tipul de senzor corespunzător indexului

        // Generează o valoare aleatorie pentru tipul de senzor selectat
        switch (sensorTypeRandom)
        {
            case SensorType.SkinTemperature: // Dacă tipul este SkinTemperature
                minNumber = 30; // Limita minimă
                maxNumber = 40; // Limita maximă
                valueRandom = myRandom.Next(minNumber * 10, maxNumber * 10 + 1) / 10.0; // Valoare cu o zecimală
                break;
            case SensorType.BloodGlucose: // Dacă tipul este BloodGlucose
                minNumber = 60; // Limita minimă
                maxNumber = 300; // Limita maximă
                valueRandom = myRandom.Next(minNumber, maxNumber + 1); // Valoare între limite
                break;
            case SensorType.HeartRate: // Dacă tipul este HeartRate
                minNumber = 30; // Limita minimă
                maxNumber = 200; // Limita maximă
                valueRandom = myRandom.Next(minNumber, maxNumber + 1); // Valoare între limite
                break;
            default: // Dacă tipul este necunoscut sau None
                valueRandom = 0; // Valoare implicită
                Console.WriteLine($"Warning: Generated default value for unexpected SensorType: {sensorTypeRandom}"); // Afișează un avertisment
                break;
        }

        // Creează un obiect SensorBase cu valorile generate
        SensorBase sensorRandom = new SensorBase(patientCode, sensorTypeRandom, valueRandom, DateTime.Now);

        // Declanșează evenimentul dacă există abonați
        if (newSensorValueEvent != null)
        {
            newSensorValueEvent(sensorRandom); // Invocă evenimentul
        }
    }

    // Constructor care acceptă și codul pacientului
    public PumpSensorValues(int periodSecondsBetweenValues, string patientCode)
    {
        this.patientCode = patientCode; // Inițializează codul pacientului
        myRandom = new Random(); // Inițializează generatorul de numere aleatoare
        timerBase = new System.Timers.Timer(); // Creează un nou timer
        timerBase.Interval = periodSecondsBetweenValues * 1000; // Setează intervalul timer-ului în milisecunde
        timerBase.Elapsed += new ElapsedEventHandler(timerBase_Elapsed); // Asociază metoda care se execută la fiecare tick al timer-ului
    }
}
// Clasa pentru reprezentarea unei valori de senzor
public class SensorBase
{
    public string PatientCode { get; } // Codul pacientului
    public SensorType SensorType { get; } // Tipul senzorului
    public double Value { get; } // Valoarea măsurată
    public DateTime Timestamp { get; } // Timpul măsurării

    // Constructor care inițializează toate proprietățile
    public SensorBase(string patientCode, SensorType sensorType, double value, DateTime timestamp)
    {
        PatientCode = patientCode; // Setează codul pacientului
        SensorType = sensorType; // Setează tipul senzorului
        Value = value; // Setează valoarea măsurată
        Timestamp = timestamp; // Setează timpul măsurării
    }
}

