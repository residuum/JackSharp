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
	static class ClientApi
	{
		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_client_open")]
		public static extern unsafe UnsafeStructs.jack_client_t* Open (string clientName, JackOptions options, params IntPtr[] status);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_client_close")]
		public static extern unsafe int Close (UnsafeStructs.jack_client_t* client);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_client_name_size")]
		public static extern int GetNameSize ();

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_get_client_name")]
		public static extern unsafe IntPtr GetName (UnsafeStructs.jack_client_t* client);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_get_uuid_for_client_name")]
		public static extern unsafe IntPtr GetUuidForName (UnsafeStructs.jack_client_t* client, string name);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_get_client_name_by_uuid")]
		public static extern unsafe IntPtr GetNameByUuid (UnsafeStructs.jack_client_t* client, string uuid);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_activate")]
		public static extern unsafe int Activate (UnsafeStructs.jack_client_t* client);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_deactivate")]
		public static extern unsafe int Deactivate (UnsafeStructs.jack_client_t* client);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_client_thread_id")]
		public static extern unsafe IntPtr GetThreadId (UnsafeStructs.jack_client_t* client);
	}
}
