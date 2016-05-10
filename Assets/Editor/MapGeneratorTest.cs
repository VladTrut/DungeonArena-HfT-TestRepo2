using UnityEngine;
using System.Collections;
using NUnit.Framework;

[TestFixture]
public class MapGeneratorTest {

	[SetUp]
	public void Initialisation(){

	}

	[Test]
	public void CreateMazeTest(){
		MazeRoom[,] tester = new MazeRoom[2, 2];
		tester [0, 0] = new MazeRoom ();
		tester [0, 1] = new MazeRoom ();
		tester [1, 0] = new MazeRoom ();
		tester [1, 1] = new MazeRoom ();
		MazeRoom[,] labyrinth = MazeAlgorithm.CreateMaze (2, 2, new System.Random (1));
		Assert.AreNotEqual (labyrinth, tester);
	}

	[Test]
	public void MapMazeRoomToPrefabTest(){
		MazeRoom testRoom = new MazeRoom ();
		testRoom.NorthWall = false;
		testRoom.SouthWall = false;
		string shouldResult = "0101-A";
		string result = MazeAlgorithm.MapMazeRoomToPrefab (testRoom);
		Assert.AreEqual (result, shouldResult);
	}


}
