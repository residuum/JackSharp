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
using JackSharp.Pointers;
using JackSharp.Ports;

namespace JackSharp
{
    static class BufferOperations
    {
        public static void DeinterleaveAudio(this float[] interleavedAudio, uint buffersize, ref FloatPointer[] buffers)
        {
            int bufferCount = buffers.Length;
            for (uint i = 0; i < buffersize; i++)
            {
                for (int j = 0; j < bufferCount; j++)
                {
                    buffers[j].Array[i] = interleavedAudio[i * bufferCount + j];
                }
            }
            for (int i = 0; i < bufferCount; i++)
            {
                buffers[i].Finish();
            }
        }

        public static FloatPointer[] GetAudioBuffers(this Port[] ports, uint nframes)
        {
            FloatPointer[] inBuffers = new FloatPointer[ports.Length];
            for (int i = 0; i < ports.Length; i++)
            {
                Port audioInPort = ports[i];
                inBuffers[i] = audioInPort.GetBuffer(nframes);
            }
            return inBuffers;
        }

        public static float[] InterleaveAudio(this FloatPointer[] buffers, uint buffersize)
        {
            int bufferCount = buffers.Length;
            float[] result = new float[bufferCount * buffersize];
            for (uint i = 0; i < buffersize; i++)
            {
                for (int j = 0; j < bufferCount; j++)
                {
                    result[i * bufferCount + j] = buffers[j].Array[i];
                }
            }
            return result;
        }
    }
}
