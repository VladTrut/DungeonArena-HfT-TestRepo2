using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnityTest
{
	/// <summary>
	/// The partial NetworkManager manages the assignment of the roles to the players.
	/// </summary>
	/// <author>Marcel Mayer</author>
	public partial class NetworkMan : NetworkManager 
	{
		public Role[] roleList = new Role[4];
		public int nextRole = 0;

		public const string boss = "Boss";
		public const string closeCombatFighter = "CloseCombatFighter";
		public const string distanceFighter = "DistanceFighter";
		public const string magicDude = "MagicDude";

		/// <summary>
		/// Initializes the list of available roles.
		/// </summary>
		public void InitRoleList()
		{
			CreateNewRole (boss);
			CreateNewRole (closeCombatFighter);
			CreateNewRole (distanceFighter);
			CreateNewRole (magicDude);
		}

		/// <summary>
		/// Creates a new role with the name given as parameter.
		/// </summary>
		/// <param name="roleName">Role name.</param>
		public void CreateNewRole(string roleName)
		{
			while (true) 
			{
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
		/// Called every time when a new client connects.
		/// Assigns a role to the new client and starts the match when all players are on board.
		/// </summary>
		/// <param name="conn">Connection between the new client and the server.</param>
		public override void OnServerConnect(NetworkConnection conn)
		{
			if (!roleList.Select(r => r.Id).Contains(conn.connectionId)) 
			{
				base.OnServerConnect (conn);

				//Get random role
				Role role = GetRandomRole(conn.connectionId);

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
		/// Gets a random role from the role list.
		/// </summary>
		/// <returns>The random role.</returns>
		/// <param name="id">ID that should be assigned to the role.</param>
		public Role GetRandomRole(int id)
		{
			Role role = roleList [nextRole];
			role.Id = id;
			nextRole++;

			return role;
		}

		/// <summary>
		/// Called every time when a new client disconnects.
		/// Frees the role so that it can be assigned to another client.
		/// </summary>
		/// <param name="conn">Connection between the new client and the server.</param>
		public override void OnServerDisconnect(NetworkConnection conn)
		{
			base.OnServerDisconnect (conn);

			//Move the role of the disconnecting client to the end and set its id to -1
			InsertRole(conn.connectionId);
		}

		/// <summary>
		/// Inserts a role by moving it to the end and decreasing nextRole.
		/// </summary>
		/// <param name="id">ID that should be inserted.</param>
		public void InsertRole(int id)
		{
			for (int i = 0; i < 4; i++) 
			{
				if (roleList[i].Id == id) 
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
		/// This method is called by the server and informs the clients that the match may start now.
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

		public override bool Equals (object obj)
		{
			if (obj.GetType() != typeof(Role)) 
			{
				return false;
			}

			Role role = (Role)obj;

			if (this.Id == role.Id && this.Name == role.Name) 
			{
				return true;
			}

			return false;
		}
	}
}