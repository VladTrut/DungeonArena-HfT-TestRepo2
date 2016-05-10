using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class RandomMapGenerator : MonoBehaviour {

    #region Properties

    public int mazeWidth = 1;
    public int mazeHeight = 1;
    public int randomSeed = 1;

    #endregion Properties

    #region UnityInteraction

    /// <summary>
    /// Is Called Upon at the Initialisation of the Object holding this Script
    /// </summary>
    void Start () {
        System.Random rng;
        if(randomSeed == 0)
        {
            rng = new System.Random();
        }
        else
        {
            rng = new System.Random(randomSeed);
        }
        MazeRoom[,] labyrinth = MazeAlgorithm.CreateMaze(mazeWidth, mazeHeight, rng);
        CreateMazeInScene(labyrinth);
	}
	
	/// <summary>
    /// Update is Called Once Per Frame
    /// </summary>
	void Update () {

	}

    #endregion UnityInteraction

    #region Methods

    /// <summary>
    /// Creates an Random Maze inside the Scene
    /// </summary>
    /// <param name="labyrinth">MazeData as an 2D-Array of MazeRooms</param>
    private void CreateMazeInScene(MazeRoom[,] labyrinth)
    {
        //initialisation//
        const float mapTileWidth = 25; //auslagern...
        const float mapTileHeight = 12.5f; // auslagern...
        //initialisation END//
        
        //loop through the labyrinth//
        for(int currentVerticalPosition = 0; currentVerticalPosition < labyrinth.GetLength(0); currentVerticalPosition++)
        {
            for(int currentHorizontalPosition = 0; currentHorizontalPosition < labyrinth.GetLength(1); currentHorizontalPosition++)
            {
                //calculate position for the GameObject(MapTile)
                float horizontalPositionInScene = currentHorizontalPosition * mapTileWidth;
                float verticalPositionInScene = currentVerticalPosition * mapTileHeight;
                Vector3 placementPosition = new Vector3(horizontalPositionInScene, verticalPositionInScene, 0);
                //Loading and initialization of the GameObject(MapTile)
                string nameOfTile = MazeAlgorithm.MapMazeRoomToPrefab(labyrinth[currentVerticalPosition, currentHorizontalPosition]);
                var mapTile = Instantiate(Resources.Load(nameOfTile), placementPosition, new Quaternion());
                mapTile.name = "MapTile[" + currentVerticalPosition + "," + currentHorizontalPosition + "]";
            }
        }
        //loop END//
    }

    #endregion Methods

}
