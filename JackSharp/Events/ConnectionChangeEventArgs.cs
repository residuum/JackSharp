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
using JackSharp.Ports;

namespace JackSharp.Events
{
	/// <summary>
	/// Connection change event arguments.
	/// </summary>
	public class ConnectionChangeEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the outlet.
		/// </summary>
		/// <value>The outlet.</value>
		public PortReference Outlet { get; private set; }

		/// <summary>
		/// Gets the inlet.
		/// </summary>
		/// <value>The inlet.</value>
		public PortReference Inlet { get; private set; }

		/// <summary>
		/// Gets the type of the change.
		/// </summary>
		/// <value>The type of the change.</value>
		public ChangeType ChangeType { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="JackSharp.Events.ConnectionChangeEventArgs"/> class.
		/// </summary>
		/// <param name="outlet">Outlet.</param>
		/// <param name="inlet">Inlet.</param>
		/// <param name="changeType">Change type.</param>
		public ConnectionChangeEventArgs (PortReference outlet, PortReference inlet, ChangeType changeType)
		{
			Outlet = outlet;
			Inlet = inlet;
			ChangeType = changeType;
		}
	}
}