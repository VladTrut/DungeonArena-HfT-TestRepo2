using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public Transform[] enemySpawns;
	public GameObject Bat;

	// Use this for initialization
	void Start () {

		Spawn();
	}

	void Spawn()
	{
		for (int i = 0; i < enemySpawns.Length; i++)
		{
			int coinFlip = Random.Range (0, 2);
			if (coinFlip > 0)
				Instantiate(Bat, enemySpawns[i].position, Quaternion.identity);
		}
	}

}
