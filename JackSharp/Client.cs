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
using JackSharp.Pointers;
using JackSharp.Ports;
using JackSharp.ApiWrapper;
using JackSharp.Events;
using JackSharp.Processing;

namespace JackSharp
{
	public class Client : IDisposable
	{
		readonly string _name;
		AudioInPort[] _audioInPorts;
		AudioOutPort[] _audioOutPorts;
		MidiInPort[] _midiInPorts;
		MidiOutPort[] _midiOutPorts;
		unsafe UnsafeStructs.jack_client_t* _jackClient;
		bool _isStarted;

		/// <summary>
		/// Delegates to be called on the process callback of Jack. Multiple Actions can be added.
		/// </summary>
		public Action<Chunk> ProcessFunc { get; set; }

		public Client (string name, int audioInPorts = 0, int audioOutPorts = 0, int midiInPorts = 0,
		               int midiOutPorts = 0)
		{
			_name = name;
			SetUpPorts (audioInPorts, audioOutPorts, midiInPorts, midiOutPorts);

			SetUpCallbacks ();
		}

		~Client ()
		{
			Dispose (false);
		}

		public void Dispose ()
		{
			Dispose (true);
			GC.SuppressFinalize (this);
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
			_bufferSizeCallback = OnBufferSizeChange;
			_sampleRateCallback = OnSampleRateChange;
		}

		public int SampleRate { get; private set; }

		public int BufferSize { get; private set; }

		public IEnumerable<MidiOutPort> MidiOutPorts { get { return _midiOutPorts; } }

		public IEnumerable<MidiInPort> MidiInPorts { get { return _midiInPorts; } }

		public IEnumerable<AudioOutPort> AudioOutPorts { get { return _audioOutPorts; } }

		public IEnumerable<AudioInPort> AudioInPorts { get { return _audioInPorts; } }

		Callbacks.JackProcessCallback _processCallback;
		Callbacks.JackBufferSizeCallback _bufferSizeCallback;

		public event EventHandler<BufferSizeEventArgs> BufferSizeChanged;

		Callbacks.JackSampleRateCallback _sampleRateCallback;

		public event EventHandler<SampleRateEventArgs> SampleRateChanged;

		unsafe void WireUpCallbacks ()
		{
			ClientCallbackApi.jack_set_process_callback (_jackClient, _processCallback, IntPtr.Zero);
			ClientCallbackApi.jack_set_buffer_size_callback (_jackClient, _bufferSizeCallback, IntPtr.Zero);
			ClientCallbackApi.jack_set_sample_rate_callback (_jackClient, _sampleRateCallback, IntPtr.Zero);
		}

		/// <summary>
		/// Activates the client and connects to Jack.
		/// </summary>
		/// <returns>[true] is starting was successful, else [false].</returns>
		public unsafe bool Start ()
		{
			if (_isStarted) {
				return false;
			}
			if (!Open ()) {
				return false;
			}
			int status = ClientApi.jack_activate (_jackClient);
			if (status != 0) {
				return false;
			}
			SampleRate = (int)Invoke.jack_get_sample_rate (_jackClient);
			BufferSize = (int)Invoke.jack_get_buffer_size (_jackClient);
			_isStarted = true;
			return true;
		}

		unsafe bool Open ()
		{
			if (_jackClient != null) {
				return true;
			}
			_jackClient = ClientApi.jack_client_open (_name, JackOptions.JackNullOption, IntPtr.Zero);
			if (_jackClient == null) {
				return false;
			}
			CreatePorts ();
			WireUpCallbacks ();
			return true;
		}

		int OnProcess (uint nframes, IntPtr arg)
		{
			AudioBuffer[] audioInBuffers = _audioInPorts.Select (p => p.GetAudioBuffer (nframes)).ToArray ();
			AudioBuffer[] audioOutBuffers = _audioOutPorts.Select (p => p.GetAudioBuffer (nframes)).ToArray ();
			MidiEventCollection[] midiInEvents = _midiInPorts.Select (p => p.GetMidiBuffer (nframes)).ToArray ();

			if (ProcessFunc != null) {
				ProcessFunc (new Chunk {
					AudioIn = audioInBuffers,
					AudioOut = audioOutBuffers,
					MidiIn = midiInEvents
				});
			}
			foreach (var audioInBuffer in audioInBuffers) {
				audioInBuffer.CopyToPointer ();
			}
			foreach (var audioOutBuffer in audioOutBuffers) {
				audioOutBuffer.CopyToPointer ();
			}

			return 0;
		}

		int OnSampleRateChange (uint nframes, IntPtr arg)
		{
			SampleRate = (int)nframes;
			if (SampleRateChanged != null) {
				SampleRateChanged (this, new SampleRateEventArgs (SampleRate));
			}
			return 0;
		}

		int OnBufferSizeChange (uint nframes, IntPtr arg)
		{
			BufferSize = (int)nframes;
			if (BufferSizeChanged != null) {
				BufferSizeChanged (this, new BufferSizeEventArgs (BufferSize));
			}
			return 0;
		}

		public unsafe bool Stop ()
		{
			bool status = ClientApi.jack_deactivate (_jackClient) == 0;
			_isStarted = !status;

			return status;
		}

		unsafe void Close ()
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
			int status = ClientApi.jack_client_close (_jackClient);
			if (status == 0) {
				_jackClient = null;
			}
			return;
		}

		unsafe void CreatePorts ()
		{
			for (int i = 0; i < _audioInPorts.Length; i++) {
				_audioInPorts [i] = new AudioInPort (_jackClient, i);
			}
			for (int i = 0; i < _audioOutPorts.Length; i++) {
				_audioOutPorts [i] = new AudioOutPort (_jackClient, i);
			}
			for (int i = 0; i < _midiInPorts.Length; i++) {
				_midiInPorts [i] = new MidiInPort (_jackClient, i);
			}
			for (int i = 0; i < _midiOutPorts.Length; i++) {
				_midiOutPorts [i] = new MidiOutPort (_jackClient, i);
			}
		}

		void Dispose (bool isDisposing)
		{
			Stop ();
			Close ();
		}
	}
}
