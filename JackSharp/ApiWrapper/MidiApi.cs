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
using System.Runtime.InteropServices;
using JackSharp.Pointers;
using System;

namespace JackSharp.ApiWrapper
{
	static class MidiApi
	{
		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_midi_get_event_count")]
		public static extern uint GetEventCount (IntPtr port_buffer);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_midi_event_get")]
		public static extern unsafe int GetEvent (UnsafeStructs.jack_midi_event_t* ev, IntPtr port_buffer, uint event_index);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_midi_clear_buffer")]
		public static extern unsafe void ClearBuffer (float* port_buffer);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_midi_max_event_size")]
		public static extern uint GetMaxEventSize (IntPtr port_buffer);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_midi_event_reserve")]
		public static extern unsafe byte* ReserveEvent (float* port_buffer, uint time, uint data_size);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_midi_event_write")]
		public static extern unsafe int WriteEvent (IntPtr port_buffer, uint time, byte* data, uint data_size);

		[DllImport (Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_midi_get_lost_event_count")]
		public static extern uint GetLostEventCount (IntPtr port_buffer);
	}
}
