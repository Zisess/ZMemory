using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace zissMemory
{
    internal class zm
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        static extern bool ReadProcessMemory(IntPtr hProcess, long lpBaseAddress, [Out] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, long lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        private Process process;
        private IntPtr proc_Handle;
        private int bytesWritten;
        private int bytesRead;
        public void SetProcess(string processName)
        {
            Process[]  processList = Process.GetProcessesByName(processName);
            process = processList[0];
            if (processList.Length == 0)
            {
                Log("Process not found: " + processName);
            }

            if (OpenProcess((int)0xFFFF, false, process.Id) == IntPtr.Zero)
            {
                Console.WriteLine("Failed to open process");
            }

            proc_Handle = process.Handle;
        }

        public long GetModuleBaseAddress(string moduleName)
        {
            long moduleBaseAddress = 0;
            ProcessModuleCollection modules = process.Modules;
            foreach (ProcessModule module in modules)
            {
                if (module.ModuleName == moduleName)
                {
                    Log("\t" + module.ModuleName + ": 0x" + module.BaseAddress.ToString("X8"));
                    moduleBaseAddress = (long)module.BaseAddress;
                    return moduleBaseAddress;
                }
            }

            Log("Could not find module: " + moduleName);
            return moduleBaseAddress;
        }
        public void WriteMemory(long address, byte[] value)
        {
            if (!WriteProcessMemory(proc_Handle, address, value, value.Length, out bytesWritten))
                Log("\t" + "Failed to write memory");
            else
                Log("\t" + "Applied " + bytesWritten + " bytes to memory");
        }
        public void WriteMemory(long address, int value)
        {
            byte[] value2Byte = BitConverter.GetBytes(value);
            if (!WriteProcessMemory(proc_Handle, address, value2Byte, value2Byte.Length, out bytesWritten))
                Log("\t" + "Failed to write memory");
            else
                Log("\t" + "Applied " + bytesWritten + " bytes to memory");
        }
        public void WriteMemory(long address, float value)
        {
            byte[] value2Byte = BitConverter.GetBytes(value);
            if (!WriteProcessMemory(proc_Handle, address, value2Byte, value2Byte.Length, out bytesWritten))
                Log("\t" + "Failed to write memory");
            else
                Log("\t" + "Applied " + bytesWritten + " bytes to memory");
        }
        public void WriteMemory(long address, double value)
        {
            byte[] value2Byte = BitConverter.GetBytes(value);
            if (!WriteProcessMemory(proc_Handle, address, value2Byte, value2Byte.Length, out bytesWritten))
                Log("\t" + "Failed to write memory");
            else
                Log("\t" + "Applied " + bytesWritten + " bytes to memory");
        }
        public void WriteMemory(long address, byte value)
        {
            byte[] value2Byte = BitConverter.GetBytes(value);
            if (!WriteProcessMemory(proc_Handle, address, value2Byte, value2Byte.Length, out bytesWritten))
                Log("\t" + "Failed to write memory");
            else
                Log("\t" + "Applied " + bytesWritten + " bytes to memory");
        }
        public void WriteMemory(long address, string value)
        {
            var value2Byte = Encoding.ASCII.GetBytes(value);
            if (!WriteProcessMemory(proc_Handle, address, value2Byte, value2Byte.Length, out bytesWritten))
                Log("\t" + "Failed to write memory");
            else
                Log("\t" + "Applied " + bytesWritten + " bytes to memory");
        }
        public byte[] ReadMemory(long address, int length)
        {
            var buffer = new byte[length];
            ReadProcessMemory(proc_Handle, address, buffer, buffer.Length, out bytesRead);
            return buffer;
        }
        public int ReadInteger(long address)
        {
            byte[] buffer = new byte[8];
            ReadProcessMemory(proc_Handle, address, buffer, 4, out bytesRead);
            return BitConverter.ToInt32(buffer, 0);
        }
        public float ReadFloat(long address, int length)
        {
            byte[] buffer = new byte[8];
            ReadProcessMemory(proc_Handle, address, buffer, 8, out bytesRead);
            return BitConverter.ToSingle(buffer, 0);
        }
        public double ReadDouble(long address, int length)
        {
            byte[] buffer = new byte[8];
            ReadProcessMemory(proc_Handle, address, buffer, 8, out bytesRead);
            return BitConverter.ToDouble(buffer, 0);
        }
        public string ReadString(long address, int length)
        {
            byte[] buffer = new byte[length];
            ReadProcessMemory(proc_Handle, address, buffer, length, out bytesRead);
            return System.Text.Encoding.UTF8.GetString(buffer);
        }
        public long GetPointerAddress(long lpBaseAddress, int[] offsets)
        {
            foreach (int offset in offsets)
            {
                lpBaseAddress = ReadInteger(lpBaseAddress);
                lpBaseAddress += offset;
            }
            return lpBaseAddress;
        }
        static void Log(string message, bool print = true)
        {
            using (StreamWriter writer = new StreamWriter("log.log", true))
            {
                writer.WriteLine(DateTime.Now + ": " + message);
                if (print)
                    Console.WriteLine(DateTime.Now + ": " + message);
            }
            
        }
    }
}
