# Health Monitor

Health Monitor is a C#/.NET solution that simulates remote patient monitoring. It generates live sensor readings, displays them in a Windows Forms dashboard, stores them in SQLite, and supports TCP/IP communication for client-server telemetry.

## Features

- Real-time simulation of:
  - Heart rate
  - Skin temperature
  - Blood glucose
- Live dashboard updates in a `DataGridView`
- Alarm panel for out-of-range readings
- Historical filtering by patient and date
- Local SQLite persistence
- Optional TCP/IP data transmission to a remote endpoint

## Solution Structure

- `/CommonReferences` – shared enums and types
- `/HealthMonitor` – core sensor generation logic
- `/DataStore` – SQLite data access layer
- `/TCPCommunication` – TCP client/server transport
- `/DataPresentation` – Windows Forms UI

## Requirements

- .NET SDK 9.0+
- Windows (required for `DataPresentation`, which targets `net9.0-windows`)

## Build

From the repository root:

```bash
dotnet build HealthMonitor/HealthMonitor.sln
```

> Note: Building the full solution on Linux/macOS will fail for the Windows Forms project unless Windows targeting is explicitly enabled.

## Run

Run the Windows Forms application project:

```bash
dotnet run --project DataPresentation/DataPresentation.csproj
```

## Database Configuration

SQLite connection strings are currently hardcoded in:

- `DataPresentation/App.config`
- `DataStore/App.config`

Before running on a new machine, update the `Data Source=...` path to a valid local path.

## TCP/IP Notes

- `TCPCommServer` listens on a configured IP/port.
- `TCPCommClient` can send randomized sensor values through TCP/IP.
- The UI includes a communication mode toggle (local vs. TCP/IP).

## Screenshot

![Health Monitor UI](https://github.com/user-attachments/assets/ff53f7d5-7e70-4a94-a3db-6bc2d3dd8446)
