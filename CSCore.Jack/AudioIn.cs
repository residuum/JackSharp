// Author:
//	   Thomas Mayer <thomas@residuum.org>
//
// Copyright (c) 2017 Thomas Mayer
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
using System.Linq;
using CSCore.SoundIn;
using JackSharp;
using JackSharp.Ports;
using JackSharp.Processing;

namespace CSCore.Jack
{
	public class AudioIn : ISoundIn
	{
		readonly Processor _client;
		RecordingState _recordingState;

		public AudioIn (Processor client)
		{
			_client = client;
			_recordingState = RecordingState.Stopped;
		}

		void ProcessAudio (ProcessBuffer processingChunk)
		{
			int bufferCount = processingChunk.AudioIn.Length;
			if (bufferCount == 0) {
				return;
			}
			int bufferSize = processingChunk.AudioIn [0].BufferSize;
			int floatsCount = bufferCount * bufferSize;
			int bytesCount = floatsCount * sizeof(float);
			float[] interlacedSamples = BufferOperations.InterlaceAudio (processingChunk.AudioIn, bufferSize, bufferCount);
			byte[] waveInData = new byte[bytesCount];
			Buffer.BlockCopy (interlacedSamples, 0, waveInData, 0, bytesCount);
			if (DataAvailable != null) {
				DataAvailable (this, new DataAvailableEventArgs (waveInData, 0, waveInData.Length, WaveFormat));
			}
		}

		~AudioIn ()
		{
			Dispose (false);
		}

		public void Dispose ()
		{
			Dispose (true);
			GC.SuppressFinalize (this);
		}

		void Dispose (bool isDisposing)
		{
			_client.ProcessFunc -= ProcessAudio;
			Stop ();
		}


		public void Initialize ()
		{
			_client.ProcessFunc += ProcessAudio;
			_client.Start ();
		}

		public void Start ()
		{
			if (_recordingState == RecordingState.Recording) {
				return;
			}
			if (_client.Start ()) {
				_recordingState = RecordingState.Recording;
			}
		}

		public void Stop ()
		{
			if (_recordingState == RecordingState.Stopped) {
				return;
			}
			if (_client.Stop ()) {
				_recordingState = RecordingState.Stopped;
				if (Stopped != null) {
					Stopped (this, new RecordingStoppedEventArgs ());
				}
			}
		}

		public WaveFormat WaveFormat {
			get {
				return new WaveFormat (_client.SampleRate, 32, _client.AudioInPorts.Count ());
			}
		}

		public RecordingState RecordingState {
			get { return _recordingState; }
		}

		public event EventHandler<DataAvailableEventArgs> DataAvailable;
		public event EventHandler<RecordingStoppedEventArgs> Stopped;
	}
}
