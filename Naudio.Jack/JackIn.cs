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
using System.Linq;
using JackSharp;
using JackSharp.Ports;
using JackSharp.Processing;
using NAudio.Wave;

namespace Naudio.Jack
{
	public class JackIn : IWaveIn
	{
		readonly Client _client;
		bool _isRecording;

		public JackIn (Client client)
		{
			_client = client;
			_client.ProcessFunc += ProcessAudio;
		}

		~JackIn ()
		{
			Dispose (true);
		}

		public void Dispose ()
		{
			Dispose (false);
			GC.SuppressFinalize (this);
		}

		void Dispose (bool isDisposing)
		{
			StopRecording ();
		}

		void ProcessAudio (Chunk processingChunk)
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
				DataAvailable (this, new WaveInEventArgs (waveInData, bytesCount));
			}

		}

		public void StartRecording ()
		{
			if (_isRecording) {
				return;
			}
			if (_client.Start ()) {
				_isRecording = true;
			}
		}

		public void StopRecording ()
		{
			if (!_isRecording) {
				return;
			}
			if (_client.Stop ()) {
				_isRecording = false;
				if (RecordingStopped != null) {
					RecordingStopped (this, new StoppedEventArgs ());
				}
			}
		}

		public WaveFormat WaveFormat {
			get {
				return WaveFormat.CreateIeeeFloatWaveFormat (_client.SampleRate, _client.AudioInPorts.Count ());
			}
			set {
				throw new NotImplementedException ();
			}
		}

		public event EventHandler<WaveInEventArgs> DataAvailable;
		public event EventHandler<StoppedEventArgs> RecordingStopped;
	}
}
