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
using JackSharp.Events;
using JackSharp.Pointers;

namespace JackSharp
{
	public abstract class ClientBase: IDisposable
	{
		internal unsafe UnsafeStructs.jack_client_t* JackClient;
		protected bool IsStarted;
		protected readonly string Name;

		protected ClientBase (string name)
		{
			Name = name;
			SetUpBaseCallbacks ();
		}

		~ClientBase ()
		{
			Dispose (false);
		}

		public void Dispose ()
		{
			Dispose (true);
			GC.SuppressFinalize (this);
		}

		public int SampleRate { get; private set; }

		public int BufferSize { get; private set; }


		Callbacks.JackBufferSizeCallback _bufferSizeCallback;

		public event EventHandler<BufferSizeEventArgs> BufferSizeChanged;

		Callbacks.JackSampleRateCallback _sampleRateCallback;

		public event EventHandler<SampleRateEventArgs> SampleRateChanged;

		Callbacks.JackShutdownCallback _shutdownCallback;

		Callbacks.JackInfoShutdownCallback _jackInfoShutdown;

		public event EventHandler<EventArgs> Shutdown;

		object _jackErrorFunction;

		object _jackInfoFunction;

		public event EventHandler<XrunEventArgs> Xrun;

		Callbacks.JackXRunCallback _jackXrunCallback;

		void SetUpBaseCallbacks ()
		{
			_bufferSizeCallback = OnBufferSizeChange;
			_sampleRateCallback = OnSampleRateChange;
			_shutdownCallback = OnShutdown;
			_jackInfoShutdown = OnInfoShutdown;
			//	_jackErrorFunction = OnJackError;
			//	_jackInfoFunction = OnJackInfo;
			_jackXrunCallback = OnJackXrun;
		}

		protected unsafe void WireUpBaseCallbacks ()
		{
			ClientCallbackApi.jack_set_buffer_size_callback (JackClient, _bufferSizeCallback, IntPtr.Zero);
			ClientCallbackApi.jack_set_sample_rate_callback (JackClient, _sampleRateCallback, IntPtr.Zero);
			ClientCallbackApi.jack_on_shutdown (JackClient, _shutdownCallback, IntPtr.Zero);
			ClientCallbackApi.jack_on_info_shutdown (JackClient, _jackInfoShutdown, IntPtr.Zero);
			//	ClientCallbackApi.jack_set_error_function (JackClient, _jackErrorFunction, IntPtr.Zero);
			//	ClientCallbackApi.jack_set_info_function (JackClient, _jackInfoFunction, IntPtr.Zero);
			ClientCallbackApi.jack_set_xrun_callback (JackClient, _jackXrunCallback, IntPtr.Zero);
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

		unsafe void OnInfoShutdown (JackStatus code, string reason, IntPtr arg)
		{
			if (JackClient != null) {
				Close ();
			}
			if (Shutdown != null) {
				Shutdown (this, new EventArgs ());
			}
		}

		unsafe void OnShutdown (IntPtr args)
		{
			if (JackClient != null) {
				Close ();
			}
			if (Shutdown != null) {
				Shutdown (this, new EventArgs ());
			}
		}

		unsafe int OnJackXrun (IntPtr args)
		{
			float xrunDelay = Invoke.jack_get_xrun_delayed_usecs (JackClient);
			if (xrunDelay > 0 && Xrun != null) { 
				Xrun (this, new XrunEventArgs (xrunDelay));
			}
			return 0;
		}

		protected abstract bool Open ();

		protected unsafe ClientStatus BaseOpen ()
		{
			if (JackClient != null) {
				return ClientStatus.AlreadyThere;
			}
			JackClient = ClientApi.jack_client_open (Name, JackOptions.JackNullOption, IntPtr.Zero);
			if (JackClient == null) {
				return ClientStatus.Failure;
			}
			return ClientStatus.New;
		}

		protected virtual unsafe bool Start ()
		{
			if (IsStarted) {
				return false;
			}
			if (!Open ()) {
				return false;
			}
			int status = ClientApi.jack_activate (JackClient);
			if (status != 0) {
				return false;
			}
			SampleRate = (int)Invoke.jack_get_sample_rate (JackClient);
			BufferSize = (int)Invoke.jack_get_buffer_size (JackClient);
			IsStarted = true;
			return true;
		}

		protected virtual unsafe bool Stop ()
		{
			bool status = ClientApi.jack_deactivate (JackClient) == 0;
			IsStarted = !status;
			return status;
		}

		protected virtual unsafe void Close ()
		{
			int status = ClientApi.jack_client_close (JackClient);
			if (status == 0) {
				JackClient = null;
			}
		}


		protected void Dispose (bool isDisposing)
		{
			Stop ();
			Close ();
		}

		protected enum ClientStatus
		{
			AlreadyThere,
			New,
			Failure
		}
	}
}

