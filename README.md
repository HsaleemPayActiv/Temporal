# Temporal .NET Project

This repository contains a .NET project integrated with Temporal workflow engine. Follow these instructions to set up and run the project.

## Prerequisites

### 1. Install .NET SDK
1. Download .NET SDK 8.0 or later from [official .NET download page](https://dotnet.microsoft.com/download)
2. Run the installer
3. Verify installation by opening a terminal and running:
   ```bash
   dotnet --version
   ```

### 2. Install Temporal CLI

#### Alternative Installation (Manual-Windows) [https://docs.temporal.io/cli]
1. Download the latest Temporal CLI from [https://temporal.download/cli/archive/latest?platform=windows&arch=amd64]
2. Extract the downloaded archive
3. Add the extracted directory to your system's PATH environment variable

Verify Temporal CLI installation:
```bash
temporal --version
```

## Project Setup

1. Clone this repository:
   ```bash
   git clone [repository-url]
   cd Temporal
   ```

2. Restore .NET dependencies:
   ```bash
   dotnet restore
   ```

## Running the Application

### 1. Start Temporal Server
1. Start the Temporal development server:
   ```bash
   temporal server start-dev
   ```
   This will start the Temporal server on `localhost:7233`

2. Access Temporal Web UI:
   - Open your browser and navigate to `http://localhost:8233`
   - You can use this UI to monitor workflows and activities

### 2. Start the .NET Application
1. Navigate to the project directory
2. Run the application:
   ```bash
   dotnet watch run
   ```

## Project Structure
- `Worker/`: Contains the Temporal worker implementation
- `Activities/`: Contains activity implementations
- `Workflows/`: Contains workflow definitions

## Additional Resources
- [Temporal Documentation](https://docs.temporal.io/)
- [Temporal .NET SDK Documentation](https://docs.temporal.io/dev-guide/dotnet)
- [.NET Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [.NET Sample](https://github.com/temporalio/money-transfer-project-template-dotnet)