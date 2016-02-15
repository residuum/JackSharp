// Author:
//       Thomas Mayer <thomas@residuum.org>
//
// Copyright (c) 2016 Thomas Mayer
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using JackSharp.Pointers;

namespace JackSharp.Ports
{
	public class AudioInPort : Port
	{
		internal unsafe AudioInPort (UnsafeStructs.jack_client_t* jackClient, int index) : base (jackClient, index, Direction.In, PortType.Audio)
		{
		}
	}

	public class AudioOutPort : Port
	{
		internal unsafe AudioOutPort (UnsafeStructs.jack_client_t* jackClient, int index) : base (jackClient, index, Direction.Out, PortType.Audio)
		{
		}
	}

	public class MidiInPort : Port
	{
		internal unsafe MidiInPort (UnsafeStructs.jack_client_t* jackClient, int index) : base (jackClient, index, Direction.In, PortType.Midi)
		{
		}
	}

	public class MidiOutPort : Port
	{
		internal unsafe MidiOutPort (UnsafeStructs.jack_client_t* jackClient, int index) : base (jackClient, index, Direction.Out, PortType.Midi)
		{
		}
	}
}
