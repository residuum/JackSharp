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
using System.Threading;
using JackSharp;
using JackSharpTest.Dummies;
using NUnit.Framework;

namespace JackSharpTest
{
	[TestFixture]
	class ControllerTest
	{
		static Controller _controller;

		[SetUp]
		public static void CreateClient ()
		{
			_controller = new Controller ("testController");
		}

		[Test]
		public virtual void Start ()
		{
			Assert.IsTrue (_controller.Start ());
			_controller.Stop ();
		}

		[Test]
		public virtual void InitialPorts ()
		{
			ControllerReceiver receiver = new ControllerReceiver ();
			_controller.PortChanged += receiver.PortChanged;
			_controller.Start ();
			Thread.Sleep (100);
			_controller.Stop ();
			Assert.AreNotEqual (0, receiver.PortsFound);
		}

		[Test]
		public virtual void ConnectAndDisconnectPorts ()
		{
			ControllerReceiver receiver = new ControllerReceiver ();
			_controller.PortChanged += receiver.PortChanged;
			_controller.ConnectionChanged += receiver.ConnectionChanged;
			_controller.Start ();
			_controller.Connect (receiver.FirstOutPort, receiver.FirstInPort);
			Thread.Sleep (100);
			Assert.AreNotEqual (0, receiver.ConnectionsFound);
			_controller.Disconnect (receiver.FirstOutPort, receiver.FirstInPort);
			Thread.Sleep (100);
			Assert.AreEqual (0, receiver.ConnectionsFound);
			_controller.Stop ();
		}

		[Test]
		public virtual void ConnectConnectedPorts ()
		{
			ControllerReceiver receiver = new ControllerReceiver ();
			_controller.PortChanged += receiver.PortChanged;
			_controller.ConnectionChanged += receiver.ConnectionChanged;
			_controller.Start ();
			Assert.True (_controller.Connect (receiver.FirstOutPort, receiver.FirstInPort));
			Thread.Sleep (100);
			Assert.False (_controller.Connect (receiver.FirstOutPort, receiver.FirstInPort));
			Thread.Sleep (100);
			_controller.Disconnect (receiver.FirstOutPort, receiver.FirstInPort);
			Thread.Sleep (100);
			_controller.Stop ();
		}

		[Test]
		public virtual void DisconnectDisconnectedPorts()
		{
			ControllerReceiver receiver = new ControllerReceiver();
			_controller.PortChanged += receiver.PortChanged;
			_controller.ConnectionChanged += receiver.ConnectionChanged;
			_controller.Start();
			Assert.True(_controller.Connect(receiver.FirstOutPort, receiver.FirstInPort));
			Thread.Sleep(100);
			Assert.True(_controller.Disconnect(receiver.FirstOutPort, receiver.FirstInPort));
			Thread.Sleep(100);
			Assert.False(_controller.Disconnect(receiver.FirstOutPort, receiver.FirstInPort));
			Thread.Sleep(100);
			_controller.Stop();
		}

		[Test]
		public virtual void CanFindPhysicalPorts ()
		{
			ControllerReceiver receiver = new ControllerReceiver ();
			_controller.PortChanged += receiver.PortChanged;
			_controller.Start ();
			Thread.Sleep (100);
			Assert.AreNotEqual (0, receiver.PhysicalPortsFound);
			_controller.Stop ();
		}
		
		[TearDown]
		public static void DestroyClient ()
		{
			_controller.Dispose ();
		}
	}
}
