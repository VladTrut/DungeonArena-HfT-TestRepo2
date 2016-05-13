using UnityEngine;
using System.Collections;

public class ArrowDamage : MonoBehaviour 
{

	public float damage;
	public AudioClip arrowHitSound;

	void Start () 
	{
		damage = 1f;
	}

	void Update () 
	{
	
	}

	void OnTriggerEnter2D (Collider2D other) 
	{
		if (other.tag == "DestructableEntity") 
		{
			other.SendMessage ("DealDamage", damage);

			if (arrowHitSound != null)
				AudioSource.PlayClipAtPoint (arrowHitSound, this.transform.position);
			
			Destroy (gameObject);

		}
		else if(other.tag == "Map")
		{
			if (arrowHitSound != null)
				AudioSource.PlayClipAtPoint (arrowHitSound, this.transform.position);
			
			Destroy (gameObject);
		}
	}


}
