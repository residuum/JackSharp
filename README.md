# Jack-Sharp
Jack-Sharp is a .NET/mono binding for [Jackd](http://jackaudio.org/), and contains two projects: JackSharp and NAudio.Jack, a Binding of JackSharp for NAudio.

## Tested Platforms
* Debian GNU/Linux sid amd64 and i386
* Windows 8.1

## JackSharp
C# Wrapper around libjack API. Uses the following classes to structure the API into manageable chunks. Abstracts away all pointers.

Install via NuGet: [nuget install JackSharp](https://www.nuget.org/packages/JackSharp/).

### ClientBase: Base Class for Client and Controller
Emits events on general Jack information, that consumers can subscribe to. See the source code comments for details.

### Client
Audio and MIDI client. Useful for creating an application with inputs and outputs.

Add your logic for processing a buffer on audio and MIDI input and output by adding a `Func<JackSharp.Processing.ProcessBuffer>` to `ProcessFunc` of an instance of `JackSharp.Client`. Multiple methods can be added and removed.

### Controller
Client for controlling the Jack server and connections. Useful for creating a control application.

Can connect and disconnect ports of different applications.

If your application needs functionality from both `Client` and `Controller`, then you must create instances of both classes in your consumer with different names.

## NAudio.Jack
Binding for `JackSharp.Client` for [NAudio](https://github.com/naudio). It contains implementations for `IWavePlayer` and `IWaveIn`.

Install via NuGet: [nuget install NAudio.Jack](https://www.nuget.org/packages/NAudio.Jack/).

## Running Unit Tests
Unit test require an already running instance of Jack to run successfully. 
