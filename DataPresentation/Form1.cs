namespace DataPresentation
{
    using CommonReferences;
    using TCPCommunication;

    public partial class DataPresentation : Form
    {
        private bool displayTheReceivingData = true;
        //private bool sendAlsoThroughTCP = false;
        private TCPCommunication.TCPCommServer tcpCommServer;
        private TCPCommunication.TCPCommClient tcpCommClient;
        public DataPresentation()
        {
            InitializeComponent();

            // Initialize tbTimerPeriod with the default value of 3
            tbTimerPeriod.Text = "99";

            cbPatientCodeStart.DataSource = Enum.GetValues(typeof(PatCodeEnum));
            cbFilterPatient.DataSource = Enum.GetValues(typeof(PatCodeEnum));

            PumpSensorValues sensorValuesPump = new PumpSensorValues(Convert.ToInt32(tbTimerPeriod.Text));
            sensorValuesPump.StartPumping();
            sensorValuesPump.newSensorValueEvent += new OnNewSensorValue(OnNewSensorValueHandler);

            tcpCommServer = new TCPCommServer(); // Create and start the TCPCommServer
            tcpCommServer.newSignalReceivedEvent += new NewSignalReceived(tcpCommServer_newSignalReceivedEvent);

            tcpCommClient = new TCPCommClient(); // Initialize tcpCommClient to avoid nullability issues
        }

        void tcpCommServer_newSignalReceivedEvent(SensorBase sensorValue)
        {
            //display this event on the datagrid
            OnNewSensorValueHandler(sensorValue);
        }

        private void DataPresentation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tcpCommServer != null) tcpCommServer.Dispose();
            if (tcpCommClient != null) tcpCommClient.Dispose();
        }

        List<SensorBase> sensorBaseList = new List<SensorBase>();

        // Update the CheckForAlarm method to include autoscrolling
        private void CheckForAlarm(SensorBase sensor)
        {
            // Define thresholds for each sensor type
            const double SkinTemperatureLow = 32.0;
            const double SkinTemperatureHigh = 38.0;
            const double HeartRateLow = 50.0;
            const double HeartRateHigh = 110.0;
            const double BloodGlucoseLow = 70.0;
            const double BloodGlucoseHigh = 140.0;

            bool isAlarmTriggered = false;
            string alarmMessage = $"Alarm triggered for {sensor.PatientCode} - {sensor.SensorType}: {sensor.Value}";

            // Check thresholds based on sensor type
            switch (sensor.SensorType)
            {
                case SensorType.SkinTemperature:
                    if (sensor.Value < SkinTemperatureLow || sensor.Value > SkinTemperatureHigh)
                        isAlarmTriggered = true;
                    break;
                case SensorType.HeartRate:
                    if (sensor.Value < HeartRateLow || sensor.Value > HeartRateHigh)
                        isAlarmTriggered = true;
                    break;
                case SensorType.BloodGlucose:
                    if (sensor.Value < BloodGlucoseLow || sensor.Value > BloodGlucoseHigh)
                        isAlarmTriggered = true;
                    break;
            }

            // Display alarm in richTextBox1 if necessary
            if (isAlarmTriggered)
            {
                if (richTextBox1 != null && richTextBox1.IsHandleCreated && !richTextBox1.IsDisposed)
                {
                    richTextBox1.Invoke(new Action(() =>
                    {
                        richTextBox1.AppendText(alarmMessage + Environment.NewLine);
                        richTextBox1.SelectionStart = richTextBox1.Text.Length;
                        richTextBox1.ScrollToCaret();
                    }));
                }
            }
        }

        // Update the OnNewSensorValueHandler method to include alarm checking
        void OnNewSensorValueHandler(SensorBase sensorBaseArg)
        {
            DataStore.DAL_PatientData.AddData(sensorBaseArg); // Save data to the database
            sensorBaseList.Insert(0, sensorBaseArg);

            // Only update the data grid if the user is viewing live data
            if (displayTheReceivingData)
            {
                if (this.IsHandleCreated && !this.IsDisposed)
                {
                    this.BeginInvoke(new Action(() => BindDataGridToListOfValues(true)));
                }
            }

            // Check for alarm conditions
            CheckForAlarm(sensorBaseArg);
        }


        private void BindDataGridToListOfValues(bool forceUpdate = true)
        {
            if (!displayTheReceivingData && !forceUpdate)
            {
                // Skip updating the grid if the user is viewing filtered data
                return;
            }

            dgSensorBaseList.DataSource = null;

            var formattedSensorBaseList = sensorBaseList.Select(sensor => new
            {
                PatientCode = sensor.PatientCode, // Added PatientCode to the data binding
                SensorType = sensor.SensorType,
                Value = sensor.Value,
                Timestamp = sensor.Timestamp.ToString("o")
            }).ToList();

            dgSensorBaseList.DataSource = formattedSensorBaseList;
        }

        private void dgSensorBaseList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void cbPatientCodeStart_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPatientCodeStart.SelectedItem is PatCodeEnum selectedPatient)
            {
                // Enable the "Start Monitoring" button
                bStartPumping.Enabled = true;

                // Optionally, display a message or update the UI
                MessageBox.Show($"Selected patient: {selectedPatient}");
            }
        }



        private void bStartPumping_Click(object sender, EventArgs e)
        {
            if (cbPatientCodeStart.SelectedItem is PatCodeEnum selectedPatient && !string.IsNullOrEmpty(tbTimerPeriod.Text))
            {
                try
                {
                    int timePeriodSeconds = Convert.ToInt32(tbTimerPeriod.Text);
                    startPumping(selectedPatient, timePeriodSeconds);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Invalid time period: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a patient and enter a valid time period.");
            }
        }

        private void bStopPumping_Click(object sender, EventArgs e)
        {
            if (cbPatientCodeStart.SelectedItem is PatCodeEnum currPatientStop)
            {
                if (dictPatientPump.ContainsKey(currPatientStop))
                {
                    PumpSensorValues pumpToBeStopped = dictPatientPump[currPatientStop];
                    pumpToBeStopped.StopPumping();
                    dictPatientPump.Remove(currPatientStop);
                }
                else
                {
                    MessageBox.Show("The selected patient has no pump values started");
                }
            }
            else
            {
                MessageBox.Show("Please select a valid patient code.");
            }
        }

        Dictionary<PatCodeEnum, PumpSensorValues> dictPatientPump = new Dictionary<PatCodeEnum, PumpSensorValues>();
        public void startPumping(PatCodeEnum PatCodeEnum, int periodSeconds)
        {
            if (dictPatientPump.ContainsKey(PatCodeEnum))
            {
                MessageBox.Show("The selected patient has the pump already started");
                return;
            }

            // Corrected constructor call to pass the correct argument types
            PumpSensorValues sensorValuesPump = new PumpSensorValues(periodSeconds, PatCodeEnum.ToString());
            sensorValuesPump.StartPumping();
            sensorValuesPump.newSensorValueEvent += new OnNewSensorValue(OnNewSensorValueHandler);
            dictPatientPump.Add(PatCodeEnum, sensorValuesPump);
        }
        // Add these calls at the end of the DataPresentation class
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Set radioButton2 to be checked by default
            radioButton2.Checked = true;

            // Simulate a button click to start pumping
            bStartPumping_Click(this, EventArgs.Empty);

            // Simulate a button click to stop pumping
            bStopPumping_Click(this, EventArgs.Empty);

            // Directly call startPumping with a sample patient code and time period
            startPumping(PatCodeEnum.Patient_01, 5);
        }

        List<SensorBase> sensorBaseListPast = new List<SensorBase>();
        private void bFilter_Click(object sender, EventArgs e)
        {
            displayTheReceivingData = false;

            if (cbFilterPatient.SelectedItem is PatCodeEnum selectedPatient)
            {
                DateTime selectedDay = dtpFilterDate.Value.Date;

                try
                {
                    DataStore.DAL_PatientData.PatCodeEnum convertedPatientCode =
                        (DataStore.DAL_PatientData.PatCodeEnum)Enum.Parse(
                            typeof(DataStore.DAL_PatientData.PatCodeEnum),
                            selectedPatient.ToString()
                        );

                    sensorBaseListPast = DataStore.DAL_PatientData.GetData(convertedPatientCode, selectedDay);

                    if (sensorBaseListPast.Any())
                    {
                        dgSensorBaseList.DataSource = null;
                        dgSensorBaseList.DataSource = sensorBaseListPast.Select(sensor => new
                        {
                            PatientCode = sensor.PatientCode,
                            SensorType = sensor.SensorType,
                            Value = sensor.Value,
                            Timestamp = sensor.Timestamp.ToString("o")
                        }).ToList();
                    }
                    else
                    {
                        MessageBox.Show("No data found for the selected patient and date.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error fetching data: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Please select a valid patient code.");
            }
        }

        private void bReceive_Click(object sender, EventArgs e)
        {
            displayTheReceivingData = true;

            // Ensure the grid displays the received data
            BindDataGridToListOfValues(forceUpdate: true);

            // Use displayTheReceivingData to toggle UI behavior
            if (displayTheReceivingData)
            {
                MessageBox.Show("Receiving live data.");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                // Ensure a patient is selected and a valid time interval is provided
                if (cbPatientCodeStart.SelectedItem is PatCodeEnum selectedPatient && int.TryParse(tbTimerPeriod.Text, out int timePeriodSeconds))
                {
                    // Set the server IP to localhost (127.0.0.1) for sending data to this computer
                    string serverIP = "127.0.0.1";
                    tcpCommClient = new TCPCommClient(serverIP);

                    // Randomize sensor type
                    Random random = new Random();
                    SensorType randomSensor = (SensorType)random.Next(1, 4); // Skip 'None' (index 0)

                    // Randomize signal value based on sensor type
                    double randomValue = randomSensor switch
                    {
                        SensorType.SkinTemperature => random.NextDouble() * (38.0 - 32.0) + 32.0, // Range: 32.0 to 38.0
                        SensorType.HeartRate => random.Next(50, 111), // Range: 50 to 110
                        SensorType.BloodGlucose => random.Next(70, 141), // Range: 70 to 140
                        _ => 0.0
                    };

                    // Use current date and time
                    DateTime currentTime = DateTime.Now;

                    // Send randomized data for the selected patient
                    tcpCommClient.SendSignalData(selectedPatient.ToString(), randomSensor.ToString(), currentTime, randomValue);

                    // Notify the user
                    MessageBox.Show($"Randomized data is being sent for {selectedPatient} every {timePeriodSeconds} seconds through TCP/IP.");
                }
                else
                {
                    MessageBox.Show("Please select a valid patient and enter a valid time interval.");
                }
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void tbTimerPeriod_TextChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                // Add logic here if needed
            }
        }

        private System.Timers.Timer? dataSendTimer;

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                // Ensure a patient is selected and a valid time interval is provided
                if (cbPatientCodeStart.SelectedItem is PatCodeEnum selectedPatient && int.TryParse(tbTimerPeriod.Text, out int timePeriodSeconds))
                {
                    // Stop any existing timer
                    dataSendTimer?.Stop();
                    dataSendTimer?.Dispose();

                    // Set the server IP to localhost (127.0.0.1) for sending data to this computer
                    string serverIP = "127.0.0.1";
                    tcpCommClient = new TCPCommClient(serverIP);

                    // Create a new timer
                    dataSendTimer = new System.Timers.Timer(timePeriodSeconds * 1000); // Convert seconds to milliseconds
                    dataSendTimer.Elapsed += (s, args) =>
                    {
                        // Randomize sensor type
                        Random random = new Random();
                        SensorType randomSensor = (SensorType)random.Next(1, 4); // Skip 'None' (index 0)

                        // Randomize signal value based on sensor type
                        double randomValue = randomSensor switch
                        {
                            SensorType.SkinTemperature => random.NextDouble() * (38.0 - 32.0) + 32.0, // Range: 32.0 to 38.0
                            SensorType.HeartRate => random.Next(50, 111), // Range: 50 to 110
                            SensorType.BloodGlucose => random.Next(70, 141), // Range: 70 to 140
                            _ => 0.0
                        };

                        // Use current date and time
                        DateTime currentTime = DateTime.Now;

                        // Send randomized data for the selected patient
                        tcpCommClient.SendSignalData(selectedPatient.ToString(), randomSensor.ToString(), currentTime, randomValue);
                    };

                    // Start the timer
                    dataSendTimer.Start();

                    // Notify the user
                    MessageBox.Show($"Randomized data is being sent for {selectedPatient} every {timePeriodSeconds} seconds through TCP/IP.");
                }
                else
                {
                    MessageBox.Show("Please select a valid patient and enter a valid time interval.");
                }
            }
            else
            {
                // Stop the timer if the radio button is unchecked
                dataSendTimer?.Stop();
                dataSendTimer?.Dispose();
                dataSendTimer = null;
            }
        }
        // Rename one of the duplicate methods to resolve the conflict
        private void radioButton1_CheckedChanged_Alternate(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                // Ensure a patient is selected and a valid time interval is provided
                if (cbPatientCodeStart.SelectedItem is PatCodeEnum selectedPatient && int.TryParse(tbTimerPeriod.Text, out int timePeriodSeconds))
                {
                    // Stop any existing timer
                    dataSendTimer?.Stop();
                    dataSendTimer?.Dispose();

                    // Set the server IP to localhost (127.0.0.1) for sending data to this computer
                    string serverIP = "127.0.0.1";
                    tcpCommClient = new TCPCommClient(serverIP);

                    // Create a new timer
                    dataSendTimer = new System.Timers.Timer(timePeriodSeconds * 1000); // Convert seconds to milliseconds
                    dataSendTimer.Elapsed += (s, args) =>
                    {
                        // Randomize sensor type
                        Random random = new Random();
                        SensorType randomSensor = (SensorType)random.Next(1, 4); // Skip 'None' (index 0)

                        // Randomize signal value based on sensor type
                        double randomValue = randomSensor switch
                        {
                            SensorType.SkinTemperature => random.NextDouble() * (38.0 - 32.0) + 32.0, // Range: 32.0 to 38.0
                            SensorType.HeartRate => random.Next(50, 111), // Range: 50 to 110
                            SensorType.BloodGlucose => random.Next(70, 141), // Range: 70 to 140
                            _ => 0.0
                        };

                        // Use current date and time
                        DateTime currentTime = DateTime.Now;

                        // Send randomized data for the selected patient
                        tcpCommClient.SendSignalData(selectedPatient.ToString(), randomSensor.ToString(), currentTime, randomValue);
                    };

                    // Start the timer
                    dataSendTimer.Start();

                    // Notify the user
                    MessageBox.Show($"Randomized data is being sent for {selectedPatient} every {timePeriodSeconds} seconds through TCP/IP.");
                }
                else
                {
                    MessageBox.Show("Please select a valid patient and enter a valid time interval.");
                }
            }
            else
            {
                // Stop the timer if the radio button is unchecked
                dataSendTimer?.Stop();
                dataSendTimer?.Dispose();
                dataSendTimer = null;
            }
        }
        }
        //private void ApplyFilters()
        //{
        //    // Get selected patient code from the combobox
        //    var selectedPatient = cbFilterPatient.SelectedItem as PatCodeEnum?;

        //    // Get selected date from the DateTimePicker
        //    var selectedDate = dtpFilterDate.Value.Date;

        //    // Convert DataPresentation.PatCodeEnum to DataStore.DAL_PatientData.PatCodeEnum
        //    var convertedPatientCode = (DataStore.DAL_PatientData.PatCodeEnum)(selectedPatient ?? PatCodeEnum.Patient_01);

        //    // Fetch filtered data directly from the database
        //    var filteredList = DataStore.DAL_PatientData.GetData(
        //        convertedPatientCode, // Use the converted enum value
        //        selectedDate
        //    ).Select(sensor => new
        //    {
        //        PatientCode = sensor.PatientCode,
        //        SensorType = sensor.SensorType,
        //        Value = sensor.Value,
        //        Timestamp = sensor.Timestamp.ToString("o")
        //    }).ToList();

        //    // Update the DataGridView
        //    dgSensorBaseList.DataSource = null;
        //    dgSensorBaseList.DataSource = filteredList;
        //}



    } // <-- Added this closing brace

    