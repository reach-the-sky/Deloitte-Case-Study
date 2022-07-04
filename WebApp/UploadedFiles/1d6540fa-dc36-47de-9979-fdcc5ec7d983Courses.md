# [Learn C#.Net Core With Real World Examples](https://deloitteprehirelearning.udemy.com/course/csharp-from-scratch/)
<hr>
<hr>
## *Learn C#.Net Core from scratch - Basic OOPs*
<hr>
- ### Day 1: Introduction to DLL - EXE
- EXE is standalone executable component
	- `csc filename.cs`
- DLL is a reusable component usable by exe files
	- `csc /target:Library filename.cs`
- Assemblies
	- DLLs or EXEs
	- manifest - info about itself
	- metadata - info about classes
	- `ildasm` to check metadata and manifest

- ### Day 2: Data Types - Control Structures
#### Value types
- **Signed integral** : sbyte, short, int, long
- **Unsigned integral** : byte, ushort, uint, ulong
- **Characters** : char
- **floating point** : float, double
- **Boolean**
- **Enum types**
- **Struct types**
- **Nullable value types**

#### Reference types
- **String**
- **Array**
- **Classes**
- **Interface**

- ### Day 3: Arrays - ForEach - Structures - var vs Dynamic types
basics about arrays, foreach loop, structures(same as c)
var and dynamic keywords for variable declaraction 
datatype of var is fixed during initialization
datatype of dynamic can be changed after initialization

- ### Day 4: Classes - Objects - Fields - Methods - Constructors
- ### Day 5: Constructor overloading - this keyword - method overloading - properties
- ### Day 6: Static Keyword


## Learn how to write high performance and scalable .NET Core and ASP.NET Core applications in C#

- Measuring CPU
	- Two types of profilers:
	1. Sampling profiler: high frequency sampling, only shows CUP work
		1. Inclusive samples: samples that contain the method
		2. Exclusive samples: samples with the given function on the top of the stack
	2. Instrumenting profiler: Injects code to measure every method, also shows non CUP related work.

- Measuring Memory
	- .NET has automatic memory management
	- GC Root
	- Tw things to measure:
		- which methods allocate a significant amount of memory?
		- which GC roots prevent large number of objects from being GCed?
	- GC Settings: Server, concurrent
	- .NET Core: no performance counters

- Visual studio performance tools
	- PerfTips
	- Performance Profiler
	- Diagnostic Tools

- Event Tracing
	- ETW (Event Tracing for Windows): high frequency logging framework built into Windows to giagnose performance issues
	- PerfView: Collects and visualizes ETW events

- MicroBenchmarking
	- BenchmarkDotNet