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

namespace JackSharp.Pointers
{
    class UnsafeStructs
    {
        internal struct jack_client_t
        {
            public ulong unique_1;
            public ulong usecs;
            public uint frame_rate;
            public uint frame;
            public JackPosition valid;

        }

        internal struct jack_port_t
        {
        }
        internal struct jack_midi_event_t
        {
            public uint time;
            public uint size;
            public unsafe byte* buffer;
        }

        [Flags]
        internal enum JackPosition
        {
            JackPositionBBT = 1,
            JackPositionTimecode = 2,
            JackBBTFrameOffset = 4,
            JackAudioVideoRatio = 8,
            JackVideoFrameOffset = 16
        }
    }
}
