// Author:
//	   Thomas Mayer <thomas@residuum.org>
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
	/// <summary>
	/// Port reference for Controller.
	/// </summary>
	public class PortReference
	{
		/// <summary>
		/// Gets the direction.
		/// </summary>
		/// <value>The direction.</value>
		public Direction Direction { get; private set; }

		/// <summary>
		/// Gets whether the port is corresponding to a physical inlet or outlet.
		/// </summary>
		/// <value>[true] if port is physical.</value>
		public bool IsPhysicalPort { get; private set; }

		/// <summary>
		/// Gets the type of the port.
		/// </summary>
		/// <value>The type of the port.</value>
		public PortType PortType { get; private set; }

		internal string FullName { get; set; }

		/// <summary>
		/// Gets the name of the client.
		/// </summary>
		/// <value>The name of the client.</value>
		public string ClientName {
			get { return FullName.Split (new char[] { ':' }, 2) [0]; }
		}

		/// <summary>
		/// Gets the name of the port.
		/// </summary>
		/// <value>The name of the port.</value>
		public string PortName {
			get { return FullName.Split (new char[] { ':' }, 2) [1]; }
		}

		internal unsafe UnsafeStructs.jack_port_t* PortPointer { get; private set; }

		internal unsafe PortReference (UnsafeStructs.jack_port_t* portPointer)
		{
			PortPointer = portPointer;
			FullName = PortApi.GetName (portPointer).PtrToString ();
			bool isPhysicalPort;
			Direction direction;
			ReadJackPortFlags (portPointer, out direction, out isPhysicalPort);
			Direction = direction;
			IsPhysicalPort = isPhysicalPort;
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

		static unsafe void ReadJackPortFlags (UnsafeStructs.jack_port_t* portPointer, out Direction direction, out bool isPhysicalPort)
		{
			isPhysicalPort = false;
			JackPortFlags portFlags = (JackPortFlags)PortApi.GetPortFlags (portPointer);
			if ((portFlags & JackPortFlags.JackPortIsPhysical) == JackPortFlags.JackPortIsPhysical) {
				isPhysicalPort = true;
			}
			if ((portFlags & JackPortFlags.JackPortIsInput) == JackPortFlags.JackPortIsInput) {
				direction = Direction.In;
				return;
			}
			if ((portFlags & JackPortFlags.JackPortIsOutput) == JackPortFlags.JackPortIsOutput) {
				direction = Direction.Out;
				return;
			}
			throw new IndexOutOfRangeException ("jack_port_flags");
		}

		public override bool Equals (object obj)
		{
			PortReference otherPort = obj as PortReference;
			return Equals (otherPort);
		}

		public bool Equals (PortReference other)
		{
			if (other == null)
				return false;
			unsafe {
				return PortPointer == other.PortPointer;
			}
		}

		public override int GetHashCode ()
		{
			unsafe {
				return ((IntPtr)PortPointer).GetHashCode ();
			}
		}

		public static bool operator == (PortReference a, PortReference b)
		{
			if (object.ReferenceEquals (a, b)) {
				return true;
			}

			if (((object)a == null) || ((object)b == null)) {
				return false;
			}
			return (a.Equals (b));
		}

		public static bool operator != (PortReference a, PortReference b)
		{
			return !(a == b);
		}
	}
}
