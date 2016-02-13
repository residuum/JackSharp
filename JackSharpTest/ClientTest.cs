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
using JackSharp;
using NUnit.Framework;

namespace JackSharpTests
{
	[TestFixture]
	public class ClientTest
	{

		[Test]
		public virtual void TestOpen ()
		{
			using (Client client = new Client ("testing")) {
				Assert.IsTrue (client.Open ());
				client.Close ();
			}
		}

		[Test]
		public virtual void TestClose ()
		{
			using (Client client = new Client ("testing")) {
				client.Open ();
				Assert.IsTrue (client.Close ());
			}
		}

		[Test]
		public virtual void TestCloseIfNotOpened ()
		{
			using (Client client = new Client ("testing")) {
				Assert.IsFalse (client.Close ());
			}
		}

		[Test]
		public virtual void TestSampleRate ()
		{
			using (Client client = new Client ("testing")) {
				client.Open ();
				Assert.IsTrue (client.SampleRate == 44100 || client.SampleRate == 48000);
				client.Close ();
			}

		}

		[Test]
		public virtual void TestBufferSize ()
		{
			using (Client client = new Client ("testing")) {
				client.Open ();
				Assert.IsTrue (client.BufferSize > 0);
				client.Close ();
			}

		}
	}
}
