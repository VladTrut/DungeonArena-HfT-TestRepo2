using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput; 

public class JoystickMovement : MonoBehaviour {

	public float maxSpeed = 10.0f;

	private Rigidbody2D rb2d;
	private Animator anim;
	[HideInInspector] //loockingRight wird im Inspector nicht angezeigt.
	public bool lookingRight = true;

	void Start () 
	{
		rb2d = GetComponent<Rigidbody2D> (); //Reference auf das Componont
		anim = GetComponent<Animator> ();

	}

	void Update () 
	{

	}

	void FixedUpdate () //fixierte Frame
	{
		float inputH = CrossPlatformInputManager.GetAxis ("Horizontal");
		float inputV = CrossPlatformInputManager.GetAxis ("Vertical");

		Vector2 moveVec = new Vector2 (CrossPlatformInputManager.GetAxis ("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical")) * maxSpeed;
		rb2d.AddForce (moveVec);

		if (inputH != 0)
			anim.SetFloat ("Speed", Mathf.Abs (inputH));
		else 
			anim.SetFloat ("Speed", 0.0f);

		if (inputV != 0)
			anim.SetFloat ("Speed", Mathf.Abs (inputV));
		else 
			anim.SetFloat ("Speed", 0.0f);

		if ((inputH > 0 && !lookingRight) || (inputH < 0 && lookingRight)) //Falls geht nach Rechts aber guckt nach Links (und umgekehrt)			
			Flip ();

		if (CrossPlatformInputManager.GetButton("Attack")) 
		{
			anim.SetTrigger ("Attacking");
		}

		/*
		float inputH = Input.GetAxis ("Horizontal"); //Wert zwieschen -1.0 und 1.0
		if (Input.GetAxis ("Horizontal")!= 0)
			anim.SetFloat ("Speed", Mathf.Abs (inputH));
		rb2d.velocity = new Vector2 (inputH * maxSpeed, rb2d.velocity.y);

		float inputV = Input.GetAxis ("Vertical"); 
		if(Input.GetAxis ("Vertical")!= 0)
			anim.SetFloat ("Speed", Mathf.Abs (inputV));
		rb2d.velocity = new Vector2 (rb2d.velocity.x, inputV * maxSpeed);

		if ((inputH > 0 && !lookingRight) || (inputH < 0 && lookingRight)) //Falls geht nach Rechts aber guckt nach Links (und umgekehrt)			
			Flip ();

		if (Input.GetButton ("Fire1")) 
		{
			anim.SetTrigger ("Attacking");
		}*/
	}


	public void Flip()
	{
		lookingRight = !lookingRight;
		Vector3 myScale = transform.localScale;
		myScale.x = myScale.x * -1; //myScale.x *= -1;
		transform.localScale = myScale;
	}
}