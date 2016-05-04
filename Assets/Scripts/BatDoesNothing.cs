using UnityEngine;
using System.Collections;

public class BatDoesNothing : MonoBehaviour {


	//Script ein/ausschalten
	private BatPatrol batPatrol;
	private BatChasesPlayer batChasesPlayer;
	private BatDoesNothing batDoesNothing;

	//Animation
	private Animator anim;

	// Use this for initialization
	void Start () {

		batChasesPlayer = GetComponent<BatChasesPlayer> ();
		batPatrol = GetComponent<BatPatrol> ();
		batDoesNothing = GetComponent<BatDoesNothing> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
//	void OnTriggerEnter2D(Collider2D other) {
//		if (other.tag == "Player") {
//
//			BatChasesPlayerEnabled ();
//
//
//			anim.SetBool ("Running", false);
//			anim.SetBool ("Attacking", true);
//
//
//
//
//
//
//
//
//
//		}}
//
//	void OnTriggerStay2D(Collider2D other) {
//		if (other.tag == "Player") {
//
//			BatChasesPlayerEnabled ();
//
//			anim.SetBool ("Running", false);
//			anim.SetBool ("Attacking", true);
//
//
//
//
//
//		}}
//	void OnTriggerExit2D(Collider2D other) {
//		if (other.tag == "Player") {
//
//
//			BatPatrolEnabled ();
//
//			anim.SetBool ("Attacking", false);
//			anim.SetBool ("Running", true);
//
//
//
//		}}
//	void BatChasesPlayerEnabled() {
//
//
//		batPatrol.enabled = !batPatrol.enabled;
//		batDoesNothing.enabled = !batDoesNothing.enabled;
//		batChasesPlayer.enabled = true;
//
//
//	}
//
//	void BatPatrolEnabled() {
//
//		batChasesPlayer.enabled = !batChasesPlayer.enabled;
//		batPatrol.enabled = true;
//
//	
}