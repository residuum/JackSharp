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
		public virtual void Start ()
		{
			using (Processor client = new Processor ("testClient")) {
				Assert.IsTrue (client.Start ());
				client.Stop ();
			}
		}

		[Test]
		public virtual void DoubleStart ()
		{
			using (Processor client = new Processor ("testing")) {
				Assert.IsTrue (client.Start ());
				Assert.IsFalse (client.Start ());
			}
		}

		[Test]
		public virtual void StartAfterStopped ()
		{
			using (Processor client = new Processor ("testing", 1)) {
				ClientReceiver receiver = new ClientReceiver ();
				client.ProcessFunc += receiver.ChannelCounterAction;
				Assert.IsTrue (client.Start ());
				client.Stop ();
				Assert.IsTrue (client.Start ());
				Assert.AreEqual (1, client.AudioInPorts.Count ());
				Thread.Sleep (100);
				Assert.AreEqual (1, receiver.Called);
			}
		}

		[Test]
		public virtual void Stop ()
		{
			using (Processor client = new Processor ("testing")) {
				client.Start ();
				Assert.IsTrue (client.Stop ());
			}
		}

		[Test]
		public virtual void StopIfNotStarted ()
		{
			using (Processor client = new Processor ("testing")) {
				Assert.IsFalse (client.Stop ());
			}
		}

		[Test]
		public virtual void SampleRate ()
		{
			using (Processor client = new Processor ("testing")) {
				client.Start ();
				Assert.IsTrue (client.SampleRate == 44100 || client.SampleRate == 48000);
				client.Stop ();
			}
		}

		[Test]
		public virtual void BufferSize ()
		{
			using (Processor client = new Processor ("testing")) {
				client.Start ();
				Assert.IsTrue (client.BufferSize > 0);
				client.Stop ();
			}
		}

		[Test]
		public virtual void AutoConnect ()
		{
			using (Controller controller = new Controller ("testController"))
			using (Processor client = new Processor ("testClient", 2, 2, 0, 0, true)) {
				ControllerReceiver receiver = new ControllerReceiver ();
				controller.ConnectionChanged += receiver.ConnectionChanged;
				controller.Start ();
				Thread.Sleep (100);
				int connectionsWithoutClient = receiver.ConnectionsFound;
				client.Start ();
				Thread.Sleep (100);
				Assert.AreNotEqual (connectionsWithoutClient, receiver.ConnectionsFound);
				client.Stop ();
				controller.Stop ();
			}
		}

		[Test]
		public virtual void DefaultNameFormat ()
		{
			using (Controller controller = new Controller ("testController"))
			using (Processor client = new Processor ("testClient", 1)) {
				client.Start ();
				Thread.Sleep (100);
				Assert.AreEqual ("audioin_1", client.AudioInPorts.First ().Name);
				client.Stop ();
				controller.Stop ();
			}
		}

		[Test]
		public virtual void ChangeNameFormat ()
		{
			using (Controller controller = new Controller ("testController"))
			using (Processor client = new Processor ("testClient", 1)) {
				client.PortNameFormat = "my_{direction}-{type}_{index}";
				client.Start ();
				Thread.Sleep (100);
				Assert.AreEqual ("my_in-audio_1", client.AudioInPorts.First ().Name);
				client.Stop ();
				controller.Stop ();
			}
		}
	}
}
