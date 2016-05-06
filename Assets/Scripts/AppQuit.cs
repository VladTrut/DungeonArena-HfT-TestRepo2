using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace UnityTest
{
	public class AppQuit : NetworkBehaviour 
	{
		/// <summary>
		/// Called when the application quits.
		/// </summary>
		void OnApplicationQuit()
		{
			NetworkMan networkman = gameObject.GetComponent<NetworkMan> ();
			networkman.DestroyMatch ();
		}
	}
}
