using UnityEngine;
using System.Collections;

public class CyberRangerAnimationEvents : MonoBehaviour 
{
	public int sortingOrder = 1;
	public SpriteRenderer sprite;

	public void SetArrowOrderInLayer()
	{
		sprite = GetComponent<SpriteRenderer>();

		if (sprite)
		{
			sprite.sortingOrder = sortingOrder;
		}

	}

	public void ArrowToHand()
	{
		//GameObject Arrow2 = GameObject.Find ("CyberRanger/ROOT_TAZ/Arrow2");
		GameObject Arrow2 = GameObject.Find ("CyberRanger/Arrow2");
		Debug.Log("found :"+Arrow2);

		GameObject Hand = GameObject.Find ("CyberRanger/ROOT_TAZ/POYAS/torso/L_ARM/bone_005");
		Debug.Log("found :"+Hand);

		Arrow2.transform.parent = Hand.transform;

		//Arrow2.transform.localPosition = Vector3(0,0,0); 
		//GetComponent<Rigidbody>().isKinematic = true; 


	}

	public void ArrowBack()
	{
		GameObject Arrow2 = GameObject.Find ("CyberRanger/ROOT_TAZ/POYAS/torso/L_ARM/bone_005/Arrow2");
		//GameObject ROOT_TAZ = GameObject.Find ("CyberRanger/ROOT_TAZ");
		GameObject Root = GameObject.Find ("CyberRanger");

		Arrow2.transform.parent = Root.transform;
	}

	public void ArrowSetNull()
	{		
		GameObject Arrow2 = GameObject.Find ("CyberRanger/ROOT_TAZ/POYAS/torso/L_ARM/bone_005/l_hand_bone/l_hand/Arrow2");
		GameObject empty = GameObject.Find("GameObject");
		Arrow2.transform.parent = empty.transform; 
	}

	public void ArrowSetDefault()
	{
		GameObject Arrow2 = GameObject.Find ("GameObject/Arrow2");
		GameObject Hand = GameObject.Find ("CyberRanger/ROOT_TAZ/POYAS/torso/L_ARM/bone_005/l_hand_bone/l_hand");
		Arrow2.transform.parent = Hand.transform;
	}

}