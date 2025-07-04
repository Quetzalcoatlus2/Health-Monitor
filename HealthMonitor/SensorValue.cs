using System;
using System.Globalization; // Ensure this is present
using CommonReferences; // Ensure this is present

// Clasa pentru reprezentarea unei valori de senzor
public class SensorValue
{
    private string patientCode = string.Empty; // Codul pacientului, inițializat pentru a evita CS8618 (nullable warning)
    private SensorType type; // Tipul senzorului
    private double value; // Valoarea măsurată de senzor
    private DateTime timeStamp; // Timpul la care a fost măsurată valoarea

    #region properties
    // Proprietate pentru accesarea și setarea codului pacientului
    public string PatientCode
    {
        get { return patientCode; } // Returnează codul pacientului
        set { patientCode = value; } // Setează codul pacientului
    }

    // Proprietate pentru accesarea și setarea tipului de senzor
    public SensorType Type
    {
        get { return type; } // Returnează tipul senzorului
        set { type = value; } // Setează tipul senzorului
    }

    // Proprietate pentru accesarea și setarea valorii măsurate de senzor
    public double Value
    {
        get { return this.value; } // Returnează valoarea senzorului
        set { this.value = value; } // Setează valoarea senzorului
    }

    // Proprietate pentru accesarea și setarea timestamp-ului
    public DateTime TimeStamp
    {
        get { return timeStamp; } // Returnează timestamp-ul
        set { timeStamp = value; } // Setează timestamp-ul
    }

    // Proprietate pentru accesarea și setarea timestamp-ului ca șir de caractere
    public string TimeStampString
    {
        get
        {
            // Returnează timestamp-ul în format ISO 8601
            return timeStamp.ToString("o", CultureInfo.InvariantCulture);
        }
        set
        {
            // Setează timestamp-ul prin parsarea unui șir în format ISO 8601
            timeStamp = DateTime.ParseExact(value, "o", CultureInfo.InvariantCulture);
        }
    }
    #endregion

    #region constructors
    // Constructor implicit care inițializează toate câmpurile cu valori implicite
    public SensorValue()
    {
        this.patientCode = string.Empty; // Inițializează codul pacientului cu un șir gol
        this.type = SensorType.None; // Inițializează tipul senzorului la "None"
        this.value = 0.0; // Inițializează valoarea senzorului la 0.0
        this.timeStamp = DateTime.MinValue; // Inițializează timestamp-ul la cea mai mică valoare posibilă
    }

    // Constructor care inițializează tipul, valoarea și timestamp-ul
    public SensorValue(SensorType type, double value, DateTime timeStamp)
    {
        this.patientCode = string.Empty; // Inițializează codul pacientului cu un șir gol
        this.type = type; // Setează tipul senzorului
        this.value = value; // Setează valoarea senzorului
        this.timeStamp = timeStamp; // Setează timestamp-ul
    }

    // Constructor care inițializează tipul, valoarea și timestamp-ul ca șir de caractere
    public SensorValue(SensorType type, double value, string timeStampString)
    {
        this.patientCode = string.Empty; // Inițializează codul pacientului cu un șir gol
        this.type = type; // Setează tipul senzorului
        this.value = value; // Setează valoarea senzorului
        this.TimeStampString = timeStampString; // Setează timestamp-ul folosind proprietatea TimeStampString
    }

    // Constructor care inițializează toate câmpurile
    public SensorValue(string patientCode, SensorType type, double value, DateTime timeStamp)
    {
        this.patientCode = patientCode; // Setează codul pacientului
        this.type = type; // Setează tipul senzorului
        this.value = value; // Setează valoarea senzorului
        this.timeStamp = timeStamp; // Setează timestamp-ul
    }
    #endregion
}
