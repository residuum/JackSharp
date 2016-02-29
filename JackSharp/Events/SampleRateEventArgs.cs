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
	/// Sample rate event arguments.
	/// </summary>
	public class SampleRateEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the sample rate.
		/// </summary>
		/// <value>The sample rate.</value>
		public int SampleRate { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="JackSharp.Events.SampleRateEventArgs"/> class.
		/// </summary>
		/// <param name="sampleRate">Sample rate.</param>
		public SampleRateEventArgs (int sampleRate)
		{
			SampleRate = sampleRate;
		}
	}
}
