using UnityEngine;
using System.Collections;

public class BatPatrol : MonoBehaviour {


	//Script ein/ausschalten
	private BatPatrol batPatrol;
	private BatChasesPlayer batChasesPlayer;
	private BatDoesNothing batDoesNothing;
	float randomBatWait = 0.0f;
	float randomSeconds = 0.0f;
	float randomStartPatrolSec =0.0f;

	//Bewegung
	public float moveSpeed;
	public bool moveRight;


	//Patroullie
	public Transform wallCheck;
	public float wallCheckRadius;
	public LayerMask whatIsWall;
	private bool hittingWall;


	//Animation
	private Animator anim;

	// Use this for initialization
	void Start () {



		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		// intervall 1 bis 10) wenn random 1 größer 7 dann doenothing enabled




	

		//random 2 gibt an wieviel secunden das script anbleibt (intervall(2-5)
		//if random1  doenothing enalbled , waitsomeseconds batpatrol enabled.
		//else, der rest

		hittingWall = Physics2D.OverlapCircle (wallCheck.position, wallCheckRadius, whatIsWall);
		//atEdge = Physics2D.OverlapCircle (edgeCheck.position, wallCheckRadius, whatIsWall);

		if (hittingWall) {
			moveRight = !moveRight;
		}

		if (moveRight) {
			anim.SetBool ("Running", true);
			transform.localScale = new Vector3 (-1f, 1f, 1f);
			GetComponent<Rigidbody2D> ().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);

		}
		else {
			anim.SetBool ("Running", true);
			transform.localScale = new Vector3 (1f, 1f, 1f);
			GetComponent<Rigidbody2D> ().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
		}





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
//
//	void BatChasesPlayerEnabled() {
//
//
//		batPatrol.enabled = !batPatrol.enabled;
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
//	}



//	IEnumerator WaitSomeSeconds(){
//
//		randomSeconds = Random.Range (2, 5);
//		Debug.Log ("Warte so viele sekunden bevor es weiter geht: " + randomSeconds);
//		yield return new WaitForSeconds (randomSeconds);
//
//
//	}
}
