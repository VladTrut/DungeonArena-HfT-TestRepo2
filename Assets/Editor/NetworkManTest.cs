using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections;
using NUnit.Framework;

[TestFixture]
public class NetworkManTest : NetworkBehaviour
{
	NetworkMan networkManager;

	[SetUp]
	public void SetUp()
	{
		networkManager = new NetworkMan ();
		networkManager.Initialize ();
	}

	[TearDown]
	public void TearDown()
	{
		networkManager = null;
	}

	[Test]
	public void TestInitialize()
	{
		Assert.IsNotNull (networkManager.matchMaker);
		Assert.That (networkManager.matchMaker.isActiveAndEnabled);
	}

	[Test]
	public void TestOnMatchFailure()
	{
		ListMatchResponse response = new ListMatchResponse ();
		response.SetFailure("Failure");

		networkManager.OnMatchList (response);

		Assert.IsTrue (isServer);
	}

	[Test]
	public void TestOnMatchListEmpty()
	{
		ListMatchResponse response = new ListMatchResponse ();
		response.SetSuccess ();
		networkManager.matches = null;

		networkManager.OnMatchList (response);

		Assert.IsTrue (isServer);
	}

	[Test]
	public void TestOnMatchOK()
	{
		ListMatchResponse response = new ListMatchResponse ();
		response.SetSuccess ();

		networkManager.matches = new System.Collections.Generic.List<MatchDesc> ();

		MatchDesc match1 = new MatchDesc ();
		match1.currentSize = 4;
		match1.maxSize = 4;
		networkManager.matches.Add (match1);

		MatchDesc match2 = new MatchDesc ();
		match2.currentSize = 3;
		match2.maxSize = 4;
		networkManager.matches.Add (match2);

		networkManager.OnMatchList (response);

		Assert.IsTrue (isClient);
	}

	[Test]
	public void TestOnMatchNoFreePlace()
	{
		ListMatchResponse response = new ListMatchResponse ();
		response.SetSuccess ();

		networkManager.matches = new System.Collections.Generic.List<MatchDesc> ();

		MatchDesc match1 = new MatchDesc ();
		match1.currentSize = 4;
		match1.maxSize = 4;
		networkManager.matches.Add (match1);

		MatchDesc match2 = new MatchDesc ();
		match2.currentSize = 4;
		match2.maxSize = 4;
		networkManager.matches.Add (match2);

		networkManager.OnMatchList (response);

		Assert.IsTrue (isServer);
	}
}
