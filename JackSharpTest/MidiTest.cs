using System.Threading;
using JackSharp;
using JackSharpTest.Dummies;
using NUnit.Framework;

namespace JackSharpTest
{
	[TestFixture]
	public class MidiTest
	{
		static Client _client;

		[SetUp]
		public static void CreateClient ()
		{
			_client = new Client ("testMidi", 0, 1, 1, 1);
		}

		[Test]
		public virtual void MidiPlayNote ()
		{
			CallbackReceiver receiver = new CallbackReceiver ();
			_client.ProcessFunc = receiver.PlayMidiNoteAction;
			_client.Start ();
			Thread.Sleep (200);
			Assert.IsTrue (receiver.Called > 0);
		}

		[TearDown]
		public static void DestroyClient ()
		{
			_client.Dispose ();
		}
	}
}
