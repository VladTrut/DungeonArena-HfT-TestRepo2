using UnityEngine;
using System.Collections;

public class BatChasesPlayer : MonoBehaviour 
{

	public Transform target;
	public float speed;
	private float minDistance =1f;
	private float range;


	// Use this for initialization

	void start() 
	{
	
	

	}
	
	// Update is called once per frame
	void Update () 
	{

		range = Vector2.Distance (transform.position, target.position);
		if (range > minDistance) 
		{
			transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
		}
	
	}
}
