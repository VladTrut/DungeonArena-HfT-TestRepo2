using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace UnityTest
{
	/// <summary>
	/// The AppQuit class manages the destruction of the match on the master server.
	/// </summary>
	/// <author>Marcel Mayer</author>
	public class AppQuit : NetworkBehaviour 
	{
		/// <summary>
		/// Called when the application quits.
		/// </summary>
		void OnApplicationQuit()
		{
			NetworkMan networkman = gameObject.GetComponent<NetworkMan> ();
			if (networkman != null) 
			{
				networkman.DestroyMatch ();
			}
		}
	}
}
