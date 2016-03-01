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
using JackSharp.ApiWrapper;
using JackSharp.Events;
using JackSharp.Pointers;
using JackSharp.Ports;

namespace JackSharp
{
	/// <summary>
	/// Base class for JackClients.
	/// </summary>
	public abstract class Client: IDisposable
	{
		internal unsafe UnsafeStructs.jack_client_t* JackClient;

		/// <summary>
		/// Gets whether the client is connected to Jack.
		/// </summary>
		/// <value>[true] if client is connected to Jack.</value>
		public bool IsConnectedToJack { get; private set; }

		protected readonly string Name;

		protected Client (string name)
		{
			Name = name;
			SetUpBaseCallbacks ();
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

		/// <summary>
		/// Gets the sample rate.
		/// </summary>
		/// <value>The sample rate.</value>
		public int SampleRate { get; private set; }

		/// <summary>
		/// Gets the size of the buffer.
		/// </summary>
		/// <value>The size of the buffer.</value>
		public int BufferSize { get; private set; }


		Callbacks.JackBufferSizeCallback _bufferSizeCallback;

		/// <summary>
		/// Occurs when buffer size has changed.
		/// </summary>
		public event EventHandler<BufferSizeEventArgs> BufferSizeChanged;

		Callbacks.JackSampleRateCallback _sampleRateCallback;

		/// <summary>
		/// Occurs when sample rate has changed.
		/// </summary>
		public event EventHandler<SampleRateEventArgs> SampleRateChanged;

		Callbacks.JackShutdownCallback _shutdownCallback;

		/// <summary>
		/// Occurs when jack shuts down.
		/// </summary>
		public event EventHandler<EventArgs> Shutdown;

		object _jackErrorFunction;

		object _jackInfoFunction;

		/// <summary>
		/// Occurs on xrun.
		/// </summary>
		public event EventHandler<XrunEventArgs> Xrun;

		public event EventHandler<NotAvailableEventArgs> NotAvailable;

		protected void InvokeNotAvaible (string eventName)
		{
			if (NotAvailable != null) {
				NotAvailable (this, new NotAvailableEventArgs ("Port Rename"));
			}
		}

		Callbacks.JackXRunCallback _jackXrunCallback;

		void SetUpBaseCallbacks ()
		{
			_bufferSizeCallback = OnBufferSizeChange;
			_sampleRateCallback = OnSampleRateChange;
			_shutdownCallback = OnShutdown;
			//	_jackErrorFunction = OnJackError;
			//	_jackInfoFunction = OnJackInfo;6
			_jackXrunCallback = OnJackXrun;
		}

		protected unsafe void WireUpBaseCallbacks ()
		{
			ClientCallbackApi.SetBufferSizeCallback (JackClient, _bufferSizeCallback, IntPtr.Zero);
			ClientCallbackApi.SetSampleRateCallback (JackClient, _sampleRateCallback, IntPtr.Zero);
			ClientCallbackApi.SetShutdownCallback (JackClient, _shutdownCallback, IntPtr.Zero);
			//ClientCallbackApi.SetErrorFunction (JackClient, _jackErrorFunction, IntPtr.Zero);
			//ClientCallbackApi.SetInfoFunction (JackClient, _jackInfoFunction, IntPtr.Zero);
			ClientCallbackApi.SetXrunCallback (JackClient, _jackXrunCallback, IntPtr.Zero);
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

		unsafe void OnShutdown (IntPtr args)
		{
			IsConnectedToJack = false;
			JackClient = null;
			if (Shutdown != null) {
				Shutdown (this, new EventArgs ());
			}
		}

		unsafe int OnJackXrun (IntPtr args)
		{
			float xrunDelay = Invoke.GetXrunDelayedUsecs (JackClient);
			if (xrunDelay > 0 && Xrun != null) { 
				Xrun (this, new XrunEventArgs (xrunDelay));
			}
			return 0;
		}

		internal abstract bool Open (bool startServer);

		protected unsafe ClientStatus BaseOpen (bool startServer)
		{
			if (JackClient != null) {
				return ClientStatus.AlreadyThere;
			}
			JackOptions startOptions = startServer ? JackOptions.JackNullOption : JackOptions.JackNoStartServer;
			JackClient = ClientApi.Open (Name, startOptions, IntPtr.Zero);
			if (JackClient == null) {
				return ClientStatus.Failure;
			}
			return ClientStatus.New;
		}

		protected virtual unsafe bool Start (bool startServer)
		{
			if (IsConnectedToJack) {
				return false;
			}
			if (!Open (startServer)) {
				return false;
			}
			int status = ClientApi.Activate (JackClient);
			if (status != 0) {
				return false;
			}
			SampleRate = (int)Invoke.GetSampleRate (JackClient);
			BufferSize = (int)Invoke.GetBufferSize (JackClient);
			IsConnectedToJack = true;
			return true;
		}

		protected virtual unsafe bool Stop ()
		{
			bool status = ClientApi.Deactivate (JackClient) == 0;
			if (status) {
				IsConnectedToJack = false;
				Close ();
			}
			return status;
		}

		protected unsafe void Close ()
		{
			int status = ClientApi.Close (JackClient);
			if (status == 0) {
				IsConnectedToJack = false;
				JackClient = null;
			}
		}

		protected void Dispose (bool isDisposing)
		{
			Stop ();
		}

		protected enum ClientStatus
		{
			AlreadyThere,
			New,
			Failure
		}

		protected unsafe List<PortReference> GetAllJackPorts ()
		{
			IntPtr initialPorts = PortApi.GetPorts (JackClient, null, null, 0);
			List<PortReference> ports = PortListFromPointer (initialPorts);
			Invoke.Free (initialPorts);
			return ports;
		}

		protected List<PortReference> PortListFromPointer (IntPtr initialPorts)
		{
			List<PortReference> ports = initialPorts.PtrToStringArray ().Select (MapPort).ToList ();
			return ports;
		}

		unsafe PortReference MapPort (string portName)
		{
			UnsafeStructs.jack_port_t* portPointer = PortApi.GetPortByName (JackClient, portName);
			if (portPointer == null) {
				return null;
			}
			return new PortReference (portPointer);
		}
	}
}
