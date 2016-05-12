using UnityEngine;
using System.Collections;

public class ArrowDamage : MonoBehaviour {


	public float damage;

	void Start () 
	{
		damage = 1f;
	}

	void Update () 
	{
	
	}

	/*void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject) 
		{
			other.SendMessage ("ApplyDamage", damage);
			Destroy (gameObject);
		}
	}*/

	void OnTriggerEnter2D (Collider2D other) 
	{
		if (other.tag == "DestructableEntity") 
		{
			other.SendMessage ("DealDamage", damage);
			Destroy (gameObject);
		}
		else if(other.tag == "Map")
		{
			Destroy (gameObject);
		}
	}


}
