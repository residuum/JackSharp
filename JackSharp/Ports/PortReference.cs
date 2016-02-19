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
using System;
using JackSharp.ApiWrapper;
using JackSharp.Pointers;

namespace JackSharp.Ports
{
	public class PortReference
	{
		public Direction Direction { get; private set; }

		public PortType PortType { get; private set; }

		internal string FullName { get; set; }

		public string ClientName {
			get { return FullName.Split (new char[] { ':' }, 2) [0]; }
		}

		public string PortName {
			get { return FullName.Split (new char[] { ':' }, 2) [1]; }
		}

		internal unsafe UnsafeStructs.jack_port_t* PortPointer { get; private set; }

		internal unsafe PortReference (UnsafeStructs.jack_port_t* portPointer)
		{
			PortPointer = portPointer;
			FullName = PortApi.GetName (portPointer).PtrToString ();
			Direction = GetDirection (portPointer);
			PortType = GetPortType (portPointer);
		}

		static unsafe PortType GetPortType (UnsafeStructs.jack_port_t* portPointer)
		{
			string connectionTypeName = PortApi.GetType (portPointer).PtrToString ();
			switch (connectionTypeName) {
			case Constants.JACK_DEFAULT_AUDIO_TYPE:
				return PortType.Audio;
			case Constants.JACK_DEFAULT_MIDI_TYPE:
				return PortType.Midi;
			}
			throw new IndexOutOfRangeException ("jack_port_type");
		}

		static unsafe Direction GetDirection (UnsafeStructs.jack_port_t* portPointer)
		{
			JackPortFlags portFlags = (JackPortFlags)PortApi.GetPortFlags (portPointer);
			if ((portFlags & JackPortFlags.JackPortIsInput) == JackPortFlags.JackPortIsInput) {
				return Direction.In;
			}
			if ((portFlags & JackPortFlags.JackPortIsOutput) == JackPortFlags.JackPortIsOutput) {
				return Direction.Out;
			}
			throw new IndexOutOfRangeException ("jack_port_flags");
		}
	}
}
