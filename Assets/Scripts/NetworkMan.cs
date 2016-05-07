using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System;
using System.Collections;

namespace UnityTest
{
	/// <summary>
	/// The partial NetworkManager manages the connection of the clients to the server.
	/// </summary>
	/// <author>Marcel Mayer</author>
	public partial class NetworkMan : NetworkManager 
	{
		private CreateMatchResponse createMatchinfo;
		private JoinMatchResponse joinMatchinfo;

		/// <summary>
		/// Called when the NetworkManager starts.
		/// </summary>
		void Start () 
		{
			InitRoleList();
			InitializeMatchMaking ();
		}

		/// <summary>
		/// Starts the match making and finds matches
		/// </summary>
		public void InitializeMatchMaking()
		{
			StartMatchMaker ();
			matchMaker.SetProgramAppID ((UnityEngine.Networking.Types.AppID)998552);

			//Find matches
			matchMaker.ListMatches(0, int.MaxValue, "", OnMatchList);
		}

		/// <summary>
		/// Called when the list of matches were received.
		/// Joins an existing match or creates a new match.
		/// </summary>
		/// <param name="matchList">Match list.</param>
		public override void OnMatchList (ListMatchResponse matchList)
		{
			base.OnMatchList(matchList);

			if (matchList.success && matches != null) 
			{
				//Join existing match
				foreach (var match in matches) 
				{
					if (match.currentSize < match.maxSize) 
					{
						matchName = match.name;
						matchSize = (uint)match.currentSize;
						matchMaker.JoinMatch (match.networkId, "", OnMatchJoined);

						return;
					}
				}
			}

			// There is no match to join, start as server
			CreateMatch();
		}

		/// <summary>
		/// Creates a new match.
		/// </summary>
		public void CreateMatch()
		{
			matchMaker.CreateMatch("DungeonArena" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss.fff"), 
				4, true, "", OnMatchCreate);
		}
			
		/// <summary>
		/// Called when a new match was created.
		/// Saves the network ID for the later destruction of the match.
		/// </summary>
		/// <param name="matchInfo">Match info of the new match.</param>
		public override void OnMatchCreate (CreateMatchResponse matchInfo)
		{
			base.OnMatchCreate (matchInfo);

			createMatchinfo = matchInfo;
		}

		/// <summary>
		/// Called when the client joined the match.
		/// Saves the network ID for the later destruction of the match.
		/// </summary>
		/// <param name="response">Match info of the new match.</param>
		public void OnMatchJoined(JoinMatchResponse matchinfo)
		{
			base.OnMatchJoined (matchinfo);

			joinMatchinfo = matchinfo;

			StartCoroutine (CheckJoinSuccessfull ());
		}

		/// <summary>
		/// Checks if the join was successful.
		/// </summary>
		public IEnumerator CheckJoinSuccessfull()
		{
			yield return new WaitForSeconds (10.0f);

			GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");

			if (players.Length == 0) 
			{
				//Join not succesful, create new match
				StopMatchMaker();
				StopHost ();
				StopClient ();
				StopServer ();

				NetworkServer.ClearLocalObjects ();
				NetworkServer.ClearSpawners ();
				NetworkServer.SetAllClientsNotReady ();
				DestroyMatch ();

				StartMatchMaker ();
				CreateMatch();
			}
		}

		/// <summary>
		/// Destroys the match.
		/// </summary>
		public void DestroyMatch()
		{
			if (matchMaker != null) 
			{
				if (createMatchinfo != null) 
				{
					matchMaker.DestroyMatch (createMatchinfo.networkId, OnMatchDestroy);
				} 
				else if (joinMatchinfo != null) 
				{
					matchMaker.DestroyMatch (joinMatchinfo.networkId, OnMatchDestroy);
				}
			}
		}

		/// <summary>
		/// Called when the match was destroyed.
		/// Stops the match maker.
		/// </summary>
		public void OnMatchDestroy(BasicResponse response)
		{
			StopMatchMaker ();
		}
	}
}
