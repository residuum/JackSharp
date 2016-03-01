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
using System.Collections.Generic;
using System.Linq;
using JackSharp.Ports;
using JackSharp.ApiWrapper;
using JackSharp.Pointers;
using JackSharp.Processing;

namespace JackSharp
{
	/// <summary>
	/// Processor.
	/// </summary>
	public sealed class Processor : Client
	{
		AudioInPort[] _audioInPorts;
		AudioOutPort[] _audioOutPorts;
		MidiInPort[] _midiInPorts;
		MidiOutPort[] _midiOutPorts;
		readonly bool _autoconnect;

		/// <summary>
		/// Delegates to be called on the process callback of Jack. Multiple Actions can be added.
		/// </summary>
		public Action<ProcessBuffer> ProcessFunc { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="JackSharp.Processor"/> class.
		/// </summary>
		/// <param name="name">Name.</param>
		/// <param name="audioInPorts">Number of audio in ports.</param>
		/// <param name="audioOutPorts">Number of audio out ports.</param>
		/// <param name="midiInPorts">Number of MIDI in ports.</param>
		/// <param name="midiOutPorts">Number of MIDI out ports.</param>
		/// <param name="autoconnect">If set to <c>true</c>, autoconnect inlets and outlets to physical ports.</param>
		public Processor (string name, int audioInPorts = 0, int audioOutPorts = 0, int midiInPorts = 0, int midiOutPorts = 0, bool autoconnect = false) : base (name)
		{
			_autoconnect = autoconnect;
			SetUpPorts (audioInPorts, audioOutPorts, midiInPorts, midiOutPorts);
			SetUpCallbacks ();
		}

		/// <summary>
		/// Releases unmanaged resources and performs other cleanup operations before the <see cref="JackSharp.Processor"/> is
		/// reclaimed by garbage collection.
		/// </summary>
		~Processor ()
		{
			Dispose (false);
		}

		/// <summary>
		/// Releases all resource used by the <see cref="JackSharp.Processor"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="JackSharp.Processor"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="JackSharp.Processor"/> in an unusable state. After calling
		/// <see cref="Dispose"/>, you must release all references to the <see cref="JackSharp.Processor"/> so the garbage
		/// collector can reclaim the memory that the <see cref="JackSharp.Processor"/> was occupying.</remarks>
		public new void Dispose ()
		{
			Dispose (true);
		}

		void SetUpPorts (int audioInPorts, int audioOutPorts, int midiInPorts, int midiOutPorts)
		{
			_audioInPorts = new AudioInPort[audioInPorts];
			_audioOutPorts = new AudioOutPort[audioOutPorts];
			_midiInPorts = new MidiInPort[midiInPorts];
			_midiOutPorts = new MidiOutPort[midiOutPorts];
		}

		void SetUpCallbacks ()
		{
			_processCallback = OnProcess;
		}

		/// <summary>
		/// Gets the MIDI out ports.
		/// </summary>
		/// <value>The MIDI out ports.</value>
		public IEnumerable<MidiOutPort> MidiOutPorts { get { return _midiOutPorts; } }

		/// <summary>
		/// Gets the MIDI in ports.
		/// </summary>
		/// <value>The MIDI in ports.</value>
		public IEnumerable<MidiInPort> MidiInPorts { get { return _midiInPorts; } }

		/// <summary>
		/// Gets the audio out ports.
		/// </summary>
		/// <value>The audio out ports.</value>
		public IEnumerable<AudioOutPort> AudioOutPorts { get { return _audioOutPorts; } }

		/// <summary>
		/// Gets the audio in ports.
		/// </summary>
		/// <value>The audio in ports.</value>
		public IEnumerable<AudioInPort> AudioInPorts { get { return _audioInPorts; } }

		/// <summary>
		/// Sets the port name format.
		/// 
		/// Placeholders for replacement: 
		/// 	{direction}: will be replaced by "in" or "out".
		/// 	{type}: will be replaced by "audio" or "midi".
		/// 	{index}: 1-based index of ports.
		/// </summary>
		/// <value>The port name format.</value>
		public string PortNameFormat { private get; set; }

		Callbacks.JackProcessCallback _processCallback;

		/// <summary>
		/// Activates the client and connects to Jack.
		/// </summary>
		/// <param name="startServer">If [true], the client will start Jack if it is not running.</param>
		public new bool Start (bool startServer = false)
		{
			if (!base.Start (startServer)) {
				return false;
			}
			if (_autoconnect) {
				AutoConnectPorts ();
			}
			return true;
		}

		internal override bool Open (bool startServer)
		{
			ClientStatus status = BaseOpen (startServer);
			switch (status) {
			case ClientStatus.AlreadyThere:
				return true;
			case ClientStatus.Failure:
				return false;
			case ClientStatus.New:
				CreatePorts ();
				WireUpCallbacks ();
				WireUpBaseCallbacks ();
				return true;
			}
			return false;
		}

		unsafe void WireUpCallbacks ()
		{
			ClientCallbackApi.SetProcessCallback (JackClient, _processCallback, IntPtr.Zero);
		}

		int OnProcess (uint nframes, IntPtr arg)
		{
			AudioBuffer[] audioInBuffers = _audioInPorts.Select (p => p.GetAudioBuffer (nframes)).ToArray ();
			AudioBuffer[] audioOutBuffers = _audioOutPorts.Select (p => p.GetAudioBuffer (nframes)).ToArray ();
			MidiEventCollection<MidiInEvent>[] midiInEvents = _midiInPorts.Select (p => p.GetMidiBuffer (nframes)).ToArray ();
			MidiEventCollection<MidiOutEvent>[] midiOutEvents = _midiOutPorts.Select (p => p.GetMidiBuffer ()).ToArray ();

			if (ProcessFunc != null) {
				ProcessFunc (new ProcessBuffer (nframes, audioInBuffers, audioOutBuffers, midiInEvents, midiOutEvents));
			}
			foreach (var audioInBuffer in audioInBuffers) {
				audioInBuffer.CopyToPointer ();
			}
			foreach (var audioOutBuffer in audioOutBuffers) {
				audioOutBuffer.CopyToPointer ();
			}
			foreach (MidiEventCollection<MidiOutEvent> midiEvents in midiOutEvents) {
				midiEvents.WriteToJackMidi (nframes);
			}

			return 0;
		}

		/// <summary>
		/// Stop this instance and disconnects from Jack.
		/// </summary>
		public new bool Stop ()
		{
			DisposePorts ();
			return base.Stop ();
		}

		void DisposePorts() 
		{
			for (int i = _midiOutPorts.Length - 1; i >= 0; i--) {
				if (_midiOutPorts [i] != null) {
					_midiOutPorts [i].Dispose ();
				}
			}
			for (int i = _midiInPorts.Length - 1; i >= 0; i--) {
				if (_midiInPorts [i] != null) {
					_midiInPorts [i].Dispose ();
				}
			}
			for (int i = _audioOutPorts.Length - 1; i >= 0; i--) {
				if (_audioOutPorts [i] != null) {
					_audioOutPorts [i].Dispose ();
				}
			}
			for (int i = _audioInPorts.Length - 1; i >= 0; i--) {
				if (_audioInPorts [i] != null) {
					_audioInPorts [i].Dispose ();
				}
			}
		}

		unsafe void CreatePorts ()
		{
			for (int i = 0; i < _audioInPorts.Length; i++) {
				_audioInPorts [i] = new AudioInPort (JackClient, i, PortNameFormat);
			}
			for (int i = 0; i < _audioOutPorts.Length; i++) {
				_audioOutPorts [i] = new AudioOutPort (JackClient, i, PortNameFormat);
			}
			for (int i = 0; i < _midiInPorts.Length; i++) {
				_midiInPorts [i] = new MidiInPort (JackClient, i, PortNameFormat);
			}
			for (int i = 0; i < _midiOutPorts.Length; i++) {
				_midiOutPorts [i] = new MidiOutPort (JackClient, i, PortNameFormat);
			}
		}

		unsafe void AutoConnectPorts ()
		{
			List<PortReference> ports = GetAllJackPorts ().Where (p => p.IsPhysicalPort).ToList ();

			List<string> outlets = ports.Where (p => p.Direction == Direction.Out && p.PortType == PortType.Audio).Select (p => p.FullName).ToList ();
			List<string> inlets = _audioInPorts.Select (p => PortApi.GetName (p._port).PtrToString ()).ToList ();
			ConnectPorts (outlets, inlets);

			outlets = _audioOutPorts.Select (p => PortApi.GetName (p._port).PtrToString ()).ToList ();
			inlets = ports.Where (p => p.Direction == Direction.In && p.PortType == PortType.Audio).Select (p => p.FullName).ToList ();
			ConnectPorts (outlets, inlets);

			outlets = ports.Where (p => p.Direction == Direction.Out && p.PortType == PortType.Midi).Select (p => p.FullName).ToList ();
			inlets = _midiInPorts.Select (p => PortApi.GetName (p._port).PtrToString ()).ToList ();
			ConnectPorts (outlets, inlets);

			outlets = _midiOutPorts.Select (p => PortApi.GetName (p._port).PtrToString ()).ToList ();
			inlets = ports.Where (p => p.Direction == Direction.In && p.PortType == PortType.Midi).Select (p => p.FullName).ToList ();
			ConnectPorts (outlets, inlets);
		}

		unsafe void ConnectPorts (List<string> outlets, List<string> inlets)
		{
			for (int i = 0; i < Math.Min (outlets.Count, inlets.Count); i++) {
				PortApi.Connect (JackClient, outlets [i], inlets [i]);
			}
		}
	}
}
