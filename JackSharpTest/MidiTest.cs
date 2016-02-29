using System.Threading;
using JackSharp;
using JackSharpTest.Dummies;
using NUnit.Framework;

namespace JackSharpTest
{
	[TestFixture]
	public class MidiTest
	{
		static Processor _client;

		[SetUp]
		public static void CreateClient ()
		{
			_client = new Processor ("testMidi", 0, 1, 1, 1);
		}

		[Test]
		public virtual void MidiPlayNote ()
		{
			ClientReceiver receiver = new ClientReceiver ();
			_client.ProcessFunc += receiver.PlayMidiNoteAction;
			_client.Start ();
			Thread.Sleep (100);
			Assert.IsTrue (receiver.Called > 0);
		}

		[Test]
		public virtual void MidiSequence ()
		{
			ClientReceiver receiver = new ClientReceiver ();
			_client.ProcessFunc += receiver.SequenceMidiAction;
			_client.Start ();
			Thread.Sleep (100);
			Assert.IsTrue (receiver.Called > 0);
		}

		[TearDown]
		public static void DestroyClient ()
		{
			_client.Dispose ();
		}
	}
}
