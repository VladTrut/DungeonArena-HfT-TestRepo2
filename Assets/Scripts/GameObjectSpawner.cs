using UnityEngine;
using System.Collections;

public class GameObjectSpawner : MonoBehaviour {

    public void spawnPlayer(GameObject[] spawnPoints, int spawnPointCount)
    {
        if(spawnPoints==null || spawnPoints.Length<=0)
        {
            return;
        }
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
