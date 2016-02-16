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
using System.Runtime.InteropServices;
using JackSharp.Pointers;

namespace JackSharp.ApiWrapper
{
	static class PortApi
	{
		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe UnsafeStructs.jack_port_t* jack_port_register (UnsafeStructs.jack_client_t* client, string portName, string portType, JackPortFlags portFlags, ulong bufferSize);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe int jack_port_unregister (UnsafeStructs.jack_client_t* client, UnsafeStructs.jack_port_t* port);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe float* jack_port_get_buffer (UnsafeStructs.jack_port_t* port, uint frames);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe IntPtr jack_port_name (UnsafeStructs.jack_port_t* port);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe ulong jack_port_uuid (UnsafeStructs.jack_port_t* port);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe IntPtr jack_port_short_name (UnsafeStructs.jack_port_t* port);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe int jack_port_flags (UnsafeStructs.jack_port_t* port);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe IntPtr jack_port_type (UnsafeStructs.jack_port_t* port);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe int jack_port_is_mine (UnsafeStructs.jack_client_t* client, UnsafeStructs.jack_port_t* port);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe int jack_port_connected (UnsafeStructs.jack_port_t* port);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe int jack_port_connected_to (UnsafeStructs.jack_port_t* port, string portName);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe IntPtr jack_port_get_connections (UnsafeStructs.jack_port_t* port);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe IntPtr jack_port_get_all_connections (UnsafeStructs.jack_client_t* client, UnsafeStructs.jack_port_t* port);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe int jack_port_set_name (UnsafeStructs.jack_port_t* port, string portName);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe int jack_port_set_alias (UnsafeStructs.jack_port_t* port, string alias);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe int jack_port_unset_alias (UnsafeStructs.jack_port_t* port, string alias);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe int jack_port_get_aliases (UnsafeStructs.jack_port_t* port, IntPtr[] aliases);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe int jack_port_request_monitor (UnsafeStructs.jack_port_t* port, int onoff);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe int jack_port_request_monitor_by_name (UnsafeStructs.jack_client_t* client, string portName, int onoff);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe int jack_port_ensure_monitor (UnsafeStructs.jack_port_t* port, int onoff);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe int jack_port_monitoring_input (UnsafeStructs.jack_port_t* port);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe int jack_connect (UnsafeStructs.jack_client_t* client, string sourcePort, string destinationPort);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe int jack_disconnect (UnsafeStructs.jack_client_t* client, string sourcePort, string destinationPort);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe int jack_port_disconnect (UnsafeStructs.jack_client_t* client, UnsafeStructs.jack_port_t* port);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern int jack_port_name_size ();

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern int IntPtrype_size ();

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe uint IntPtrype_get_buffer_size (UnsafeStructs.jack_client_t* client, string portType);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void jack_port_get_latency_range (UnsafeStructs.jack_port_t* port, JackLatencyCallbackMode mode, IntPtr range);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void jack_port_set_latency_range (UnsafeStructs.jack_port_t* port, JackLatencyCallbackMode mode, IntPtr range);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe int jack_recompute_total_latencies (UnsafeStructs.jack_client_t* client);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe IntPtr jack_get_ports (UnsafeStructs.jack_client_t* client, string portNamePattern, string typeNamePattern, ulong flags);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe UnsafeStructs.jack_port_t* jack_port_by_name (UnsafeStructs.jack_client_t* client, string portName);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe UnsafeStructs.jack_port_t* jack_port_by_id (UnsafeStructs.jack_client_t* client, uint portId);
	}
}
