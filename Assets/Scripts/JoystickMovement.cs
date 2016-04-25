using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class JoystickMovement : NetworkBehaviour
{

	public float maxSpeed = 10.0f;
    public string mapObjectName = "";
    public float spawnAreaAdaption = 10f; //Manuell spawn Area anpassen
    
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

        var mapObject = GameObject.Find(mapObjectName);
        SpriteRenderer renderer = mapObject.GetComponent<SpriteRenderer>();

        float radius = renderer.bounds.extents.magnitude;
        float maxXRandomSpawnPoint = radius - spawnAreaAdaption;
        float minXRandomSpawnPoint = -radius + spawnAreaAdaption;
        float maxYRandomSpawnPoint = radius - spawnAreaAdaption;
        float minYRandomSpawnPoint = -radius + spawnAreaAdaption;

        while (true)
        {
           float spawnX = Random.Range(minXRandomSpawnPoint, maxXRandomSpawnPoint);
           float spawnY = Random.Range(minYRandomSpawnPoint, maxYRandomSpawnPoint);
           bool isEmpty = false;
           bool isGameArea = true; //wenn bereit, auf false aendern

           Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

           isEmpty = IsEmptyPosition(spawnPosition);
           //isGameArea = IsInGameArea(spawnPosition);

            if (isEmpty && isGameArea)
           {
                transform.position = spawnPosition;
                break;
           }
        }
    }

    public bool IsEmptyPosition(Vector3 targetPos)
    {
        GameObject[] allMovableThings = GameObject.FindGameObjectsWithTag("Physical"); //returns all game object with tag "Physical"
        foreach (GameObject current in allMovableThings)
        {
            if (current.transform.position == targetPos)
                return false;
        }
        return true;
    }

    public bool IsInGameArea(Vector3 targetPos)
    {
        GameObject gameArea = GameObject.FindGameObjectWithTag("GameArea"); 
        if (gameArea.transform.position == targetPos)
        {
           return true;
        }
        return false;
    }
}