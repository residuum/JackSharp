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

namespace JackSharp.Internal
{
    static class Invoke
    {
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe UnsafeStructs.jack_client_t *jack_client_open(string clientName, JackOptions options, params IntPtr[] status);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_client_close(UnsafeStructs.jack_client_t *client);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern int jack_client_name_size();
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe IntPtr jack_get_client_name(UnsafeStructs.jack_client_t *client);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe IntPtr jack_get_uuid_for_client_name(UnsafeStructs.jack_client_t *client, string name);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe IntPtr jack_get_client_name_by_uuid(UnsafeStructs.jack_client_t *client, string uuid);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_activate(UnsafeStructs.jack_client_t *client);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_deactivate(UnsafeStructs.jack_client_t *client);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe IntPtr jack_client_thread_id(UnsafeStructs.jack_client_t *client);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_is_realtime(UnsafeStructs.jack_client_t *client);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe uint jack_cycle_wait(UnsafeStructs.jack_client_t *client);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void jack_cycle_signal(UnsafeStructs.jack_client_t *client, int status);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_set_process_thread(UnsafeStructs.jack_client_t *client, Callbacks.JackThreadCallback fun, IntPtr arg);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_set_thread_init_callback(UnsafeStructs.jack_client_t *client, Callbacks.JackThreadInitCallback threadInitCallback, IntPtr arg);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void jack_on_shutdown(UnsafeStructs.jack_client_t *client, Callbacks.JackShutdownCallback function, IntPtr arg);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void jack_on_info_shutdown(UnsafeStructs.jack_client_t *client, Callbacks.JackInfoShutdownCallback function, IntPtr arg);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_set_process_callback(UnsafeStructs.jack_client_t *client, Callbacks.JackProcessCallback processCallback, IntPtr arg);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_set_freewheel_callback(UnsafeStructs.jack_client_t *client, Callbacks.JackFreewheelCallback freewheelCallback, IntPtr arg);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_set_buffer_size_callback(UnsafeStructs.jack_client_t *client, Callbacks.JackBufferSizeCallback bufsizeCallback, IntPtr arg);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_set_sample_rate_callback(UnsafeStructs.jack_client_t *client, Callbacks.JackSampleRateCallback srateCallback, IntPtr arg);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_set_client_registration_callback(UnsafeStructs.jack_client_t *client, Callbacks.JackClientRegistrationCallback registrationCallback, IntPtr arg);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_set_port_registration_callback(UnsafeStructs.jack_client_t *client, Callbacks.JackPortRegistrationCallback registrationCallback, IntPtr arg);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_set_port_rename_callback(UnsafeStructs.jack_client_t *client, Callbacks.JackPortRenameCallback renameCallback, IntPtr arg);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_set_port_connect_callback(UnsafeStructs.jack_client_t *client, Callbacks.JackPortConnectCallback connectCallback, IntPtr arg);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_set_graph_order_callback(UnsafeStructs.jack_client_t *client, Callbacks.JackGraphOrderCallback graphCallback, IntPtr arg);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_set_xrun_callback(UnsafeStructs.jack_client_t *client, Callbacks.JackXRunCallback xrunCallback, IntPtr arg);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_set_latency_callback(UnsafeStructs.jack_client_t *client, Callbacks.JackLatencyCallback latencyCallback, IntPtr arg);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_set_freewheel(UnsafeStructs.jack_client_t *client, int onoff);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_set_buffer_size(UnsafeStructs.jack_client_t *client, uint nframes);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe uint jack_get_sample_rate(UnsafeStructs.jack_client_t *client);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe uint jack_get_buffer_size(UnsafeStructs.jack_client_t *client);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe float jack_cpu_load(UnsafeStructs.jack_client_t *client);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe UnsafeStructs.jack_port_t *jack_port_register(UnsafeStructs.jack_client_t *client, string portName, string portType, JackPortFlags portFlags, ulong bufferSize);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_port_unregister(UnsafeStructs.jack_client_t *client, UnsafeStructs.jack_port_t *port);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe float *jack_port_get_buffer(UnsafeStructs.jack_port_t *port, uint frames);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe IntPtr jack_port_name(UnsafeStructs.jack_port_t *port);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe ulong jack_port_uuid(UnsafeStructs.jack_port_t *port);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe IntPtr jack_port_short_name(UnsafeStructs.jack_port_t *port);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_port_flags(UnsafeStructs.jack_port_t *port);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe IntPtr jack_port_type(UnsafeStructs.jack_port_t *port);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_port_is_mine(UnsafeStructs.jack_client_t *client, UnsafeStructs.jack_port_t *port);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_port_connected(UnsafeStructs.jack_port_t *port);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_port_connected_to(UnsafeStructs.jack_port_t *port, string portName);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe IntPtr jack_port_get_connections(UnsafeStructs.jack_port_t *port);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe IntPtr jack_port_get_all_connections(UnsafeStructs.jack_client_t *client, UnsafeStructs.jack_port_t *port);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_port_set_name(UnsafeStructs.jack_port_t *port, string portName);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_port_set_alias(UnsafeStructs.jack_port_t *port, string alias);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_port_unset_alias(UnsafeStructs.jack_port_t *port, string alias);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_port_get_aliases(UnsafeStructs.jack_port_t *port, IntPtr[] aliases);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_port_request_monitor(UnsafeStructs.jack_port_t *port, int onoff);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_port_request_monitor_by_name(UnsafeStructs.jack_client_t *client, string portName, int onoff);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_port_ensure_monitor(UnsafeStructs.jack_port_t *port, int onoff);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_port_monitoring_input(UnsafeStructs.jack_port_t *port);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_connect(UnsafeStructs.jack_client_t *client, string sourcePort, string destinationPort);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_disconnect(UnsafeStructs.jack_client_t *client, string sourcePort, string destinationPort);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_port_disconnect(UnsafeStructs.jack_client_t *client, UnsafeStructs.jack_port_t *port);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern int jack_port_name_size();
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern int IntPtrype_size();
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe uint IntPtrype_get_buffer_size(UnsafeStructs.jack_client_t *client, string portType);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void jack_port_get_latency_range(UnsafeStructs.jack_port_t *port, JackLatencyCallbackMode mode, IntPtr range);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe void jack_port_set_latency_range(UnsafeStructs.jack_port_t *port, JackLatencyCallbackMode mode, IntPtr range);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_recompute_total_latencies(UnsafeStructs.jack_client_t *client);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe IntPtr jack_get_ports(UnsafeStructs.jack_client_t *client, string portNamePattern, string typeNamePattern, ulong flags);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe UnsafeStructs.jack_port_t *jack_port_by_name(UnsafeStructs.jack_client_t *client, string portName);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe UnsafeStructs.jack_port_t *jack_port_by_id(UnsafeStructs.jack_client_t *client, uint portId);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe uint jack_frames_since_cycle_start(UnsafeStructs.jack_client_t *client);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe uint jack_frame_time(UnsafeStructs.jack_client_t *client);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe uint jack_last_frame_time(UnsafeStructs.jack_client_t *client);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe int jack_get_cycle_times(UnsafeStructs.jack_client_t *client, uint currentFrames, ulong currentUsecs, ulong nextUsecs, float periodUsecs);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe ulong jack_frames_to_time(UnsafeStructs.jack_client_t *client, uint frames);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern unsafe uint jack_time_to_frames(UnsafeStructs.jack_client_t *client, ulong frames);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern ulong jack_get_time();
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void jack_set_error_function(StringFromIntPtr.FromIntPtrDelegate func);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void jack_set_info_function(StringFromIntPtr.FromIntPtrDelegate func);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void jack_free(IntPtr ptr);
    }
}
