using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerStatus : NetworkBehaviour {

	public WeaponDamage weaponDamage;
	//public SimplePlayerController simplePlayerController;

	private StatusBar playerGUI;

	public float maxHealth;
	public float minHealth;
	public float currentHealth;

	public float maxAttack;
	public float minAttack;
	public float currentAttack;

	public float maxDefence;
	public float minDefence;
	public float currentDefence;

	public float maxSpeed;
	public float minSpeed;
	public float currentSpeed;

	// Use this for initialization
	void Start () {
		weaponDamage.SetCurrentAttackValue (currentAttack);
		GameObject gui = GameObject.Find ("MainContainer");
		playerGUI = gui.GetComponent<StatusBar> ();
		if (isLocalPlayer) {
			playerGUI.SetMaxHealth (maxHealth);
			playerGUI.SetMaxAttack (maxAttack);
			playerGUI.SetMaxDefense(maxDefence);
			playerGUI.SetMaxSpeed (maxSpeed);
			playerGUI.SetCurrentHealth (currentHealth);
			playerGUI.SetCurrentAttack (currentAttack);
			playerGUI.SetCurrentDefense (currentDefence);
			playerGUI.SetCurrentSpeed (currentSpeed);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// Deals the damage.
	/// </summary>
	/// <param name="amount">Amount.</param>
	public void DealDamage(float amount) {
		// Consideration of defence
		float myDamage;
		if (amount < 0f) {
			myDamage = amount;
		}
		else {
			if (currentDefence > amount) {
				myDamage = 0f;
			}
			else {
				myDamage = amount - currentDefence;
			}
		}
		// Consideration of defence END

		float remainingHealth = currentHealth - amount;
		if (remainingHealth <= 0f) {
			currentHealth = 0f;
			//ToDo: Intitiate Death
		}
		else {
			if (remainingHealth > maxHealth) {
				currentHealth = maxHealth;
			}
			else {
				currentHealth = remainingHealth;
			}
		}
		if (isLocalPlayer) {
			playerGUI.SetCurrentHealth (currentHealth);
		}
	}

	/// <summary>
	/// Increases the attack.
	/// </summary>
	/// <param name="amount">Amount.</param>
	public void IncreaseAttack(float amount) {
		if (amount >= maxAttack) {
			currentAttack = maxAttack;
		}
		else {
			if (amount <= minAttack) {
				currentAttack = minAttack;
			}
			else {
				currentAttack = amount;
			}
		}
		weaponDamage.SetCurrentAttackValue (currentAttack);
		if (isLocalPlayer) {
			playerGUI.SetCurrentAttack (currentAttack);
		}
	}

	/// <summary>
	/// Increases the defence.
	/// </summary>
	/// <param name="amount">Amount.</param>
	public void IncreaseDefence(float amount) {
		if (amount >= maxDefence) {
			currentDefence = maxDefence;
		}
		else {
			if (amount <= minDefence) {
				currentDefence = minDefence;
			}
			else {
				currentDefence = amount;
			}
		}
		if (isLocalPlayer) {
			playerGUI.SetCurrentDefense (currentDefence);
		}
	}

	/// <summary>
	/// Increases the speed.
	/// </summary>
	/// <param name="amount">Amount.</param>
	public void IncreaseSpeed(float amount) {
		if (amount >= maxSpeed) {
			currentSpeed = maxSpeed;
		}
		else {
			if (amount <= minSpeed) {
				currentSpeed = minSpeed;
			}
			else {
				currentSpeed = amount;
			}
		}
		//simplePlayerController.SetSpeed (currentSpeed);
		if (isLocalPlayer) {
			playerGUI.SetCurrentSpeed (currentSpeed);
		}
	}

}
