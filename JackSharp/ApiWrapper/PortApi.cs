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
		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_register")]
		public static extern unsafe UnsafeStructs.jack_port_t* Register (UnsafeStructs.jack_client_t* client, string portName, string portType, JackPortFlags portFlags, ulong bufferSize);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_unregister")]
		public static extern unsafe int Unregister (UnsafeStructs.jack_client_t* client, UnsafeStructs.jack_port_t* port);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_get_buffer")]
		public static extern unsafe float* GetBuffer (UnsafeStructs.jack_port_t* port, uint frames);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_name")]
		public static extern unsafe IntPtr GetName (UnsafeStructs.jack_port_t* port);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_uuid")]
		public static extern unsafe ulong GetUuid (UnsafeStructs.jack_port_t* port);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_short_name")]
		public static extern unsafe IntPtr GetShortName (UnsafeStructs.jack_port_t* port);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_flags")]
		public static extern unsafe int GetPortFlags (UnsafeStructs.jack_port_t* port);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_type")]
		public static extern unsafe IntPtr GetType (UnsafeStructs.jack_port_t* port);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_is_mine")]
		public static extern unsafe int IsMine (UnsafeStructs.jack_client_t* client, UnsafeStructs.jack_port_t* port);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_connected")]
		public static extern unsafe int IsConnected (UnsafeStructs.jack_port_t* port);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_connected_to")]
		public static extern unsafe int IsConnectedTo (UnsafeStructs.jack_port_t* port, string portName);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_get_connections")]
		public static extern unsafe IntPtr GetConnections (UnsafeStructs.jack_port_t* port);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_get_all_connections")]
		public static extern unsafe IntPtr GetAllConnections (UnsafeStructs.jack_client_t* client, UnsafeStructs.jack_port_t* port);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_set_name")]
		public static extern unsafe int SetName (UnsafeStructs.jack_port_t* port, string portName);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_set_alias")]
		public static extern unsafe int SetAlias (UnsafeStructs.jack_port_t* port, string alias);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_unset_alias")]
		public static extern unsafe int UnsetAlias (UnsafeStructs.jack_port_t* port, string alias);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_get_aliases")]
		public static extern unsafe int GetAliases (UnsafeStructs.jack_port_t* port, IntPtr[] aliases);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_request_monitor")]
		public static extern unsafe int RequestMonitor (UnsafeStructs.jack_port_t* port, int onoff);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_request_monitor_by_name")]
		public static extern unsafe int RequestMonitorByName (UnsafeStructs.jack_client_t* client, string portName, int onoff);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_ensure_monitor")]
		public static extern unsafe int EnsureMonitor (UnsafeStructs.jack_port_t* port, int onoff);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_monitoring_input")]
		public static extern unsafe int MonitoringInput (UnsafeStructs.jack_port_t* port);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_connect")]
		public static extern unsafe int Connect (UnsafeStructs.jack_client_t* client, string sourcePort, string destinationPort);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_disconnect")]
		public static extern unsafe int Disconnect (UnsafeStructs.jack_client_t* client, string sourcePort, string destinationPort);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_disconnect")]
		public static extern unsafe int Disconnect (UnsafeStructs.jack_client_t* client, UnsafeStructs.jack_port_t* port);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_name_size")]
		public static extern int GetNameSize ();

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_type_size")]
		public static extern int GetTypeSize ();

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_type_get_buffer_size")]
		public static extern unsafe uint GetBufferSize (UnsafeStructs.jack_client_t* client, string portType);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_get_latency_range")]
		public static extern unsafe void GetLatencyRange (UnsafeStructs.jack_port_t* port, JackLatencyCallbackMode mode, IntPtr range);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_set_latency_range")]
		public static extern unsafe void SetLatencyRange (UnsafeStructs.jack_port_t* port, JackLatencyCallbackMode mode, IntPtr range);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_recompute_total_latencies")]
		public static extern unsafe int RecomputeTotalLatencies (UnsafeStructs.jack_client_t* client);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_get_ports")]
		public static extern unsafe IntPtr GetPorts (UnsafeStructs.jack_client_t* client, string portNamePattern, string typeNamePattern, ulong flags);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_by_name")]
		public static extern unsafe UnsafeStructs.jack_port_t* GetPortByName (UnsafeStructs.jack_client_t* client, string portName);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_port_by_id")]
		public static extern unsafe UnsafeStructs.jack_port_t* GetPortById (UnsafeStructs.jack_client_t* client, uint portId);
	}
}
