using System; // Recommended to be explicit
using CommonReferences; // Ensure this is present

// Main application class
class Program
{
    // Application entry point
    static void Main(string[] args)
    {
        Console.WriteLine("Instantiating the SensorValue class\n\r"); // Displays an initialization message

        // Instantiate using the default constructor. Measurement values are set through properties
        SensorValue sensor1 = new SensorValue(); // Creates a SensorValue object
        sensor1.Type = SensorType.BloodGlucose; // Sets sensor type la "BloodGlucose"
        sensor1.TimeStamp = DateTime.Now; // Sets timestamp la momentul curent
        sensor1.Value = 100; // Sets measured value to 100 mg/dl
        DisplaySensorValues("First sensor initialized ", sensor1); // Displays sensor values

        // Instantiation using constructor with parameters
        SensorValue sensor2 = new SensorValue(SensorType.SkinTemperature, 36.7, "23-Jan-10 14:56:00"); // Creates an object with predefined values
        DisplaySensorValues("Second sensor initialized ", sensor2); // Displays sensor values

        // Creates a PumpSensorValues object for pump handling
        PumpSensorValues sensorValuesPump = new PumpSensorValues(3); // Creates a pump with 3 sensors
        sensorValuesPump.StartPumping(); // Starts the pump
        Console.WriteLine("Pump running... Press Enter to stop."); // Displays a message for the user
        Console.ReadLine(); // Waits for the user to press Enter
        sensorValuesPump.StopPumping(); // Stops the pump
        Console.WriteLine("Pump stopped."); // Displays a message that the pump was stopped
    }

    // Method to display a sensor values
    internal static void DisplaySensorValues(string headerText, SensorValue sensor)
    {
        Console.WriteLine("\t " + headerText); // Displays a header text
        Console.WriteLine("\t\t Type = {0} ", sensor.Type.ToString()); // Displays sensor type
        Console.WriteLine("\t\t TimeStamp = {0} ", sensor.TimeStampString); // Displays sensor timestamp
        Console.WriteLine("\t\t Value = {0} ", sensor.Value.ToString("0.00")); // Displays sensor value formatted with 2 decimals
    }
}
