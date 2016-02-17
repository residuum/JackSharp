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
using System.Diagnostics;
using JackSharp.Pointers;
using JackSharp.ApiWrapper;

namespace JackSharp.Ports
{
	public abstract class Port : IDisposable
	{
		internal readonly unsafe UnsafeStructs.jack_client_t* _jackClient;
		internal readonly unsafe UnsafeStructs.jack_port_t* _port;


		internal unsafe Port (UnsafeStructs.jack_client_t*jackClient, int index, Direction direction, PortType portType)
		{
			_jackClient = jackClient;
			Direction = direction;
			Name = (portType == PortType.Audio ? "audio" : "midi") + (direction == Direction.In ? "in_" : "out_") +
			(index + 1);
			PortType = portType;
			_port = RegisterPort (direction, portType);
		}

		~Port ()
		{
			Dispose (false);
		}

		public void Dispose ()
		{
			Dispose (true);
			GC.SuppressFinalize (this);
		}

		unsafe UnsafeStructs.jack_port_t* RegisterPort (Direction direction, PortType portType)
		{
			var typeName = GetTypeName (portType);
			var flags = GetJackPortFlags (direction);
			return PortApi.jack_port_register (_jackClient, Name, typeName, flags, 0);
		}

		static JackPortFlags GetJackPortFlags (Direction direction)
		{
			JackPortFlags flags = (JackPortFlags)0;
			switch (direction) {
			case Direction.In:
				flags = JackPortFlags.JackPortIsInput;
				break;
			case Direction.Out:
				flags = JackPortFlags.JackPortIsOutput;
				break;
			}
			return flags;
		}

		static string GetTypeName (PortType portType)
		{
			string typeName = "";
			switch (portType) {
			case PortType.Audio:
				typeName = Constants.JACK_DEFAULT_AUDIO_TYPE;
				break;
			case PortType.Midi:
				typeName = Constants.JACK_DEFAULT_MIDI_TYPE;
				break;
			}
			return typeName;
		}

		public PortType PortType { get; set; }

		public Direction Direction { get; set; }

		public string Name { get; set; }

		internal unsafe StructPointer<float> GetBuffer (uint nframes)
		{
			Debug.Assert (PortType == PortType.Audio);
			return new StructPointer<float> ((IntPtr)PortApi.jack_port_get_buffer (_port, nframes), nframes);
		}

		unsafe void Dispose (bool isDisposing)
		{
			PortApi.jack_port_unregister (_jackClient, _port);
		}
	}
}
