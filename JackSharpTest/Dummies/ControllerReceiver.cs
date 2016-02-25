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
using System.Collections.Generic;
using System.Linq;
using JackSharp.Events;
using JackSharp.Ports;

namespace JackSharpTest.Dummies
{
	class ControllerReceiver
	{
		public int PortsFound { get; private set; }

		public int PhysicalPortsFound { get; private set; }

		public int ConnectionsFound { get; private set; }

		List<PortReference> _ports = new List<PortReference> ();


		public void PortChanged (object sender, PortRegistrationEventArgs e)
		{
			switch (e.ChangeType) {
			case ChangeType.New:
				_ports.Add (e.Port);
				PortsFound++;
				if (e.Port.IsPhysicalPort) {
					PhysicalPortsFound++;
				}
				break;
			case ChangeType.Deleted:
				_ports.Remove (e.Port);
				PortsFound--;
				if (e.Port.IsPhysicalPort) {
					PhysicalPortsFound--;
				}
				break;
			}
		}

		public PortReference FirstOutPort {
			get { return _ports.FirstOrDefault (p => p.Direction == Direction.Out && p.PortType == PortType.Audio); }
		}

		public PortReference FirstInPort {
			get { return _ports.FirstOrDefault (p => p.Direction == Direction.In && p.PortType == PortType.Audio); }
		}

		public void ConnectionChanged (object sender, ConnectionChangeEventArgs e)
		{
			switch (e.ChangeType) {
			case ChangeType.New:
				ConnectionsFound++;
				break;
			case ChangeType.Deleted:
				ConnectionsFound--;
				break;
			}
		}

		public void IsConnectionEqual (object sender, ConnectionChangeEventArgs e)
		{
			if (e.ChangeType == ChangeType.New) {
				ConnectionsFound = (e.Inlet == FirstInPort) ? 1 : 0;
			}
		}
	}
}
