using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour {

    #region Properties

    public Image healthBar;
    public Image attackBar;
    public Image defenseBar;
    public Image speedBar;

    public float drainSpeed;
    private float sollHealth;
    private float istHealth;
    private float maxHealth;

    private float currentAttack;
    private float maxAttack;

    private float currentDefense;
    private float maxDefense;

    private float currentSpeed;
    private float maxSpeed;

    #endregion Properties

    #region UnityStuff

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if(sollHealth < istHealth)
        {
            istHealth -= 1 * drainSpeed;
            if(sollHealth > istHealth)
            {
                istHealth = sollHealth;
            }
            healthBar.fillAmount = istHealth / maxHealth;
        }
        if(sollHealth > istHealth)
        {
            istHealth = sollHealth;
            healthBar.fillAmount = istHealth / maxHealth;
        }
	}

    #endregion UnityStuff

    #region MessageMethods

    /// <summary>
    /// Sets MaxHealth for the HUD
    /// </summary>
    /// <param name="maxHealth">max health value of Player</param>
    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth * 100;
    }

    /// <summary>
    /// Sets the Current Health Value for the HUD
    /// </summary>
    /// <param name="sollHealth">current health value of Player</param>
    public void SetCurrentHealth(float sollHealth)
    {
        this.sollHealth = sollHealth * 100;
    }


    /// <summary>
    /// Sets MaxAttack value for the HUD
    /// </summary>
    /// <param name="maxAttack">max Attack Value of Player</param>
    public void SetMaxAttack(float maxAttack)
    {
        this.maxAttack = maxAttack;
    }

    /// <summary>
    /// Sets the current attack value for the HUD
    /// </summary>
    /// <param name="currentAttack">currentAttack value of Player</param>
    public void SetCurrentAttack(float currentAttack)
    {
        this.currentAttack = currentAttack;
    }


    /// <summary>
    /// Sets the max Defense value for the HUD
    /// </summary>
    /// <param name="maxDefense">max defense value of Player</param>
    public void SetMaxDefense(float maxDefense)
    {
        this.maxDefense = maxDefense;
    }

    /// <summary>
    /// Sets the current defense value for the HUD
    /// </summary>
    /// <param name="currentDefense">max defense value of Player</param>
    public void SetCurrentDefense(float currentDefense)
    {
        this.currentDefense = currentDefense;
    }


    /// <summary>
    /// Sets the max speed value for the HUD
    /// </summary>
    /// <param name="maxSpeed">max speed value of Player</param>
    public void SetMaxSpeed(float maxSpeed)
    {
        this.maxSpeed = maxSpeed;
    }

    /// <summary>
    /// Sets the current value for the HUD
    /// </summary>
    /// <param name="currentSpeed">max speed value of Player</param>
    public void SetCurrentSpeed(float currentSpeed)
    {
        this.currentSpeed = currentSpeed;
    }

    #endregion MessageMethods


}
