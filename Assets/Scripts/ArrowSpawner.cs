using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnityTest
{
	/// <summary>
	/// The ArrowSpawner class spawns a arrow for each player.
	/// </summary>
	/// <author>Marcel Mayer</author>
	public class ArrowSpawner : NetworkBehaviour {

		public GameObject arrowPrefab;
		private List<GameObject> players = new List<GameObject> ();

		/// <summary>
		/// Called when the player starts
		/// </summary>
		void Start () 
		{
			InitializeArrows ();
		}

		/// <summary>
		/// Initializes the arrows.
		/// </summary>
		public void InitializeArrows()
		{
			if (isLocalPlayer) 
			{
				//Instantiate an arrow for all other players (but not for the local player)
				foreach (var player in GameObject.FindGameObjectsWithTag("Player")) 
				{
					if (!players.Contains (player)) {
						
						var arrow = Instantiate (arrowPrefab, new Vector3 (0, 0), new Quaternion ()) as GameObject;

						//Set the required references
						Arrow arrowscript = arrow.GetComponent<Arrow> ();
						arrowscript.player = gameObject;
						arrowscript.otherPlayer = player;

						players.Add (player);
					}
				}
			}
		}
	}
}
