using System;
using System.Globalization; // Ensure this is present
using CommonReferences; // Ensure this is present

// Class for representing a sensor value
public class SensorValue
{
    private string patientCode = string.Empty; // Patient code, initialized to avoid CS8618 (nullable warning)
    private SensorType type; // Sensor type
    private double value; // Measured sensor value
    private DateTime timeStamp; // Time when the value was measured

    #region properties
    // Property for getting and setting patient code
    public string PatientCode
    {
        get { return patientCode; } // Returns patient code
        set { patientCode = value; } // Sets patient code
    }

    // Property for getting and setting sensor type
    public SensorType Type
    {
        get { return type; } // Returns sensor type
        set { type = value; } // Sets sensor type
    }

    // Property for getting and setting the measured sensor value
    public double Value
    {
        get { return this.value; } // Returns sensor value
        set { this.value = value; } // Sets sensor value
    }

    // Property for getting and setting the timestamp
    public DateTime TimeStamp
    {
        get { return timeStamp; } // Returns timestamp
        set { timeStamp = value; } // Sets timestamp
    }

    // Property for getting and setting the timestamp as a string
    public string TimeStampString
    {
        get
        {
            // Returns timestamp in ISO 8601 format
            return timeStamp.ToString("o", CultureInfo.InvariantCulture);
        }
        set
        {
            // Sets timestamp by parsing an ISO 8601 string
            timeStamp = DateTime.ParseExact(value, "o", CultureInfo.InvariantCulture);
        }
    }
    #endregion

    #region constructors
    // Default constructor initializing all fields with default values
    public SensorValue()
    {
        this.patientCode = string.Empty; // Initializes patient code with an empty string
        this.type = SensorType.None; // Initializes sensor type to "None"
        this.value = 0.0; // Initializes sensor value to 0.0
        this.timeStamp = DateTime.MinValue; // Initializes timestamp to the minimum possible value
    }

    // Constructor initializing type, value, and timestamp
    public SensorValue(SensorType type, double value, DateTime timeStamp)
    {
        this.patientCode = string.Empty; // Initializes patient code with an empty string
        this.type = type; // Sets sensor type
        this.value = value; // Sets sensor value
        this.timeStamp = timeStamp; // Sets timestamp
    }

    // Constructor initializing type, value, and timestamp as a string
    public SensorValue(SensorType type, double value, string timeStampString)
    {
        this.patientCode = string.Empty; // Initializes patient code with an empty string
        this.type = type; // Sets sensor type
        this.value = value; // Sets sensor value
        this.TimeStampString = timeStampString; // Sets timestamp using the TimeStampString property
    }

    // Constructor initializing all fields
    public SensorValue(string patientCode, SensorType type, double value, DateTime timeStamp)
    {
        this.patientCode = patientCode; // Sets patient code
        this.type = type; // Sets sensor type
        this.value = value; // Sets sensor value
        this.timeStamp = timeStamp; // Sets timestamp
    }
    #endregion
}
