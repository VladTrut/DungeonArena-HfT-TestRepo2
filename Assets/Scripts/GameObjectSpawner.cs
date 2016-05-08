using UnityEngine;
using System.Collections;

namespace UnityTest
{
    public class GameObjectSpawner : MonoBehaviour
    {

        public Vector3 spawnPlayer(GameObject[] spawnPoints, int spawnPointCount, GameObject playerObject)
        {
            Vector3 currentPlayerSpawnPosition = new Vector3(0, 0, 0);//required for testing
            if (playerObject != null)
            {
                currentPlayerSpawnPosition = playerObject.transform.position;
            }
            else
            {
                currentPlayerSpawnPosition = transform.position;
            }

            int randomSpawnId = new System.Random().Next(0, spawnPointCount);
            Vector3 spawnPosition = spawnPoints[randomSpawnId].transform.position;

            if (IsEmptyPosition(spawnPosition) && playerObject == null) //Nimm lokalen Spieler
            {
                transform.position = spawnPosition;
                currentPlayerSpawnPosition = spawnPosition;
            }
            else if (IsEmptyPosition(spawnPosition) && playerObject != null)//Nimm vorgegebenen Spieler
            {
                playerObject.transform.position = spawnPosition;
                currentPlayerSpawnPosition = spawnPosition;
            }else
            {
                spawnPlayer(spawnPoints, spawnPointCount, playerObject);//Solange wiederholen bis freier Platz gefunden
            }
            return currentPlayerSpawnPosition;
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
}