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
using System.Threading;
using CSCore.Codecs.WAV;
using Jack.CSCore;
using JackSharp;
using NUnit.Framework;

namespace Jack.CSCoreTest
{
	[TestFixture]
	public class AudioInTest
	{
		static Processor _client;
		static AudioIn _jackIn;

		[SetUp]
		public static void CreateInput ()
		{
			_client = new Processor ("testCSCoreIn", 2);
			_jackIn = new AudioIn (_client);
		}

		[Test]
		public virtual void RecordAudioFile ()
		{
			string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
			string wavFile = Path.Combine (currentDirectory, "recording.wav");
			_jackIn.Initialize ();
			WaveWriter writer = new WaveWriter (wavFile, _jackIn.WaveFormat);
			_jackIn.DataAvailable += (sender, args) => {
				writer.Write (args.Data, 0, args.ByteCount);
			};
			_jackIn.Stopped += (sender, e) => {
				writer.Dispose ();
				long fileSize = new FileInfo (wavFile).Length;
				Assert.AreNotEqual (0, fileSize);
			};
			_jackIn.Start ();
			Thread.Sleep (100);
			_jackIn.Stop ();
		}

		[TearDown]
		public static void DestroyClient ()
		{
			_jackIn.Dispose ();
			_client.Dispose ();
		}
	}
}
