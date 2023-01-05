using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;

namespace ProcessMonitor.Tests
{
    public class ProcessMonitorTests
    {

        public void TestKillProcess()
        {
            Process process = Process.Start("notepad.exe");
            ProcessMonitorTest.Program.Main(new string[] { "notepad", "1", "1" });
            System.Threading.Thread.Sleep(120000);
        }
    }
}
