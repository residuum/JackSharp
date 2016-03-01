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
	/// <summary>
	/// Port for client to contain data to process.
	/// </summary>
	public abstract class Port : IDisposable
	{
		internal unsafe UnsafeStructs.jack_client_t* _jackClient;
		internal unsafe UnsafeStructs.jack_port_t* _port;

		/// <summary>
		/// Gets the type of the port.
		/// </summary>
		/// <value>The type of the port.</value>
		public PortType PortType { get; private set; }

		/// <summary>
		/// Gets the direction of the port.
		/// </summary>
		/// <value>The direction.</value>
		public Direction Direction { get; private set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }

		internal unsafe Port (UnsafeStructs.jack_client_t*jackClient, int index, Direction direction, PortType portType, string nameFormat)
		{
			if (nameFormat == null) {
				nameFormat = "{type}{direction}_{index}";
			}
			_jackClient = jackClient;
			Direction = direction;
			Name = CreateName (nameFormat, index, direction, portType);
			PortType = portType;
			_port = RegisterPort (direction, portType);
		}

		private string CreateName (string nameFormat, int index, Direction direction, PortType portType)
		{
			string typeName = portType == PortType.Audio ? "audio" : "midi";
			string directionName = direction == Direction.In ? "in" : "out";
			return nameFormat.Replace ("{type}", typeName)
	            .Replace ("{direction}", directionName)
	            .Replace ("{index}", (index + 1).ToString ());
		}

		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the <see cref="JackSharp.Ports.Port"/>
		/// is reclaimed by garbage collection.
		/// </summary>
		~Port ()
		{
			Dispose (false);
		}

		/// <summary>
		/// Releases all resource used by the <see cref="JackSharp.Ports.Port"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="JackSharp.Ports.Port"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="JackSharp.Ports.Port"/> in an unusable state. After calling
		/// <see cref="Dispose"/>, you must release all references to the <see cref="JackSharp.Ports.Port"/> so the garbage
		/// collector can reclaim the memory that the <see cref="JackSharp.Ports.Port"/> was occupying.</remarks>
		public void Dispose ()
		{
			Dispose (true);
			GC.SuppressFinalize (this);
		}

		unsafe UnsafeStructs.jack_port_t* RegisterPort (Direction direction, PortType portType)
		{
			var typeName = GetTypeName (portType);
			var flags = GetJackPortFlags (direction);
			return PortApi.Register (_jackClient, Name, typeName, flags, 0);
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

		internal unsafe StructPointer<float> GetBuffer (uint nframes)
		{
			Debug.Assert (PortType == PortType.Audio);
			return new StructPointer<float> ((IntPtr)PortApi.GetBuffer (_port, nframes), nframes);
		}

		unsafe void Dispose (bool isDisposing)
		{
			if (_jackClient == null || _port == null) {
				return;
			}
			if (PortApi.Unregister (_jackClient, _port) == 0){
				_jackClient = null;
				_port = null;		
			}
		}

		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="JackSharp.Ports.Port"/>.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="JackSharp.Ports.Port"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
		/// <see cref="JackSharp.Ports.Port"/>; otherwise, <c>false</c>.</returns>
		public override bool Equals (object obj)
		{
			Port otherPort = obj as Port;
			return Equals (otherPort);
		}

		/// <summary>
		/// Determines whether the specified <see cref="JackSharp.Ports.Port"/> is equal to the current <see cref="JackSharp.Ports.Port"/>.
		/// </summary>
		/// <param name="other">The <see cref="JackSharp.Ports.Port"/> to compare with the current <see cref="JackSharp.Ports.Port"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="JackSharp.Ports.Port"/> is equal to the current
		/// <see cref="JackSharp.Ports.Port"/>; otherwise, <c>false</c>.</returns>
		public bool Equals (Port other)
		{
			if (other == null)
				return false;
			unsafe {
				return _port == other._port && _jackClient == other._jackClient;
			}
		}

		/// <summary>
		/// Serves as a hash function for a <see cref="JackSharp.Ports.Port"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		public override int GetHashCode ()
		{
			unsafe {
				return ((IntPtr)_port).GetHashCode () << 3 & ((IntPtr)_jackClient).GetHashCode ();
			}
		}

		/// <param name="a">The Port a.</param>
		/// <param name="b">The Port b.</param>
		public static bool operator == (Port a, Port b)
		{
			if (object.ReferenceEquals (a, b)) {
				return true;
			}

			if (((object)a == null) || ((object)b == null)) {
				return false;
			}
			return (a.Equals (b));
		}

		/// <param name="a">The Port a.</param>
		/// <param name="b">The Port b.</param>
		public static bool operator != (Port a, Port b)
		{
			return !(a == b);
		}
	}
}
