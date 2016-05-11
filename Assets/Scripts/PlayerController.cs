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
	public GameObject arrowPrefab;
	public Transform spawnPoint;
	public float arrowSpeed = 100f;

	private Rigidbody2D rb2d;
	private Animator anim;
	private Collider2D other;

	[HideInInspector] 
	public bool lookingRight = true;

	private bool isAttacking = false;
	private bool isCollision = false;

	void Start () 
	{
		rb2d = GetComponent<Rigidbody2D> (); //Reference auf das Component
		anim = GetComponent<Animator> ();

	}

	void Update () 
	{
		if (CrossPlatformInputManager.GetButton("Attack") && !isAttacking) //Attack soll immer in Update stehen
		{
			isAttacking = true;	
		}
	}

	void FixedUpdate () //fixierte Frame
	{
		if (!isLocalPlayer)
		{
			return;
		}

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


		//Bow or knife attack
		if (isAttacking && !isCollision) 
		{
			anim.SetTrigger ("BowAttacking");

			if (lookingRight) 
			{

				GameObject arrow = (GameObject) Instantiate (arrowPrefab, spawnPoint.position, Quaternion.Euler(new Vector3 (0, 0, -90))); 
				arrow.GetComponent<ArrowCR>().SetDirection(Vector2.right);

				//GameObject arrow = (GameObject) Instantiate (arrowPrefab, spawnPoint.position, Quaternion.identity);

				/*if (lookingRight)
				arrow.GetComponent<Rigidbody2D> ().AddForce (Vector3.right * arrowSpeed);
			else
				arrow.GetComponent<Rigidbody2D> ().AddForce (Vector3.left * arrowSpeed);*/
			} 
			else 
			{
				GameObject arrow = (GameObject) Instantiate (arrowPrefab, spawnPoint.position, Quaternion.Euler(new Vector3 (0, 0, -90)));  
				arrow.GetComponent<ArrowCR>().SetDirection(Vector2.left);
			}

			isAttacking = false;
		}

		if (isAttacking && isCollision) 
		{
			anim.SetTrigger ("KnifeAttacking");
			isAttacking = false;
		}
	}

	/*public void ArrowShoot(int value)
	{
		
	}*/


	public void Flip()
	{
		lookingRight = !lookingRight;
		Vector3 myScale = transform.localScale;
		myScale.x = myScale.x * -1; //myScale.x *= -1;
		transform.localScale = myScale;
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject) 
		{
			isCollision = true;
			Debug.Log ("Collision exist!");
		} 
	}

	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.gameObject)  
		{
			isCollision = false;
			Debug.Log ("Collision does not exist!");
		}
	}

}

