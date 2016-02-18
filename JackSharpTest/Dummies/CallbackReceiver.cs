// Author:
//	   Thomas Mayer <thomas@residuum.org>
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
using JackSharp.Processing;

namespace JackSharpTest.Dummies
{
	public class CallbackReceiver
	{
		public int Called { get; private set; }

		public Action<Chunk> CopyInToOutAction;
		public Action<Chunk> PlayMidiNoteAction;
		public Action<Chunk> ChannelCounterAction;
		public Action<Chunk> CallBackOneAction;
		public Action<Chunk> CallBackTwoAction;

		public CallbackReceiver ()
		{
			CopyInToOutAction = CopyInToOut;
			PlayMidiNoteAction = PlayMidiNote;
			ChannelCounterAction = ChannelCounter;
			CallBackOneAction = CallBackOne;
			CallBackTwoAction = CallBackTwo;
		}

		void CopyInToOut (Chunk processItems)
		{
			for (var i = 0; i < Math.Min (processItems.AudioIn.Length, processItems.AudioOut.Length); i++) {
				Array.Copy (processItems.AudioIn [i].Audio, processItems.AudioOut [i].Audio, processItems.AudioIn [i].BufferSize);
			}
			Called++;
		}

		void PlayMidiNote (Chunk processItems)
		{
			foreach (MidiEventCollection eventCollection in processItems.MidiIn) {
				Called++;
			}
		}

		void ChannelCounter (Chunk processItems)
		{
			Called = processItems.AudioIn.Length;
		}

		void CallBackOne (Chunk processItems)
		{
			Called |= 1;
		}

		void CallBackTwo (Chunk processItems)
		{
			Called |= 2;
		}
	}
}