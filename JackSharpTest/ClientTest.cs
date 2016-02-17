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
using System.Linq;
using System.Threading;
using JackSharp;
using JackSharpTest.Dummies;
using NUnit.Framework;

namespace JackSharpTest
{
	[TestFixture]
	public class ClientTest
	{
		[Test]
		public virtual void TestStart ()
		{
			using (Client client = new Client ("testing")) {
				Assert.IsTrue (client.Start ());
				client.Stop ();
			}
		}

		[Test]
		public virtual void TestDoubleStart ()
		{
			using (Client client = new Client ("testing")) {
				Assert.IsTrue (client.Start ());
				Assert.IsFalse (client.Start ());
			}
		}

		[Test]
		public virtual void TestStartAfterStopped ()
		{
			using (Client client = new Client ("testing", 1)) {
				CallbackReceiver receiver = new CallbackReceiver ();
				client.ProcessFunc = receiver.ChannelCounterAction;
				Assert.IsTrue (client.Start ());
				client.Stop ();
				Assert.IsTrue (client.Start ());
				Assert.AreEqual (client.AudioInPorts.Count (), 1);
				Thread.Sleep (200);
				Assert.IsTrue (receiver.Called > 0);
			}
		}

		[Test]
		public virtual void TestStop ()
		{
			using (Client client = new Client ("testing")) {
				client.Start ();
				Assert.IsTrue (client.Stop ());
			}
		}

		[Test]
		public virtual void TestStopIfNotStarted ()
		{
			using (Client client = new Client ("testing")) {
				Assert.IsFalse (client.Stop ());
			}
		}

		[Test]
		public virtual void TestSampleRate ()
		{
			using (Client client = new Client ("testing")) {
				client.Start ();
				Assert.IsTrue (client.SampleRate == 44100 || client.SampleRate == 48000);
				client.Stop ();
			}

		}

		[Test]
		public virtual void TestBufferSize ()
		{
			using (Client client = new Client ("testing")) {
				client.Start ();
				Assert.IsTrue (client.BufferSize > 0);
				client.Stop ();
			}
		}
	}
}
