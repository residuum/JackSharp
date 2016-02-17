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
using System.Collections.Generic;

namespace JackSharpTest.Dummies
{
	public class CallbackReceiver
	{
		public int Called { get; set; }

		public Action<ProcessingChunk> CopyInToOutAction;
		public Action<ProcessingChunk> PlayMidiNoteAction;
		public Action<ProcessingChunk> ChannelCounterAction;
		public Action<ProcessingChunk> CallBackOneAction;
		public Action<ProcessingChunk> CallBackTwoAction;

		public CallbackReceiver ()
		{
			CopyInToOutAction = CopyInToOut;
			PlayMidiNoteAction = PlayMidiNote;
			ChannelCounterAction = ChannelCounter;
			CallBackOneAction = CallBackOne;
			CallBackTwoAction = CallBackTwo;
		}

		void CopyInToOut (ProcessingChunk processItems)
		{
			for (var i = 0; i < Math.Min (processItems.AudioIn.Length, processItems.AudioOut.Length); i++) {
				Array.Copy (processItems.AudioIn [i].Audio, processItems.AudioOut [i].Audio, processItems.AudioIn [i].BufferSize);
			}
			Called++;
		}

		void PlayMidiNote (ProcessingChunk processItems)
		{
			foreach (MidiEventCollection eventCollection in processItems.MidiIn) {
				foreach (JackMidiEvent midiEvent in eventCollection) {
					Called++;
				}
			}
		}

		void ChannelCounter (ProcessingChunk processItems)
		{
			Called = processItems.AudioIn.Length;
		}

		void CallBackOne (ProcessingChunk processItems)
		{
			Called |= 1;
		}

		void CallBackTwo (ProcessingChunk processItems)
		{
			Called |= 2;
		}
	}
}