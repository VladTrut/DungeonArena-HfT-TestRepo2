using UnityEngine;
using System.Collections;

public class CanvasScript : MonoBehaviour {

    GameObject myMenue;

    // Use this for initialization
    void Start () {
        GameObject myMenue = GameObject.Find("Menue");
        myMenue.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
