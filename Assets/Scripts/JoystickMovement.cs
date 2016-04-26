using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class JoystickMovement : NetworkBehaviour
{

	public float maxSpeed = 10.0f;
    
    private Rigidbody2D rb2d;
	private Animator anim;
	[HideInInspector] //loockingRight wird im Inspector nicht angezeigt.
	public bool lookingLeft = true;

	void Start ()
	{
		rb2d = GetComponent<Rigidbody2D> (); //Reference auf das Componont
		anim = GetComponent<Animator> ();

	}

	void Update ()
	{
		
	}

	void FixedUpdate () //fixierte Frame
	{
        if (!isLocalPlayer)
        {
            return;
        }

        float inputH = CrossPlatformInputManager.GetAxis ("Horizontal");
		float inputV = CrossPlatformInputManager.GetAxis ("Vertical");

		Vector2 moveVec = new Vector2 (inputH, inputV) * maxSpeed;
		rb2d.AddForce (moveVec);

		// set running animation
		if (inputH != 0f || inputV != 0f)
			anim.SetBool ("Running", true);
		else
			anim.SetBool ("Running", false);

		if ((inputH > 0 && lookingLeft) || (inputH < 0 && !lookingLeft)) //Falls geht nach Rechts aber guckt nach Links (und umgekehrt)			
			Flip ();

        if (CrossPlatformInputManager.GetButton ("Attack")) {
			anim.SetTrigger ("Attacking");
		}
	}


	public void Flip ()
	{
		lookingLeft = !lookingLeft;
		Vector3 myScale = transform.localScale;
		myScale.x = myScale.x * -1; //myScale.x *= -1;
		transform.localScale = myScale;
    }

    //Networking: Initialisiert den lokalen Spieler.
    public override void OnStartLocalPlayer()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("PlayerSpawnPoint"); //Alle Spawnobjekte(Gameobjects) müssen den Tag "PlayerSpawnPoint" haben!
        int spawnPointCount = spawnPoints.Length;
        spawnPlayer(spawnPoints, spawnPointCount);
    }

    private void spawnPlayer(GameObject[] spawnPoints, int spawnPointCount)
    {
        int index = 0;
        foreach (GameObject current in spawnPoints)
        {
            Vector3 spawnPosition = current.transform.position;

            if (IsEmptyPosition(spawnPosition))
            {
                transform.position = spawnPosition;
                break;
            }

            if (index >= spawnPointCount)
            {
                spawnPlayer(spawnPoints, spawnPointCount); //Wiederholen bis Spieler gespawned
                break;
            }

            index++;
        }
    }

    private bool IsEmptyPosition(Vector3 targetPos)
    {
        GameObject[] allMovableThings = GameObject.FindGameObjectsWithTag("Physical"); //returns all game object with tag "Physical"
        foreach (GameObject current in allMovableThings)
        {
            if (current.transform.position == targetPos)
                return false;
        }
        return true;
    }
}