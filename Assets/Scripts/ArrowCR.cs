using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody2D))] //will add the Rigitbody2D component to the object if it is not existing

public class ArrowCR : MonoBehaviour {

	public float arrowSpeed;
	private Rigidbody2D myRigidbody;
	private Vector2 direction;


	void Start () 
	{
		myRigidbody = GetComponent<Rigidbody2D> ();
		arrowSpeed = 20;
	}

	void FixedUpdate()
	{
		myRigidbody.velocity = direction * arrowSpeed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetDirection(Vector2 direction)
	{
		this.direction = direction;
	}
}
