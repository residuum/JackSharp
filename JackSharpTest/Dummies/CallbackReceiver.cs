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
using JackSharp;

namespace JackSharpTest.Dummies
{
	public class CallbackReceiver
	{
		public int Called { get; set; }

		public void CopyInToOut (ProcessingChunk processItems)
		{
			for (var i = 0; i < Math.Min (processItems.AudioIn.Length, processItems.AudioOut.Length); i++) {
				Array.Copy (processItems.AudioIn [i].Audio, processItems.AudioOut [i].Audio, processItems.AudioIn [i].BufferSize);
		        
			}
			Called++;
		}

		public void PlayMidiNote (ProcessingChunk processItems)
		{
			Called++;
			//throw new NotImplementedException();
		}

		public void ChannelCounter (ProcessingChunk processItems)
		{
			Called = processItems.AudioIn.Length;
		}

		public void CallBackOne (ProcessingChunk processItems)
		{
			if (Called == 0){
				Called = 1;
			}
			Called *= 2;
		}

		public void CallBackTwo (ProcessingChunk processItems)
		{
			if (Called == 0){
				Called = 1;
			}
			Called *= 3;
		}

	}
}

