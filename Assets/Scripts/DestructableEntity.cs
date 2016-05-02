using UnityEngine;
using System.Collections;

public class DestructableEntity : MonoBehaviour {

	private Animator anim;

	public float maxHealth;
	private float currentHealth;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Deals the damage.
	/// </summary>
	/// <param name="amount">Amount.</param>
	public void DealDamage(float amount) {
		float remainingHealth = currentHealth - amount;
		if (remainingHealth <= 0f) {
			currentHealth = 0f;
			Destroy (this.gameObject, 3f);
		}
		else {
			if (remainingHealth > maxHealth) {
				currentHealth = maxHealth;
			}
			else {
				currentHealth = remainingHealth;
			}
		}
		float animationValue = (currentHealth / maxHealth) * 3f;
		anim.SetFloat("Energy", animationValue);
	}

}
