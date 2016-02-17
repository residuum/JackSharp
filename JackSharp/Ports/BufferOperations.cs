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

namespace JackSharp.Ports
{
	public static class BufferOperations
	{
		internal static AudioBuffer GetAudioBuffer (this Port port, uint nframes)
		{
			StructPointer<float> buffer = port.GetBuffer (nframes);
			return new AudioBuffer (port, nframes, buffer);
		}

		internal static MidiEventCollection GetMidiBuffer (this MidiInPort port, uint nframes)
		{
			MidiEventCollection eventCollection = new MidiEventCollection (port);
			foreach (JackMidiEvent midiEvent in port.GetMidiEvents(nframes)) {
				eventCollection.AddEvent (midiEvent);
			}
			return eventCollection;
		}

		public static float[] InterlaceAudio (AudioBuffer[] audioBuffers, int bufferSize, int bufferCount)
		{
			float[] interlaced = new float[bufferSize * bufferCount];

			for (int i = 0; i < bufferSize; i++) {
				for (int j = 0; j < bufferSize; j++) {
					interlaced [i * bufferCount + j] = audioBuffers [j].Audio [i];
				}
			}
			return interlaced;
		}

		public static void DeinterlaceAudio (float[] interlaced, AudioBuffer[] audioBuffers, int bufferSize, int bufferCount)
		{
			for (int i = 0; i < bufferSize; i++) {
				for (int j = 0; j < bufferSize; j++) {
					audioBuffers [j].Audio [i] = interlaced [i * bufferCount + j];
				}
			}
		}
	}
}
