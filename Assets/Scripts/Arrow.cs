using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

namespace UnityTest
{
	/// <summary>
	/// The Arrow class represents an arrow which points at the other players
	/// </summary>
	/// <author>Marcel Mayer</author>
	public class Arrow : NetworkBehaviour {

		public GameObject player;
		public GameObject otherPlayer;

		public float rotation;

		//Map limits
		public const float leftLimit = -8;
		public const float rightLimit = 8;
		public const float lowerLimit = -6;
		public const float upperLimit = 6;

		//Map width and height
		public const float width = 2000;
		public const float height = 1496;

		/// <summary>
		/// Called once per frame. Rotates and moves the arrow if necessary.
		/// </summary>
		void Update () 
		{
			AddNewArrows ();

			if (player != null) 
			{
				//Calculate angle
				var camera = GameObject.FindGameObjectWithTag("MainCamera");

				float deltaX = otherPlayer.transform.position.x - camera.transform.position.x;
				float deltaY = otherPlayer.transform.position.y - camera.transform.position.y;

				float angle = CalculateAngle (deltaX, deltaY);

				//Rotate arrow
				RotateArrow (angle);

				//Move arrow to the border
				float[] pos = MoveArrowToBorder(angle, deltaX, deltaY);
				CheckMapBorders (ref pos);
				SetArrowRelativeToCamera (ref pos);
				DisableArrowIfInSameMap (ref pos);

				gameObject.transform.position = new Vector3 (pos[0], pos[1], 0);
			}
		}

		/// <summary>
		/// Adds new arrows for new players.
		/// </summary>
		public void AddNewArrows()
		{
			foreach (var player in GameObject.FindGameObjectsWithTag("Player")) 
			{
				ArrowSpawner arrowSpawner = player.GetComponent<ArrowSpawner> ();
				arrowSpawner.InitializeArrows ();
			}
		}

		/// <summary>
		/// Calculates the angle between the camera and the other player.
		/// </summary>
		/// <returns>The angle between the camera and the other player.</returns>
		/// <param name="deltaX">Delta x of the camera and the other player.</param>
		/// <param name="deltaY">Delta y of the camera and the other player.</param>
		public float CalculateAngle(float deltaX, float deltaY)
		{
			float angle = Mathf.Atan (deltaY / deltaX) * 360 / (2 * Mathf.PI);
			angle = (deltaX < 0) ? angle + 180 : angle;

			return angle;
		}

		/// <summary>
		/// Rotates the arrow by the angle given as parameter.
		/// </summary>
		/// <param name="angle">The angle in degree.</param>
		public void RotateArrow(float angle)
		{
			gameObject.transform.Rotate (0, 0, angle - rotation);
			rotation = angle;
		}

		/// <summary>
		/// Calculates the aspect radio angle of the map.
		/// </summary>
		/// <returns>The aspect radio angle of the map.</returns>
		/// <param name="width">The width of the map.</param>
		/// <param name="height">The height of the map.</param>
		public float CalculateAspectRadioAngle(float width, float height)
		{
			return Mathf.Atan (height / width) * 360 / (2 * Mathf.PI);
		}

		/// <summary>
		/// Moves the arrow to border of the map.
		/// </summary>
		/// <returns>The position of the arrow.</returns>
		/// <param name="angle">Angle of the arrow.</param>
		/// <param name="deltaX">Delta x of the camera and the other player.</param>
		/// <param name="deltaY">Delta y of the camera and the other player.</param>
		public float[] MoveArrowToBorder(float angle, float deltaX, float deltaY)
		{
			float[] pos = new float[2];
			float aspectRadioAngle = CalculateAspectRadioAngle (width, height);

			if (angle < aspectRadioAngle && angle > -aspectRadioAngle) 
			{
				//right border
				pos[0] = rightLimit;
				pos[1] = deltaY * pos[0] / deltaX;
			} 
			else if (angle < (180 - aspectRadioAngle) && angle > -aspectRadioAngle) 
			{
				//upper border
				pos[1] = upperLimit;
				pos[0] = deltaX * pos[1] / deltaY;
			} 
			else if (angle < (180 + aspectRadioAngle) && angle > -aspectRadioAngle)
			{
				//left border
				pos[0] = leftLimit;
				pos[1] = deltaY * pos[0] / deltaX;
			} 
			else 
			{
				//lower border
				pos[1] = lowerLimit;
				pos[0] = deltaX * pos[1] / deltaY;
			}

			return pos;
		}

		/// <summary>
		/// Checks if the position given as parameter hits the map borders.
		/// </summary>
		/// <param name="pos">Position to check.</param>
		public void CheckMapBorders(ref float[] pos)
		{
			pos[0] = (pos[0] <= leftLimit) ? leftLimit : pos[0];
			pos[0] = (pos[0] >= rightLimit) ? rightLimit : pos[0];

			pos[1] = (pos[1] <= lowerLimit) ? lowerLimit : pos[1];
			pos[1] = (pos[1] >= upperLimit) ? upperLimit : pos[1];
		}

		/// <summary>
		/// Sets the arrow relative to the camera of the local player.
		/// </summary>
		/// <param name="pos">Position that should be changed.</param>
		public void SetArrowRelativeToCamera(ref float[] pos)
		{
			var camera = GameObject.FindGameObjectWithTag("MainCamera");

			pos[0] += camera.transform.position.x;
			pos[1] += camera.transform.position.y;
		}

		/// <summary>
		/// Disables the arrow if it is in same map than the local player.
		/// </summary>
		/// <param name="pos">Position of the arrow.</param>
		public void DisableArrowIfInSameMap(ref float[] pos)
		{
			var camera = GameObject.FindGameObjectWithTag("MainCamera");
			var map = GameObject.FindGameObjectWithTag ("Map");

			if ((Mathf.Abs(camera.transform.position.x - otherPlayer.transform.position.x)
					< Mathf.Abs(map.transform.position.x) &&
				Mathf.Abs(camera.transform.position.y - otherPlayer.transform.position.y)
					< Mathf.Abs(map.transform.position.y)) ||
				player.Equals(otherPlayer))
			{
				pos[0] = 9999;
				pos[1] = 9999;
			}
		}
	}
}
