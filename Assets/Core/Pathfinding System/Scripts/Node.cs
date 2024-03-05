//Nicholas Tvaroha

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    public int gridX; //X Position in the Node Array
    public int gridY; //Y Position in the Node Array
    public bool walkable; //Tells if this note is walkable
    public Vector3 position; //World position of the node

    public Node parent; //What Node came before this one

    public int gCost; //Cost of moving to next square
    public int hCost; //Distance to goal from this node
    public int FCost { get { return hCost + gCost; } }

    public GridCondition condition; //Grid condition of this node

    public Node(bool _IsWalkable, Vector3 a_Pos, int a_gridX, int a_gridY, GridCondition a_condition)
    {
        walkable = _IsWalkable;
        position = a_Pos;
        gridX = a_gridX; 
        gridY = a_gridY;
        condition = a_condition;
        gCost = 0;
        hCost = 0;
    }
}
