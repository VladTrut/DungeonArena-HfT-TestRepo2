using UnityEngine;
using System.Collections;

public class BatPatrol : MonoBehaviour 
{


	//Script ein/ausschalten
	private BatPatrol batPatrol;
	private BatChasesPlayer batChasesPlayer;
	private BatDoesNothing batDoesNothing;
	float randomBatWait = 0.0f;
	float randomSeconds = 0.0f;
	float randomStartPatrolSec = 0.0f;

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

	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void Update () 
	{
		// intervall 1 bis 10) wenn random 1 größer 7 dann doenothing enabled




	

		//random 2 gibt an wieviel secunden das script anbleibt (intervall(2-5)
		//if random1  doenothing enalbled , waitsomeseconds batpatrol enabled.
		//else, der rest

		hittingWall = Physics2D.OverlapCircle (wallCheck.position, wallCheckRadius, whatIsWall);
		//atEdge = Physics2D.OverlapCircle (edgeCheck.position, wallCheckRadius, whatIsWall);

		if (hittingWall) 
		{
			moveRight = !moveRight;
		}

		if (moveRight) 
		{
			anim.SetBool ("Running", true);
			transform.localScale = new Vector3 (-1f, 1f, 1f);
			rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);

		}
		else 
		{
			anim.SetBool ("Running", true);
			transform.localScale = new Vector3 (1f, 1f, 1f);
			rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
		}
	}
}
