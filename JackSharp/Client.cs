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
using JackSharp.Processing;

namespace JackSharp
{
	public class Client : ClientBase
	{
		AudioInPort[] _audioInPorts;
		AudioOutPort[] _audioOutPorts;
		MidiInPort[] _midiInPorts;
		MidiOutPort[] _midiOutPorts;

		/// <summary>
		/// Delegates to be called on the process callback of Jack. Multiple Actions can be added.
		/// </summary>
		public Action<ProcessBuffer> ProcessFunc { get; set; }

		public Client (string name, int audioInPorts = 0, int audioOutPorts = 0, int midiInPorts = 0, int midiOutPorts = 0) : base (name)
		{
			SetUpPorts (audioInPorts, audioOutPorts, midiInPorts, midiOutPorts);
			SetUpCallbacks ();
		}

		~Client ()
		{
			base.Dispose (false);
		}

		public new void Dispose ()
		{
			base.Dispose ();
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

		Callbacks.JackProcessCallback _processCallback;

		/// <summary>
		/// Activates the client and connects to Jack.
		/// </summary>
		public new bool Start ()
		{
			return base.Start ();
		}

		protected override bool Open ()
		{
			ClientStatus status = BaseOpen ();
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
		/// Stop this instance and siconnects from Jack.
		/// </summary>
		public new bool Stop ()
		{
			return base.Stop ();
		}

		protected override void Close ()
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
			base.Close ();
		}

		unsafe void CreatePorts ()
		{
			for (int i = 0; i < _audioInPorts.Length; i++) {
				_audioInPorts [i] = new AudioInPort (JackClient, i);
			}
			for (int i = 0; i < _audioOutPorts.Length; i++) {
				_audioOutPorts [i] = new AudioOutPort (JackClient, i);
			}
			for (int i = 0; i < _midiInPorts.Length; i++) {
				_midiInPorts [i] = new MidiInPort (JackClient, i);
			}
			for (int i = 0; i < _midiOutPorts.Length; i++) {
				_midiOutPorts [i] = new MidiOutPort (JackClient, i);
			}
		}
	}
}
