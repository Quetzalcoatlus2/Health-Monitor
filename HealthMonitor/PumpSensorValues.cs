using System; // For Random, DateTime, Enum
using System.Timers; // For Timer, ElapsedEventArgs
using CommonReferences; // Ensure this is present

// Delegate definition for new sensor value generation event
public delegate void OnNewSensorValue(SensorBase sensorBaseArg);
// Class for generating sensor values
public class PumpSensorValues
{
    // Event triggered when a new sensor value is generated
    public event OnNewSensorValue? newSensorValueEvent;

    System.Timers.Timer timerBase; // Timer for periodic value generation
    Random myRandom; // Random number generator
    string patientCode = "DefaultPatientCode"; // Patient code (default value)

    // Constructor initializing timer and random generator
    public PumpSensorValues(int periodSecondsBetweenValues)
    {
        myRandom = new Random(); // Initializes random number generator
        patientCode = string.Empty; // Initializes patient code with an empty string
        timerBase = new System.Timers.Timer(); // Creates a new timer
        timerBase.Interval = periodSecondsBetweenValues * 1000; // Sets timer interval in milliseconds
        timerBase.Elapsed += new ElapsedEventHandler(timerBase_Elapsed); // Binds handler executed at each timer tick
    }

    // Method to start sensor value generation
    public void StartPumping()
    {
        Console.WriteLine("Starting sensor value generation..."); // Displays start message
        timerBase.Start(); // Starts timer
    }

    // Method to stop sensor value generation
    public void StopPumping()
    {
        Console.WriteLine("Stopping sensor value generation..."); // Displays stop message
        timerBase.Stop(); // Stops timer
    }

    // Method executed on each timer tick
    private void timerBase_Elapsed(object? sender, ElapsedEventArgs e)
    {
        int minNumber, maxNumber; // Variables for generated value bounds
        double valueRandom; // Variable for random generated value

        // Gets all values defined in SensorType enum
        SensorType[] sensorTypeValues = (SensorType[])Enum.GetValues(typeof(SensorType));
        int maxSensorIndex = sensorTypeValues.Length - 1; // Gets maximum number of sensor types

        // Generates random index for a sensor type (excluding None)
        int typeRandomIndex = myRandom.Next(1, maxSensorIndex + 1); // Index between 1 and the max number of types
        SensorType sensorTypeRandom = sensorTypeValues[typeRandomIndex]; // Selects sensor type corresponding to the index

        // Generates a random value for the selected sensor type
        switch (sensorTypeRandom)
        {
            case SensorType.SkinTemperature: // If type is SkinTemperature
                minNumber = 30; // Minimum limit
                maxNumber = 40; // Maximum limit
                valueRandom = myRandom.Next(minNumber * 10, maxNumber * 10 + 1) / 10.0; // Value with one decimal
                break;
            case SensorType.BloodGlucose: // If type is BloodGlucose
                minNumber = 60; // Minimum limit
                maxNumber = 300; // Maximum limit
                valueRandom = myRandom.Next(minNumber, maxNumber + 1); // Value within limits
                break;
            case SensorType.HeartRate: // If type is HeartRate
                minNumber = 30; // Minimum limit
                maxNumber = 200; // Maximum limit
                valueRandom = myRandom.Next(minNumber, maxNumber + 1); // Value within limits
                break;
            default: // If type is unknown or None
                valueRandom = 0; // Default value
                Console.WriteLine($"Warning: Generated default value for unexpected SensorType: {sensorTypeRandom}"); // Displays warning
                break;
        }

        // Creates a SensorBase object with generated values
        SensorBase sensorRandom = new SensorBase(patientCode, sensorTypeRandom, valueRandom, DateTime.Now);

        // Triggers event if there are subscribers
        if (newSensorValueEvent != null)
        {
            newSensorValueEvent(sensorRandom); // Invokes event
        }
    }

    // Constructor also accepting patient code
    public PumpSensorValues(int periodSecondsBetweenValues, string patientCode)
    {
        this.patientCode = patientCode; // Initializes patient code
        myRandom = new Random(); // Initializes random number generator
        timerBase = new System.Timers.Timer(); // Creates a new timer
        timerBase.Interval = periodSecondsBetweenValues * 1000; // Sets timer interval in milliseconds
        timerBase.Elapsed += new ElapsedEventHandler(timerBase_Elapsed); // Binds handler executed at each timer tick
    }
}
// Class for representing a sensor value
public class SensorBase
{
    public string PatientCode { get; } // Patient code
    public SensorType SensorType { get; } // Sensor type
    public double Value { get; } // Measured value
    public DateTime Timestamp { get; } // Measurement time

    // Constructor initializing all properties
    public SensorBase(string patientCode, SensorType sensorType, double value, DateTime timestamp)
    {
        PatientCode = patientCode; // Sets patient code
        SensorType = sensorType; // Sets sensor type
        Value = value; // Sets measured value
        Timestamp = timestamp; // Sets measurement time
    }
}

