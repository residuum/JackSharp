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
using NUnit.Framework;
using JackSharp;
using JackSharpTest.Dummies;
using JackSharp.Ports;
using System.Threading;

namespace JackSharpTest
{
	[TestFixture]
	class ServerStartTest
	{
		[Test]
		public virtual void StartingServer ()
		{
			using (Controller controller = new Controller ("testController")) {
				controller.Start (true);
				Thread.Sleep (100);
				Assert.IsTrue (controller.IsConnectedToJack);
				controller.Stop ();
			}			
		}

		[Test]
		public virtual void NotStartingServer ()
		{
			using (Controller controller = new Controller ("testController")) {
				controller.Start ();
				Thread.Sleep (100);
				Assert.IsFalse (controller.IsConnectedToJack);
				controller.Stop ();
			}			
		}

		[Test]
		public virtual void ShutdownEvent ()
		{
			using (Controller controller = new Controller ("testController"))
			using (Processor client = new Processor ("TestClient", 2, 2)) {
				ShutdownReceiver receiver = new ShutdownReceiver ();
				controller.Start (true);
				client.Shutdown += receiver.OnShutdown;
				client.Start ();
				Thread.Sleep (100);
				controller.Stop ();
				Thread.Sleep (100);
				Assert.AreEqual (1, receiver.Shutdowns);
			}			
		}
	}
}

