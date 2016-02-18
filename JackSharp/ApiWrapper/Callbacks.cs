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

namespace JackSharp.ApiWrapper
{
	static class Callbacks
	{
		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		public delegate void JackPortRegistrationCallback (uint port,int register,IntPtr args);

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		public delegate void JackPortConnectCallback (uint a,uint b,int connect,IntPtr args);

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		public delegate void JackShutdownCallback (IntPtr args);

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		public delegate int JackXRunCallback (IntPtr args);

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		public delegate void JackFreewheelCallback (int starting,IntPtr arg);

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		public delegate int JackBufferSizeCallback (uint nframes,IntPtr arg);

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		public delegate int JackSampleRateCallback (uint nframes,IntPtr arg);

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		public delegate void JackPortRenameCallback (uint port,string old_name,string new_name,IntPtr arg);

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		public delegate int JackProcessCallback (uint nframes,IntPtr arg);

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		public delegate void JackInfoShutdownCallback (JackStatus code,string reason,IntPtr arg);

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		public delegate void JackClientRegistrationCallback (string name,int register,IntPtr arg);

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		public delegate int JackGraphOrderCallback (IntPtr arg);

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		public delegate void JackLatencyCallback (JackLatencyCallbackMode mode,IntPtr arg);

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		public delegate void JackThreadInitCallback ();

		[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
		public delegate void JackThreadCallback (IntPtr arg);
	}
}