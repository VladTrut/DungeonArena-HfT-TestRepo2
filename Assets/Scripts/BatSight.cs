using UnityEngine;
using System.Collections;

public class BatSight : MonoBehaviour {

	private BatPatrol batPatrol;
	private BatChasesPlayer batChasesPlayer;

	// Use this for initialization
	void Start () {
		batChasesPlayer = GetComponent<BatChasesPlayer> ();
		batPatrol = GetComponent<BatPatrol> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.tag == "Player") {

			Debug.Log ("Spieler ist is Sichtweite!");

			BatChasesPlayerEnabled ();
		}
	}

	void OnTriggerStay2D(Collider2D other) 
	{
		if (other.tag == "Player") {

			BatChasesPlayerEnabled ();
		}
	}

	void OnTriggerExit2D(Collider2D other) 
	{
		if (other.tag == "Player") 
		{
			BatPatrolEnabled ();
		}
	}

	void BatChasesPlayerEnabled() 
	{
		//batPatrol.enabled = !batPatrol.enabled;
		//batChasesPlayer.enabled = true;
		GameObject.Find ("Bat").GetComponent<BatPatrol> ().enabled = false;

		GameObject.Find("Bat").GetComponent<BatChasesPlayer>().enabled= true;
	}

	void BatPatrolEnabled() 
	{
		//batChasesPlayer.enabled = !batChasesPlayer.enabled;
		GameObject.Find("Bat").GetComponent<BatChasesPlayer>().enabled= false;
		GameObject.Find ("Bat").GetComponent<BatPatrol> ().enabled = true;

		//batPatrol.enabled = true;
	}
}
