using UnityEngine;
using System.Collections;
using NUnit.Framework;

public class CameraMovementTest {

	CameraMovement cameraMovement = new CameraMovement();

	[Test]
	public void TestIfCameraCanReachDestination(){

		//initialization
		Vector3 cameraOriginPosition = new Vector3 (0, 0, 0);
		Vector3 someOtherPosition = new Vector3 (10, 10, 0);

		//testing

		//1. Destination reached
		cameraMovement.transform.position = cameraOriginPosition;
		Assert.That (cameraMovement.CheckIfDestinationIsReached ());

		//2. Destination not reached
		cameraMovement.changeDestination(someOtherPosition);
		Assert.That (!cameraMovement.CheckIfDestinationIsReached ());

	}

}
