using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System;
using System.Collections;
using System.Collections.Generic;

public class NetworkMan : NetworkManager 
{
	private NetworkMatch networkMatch;

	// Use this for initialization
	void Start () 
	{
		Initialize ();
	}

	public void Initialize()
	{
		StartMatchMaker ();
		matchMaker.SetProgramAppID ((UnityEngine.Networking.Types.AppID)998552);

		//Find matches
		matchMaker.ListMatches(0, int.MaxValue, "", OnMatchList);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public override void OnMatchList (ListMatchResponse matchList)
	{
		base.OnMatchList(matchList);

		if (matchList.success && matches != null) 
		{
			//Join existing match
			foreach (var match in matches) 
			{
				if (match.currentSize < match.maxSize) {
					matchName = match.name;
					matchSize = (uint)match.currentSize;
					matchMaker.JoinMatch (match.networkId, "", OnMatchJoined);

					return;
				}
			}
		}

		// There is no match to join, start as server
		matchMaker.CreateMatch("DungeonArena" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss.fff"), 
			4, true, "", OnMatchCreate);
	}
}
