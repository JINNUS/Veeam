using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace ProcessMonitorTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Check if the correct number of arguments were provided
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: ProcessMonitor processName maxLifetime monitoringFrequency");
                return;
            }

            // Parse the arguments
            string processName = args[0];
            int maxLifetime = int.Parse(args[1]);
            int monitoringFrequency = int.Parse(args[2]);

            // Create a log file
            StreamWriter logFile = new StreamWriter("log.txt", true);

            // Start the monitoring loop
            while (true)
            {
                // Check if the user pressed the 'q' key to stop the utility
                if (Console.KeyAvailable && Console.ReadKey().Key == ConsoleKey.Q)
                {
                    break;
                }

                // Get the process with the specified name
                Process[] processes = Process.GetProcessesByName(processName);

                // Iterate through the processes
                foreach (Process process in processes)
                {
                    // Check if the process has exceeded the maximum lifetime
                    if (process.StartTime < DateTime.Now.AddMinutes(-maxLifetime))
                    {
                        // Kill the process and log the action
                        process.Kill();
                        logFile.WriteLine($"{DateTime.Now}: Process {processName} killed (duration exceeded {maxLifetime} minutes)");
                    }
                }

                // Sleep for the specified monitoring frequency
                Thread.Sleep(monitoringFrequency * 60000);
            }

            // Close the log file
            logFile.Close();
        }
    }
}

