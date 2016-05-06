using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace UnityTest
{
	/// <summary>
	/// The NetworkManager manages the connection of the clients to the server.
	/// </summary>
	/// <author>Marcel Mayer</author>
	public class NetworkMan : NetworkManager 
	{
		private CreateMatchResponse matchinfo;

		private Role[] roleList = new Role[4];
		private int nextRole = 0;

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
		/// Initializes the list of available roles.
		/// </summary>
		private void InitRoleList()
		{
			CreateNewRole ("Player1");
			CreateNewRole ("Player2");
			CreateNewRole ("Player3");
			CreateNewRole ("Player4");
		}

		/// <summary>
		/// Creates a new role with the name given as parameter.
		/// </summary>
		/// <param name="roleName">Role name.</param>
		private void CreateNewRole(string roleName)
		{
			while (true) {
				int randomNumer = new System.Random ().Next (0, 4);

				if (roleList[randomNumer] == null) 
				{
					//Available position in the array found
					roleList [randomNumer] = new Role(roleName);
					return;
				}
			}
		}

		/// <summary>
		/// Result event of ListMatches.
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
			matchMaker.CreateMatch("DungeonArena" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss.fff"), 
				4, true, "", OnMatchCreate);
		}

		public void OnMatchJoined(JoinMatchResponse response)
		{
			base.OnMatchJoined (response);

			CheckJoinSuccessfull ();
		}

		/// <summary>
		/// Checks if the join was successfull.
		/// </summary>
		public void CheckJoinSuccessfull()
		{
			GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
			Debug.Log ("count: " + players.Length);
		}

		/// <summary>
		/// Called when a new match was created.
		/// </summary>
		/// <param name="matchInfo">Match info of the new match.</param>
		public override void OnMatchCreate (CreateMatchResponse matchInfo)
		{
			base.OnMatchCreate (matchInfo);

			this.matchinfo = matchInfo;
		}

		/// <summary>
		/// Destroys the match.
		/// </summary>
		public void DestroyMatch()
		{
			if (this.matchinfo != null) 
			{
				matchMaker.DestroyMatch (this.matchinfo.networkId, OnMatchDestroy);
			}
		}

		/// <summary>
		/// Called when the match was destroyed.
		/// </summary>
		public void OnMatchDestroy(BasicResponse response)
		{
			StopMatchMaker ();
		}

		/// <summary>
		/// Called every time when a new client connects.
		/// </summary>
		/// <param name="conn">Connection between the new client and the server.</param>
		public override void OnServerConnect(NetworkConnection conn)
		{
			if (!roleList.Select(r => r.Id).Contains(conn.connectionId)) 
			{
				base.OnServerConnect (conn);

				//Get random role
				Role role = roleList [nextRole];
				role.Id = conn.connectionId;
				nextRole++;

				//Assig role to client
				RpcClientStatusChanged (role.Name);

				//Start game
				if (nextRole == 4) 
				{
					RpcStartMatch ();
				}
			}
		}

		/// <summary>
		/// Called every time when a new client disconnects.
		/// </summary>
		/// <param name="conn">Connection between the new client and the server.</param>
		public override void OnServerDisconnect(NetworkConnection conn)
		{
			base.OnServerDisconnect (conn);

			//Move the role of the disconnecting client to the end and set its id to -1
			for (int i = 0; i < 4; i++) 
			{
				if (roleList[i].Id == conn.connectionId) 
				{
					Role swap = roleList [i];
					roleList [i] = roleList [3];
					roleList [3] = swap;

					roleList [3].Id = -1;
					nextRole--;
				}
			}
		}

		/// <summary>
		/// This method is called by the server and assigns a role to the client.
		/// </summary>
		/// <param name="newRole">Role that should be assigned to the client.</param>
		[RPC]
		public void RpcClientStatusChanged(string newRole)
		{
			Debug.Log ("Role: " + newRole);
		}

		/// <summary>
		/// This method is called by the server and informs the clients that the game may start now.
		/// </summary>
		[RPC]
		public void RpcStartMatch()
		{
			Debug.Log ("Start match!");
		}
	}

	/// <summary>
	/// Represents the role of a player.
	/// </summary>
	/// <author>Marcel Mayer</author>
	public class Role
	{
		public int Id { get; set; }
		public string Name { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Role"/> class.
		/// </summary>
		/// <param name="name">The name of the role.</param>
		public Role(string name)
		{
			this.Id = -1;
			this.Name = name;
		}
	}
}