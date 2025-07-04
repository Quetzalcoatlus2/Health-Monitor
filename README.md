# Health-Monitor

This project simulates a remote health monitoring system using a Windows Forms application developed in C# with .NET. It emulates three key physiological parameters: heart rate, skin temperature, and blood glucose. These values are generated in real time using the Random class and scheduled with System.Timers.Timer. Each parameter is associated with a specific patient and timestamp, simulating live sensor input.

The data is displayed dynamically in the graphical interface using a DataGridView, where updates are handled asynchronously to ensure smooth and thread-safe GUI interaction. Since sensor data generation happens in background threads, updates to the UI use BeginInvoke to marshal calls to the main thread.

All measured values are saved locally in a SQLite database, a lightweight embedded SQL engine. The database stores the patient's unique identifier, the type of sensor, the measured value, and the exact time of measurement. A filtering mechanism is available in the interface to retrieve historical sensor values for any given patient on a specific date.

The project follows an object-oriented architecture, using encapsulation and events to simulate the decoupling between sensors and data handlers. Events are triggered each time a new sensor value is generated, and the GUI layer subscribes to these events to receive and display the data.

In addition to the simulation and local monitoring interface, the system includes a TCP/IP communication model designed for client-server interaction. This model allows the patient-side application to send sensor values in real time over a network to a remote doctor's interface. This setup emulates a remote medical telemetry system, where doctors can monitor a patient’s vital signs from another location.

The project includes deployment functionality via a setup application, which packages the executable, handles dependencies like the .NET Framework, and allows users to install the system on Windows machines with ease.
