using UnityEngine;
using System.Collections;

public class PlayerEnterDetector : MonoBehaviour {

	public Transform cameraTarget;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Triggered if Object that holds script has a Trigger-Collision Box that is entered
	void OnTriggerEnter2D(Collider2D other){
		CheckIfCollisionObjectIsCameraTarget (other);
	}

	/// <summary>
	/// Checks if collision object is camera target.
	/// </summary>
	/// <returns><c>true</c>, if if collision object is camera target was checked, <c>false</c> otherwise.</returns>
	/// <param name="other">Other.</param>
	public bool CheckIfCollisionObjectIsCameraTarget(Collider2D other){
		if (other.tag == "CameraCollisionTarget") {
			Vector3 newCameraPosition = new Vector3 (cameraTarget.position.x, cameraTarget.position.y, Camera.main.transform.position.z);
			Camera.main.SendMessage ("changeDestination", newCameraPosition);
			return true;
		} else {
			return false;
		}
	}

}
