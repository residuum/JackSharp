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
	public class Controller : ClientBase
	{
		Callbacks.JackClientRegistrationCallback _clientRegistration;

		/// <summary>
		/// Occurs when a client has changed.
		/// </summary>
		public event EventHandler<ClientRegistrationEventArgs> ClientChanged;

		Callbacks.JackPortRegistrationCallback _portRegistration;

		/// <summary>
		/// Occurs when a port has changed.
		/// </summary>
		public event EventHandler<PortRegistrationEventArgs> PortChanged;

		Callbacks.JackPortRenameCallback _portRename;
		Callbacks.JackPortConnectCallback _portConnect;

		/// <summary>
		/// Occurs when a connection has changed.
		/// </summary>
		public event EventHandler<ConnectionChangeEventArgs> ConnectionChanged;

		public Controller (string name) : base (name)
		{
			SetUpCallbacks ();
		}

		void SetUpCallbacks ()
		{
			_clientRegistration = OnClientRegistration;
			_portRegistration = OnPortRegistration;
			_portRename = OnPortRename;
			_portConnect = OnPortConnect;
		}

		void OnClientRegistration (string name, int register, IntPtr arg)
		{
			if (ClientChanged != null) {
				ClientChanged (this, new ClientRegistrationEventArgs (name, register > 0 ? ChangeType.New : ChangeType.Deleted));
			}
		}

		void OnPortRegistration (uint portId, int register, IntPtr args)
		{
			if (PortChanged != null) {
				PortReference port = MapPort (portId);
				PortChanged (this,
					new PortRegistrationEventArgs (port, register > 0 ? ChangeType.New : ChangeType.Deleted));
			}
		}

		unsafe PortReference MapPort (uint portId)
		{
			UnsafeStructs.jack_port_t* portPointer = PortApi.GetPortById (JackClient, portId);
			if (portPointer == null) {
				return null;
			}
			return new PortReference (portPointer);
		}

		void OnPortRename (uint portId, string oldName, string newName, IntPtr arg)
		{
			if (PortChanged != null) {
				PortReference port = MapPort (portId);
				port.FullName = newName;
				PortChanged (this, new PortRegistrationEventArgs (port, ChangeType.Renamed));
			}
		}

		void OnPortConnect (uint a, uint b, int connect, IntPtr args)
		{
			if (ConnectionChanged != null) {
				PortReference outlet = MapPort (a);
				PortReference inlet = MapPort (b);
				ConnectionChanged (this,
					new ConnectionChangeEventArgs (outlet, inlet, connect > 0 ? ChangeType.New : ChangeType.Deleted));
			}
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
				SendPortsAndConnections ();
				WireUpCallbacks ();
				WireUpBaseCallbacks ();
				return true;
			}
			return false;
		}

		void SendPortsAndConnections ()
		{
			var allPorts = GetAndSendPorts ();
			GetAndSendConnections (allPorts);
		}

		unsafe void GetAndSendConnections (List<PortReference> allPorts)
		{
			foreach (PortReference port in allPorts) {
				// In ports are connected to out ports, so we only need to map these.
				if (port.Direction == Direction.Out) {
					IntPtr connectedPortNames = PortApi.GetConnections (port.PortPointer);
					List<PortReference> connectedPorts = PortListFromPointer (connectedPortNames);

					if (ConnectionChanged != null) {
						foreach (PortReference connected in connectedPorts) {
							ConnectionChanged (this, new ConnectionChangeEventArgs (port, connected, ChangeType.New));
						}
					}
					Invoke.Free (connectedPortNames);
				}
			}
		}

		unsafe List<PortReference> GetAndSendPorts ()
		{
			List<PortReference> ports = GetAllJackPorts ();
			if (PortChanged != null) {
				foreach (PortReference port in ports) {
					PortChanged (this, new PortRegistrationEventArgs (port, ChangeType.New));
				}
			}
			return ports;
		}

		unsafe void WireUpCallbacks ()
		{
			PortCallbackApi.SetClientRegistrationCallback (JackClient, _clientRegistration, IntPtr.Zero);
			PortCallbackApi.SetPortRegistrationCallback (JackClient, _portRegistration, IntPtr.Zero);
			//PortCallbackApi.SetPortRenameCallback (JackClient, _portRename, IntPtr.Zero);
			PortCallbackApi.SetPortConnectCallback (JackClient, _portConnect, IntPtr.Zero);
		}

		/// <summary>
		/// Activates the client and connects to Jack.
		/// </summary>
		public new bool Start ()
		{
			return base.Start ();
		}

		/// <summary>
		/// Stops the client and disconnects from Jack.
		/// </summary>
		public new bool Stop ()
		{
			return base.Stop ();
		}

		/// <summary>
		/// Connect the specified outPort and inPort.
		/// </summary>
		/// <param name="outPort">Out port.</param>
		/// <param name="inPort">In port.</param>
		public bool Connect (PortReference outPort, PortReference inPort)
		{
			unsafe {
				return PortApi.Connect (JackClient, outPort.FullName, inPort.FullName) == 0;
			}
		}

		/// <summary>
		/// Disconnect the specified outPort and inPort.
		/// </summary>
		/// <param name="outPort">Out port.</param>
		/// <param name="inPort">In port.</param>
		public bool Disconnect (PortReference outPort, PortReference inPort)
		{
			unsafe {
				return PortApi.Disconnect (JackClient, outPort.FullName, inPort.FullName) == 0;
			}
		}
	}
}