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

            int index = 0;
            foreach (GameObject current in spawnPoints)
            {
                Vector3 spawnPosition = current.transform.position;

                if (IsEmptyPosition(spawnPosition) && playerObject == null)
                {
                    transform.position = spawnPosition;
                    currentPlayerSpawnPosition = spawnPosition;
                    break;
                }
                else if (IsEmptyPosition(spawnPosition) && playerObject != null)
                {
                    playerObject.transform.position = spawnPosition;
                    currentPlayerSpawnPosition = spawnPosition;
                    break;
                }

                if (index >= spawnPointCount)
                {
                    spawnPlayer(spawnPoints, spawnPointCount, playerObject); //Wiederholen bis Spieler gespawned
                    break;
                }

                index++;
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