using UnityEngine;
using System.Collections;

public class WeaponDamage : MonoBehaviour {

	private float currentAttackValue;

	// Use this for initialization
	void Start () {
		currentAttackValue = 1f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Raises the trigger enter2D event.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "DestructableEntity") {
			other.SendMessage ("DealDamage", currentAttackValue);
		}
	}

	/// <summary>
	/// Sets the current attack value.
	/// </summary>
	/// <param name="newAttackValue">New attack value.</param>
	public void SetCurrentAttackValue(float newAttackValue) {
		this.currentAttackValue = newAttackValue;
	}

}
