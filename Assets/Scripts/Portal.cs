using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

    #region Properties

    //public Portal destinationPortal;
    public GameObject destinationPortal;
    private bool isActive;

    #endregion Properties

    #region Unity Stuff

    // Use this for initialization
    void Start () {
        isActive = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isActive)
        {
            if(other.tag == "Player")
            {
                Portal scriptOfDestinationPortal = destinationPortal.GetComponent<Portal>();
                scriptOfDestinationPortal.IsActive = false;
                other.gameObject.transform.position = destinationPortal.transform.position;
            }
        }
        //if (isActive)
        //{
        //    if (other.tag == "Player")
        //    {
        //        destinationPortal.IsActive = false;
        //        other.gameObject.transform.position = destinationPortal.gameObject.transform.position;
        //    }
        //}
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            isActive = true;
        }
    }

    #endregion UnityStuff

    #region public Methods



    #endregion public Methods

    #region Get/Set

    /// <summary>
    /// Sets If the Portal is Active and can Port (true)
    /// or is deactivated (false)
    /// </summary>
    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    #endregion Get/Set

}
