using UnityEngine;
using System.Collections;

public class MovementForTest : MonoBehaviour {

    #region Properties

    public string direction;
    public float speed;

    private Rigidbody2D rb2d;

    #endregion Properties

    #region Unity Stuff

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        float x = 0;
        float y = 0;

        switch (direction)
        {
            case "right":
                x += 1;
                break;
            case "left":
                x -= 1;
                break;
            case "up":
                y += 1;
                break;
            case "down":
                y -= 1;
                break;
        }

        rb2d.AddForce(new Vector2(x, y) * speed);
    }

    #endregion Unity Stuff

}
