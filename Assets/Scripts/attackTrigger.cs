using UnityEngine;
using System.Collections;

public class attackTrigger : MonoBehaviour {




	//script aufrufen dass die bat tötet . im script: destroy gameobject, mehr nicht.

	void start() {
	}
	void update(){
	}

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "HitBox")
		{
			DestroyObject(GameObject.Find("Bat"));
		}
	}
	void OnTriggerStay2D(Collider2D other  ) {
		if(other.tag == "HitBox")
		{
			DestroyObject(GameObject.Find("Bat"));
		}
	}
}
