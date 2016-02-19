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
	static class ClientCallbackApi
	{
		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_set_process_thread")]
		public static extern unsafe int SetProcessThread (UnsafeStructs.jack_client_t* client, Callbacks.JackThreadCallback fun, IntPtr arg);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_set_thread_init_callback")]
		public static extern unsafe int SetThreadInitCallback (UnsafeStructs.jack_client_t* client, Callbacks.JackThreadInitCallback threadInitCallback, IntPtr arg);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_on_shutdown")]
		public static extern unsafe void SetShutdownCallback (UnsafeStructs.jack_client_t* client, Callbacks.JackShutdownCallback function, IntPtr arg);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_on_info_shutdown")]
		public static extern unsafe void SetInfoShutdownCallback (UnsafeStructs.jack_client_t* client, Callbacks.JackInfoShutdownCallback function, IntPtr arg);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_set_process_callback")]
		public static extern unsafe int SetProcessCallback (UnsafeStructs.jack_client_t* client, Callbacks.JackProcessCallback processCallback, IntPtr arg);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_set_freewheel_callback")]
		public static extern unsafe int SetFreewheelCallback (UnsafeStructs.jack_client_t* client, Callbacks.JackFreewheelCallback freewheelCallback, IntPtr arg);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_set_buffer_size_callback")]
		public static extern unsafe int SetBufferSizeCallback (UnsafeStructs.jack_client_t* client, Callbacks.JackBufferSizeCallback bufsizeCallback, IntPtr arg);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_set_sample_rate_callback")]
		public static extern unsafe int SetSampleRateCallback (UnsafeStructs.jack_client_t* client, Callbacks.JackSampleRateCallback srateCallback, IntPtr arg);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_set_graph_order_callback")]
		public static extern unsafe int SetGraphOrderCallback (UnsafeStructs.jack_client_t* client, Callbacks.JackGraphOrderCallback graphCallback, IntPtr arg);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_set_xrun_callback")]
		public static extern unsafe int SetXrunCallback (UnsafeStructs.jack_client_t* client, Callbacks.JackXRunCallback xrunCallback, IntPtr arg);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_set_latency_callback")]
		public static extern unsafe int SetLatencyCallback (UnsafeStructs.jack_client_t* client, Callbacks.JackLatencyCallback latencyCallback, IntPtr arg);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_set_freewheel")]
		public static extern unsafe int SetFreewheel (UnsafeStructs.jack_client_t* client, int onoff);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_set_buffer_size")]
		public static extern unsafe int SetBufferSize (UnsafeStructs.jack_client_t* client, uint nframes);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_set_error_function")]
		public static extern void SetErrorFunction (StringFromIntPtr.FromIntPtrDelegate func);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_set_info_function")]
		public static extern void SetInfoFunction (StringFromIntPtr.FromIntPtrDelegate func);
	}
}
