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
	static class Invoke
	{
		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe int jack_is_realtime (UnsafeStructs.jack_client_t* client);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe uint jack_cycle_wait (UnsafeStructs.jack_client_t* client);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe void jack_cycle_signal (UnsafeStructs.jack_client_t* client, int status);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe uint jack_get_sample_rate (UnsafeStructs.jack_client_t* client);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe uint jack_get_buffer_size (UnsafeStructs.jack_client_t* client);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe float jack_cpu_load (UnsafeStructs.jack_client_t* client);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe uint jack_frames_since_cycle_start (UnsafeStructs.jack_client_t* client);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe uint jack_frame_time (UnsafeStructs.jack_client_t* client);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe uint jack_last_frame_time (UnsafeStructs.jack_client_t* client);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe int jack_get_cycle_times (UnsafeStructs.jack_client_t* client, uint currentFrames, ulong currentUsecs, ulong nextUsecs, float periodUsecs);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe ulong jack_frames_to_time (UnsafeStructs.jack_client_t* client, uint frames);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern unsafe uint jack_time_to_frames (UnsafeStructs.jack_client_t* client, ulong frames);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern ulong jack_get_time ();

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void jack_free (IntPtr ptr);
	}
}
