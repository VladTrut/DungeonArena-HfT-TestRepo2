using System;
using UnityEngine;
using NUnit.Framework;

public class PlayerEnterDetectorTest {

	//private PlayerEnterDetector playerEnterDetector = new PlayerEnterDetector();

	[Test]
	private void AssertCollisionObjectIsCameraTarget(){
		
		////setup
		//GameObject notCameraRelevant = new GameObject ();
		//notCameraRelevant.tag = "someOtherTag";
		//notCameraRelevant.AddComponent (new BoxCollider2D ());
		//GameObject cameraRelevant = new GameObject ();
		//cameraRelevant.tag = "CameraCollisionTarget";
		//cameraRelevant.AddComponent(new BoxCollider2D());

		////test
		//Assert.That (playerEnterDetector.CheckIfCollisionObjectIsCameraTarget (cameraRelevant.GetComponent(Collider2D)));
		//Assert.That (!playerEnterDetector.CheckIfCollisionObjectIsCameraTarget (notCameraRelevant.GetComponent(Collider2D)));

	}


}
