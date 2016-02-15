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

namespace JackSharp.ApiWrapper
{
	[Flags]
	enum JackPortFlags
	{
		/**
     * if JackPortIsInput is set, then the port can receive
     * data.
     */
		JackPortIsInput = 0x1,

		/**
         * if JackPortIsOutput is set, then data can be read from
         * the port.
         */
		JackPortIsOutput = 0x2,

		/**
         * if JackPortIsPhysical is set, then the port corresponds
         * to some kind of physical I/O connector.
         */
		JackPortIsPhysical = 0x4,

		/**
         * if JackPortCanMonitor is set, then a call to
         * jack_port_request_monitor() makes sense.
         *
         * Precisely what this means is dependent on the client. A typical
         * result of it being called with TRUE as the second argument is
         * that data that would be available from an output port (with
         * JackPortIsPhysical set) is sent to a physical output connector
         * as well, so that it can be heard/seen/whatever.
         *
         * Clients that do not control physical interfaces
         * should never create ports with this bit set.
         */
		JackPortCanMonitor = 0x8,

		/**
         * JackPortIsTerminal means:
         *
         *  for an input port: the data received by the port
         *                    will not be passed on or made
         *                     available at any other port
         *
         * for an output port: the data available at the port
         *                    does not originate from any other port
         *
         * Audio synthesizers, I/O hardware interface clients, HDR
         * systems are examples of clients that would set this flag for
         * their ports.
         */
		JackPortIsTerminal = 0x10,
	}
}