# .NET Version Checker

## Description
This C# application scans a specified directory for DLL and EXE files and determines the target .NET Framework version for each file. It categorizes the files by path, filename, and target framework version, and outputs the results in a sorted (descending order based on the framework version) CSV or TXT file.

## Requirements
- .NET SDK (compatible with your project; typically .NET 5 or later)
- Mono.Cecil package

## Setup
1. **Install the .NET SDK**: Ensure that the .NET SDK is installed on your machine. If not, download and install it from the official [.NET website](https://dotnet.microsoft.com/download).

2. **Add Mono.Cecil Package**: Mono.Cecil is required to read assembly metadata without loading the assembly into the execution context. Install it by running the following command in your project directory:

    ```bash
    dotnet add package Mono.Cecil
    ```

## Running the Application
1. **Navigate to Your Project**: Open a command prompt or terminal and navigate to your project directory.

2. **Compile the Project**: Compile your project using the .NET CLI:

    ```bash
    dotnet build
    ```

3. **Run the Application**: Run the application with the following command:

    ```bash
    dotnet run
    ```

4. **Follow the Prompts**: The application will ask you to enter the directory path to scan and the output file path (including the desired filename and .csv or .txt extension). Enter these details as prompted.

5. **Check the Output**: After the application runs, check the specified output file for the results.

## Notes
- Ensure that the paths provided to the application are accessible and that you have the necessary permissions to read the files.
- The application is designed to handle .NET DLL and EXE files. Non-.NET files in the specified directory will be ignored.



