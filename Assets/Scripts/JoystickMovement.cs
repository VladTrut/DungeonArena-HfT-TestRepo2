using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class JoystickMovement : MonoBehaviour
{

	public float maxSpeed = 10.0f;

	private Rigidbody2D rb2d;
	private Animator anim;
	[HideInInspector] //loockingRight wird im Inspector nicht angezeigt.
	public bool lookingLeft = true;

	//angriff
	private bool attacking=false;
	public Collider2D attackTrigger;

	//target für bat
	public GameObject Target {get; set;}



	void Awake () {
		//angriff
		attackTrigger.enabled = !attackTrigger.enabled;
	
	}

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

		Vector2 moveVec = new Vector2 (inputH, inputV) * maxSpeed;
		rb2d.AddForce (moveVec);

		// set running animation
		if (inputH != 0f || inputV != 0f)
			anim.SetBool ("Running", true);
		else
			anim.SetBool ("Running", false);

		if ((inputH > 0 && lookingLeft) || (inputH < 0 && !lookingLeft)) //Falls geht nach Rechts aber guckt nach Links (und umgekehrt)			
			Flip ();


		//angriff

		if (CrossPlatformInputManager.GetButton ("Attack")) {

//			attacking = true;
//			attackTimer = attackCoolDown;
//
		attackTrigger.enabled = true;


			anim.SetTrigger ("Attacking");

			StartCoroutine("waitOneSecond");






		}
//		if (CrossPlatformInputManager.GetButtonDown ("Attack")) {
//			attackTrigger.enabled = enabled;
//
//		}


	}


	public void Flip ()
	{
		lookingLeft = !lookingLeft;
		Vector3 myScale = transform.localScale;
		myScale.x = myScale.x * -1; //myScale.x *= -1;
		transform.localScale = myScale;
	}

	IEnumerator waitOneSecond(){
		yield return new WaitForSeconds (1);
		attackTrigger.enabled = !attackTrigger.enabled;

		
	}


}