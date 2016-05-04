using UnityEngine;
using System.Collections;

public class CollectableEntity : MonoBehaviour {

	public float amount;
	public string valueToIncrease;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Raises the trigger collision2D event.
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerCollision2D(Collider2D other)	{
		if(other.tag == "Player" || other.tag == "Boss") {
			string methodName = "Increase" + valueToIncrease;
			SendMessage (methodName, valueToIncrease);
			Destroy (this.gameObject);
		}
	}

}
