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

using System.Collections;
using System.Collections.Generic;
using JackSharp.Ports;

namespace JackSharp.Processing
{
	/// <summary>
	/// Midi event collection.
	/// </summary>
	public class MidiEventCollection<T> : IEnumerable<T>, IProcessingItem where T: IMidiEvent
	{
		/// <summary>
		/// Gets the port.
		/// </summary>
		/// <value>The port.</value>
		public Port Port { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="JackSharp.Processing.MidiEventCollection`1"/> class.
		/// </summary>
		/// <param name="port">Port.</param>
		internal MidiEventCollection (Port port)
		{
			Port = port;
		}

		/// <summary>
		/// Adds a MIDI event.
		/// </summary>
		/// <param name="midiInEvent">Midi event.</param>
		public void AddEvent (T midiEvent)
		{
			_midiEvents.Add (midiEvent);
		}

		readonly List<T> _midiEvents = new List<T> ();

		public IEnumerator<T> GetEnumerator ()
		{
			return _midiEvents.GetEnumerator ();
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return GetEnumerator ();
		}
	}
}