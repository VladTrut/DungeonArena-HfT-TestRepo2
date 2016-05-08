using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System;
using System.Collections.Generic;

namespace UnityTest
{
    public class PlayerSetup : NetworkBehaviour
    {
        void Update()
        {
            List<Behaviour> componentsToDisable = GetComponentsToDisable();

            if (!isLocalPlayer)
            {
                foreach (Behaviour component in componentsToDisable)
                {
                    component.enabled = false;
                }
            }
        }

        //Networking: Initialisiert den lokalen Spieler.
        public override void OnStartLocalPlayer()
        {
            GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("PlayerSpawnPoint"); //Alle Spawnobjekte(Gameobjects) müssen den Tag "PlayerSpawnPoint" haben!
            int spawnPointCount = spawnPoints.Length;

            GameObjectSpawner sn = gameObject.GetComponent<GameObjectSpawner>();
            sn.spawnPlayer(spawnPoints, spawnPointCount, null); //null damit er den Standard Wert nimmt
        }

        private List<Behaviour> GetComponentsToDisable()
        {
            List<Behaviour> list = new List<Behaviour>();

            GameObject[] cameraCollissionDetector = GameObject.FindGameObjectsWithTag("CameraCollisionDetector");

            for (int i = 0; i < cameraCollissionDetector.Length; i++)
            {
                list.Add(cameraCollissionDetector[i].GetComponent<PlayerEnterDetector>());
            }

            GameObject[] cameraMovement = GameObject.FindGameObjectsWithTag("MainCamera");
            for (int i = 0; i < cameraMovement.Length; i++)
            {
                list.Add(cameraMovement[i].GetComponent<CameraMovement>());
            }

            return list;
        }
    }
}
