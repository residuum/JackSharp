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
	public class AudioBuffer : IProcessingItem
	{

		public Port Port { get; private set; }

		public uint BufferSize { get; private set; }

		internal FloatPointer PointerWrapper { get; private set; }

		public float[] Audio { get; set; }

		internal AudioBuffer (Port port, uint bufferSize, FloatPointer pointer)
		{
			BufferSize = bufferSize;
			Port = port;
			PointerWrapper = pointer;
			Audio = PointerWrapper.Array;
		}

		internal void CopyToPointer ()
		{
			PointerWrapper.Array = Audio;
			PointerWrapper.CopyToPointer ();
		}
	}
}

