# ZMemory
The zissMemory class is a .NET wrapper for low-level memory manipulation functions provided by Windows operating system. 
The class provides several methods for opening a process, reading and writing memory, and getting information about the process and its modules.
It uses DllImport to make calls to the Windows API, which provides a way to interact with the underlying system.
The class has several methods to write memory, including methods to write memory as an integer, float, double, byte, or string.
The class also provides a method to read memory.
Additionally, the class has a method to retrieve the base address of a specific module in the process.
The class logs information to the console, including errors, the success or failure of memory manipulation operations, and the application of bytes to memory.


### Features
- Open a process and retrieve its handle<br>
- Retrieve the base address of a module in a process<br>
- Read and write memory from an external process<br>
- Supports reading and writing of data types including integers, floats, doubles, bytes, and strings<br>
- Easy to use and high-level interface<br>

### Requirements
- .NET 4.0 or higher
- Windows 7 or higher
- Process must have the necessary privileges for accessing memory

## Getting Started
To use the library, you first need to create an instance of the «zm» class:

```cs
using zissMemory;
var memory = new zm();
```

Next, you can use the «SetProcess» method to open the desired process:

```cs
memory.SetProcess("processName");
```

To retrieve the base address of a module in the process, use the «GetModuleBaseAddress» method:

```cs
long moduleBaseAddress = memory.GetModuleBaseAddress("moduleName");
```

Once you have the base address of a module, you can read and write memory using the various «WriteMemory» methods.<br>
For example, to write an integer value to memory:

```cs
public void WriteMemory(long address, int value);
public void WriteMemory(long address, float value);
public void WriteMemory(long address, double value);
public void WriteMemory(long address, byte value);
public void WriteMemory(long address, string value);
```

The zissMemory class provides a simple and easy to use set of functions for accessing and manipulating the memory of a process in C#. Whether you are creating cheats or hacks for games, or modifying the behavior of a running process for debugging or testing purposes, this class can help you achieve your goals.

## Limitations
Please note that memory manipulation can be a dangerous operation, and should only be performed with caution and with a thorough understanding of the process and the system. Improper use of this class can lead to unexpected results or system crashes. Additionally, some processes may have memory protection mechanisms in place, which could prevent the successful manipulation of memory.
## Support
If you need help or have any questions, join my discord server.

## Contributions
ZMemory is an open-source project, and contributions are welcome!<br>
If you would like to contribute, please dm me via discord.







