using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Runtime.InteropServices;


namespace UnityTest { 

public class TestGameObjectSpawner 
{

    private GameObject testObject;
    private GameObjectSpawner objectSpawner;
    private GameObject[] testSpawnPoints;

    [SetUp]
    public void SetUp()
    {
        this.objectSpawner = new GameObjectSpawner();
        this.testSpawnPoints = new GameObject[2];
        this.testObject = new GameObject();

        GameObject spawnPoint1 = new GameObject();
        GameObject spawnPoint2 = new GameObject();

        this.testSpawnPoints[0] = spawnPoint1;
        this.testSpawnPoints[1] = spawnPoint2;
    }

    [Test]
    public void testSpawnPlayer()
    {
        // spawn player on empty position
        Vector3 startingPoint = testObject.transform.position;
        Vector3 currentPlayerSpawnPosition = this.objectSpawner.spawnPlayer(this.testSpawnPoints, this.testSpawnPoints.Length, testObject); 
        Assert.AreNotSame(startingPoint, currentPlayerSpawnPosition);

        // spawn player on empty position with one occupied position
        startingPoint = currentPlayerSpawnPosition;
        currentPlayerSpawnPosition = this.objectSpawner.spawnPlayer(this.testSpawnPoints, this.testSpawnPoints.Length, testObject);
        Assert.AreNotSame(startingPoint, currentPlayerSpawnPosition);
    }

    [TearDown]
    public void TearDown()
    {
        this.objectSpawner = null;
    }
 }
}