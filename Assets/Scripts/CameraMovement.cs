using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public float scrollSpeed;
	private Vector3 destinationOfCamera;


	void Start ()
    {
		destinationOfCamera = this.transform.position;
	}
	
	void Update ()
    {
		CheckIfDestinationIsReached ();
	}

	/// <summary>
	/// Checks if destination is reached.
	/// </summary>
	/// <returns><c>true</c>, if if destination is reached was checked, <c>false</c> otherwise.</returns>
	public bool CheckIfDestinationIsReached(){
		if (this.transform.position != destinationOfCamera) {
			MoveTowardsDestination ();
			return false;
		} else {
			return true;
		}
	}

	/// <summary>
	/// Moves the towards destination of the Camera.
	/// </summary>
	public void MoveTowardsDestination(){
		float totalMovementSpeed = Time.deltaTime * scrollSpeed;
		this.transform.position = Vector3.MoveTowards (this.transform.position, destinationOfCamera, totalMovementSpeed);
	}

	/// <summary>
	/// Changes the destination Of the Camera (camera will start traveling toward the new Destination)
	/// </summary>
	/// <param name="newDestination">New destination.</param>
	public void changeDestination(Vector3 newDestination){
		this.destinationOfCamera = newDestination;
	}

}
