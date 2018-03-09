# Jack-Sharp
Jack-Sharp is a .NET/mono binding for [Jack](http://jackaudio.org/), and contains two projects: JackSharp and NAudio.Jack, a binding of JackSharp for NAudio.

## Tested Platforms
* Debian GNU/Linux sid amd64 and i386
* Windows 8.1

## Dependencies
* [Jack](http://jackaudio.org/)
* .NET >= 3.5 or Mono >= 2.4.0

## JackSharp
C# Wrapper around libjack API. Uses the following classes to structure the API into manageable chunks. Abstracts away all pointers.

Install via NuGet: [nuget install JackSharp](https://www.nuget.org/packages/JackSharp/).

### Client: Base Class for Processor and Controller
Emits events on general Jack information, that consumers can subscribe to. See the source code comments for details.

### Processor
Audio and MIDI client. Useful for creating an application with inputs and outputs.

Add your logic for processing a buffer on audio and MIDI input and output by adding a `Func<JackSharp.Processing.ProcessBuffer>` to `ProcessFunc` of an instance of `JackSharp.Processor`. Multiple methods can be added and removed.

### Controller
Client for controlling the Jack server and connections. Useful for creating a control application.

Can connect and disconnect ports of different applications.

If your application needs functionality from both `Processor` and `Controller`, then you must create instances of both classes in your consumer with different names.

## NAudio.Jack
Binding for `JackSharp.Processor` for [NAudio](https://github.com/naudio). It contains implementations for `IWavePlayer` and `IWaveIn`.

Install via NuGet: [nuget install NAudio.Jack](https://www.nuget.org/packages/NAudio.Jack/).

## CSCore.Jack
Binding for `JackSharp.Processor` for [CSCore](https://github.com/filoe/cscore). It contains implementations for `ISoundOut` and `ISoundIn`.

Install via NuGet: [nuget install NAudio.Jack](https://www.nuget.org/packages/NAudio.Jack/).

## Running Unit Tests
Running unit tests can be a bit tricky, as some unit tests require an already running instance of Jack, while others require, that Jack must be started by the tested objects themselves. 

The following test class from the project `JackSharpTest` must be run without Jack:

* `ServerStartTest`
