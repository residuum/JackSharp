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

namespace JackSharp.Events
{
	/// <summary>
	/// Buffer size event arguments.
	/// </summary>
	public class BufferSizeEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the size of the buffer.
		/// </summary>
		/// <value>The size of the buffer.</value>
		public int BufferSize { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="JackSharp.Events.BufferSizeEventArgs"/> class.
		/// </summary>
		/// <param name="bufferSize">Buffer size.</param>
		public BufferSizeEventArgs (int bufferSize)
		{
			BufferSize = bufferSize;
		}
	}
}
