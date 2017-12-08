using System.Runtime.InteropServices;
using JackSharp.Pointers;

namespace JackSharp.ApiWrapper
{
    static class TransportApi
    {
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_release_timebase")]
        public static extern unsafe int ReleaseTimebase(UnsafeStructs.jack_client_t* client);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_set_sync_callback")]
        public static extern unsafe int SetSyncCallback(UnsafeStructs.jack_client_t* client, Callbacks.JackSyncCallback sync_callback, void* arg);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_set_sync_timeout")]
        public static extern unsafe int SetSyncTimeout(UnsafeStructs.jack_client_t* client, ulong timeout);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_set_timebase_callback")]
        public static extern unsafe int SetTimebaseCallback(UnsafeStructs.jack_client_t* client, int conditional,
            Callbacks.JackTimebaseCallback timebase_callback, void* arg);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_transport_locate")]
        public static extern unsafe int TransportLocate(UnsafeStructs.jack_client_t* client, uint frame);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_transport_query")]
        public static extern unsafe JackTransportState TransportQuery(UnsafeStructs.jack_client_t* client, UnsafeStructs.jack_position_t* pos);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_get_current_transport_frame")]
        public static extern unsafe uint GetCurrentTransportFrame(UnsafeStructs.jack_client_t* client);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_transport_reposition")]
        public static extern unsafe int TransportReposition(UnsafeStructs.jack_client_t* client, UnsafeStructs.jack_position_t* pos);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_transport_start")]
        public static extern unsafe void TransportStart(UnsafeStructs.jack_client_t* client);
        [DllImport(Constants.JACK_LIB_NAME, CallingConvention = CallingConvention.Cdecl, EntryPoint = "jack_transport_stop")]
        public static extern unsafe void TransportStop(UnsafeStructs.jack_client_t* client);
    }
}
