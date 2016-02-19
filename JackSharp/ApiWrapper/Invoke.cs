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
		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_is_realtime")]
		public static extern unsafe int IsRealtime (UnsafeStructs.jack_client_t* client);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_cycle_wait")]
		public static extern unsafe uint CycleWait (UnsafeStructs.jack_client_t* client);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_cycle_signal")]
		public static extern unsafe void CycleSignal (UnsafeStructs.jack_client_t* client, int status);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_get_sample_rate")]
		public static extern unsafe uint GetSampleRate (UnsafeStructs.jack_client_t* client);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_get_buffer_size")]
		public static extern unsafe uint GetBufferSize (UnsafeStructs.jack_client_t* client);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_cpu_load")]
		public static extern unsafe float GetCpuLoad (UnsafeStructs.jack_client_t* client);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_frames_since_cycle_start")]
		public static extern unsafe uint GetFramesSinceCycleStart (UnsafeStructs.jack_client_t* client);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_frame_time")]
		public static extern unsafe uint GetFrameTime (UnsafeStructs.jack_client_t* client);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_last_frame_time")]
		public static extern unsafe uint GetLastFrameTime (UnsafeStructs.jack_client_t* client);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_get_cycle_times")]
		public static extern unsafe int GetCycleTimes (UnsafeStructs.jack_client_t* client, uint currentFrames, ulong currentUsecs, ulong nextUsecs, float periodUsecs);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_frames_to_time")]
		public static extern unsafe ulong ConvertFramesToTime (UnsafeStructs.jack_client_t* client, uint frames);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_time_to_frames")]
		public static extern unsafe uint ConvertTimeToFrames (UnsafeStructs.jack_client_t* client, ulong frames);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_get_xrun_delayed_usecs")]
		public static extern unsafe float GetXrunDelayedUsecs (UnsafeStructs.jack_client_t* client);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_get_time")]
		public static extern ulong GetTime ();

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_free")]
		public static extern void Free (IntPtr ptr);
	}
}
