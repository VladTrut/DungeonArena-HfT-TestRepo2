/*
*	Alte Klasse "JoystickMovement"
*	Controller for Cyber Ranger
*	Version: 2.0
*	Autor: Vlad
*
*/

using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput; 
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

	public float maxSpeed = 50.0f; //Dies Wert muesste eine Methode der Classe "Character" übergeben.

	private Rigidbody2D rb2d;
	private Animator anim;
	[HideInInspector] //loockingRight wird im Inspector nicht angezeigt.
	public bool lookingRight = true;

	void Start () 
	{
		rb2d = GetComponent<Rigidbody2D> (); //Reference auf das Component
		anim = GetComponent<Animator> ();

	}

	void Update () 
	{
		if (CrossPlatformInputManager.GetButton("Attack")) //Attack soll immer in Update stehen
		{
			
			//if(collision)
				//anim.SetTrigger ("KnifeAttacking");
			//else
				anim.SetTrigger ("BowAttacking");
			
		}
	}

	void FixedUpdate () //fixierte Frame
	{
		/*if (!isLocalPlayer)
		{
			return;
		}*/

		float inputHAbs = Mathf.Abs(CrossPlatformInputManager.GetAxis ("Horizontal")); //nur absolute Werte
		float inputVAbs = Mathf.Abs(CrossPlatformInputManager.GetAxis ("Vertical"));

		float inputH = CrossPlatformInputManager.GetAxis ("Horizontal");	//reine Output vom Joystick
		float inputV = CrossPlatformInputManager.GetAxis ("Vertical");

		Vector2 moveVec = new Vector2 (inputH, inputV) * maxSpeed;
		rb2d.AddForce (moveVec);


		if (inputHAbs > 0.01f || inputVAbs > 0.01f) 
			anim.SetFloat ("Speed", Mathf.Sqrt(inputHAbs * inputHAbs + inputVAbs * inputVAbs)); 
		else 
			anim.SetFloat ("Speed", 0.0f);

		//Debug.Log (anim.GetFloat("Speed")); //für Debuging des Outputs des Joystiks


		if ((inputH > 0 && !lookingRight) || (inputH < 0 && lookingRight)) //Falls geht nach Rechts aber guckt nach Links (und umgekehrt)			
			Flip ();
		
	}


	public void Flip()
	{
		lookingRight = !lookingRight;
		Vector3 myScale = transform.localScale;
		myScale.x = myScale.x * -1; //myScale.x *= -1;
		transform.localScale = myScale;
	}

	void OnTriggerEnter(Collider other)
	{
		Debug.Log (gameObject.name + "was triggered by" + other.gameObject.name);
			
	}
}