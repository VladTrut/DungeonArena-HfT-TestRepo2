using UnityEngine;
using System.Collections;

public class BatSight : MonoBehaviour {

	private Animator anim;
	private BatPatrol batPatrol;
	private BatChasesPlayer batChasesPlayer;



	// Use this for initialization
	void Start () {


		batChasesPlayer = GetComponent<BatChasesPlayer> ();
		batPatrol = GetComponent<BatPatrol> ();

		anim = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {

			Debug.Log ("Spieler ist is ichtweite!");

			BatChasesPlayerEnabled ();


			anim.SetBool ("Running", false);
			anim.SetBool ("Attacking", true);









		}}

	void OnTriggerStay2D(Collider2D other) {
		if (other.tag == "Player") {

			BatChasesPlayerEnabled ();


			anim.SetBool ("Running", false);
			anim.SetBool ("Attacking", true);





		}}
	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Player") {


			BatPatrolEnabled ();

			anim.SetBool ("Attacking", false);
			anim.SetBool ("Running", true);



		}}

	void BatChasesPlayerEnabled() {


		//batPatrol.enabled = !batPatrol.enabled;
		//batChasesPlayer.enabled = true;
		GameObject.Find ("Bat").GetComponent<BatPatrol> ().enabled = false;

		GameObject.Find("Bat").GetComponent<BatChasesPlayer>().enabled= true;



	}

	void BatPatrolEnabled() {

		//batChasesPlayer.enabled = !batChasesPlayer.enabled;
		GameObject.Find("Bat").GetComponent<BatChasesPlayer>().enabled= false;
		GameObject.Find ("Bat").GetComponent<BatPatrol> ().enabled = true;

		//batPatrol.enabled = true;

	}
}
