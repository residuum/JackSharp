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
using NAudio.Wave;
using JackSharp;
using System.Linq;
using NAudio.Utils;
using JackSharp.Ports;

namespace Naudio.Jack
{
	public class JackOut : IWavePlayer, IWavePosition
	{
		Client _client;

		IWaveProvider _waveStream;

		PlaybackState _playbackState;

		CircularBuffer _buffer;

		public JackOut (Client client)
		{
			_client = client;
			_playbackState = PlaybackState.Stopped;
		}

		#region IWavePosition implementation

		public long GetPosition ()
		{
			throw new NotImplementedException ();
		}

		public WaveFormat OutputWaveFormat {
			get {
				return WaveFormat.CreateIeeeFloatWaveFormat ((int)_client.SampleRate, _client.AudioOutPorts.Count ());
			}
		}

		#endregion

		#region IWavePlayer implementation

		public event EventHandler<StoppedEventArgs> PlaybackStopped;

		public void Play ()
		{
			if (_client.Start ()) {
				_playbackState = PlaybackState.Playing;
			}
		}

		public void Stop ()
		{
			if (_client.Stop ()) {
				_playbackState = PlaybackState.Stopped;
				if (PlaybackStopped != null) {
					PlaybackStopped (this, new StoppedEventArgs ());
				}
			}
		}

		public void Pause ()
		{
			if (_client.Stop ()){
				_playbackState = PlaybackState.Paused;
			}
		}

		void ProcessAudio (ProcessingChunk processingChunk)
		{
			int bufferCount = processingChunk.AudioOut.Length;
			if (bufferCount == 0){
				return;
			}
			uint bufferSize = processingChunk.AudioOut [0].BufferSize;
			int completeSampleSize = bufferCount * bufferSize;
			float[] interlacedSamples;
			_buffer.Read (interlacedSamples, 0, completeSampleSize);
			BufferOperations.DeinterlaceAudio (interlacedSamples, processingChunk.AudioOut, bufferSize, bufferCount);
			
		}

		public void Init (IWaveProvider waveProvider)
		{
			this._waveStream = waveProvider;
			int bufferSize = (int)_client.BufferSize;

			_playbackState = PlaybackState.Stopped;
			_buffer = new CircularBuffer (_client.AudioInPorts.Length * _client.BufferSize * 2);
			_client.ProcessFunc += ProcessAudio;
		}

		public PlaybackState PlaybackState {
			get {
				return _playbackState;
			}
		}

		public float Volume {
			get {
				throw new NotImplementedException ();
			}
			set {
				throw new NotImplementedException ();
			}
		}

		#endregion

		#region IDisposable implementation

		public void Dispose ()
		{
			Stop ();
		}

		#endregion
	}
}

