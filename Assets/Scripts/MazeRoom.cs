using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Defines the Room Inside a Maze
/// </summary>
public class MazeRoom
{

    #region Properties

    private bool eastWall = true;
    private bool westWall = true;
    private bool northWall = true;
    private bool southWall = true;
    private bool isVisited = false;
    public Stack<Direction> nextDirection; //might need direct use during the Algorithm

    #endregion Properties

    #region Konstruktor

	public MazeRoom(){

	}

    public MazeRoom(Stack<Direction> visitationOrder)
    {
        this.nextDirection = visitationOrder;
    }

    #endregion Konstruktor

    #region Get/Set

    public bool EastWall
    {
        get { return eastWall; }
        set { eastWall = value; }
    }

    public bool WestWall
    {
        get { return westWall; }
        set { westWall = value; }
    }

    public bool NorthWall
    {
        get { return northWall; }
        set { northWall = value; }
    }

    public bool SouthWall
    {
        get { return southWall; }
        set { southWall = value; }
    }

    public bool IsVisited
    {
        get { return isVisited; }
        set { isVisited = value; }
    }

    #endregion Get/Set

    #region Enums

    /// <summary>
    /// Explaines what Equal numbers (1-4) equal in Directions
    /// </summary>
    public enum Direction
    {
        none = 0,
        up = 1,
        right = 2,
        left = 3,
        down = 4,
    }

    #endregion Enums

}
