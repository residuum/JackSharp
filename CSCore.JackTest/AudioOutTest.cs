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
using System.IO;
using System.Linq;
using System.Threading;
using CSCore.Codecs.WAV;
using CSCore.Jack;
using CSCore.JackTest.WaveIntegration;
using CSCore.SoundOut;
using JackSharp;
using NUnit.Framework;

namespace CSCore.JackTest
{
	[TestFixture]
	public class AudioOutTest
	{
		static Processor _client;
		static AudioOut _jackOut;

		static string GetPathToWav ()
		{
			string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
			return Path.Combine (currentDirectory, "example.wav");
		}

		[SetUp]
		public static void CreateOutput ()
		{
			_client = new Processor ("testCSCoreOut", 0, 2);
			_jackOut = new AudioOut (_client);
		}

		[Test]
		public virtual void PlayPauseStop ()
		{
			_jackOut.Play ();
			Assert.AreEqual (PlaybackState.Playing, _jackOut.PlaybackState);
			_jackOut.Pause ();
			Assert.AreEqual (PlaybackState.Paused, _jackOut.PlaybackState);
			_jackOut.Play ();
			Assert.AreEqual (PlaybackState.Playing, _jackOut.PlaybackState);
			_jackOut.Stop ();
			Assert.AreEqual (PlaybackState.Stopped, _jackOut.PlaybackState);
		}

		[Test]
		public virtual void PlayAudioFile ()
		{
			string wavFile = GetPathToWav ();
			Analyser analyser = new Analyser ();
			using (IWaveSource reader = new WaveFileReader (wavFile)) {
				_jackOut.Initialize (reader);
				_client.ProcessFunc += analyser.AnalyseOutAction;
				_jackOut.Play ();
				Thread.Sleep (100);
				_jackOut.Stop ();
			}
			Assert.AreNotEqual (0, analyser.NotEmptySamples);
		}

		[Test]
		public virtual void PlayAudioFilePaused ()
		{
			string wavFile = GetPathToWav ();
			using (IWaveSource reader = new WaveFileReader (wavFile)) {
				_jackOut.Initialize (reader);
				_jackOut.Play ();
				_jackOut.Pause ();
				Thread.Sleep (100);
				_jackOut.Stop ();
				Assert.AreEqual (0, reader.Position);
			}
		}

		[Test]
		public virtual void PlayAudioFileSilent ()
		{
			string wavFile = GetPathToWav ();
			Analyser analyser = new Analyser ();
			using (IWaveSource reader = new WaveFileReader (wavFile)) {
				_jackOut.Volume = 0;
				_jackOut.Initialize (reader);
				_client.ProcessFunc += analyser.AnalyseOutAction;
				_jackOut.Play ();
				Thread.Sleep (100);
				_jackOut.Stop ();
			}
			Assert.AreEqual (0, analyser.NotEmptySamples);
		}

		[TearDown]
		public static void DestroyClient ()
		{
			_jackOut.Dispose ();
			_client.Dispose ();
		}

	}
}
