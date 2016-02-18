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
using JackSharp.ApiWrapper;
using System.Collections.Generic;
using System;
using JackSharp.Processing;

namespace JackSharp.Ports
{
	public class MidiInPort : Port
	{
		internal unsafe MidiInPort (UnsafeStructs.jack_client_t* jackClient, int index) : base (jackClient, index, Direction.In, PortType.Midi)
		{
		}

		internal unsafe List<MidiEvent> GetMidiEvents (uint nframes)
		{
			List<MidiEvent> midiEvents = new List<MidiEvent> ();

			IntPtr portBuffer = (IntPtr)PortApi.jack_port_get_buffer (_port, nframes);
			uint eventCount = MidiApi.jack_midi_get_event_count (portBuffer);
			for (uint i = 0; i < eventCount; i++) {
				UnsafeStructs.jack_midi_event_t inEvent;
				MidiApi.jack_midi_event_get (&inEvent, portBuffer, i);
				midiEvents.Add (new MidiEvent (inEvent));
			}
			return midiEvents;
		}
	}
}
