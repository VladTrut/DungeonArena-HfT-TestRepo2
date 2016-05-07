using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class MazeAlgorithm {

    #region Methods

    /// <summary>
    /// Creates  a "perfect" Labyrinth as an Array of MazeRooms
    /// </summary>
    /// <param name="width">width of the Labyrinth</param>
    /// <param name="height">height of the Labyrinth</param>
    /// <param name="rng">random function used to Generate the Maze</param>
    /// <returns>an Perfect-Labyrinth as an Array of MazeRooms</returns>
    public static MazeRoom[,] CreateMaze(int width, int height, System.Random rng)
    {
        MazeRoom [,] labyrinth = InitiateMaze(width, height, rng); //basic Labyrinth layout
        labyrinth = DepthFirstMazeGenerator(labyrinth, rng); // Create the "real"-Labyrinth
        return labyrinth;
    }

    /// <summary>
    /// Maps the Data of a Maze Room to an Prefab-MazeTile that has equal Properties
    /// </summary>
    /// <param name="roomToMap">room that should be Mapped to an Prefab</param>
    /// <returns>the Name of the Prefab to that the MazeRoom Maps</returns>
    public static string MapMazeRoomToPrefab(MazeRoom roomToMap)
    {
        string prefabName = "";
        //Mapping the MazeRoom Walls to the Prefab Walls

        //1.coordinate equals left exit
        if (roomToMap.WestWall == true)
        {
            prefabName += "1";
        }
        else
        {
            prefabName += "0";
        }
        //2.coordinate equals top exit
        if (roomToMap.NorthWall == true)
        {
            prefabName += "1";
        }
        else
        {
            prefabName += "0";
        }
        //3.coordinate equals right exit
        if (roomToMap.EastWall == true)
        {
            prefabName += "1";
        }
        else
        {
            prefabName += "0";
        }
        //4.coordinate equals bottom exit
        if (roomToMap.SouthWall == true)
        {
            prefabName += "1";
        }
        else
        {
            prefabName += "0";
        }
        prefabName += "-";
        //Mapping the MazeRoom Walls to the Prefab Walls END

        //Selection of the specific Tile To Use
        prefabName += "A";
        //Selection of the specific Tile To Use END

        return prefabName;
    }

    /// <summary>
    /// Generates An Two-Dimensional Array that Acts as the Basic-Structure of the Labyrinth (no room has an exit to another one)
    /// </summary>
    /// <param name="width">horizontal lenght of the Labyrinth</param>
    /// <param name="height">vertical length of the Labyrinth</param>
    /// <param name="rng">Random Number Generator</param>
    /// <returns>an 2D-Array of MazeRooms, Acting as an Initial Labyrinth</returns>
    private static MazeRoom[,] InitiateMaze(int width, int height, System.Random rng)
    {
        MazeRoom[,] labyrinth = new MazeRoom[height, width];
        //Fill labyrinth with new rooms
        for (int verticalcounter = 0; verticalcounter < height; verticalcounter++)
        {
            for (int horizontalcounter = 0; horizontalcounter < width; horizontalcounter++)
            {
                Stack<MazeRoom.Direction> directionStack = CreateDirectionStack(rng);
                MazeRoom currentMazeRoom = new MazeRoom(directionStack);
                labyrinth[verticalcounter, horizontalcounter] = currentMazeRoom;
            }
        }
        return labyrinth;
    }

    /// <summary>
    /// Uses the DepthFirstSearch-Algorithm to Create a "perfect Labyrinth"
    /// (each Mazeroom is Reachable for any given Startpoint)
    /// </summary>
    /// <param name="labyrinth">BaseLabyrinth with all Walls intact</param>
    /// <param name="rng">Random Number Generator  used to determine the StartPosition of the Algorithm</param>
    /// <returns>a "perfect Labyrinth" as a 2D-Array of MazeRooms</returns>
    private static MazeRoom[,] DepthFirstMazeGenerator(MazeRoom[,] labyrinth, System.Random rng)
    {
        //Initialisation Labyrinth border
        int maxVertical = labyrinth.GetLength(0);
        int maxHorizontal = labyrinth.GetLength(1);
        //Intitialisation current Position in Labyrinth
        int currentVertical = rng.Next(maxVertical);
        int currentHorizontal = rng.Next(maxHorizontal);
        int[] currentPosition = { currentVertical, currentHorizontal };


        Stack<int[]> wayTracker = new Stack<int[]>();
        bool stackPopWasUnsuccessfull = false;

        //Algorithm START
        while (!stackPopWasUnsuccessfull) //loop continues until the pop-action of the WayStack fails for the first time
        {
            //mark current position in the labyrinth as visited and set back the position for
            //the next LabyrinthField to Check as well as the variable indication if it is possible
            //to move to a new Field
            labyrinth[currentPosition[0], currentPosition[1]].IsVisited = true;
            int[] nextPosition = new int[2]; // [0]Vertical and [1] Horizontal  position
            bool foundWay = false;

            //STEP1: CHECKING NEIGHBOURS
            //(check as long as there is is still a position in the Stack of the Current MazeRoom
            //and there wasn't a direction that was not visited yet)
            while (labyrinth[currentPosition[0], currentPosition[1]].nextDirection.Count > 0 && !foundWay)
            {
                MazeRoom.Direction directionToTest = labyrinth[currentPosition[0], currentPosition[1]].nextDirection.Pop();
                //Check if the LabyrinthField in the new Direction was already visited
                //(or ignore the direction if it leads outside the labyrinth)
                switch (directionToTest)
                {
                    case MazeRoom.Direction.right:
                        //calculation for the Field to the right from the Current-Position
                        int rightSidePosition = currentPosition[1] + 1;
                        nextPosition[0] = currentPosition[0];
                        nextPosition[1] = rightSidePosition;
                        if (rightSidePosition < maxHorizontal) //check if out-of-bounds
                        {
                            if (labyrinth[nextPosition[0], nextPosition[1]].IsVisited == false)//check if field was alreadyVisited
                            {
                                //if Field was not visited:
                                //break the right wall of the currentLabyrinthPosition and the left wall of the unvisitedLabyrinthPosition
                                //signalize that a not visitedLabyrinth was found (foundWay)
                                labyrinth[currentPosition[0], currentPosition[1]].EastWall = false;
                                labyrinth[nextPosition[0], nextPosition[1]].WestWall = false;
                                foundWay = true;
                            }
                        }
                        break;
                    case MazeRoom.Direction.left:
                        //calculation for the Field to the left from the Current-Position
                        int leftSidePosition = currentPosition[1] - 1;
                        nextPosition[0] = currentPosition[0];
                        nextPosition[1] = leftSidePosition;
                        if (leftSidePosition >= 0) //check if out-of-bounds
                        {
                            if (labyrinth[nextPosition[0], nextPosition[1]].IsVisited == false)//check if field was alreadyVisited
                            {
                                //if Field was not visited:
                                //break the left wall of the currentLabyrinthPosition and the right wall of the unvisitedLabyrinthPosition
                                //signalize that a not visitedLabyrinth was found (foundWay)
                                labyrinth[currentPosition[0], currentPosition[1]].WestWall = false;
                                labyrinth[nextPosition[0], nextPosition[1]].EastWall = false;
                                foundWay = true;
                            }
                        }
                        break;
                    case MazeRoom.Direction.up:
                        //calculation for the upper Field from the Current-Position
                        int upwardPosition = currentPosition[0] + 1;
                        nextPosition[0] = upwardPosition;
                        nextPosition[1] = currentPosition[1];
                        if (upwardPosition < maxVertical) //check if out-of-bounds
                        {
                            if (labyrinth[nextPosition[0], nextPosition[1]].IsVisited == false)//check if field was alreadyVisited
                            {
                                //if Field was not visited:
                                //break the upper wall of the currentLabyrinthPosition and the bottom wall of the unvisitedLabyrinthPosition
                                //signalize that a not visitedLabyrinth was found (foundWay)
                                labyrinth[currentPosition[0], currentPosition[1]].NorthWall = false;
                                labyrinth[nextPosition[0], nextPosition[1]].SouthWall = false;
                                foundWay = true;
                            }
                        }
                        break;
                    case MazeRoom.Direction.down:
                        //calculation for the bottom Field from the Current-Position
                        int downwardPosition = currentPosition[0] - 1;
                        nextPosition[0] = downwardPosition;
                        nextPosition[1] = currentPosition[1];
                        if (downwardPosition >= 0) //check if out-of-bounds
                        {
                            if (labyrinth[nextPosition[0], nextPosition[1]].IsVisited == false)//check if field was alreadyVisited
                            {
                                //if Field was not visited:
                                //break the bottom wall of the currentLabyrinthPosition and the upper wall of the unvisitedLabyrinthPosition
                                //signalize that a not visitedLabyrinth was found (foundWay)
                                labyrinth[currentPosition[0], currentPosition[1]].SouthWall = false;
                                labyrinth[nextPosition[0], nextPosition[1]].NorthWall = false;
                                foundWay = true;
                            }
                        }
                        break;
                }
            }
            //STEP1: CHECKING NEIGHBOURS END

            //STEP2: EVALUATE RESULTS OF NEIGHBOURSEARCH
            if (foundWay) //if a not Visited Labyrinth Field was found: add Last LabyrinthPosition to WayStack and Change currentLabyrinthPosition to the not Visited Field
            {
                wayTracker.Push(currentPosition);
                currentPosition = nextPosition;
            }
            else //if all fields were already Visited: Pop element from the WayStack (a.k.a go one step back)
            {
                if (wayTracker.Count > 0)
                {
                    currentPosition = wayTracker.Pop();
                }
                else //if there is no position left in the Stack, end the search
                {
                    stackPopWasUnsuccessfull = true;
                }
            }
            //STEP2: EVALUATE RESULTS OF NEIGHBOURSEARCH END

        }
        //Algorithm END

        return labyrinth;
    }

    /// <summary>
    /// Creates a Stack with Directions (meant to be used on the MazeObject)
    /// </summary>
    /// <param name="rng">random number generator</param>
    /// <returns>a Stack of Directions</returns>
    private static Stack<MazeRoom.Direction> CreateDirectionStack(System.Random rng)
    {
        //create a list wich contains each direction once
        List<MazeRoom.Direction> directionList = new List<MazeRoom.Direction>();
        directionList.Add(MazeRoom.Direction.up);
        directionList.Add(MazeRoom.Direction.left);
        directionList.Add(MazeRoom.Direction.right);
        directionList.Add(MazeRoom.Direction.down);
        //shuffle content of the list
        directionList = ShuffleDirectionList(directionList, rng);
        //put list content in a Stack
        Stack<MazeRoom.Direction> directionStack = new Stack<MazeRoom.Direction>();
        foreach (MazeRoom.Direction direction in directionList)
        {
            directionStack.Push(direction);
        }
        return directionStack;
    }

    /// <summary>
    /// Randomly Shuffles the Content of a Direction List
    /// </summary>
    /// <param name="list">the list to be Shuffled</param>
    /// <param name="rng">RandomObject, to generate Randomness</param>
    /// <returns>the same List with randomly Shuffled content</returns>
    private static List<MazeRoom.Direction> ShuffleDirectionList(List<MazeRoom.Direction> list, System.Random rng)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            MazeRoom.Direction value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
    }

    #endregion Methods

}
