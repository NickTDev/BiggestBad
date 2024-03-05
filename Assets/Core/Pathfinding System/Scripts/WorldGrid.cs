//Nicholas Tvaroha

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace T02.PathSystem
{
    public class WorldGrid : MonoBehaviour
    {
        public Vector2 gridWorldSize; // x is rows, y is columns
        public float nodeRadius; // Radius of a grid square (0.5)
        public float Distance; // Distance between centers of grid squares (1.0)

        Node[,] grid;
        public List<Node> FinalPath;

        float nodeDiameter;
        int gridSizeX, gridSizeY;

        public GameObject node;
        public GameObject node2;

        /// <summary>
        /// Returns the position of the center of the grid
        /// </summary>
        public Vector3 SpawnPos
        {
            get
            {
                Vector3 pos = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
                return pos;
            }
        }

        private void Start()
        {
            //Sets up relevant variables for the grid size
            nodeDiameter = nodeRadius * 2;
            gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
            gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
            CreateGrid();
        }

        /// <summary>
        /// Creates the grid based on the spreadsheet input values
        /// </summary>
        void CreateGrid()
        {
            grid = new Node[gridSizeX, gridSizeY];
            Vector3 bottomLeft = SpawnPos;

            //Sets nodes with Base tag (Normal nodes, no special features) in the grid
            foreach(GameObject square in GameObject.FindGameObjectsWithTag("Base"))
            {
                if (square != null && square.transform.position.x >= bottomLeft.x && square.transform.position.x < bottomLeft.x + gridSizeX)
                {
                    if (square.transform.position.z >= bottomLeft.z && square.transform.position.z < bottomLeft.z + gridSizeY)
                    {
                        int x = (int)(square.transform.position.x - SpawnPos.x);
                        int y = (int)(square.transform.position.z - SpawnPos.z);
                        bool walkable = true;
                        GridCondition condition = new NoneCondition();
                        grid[x, y] = new Node(walkable, square.transform.position, x, y, condition);
                    }
                }
            }

            //Sets nodes with Sand tag (Sandy nodes, slow characters) in the grid
            foreach (GameObject square in GameObject.FindGameObjectsWithTag("Sand"))
            {
                if (square != null && square.transform.position.x >= bottomLeft.x && square.transform.position.x < bottomLeft.x + gridSizeX)
                {
                    if (square.transform.position.z >= bottomLeft.z && square.transform.position.z < bottomLeft.z + gridSizeY)
                    {
                        int x = Mathf.RoundToInt(square.transform.position.x - SpawnPos.x);
                        int y = Mathf.RoundToInt(square.transform.position.z - SpawnPos.z);
                        bool walkable = true;
                        GridCondition condition = new SandCondition();
                        grid[x, y] = new Node(walkable, square.transform.position, x, y, condition);
                    }
                }
            }

            //Sets nodes with Unwalkable tag (Any node that can't be walked on) in the grid
            foreach (GameObject square in GameObject.FindGameObjectsWithTag("Unwalkable"))
            {
                if (square != null && square.transform.position.x >= bottomLeft.x && square.transform.position.x < bottomLeft.x + gridSizeX)
                {
                    if (square.transform.position.z >= bottomLeft.z && square.transform.position.z < bottomLeft.z + gridSizeY)
                    {
                        int x = Mathf.RoundToInt(square.transform.position.x - SpawnPos.x);
                        int y = Mathf.RoundToInt(square.transform.position.z - SpawnPos.z);
                        bool walkable = false;
                        GridCondition condition = new NoneCondition();
                        grid[x, y] = new Node(walkable, square.transform.position, x, y, condition);
                    }
                }
            }

            //Places a physical node at each grid space **TEMPORARY**
            //for (int k = 0; k < gridSizeY; k++)
            //{
            //    for (int x = 0; x < gridSizeX; x++)
            //    {
            //        if (grid[x, k].walkable)
            //        {
            //            if (grid[x, k].condition.conditionType == ConditionType.SANDY)
            //            {
            //                Instantiate(node2, new Vector3(grid[x, k].position.x, grid[x, k].position.y + 1, grid[x, k].position.z), Quaternion.identity);
            //            }
            //            else
            //                Instantiate(node, new Vector3(grid[x, k].position.x, grid[x, k].position.y + 1, grid[x, k].position.z), Quaternion.identity);
            //        }
            //    }
            //}
        }

        /// <summary>
        /// Translates the world position to a position in the grid
        /// </summary>
        public Node NodeFromWorldPosition(Vector3 a_WorldPosition)
        {
            int x = Mathf.RoundToInt(a_WorldPosition.x - SpawnPos.x);
            int y = Mathf.RoundToInt(a_WorldPosition.z - SpawnPos.z);

            return grid[x, y];
        }

        /// <summary>
        /// Returns nodes in each four neighboring directions: up, right, down, left
        /// </summary>
        public List<Node> GetNeighboringNodes(Node a_Node)
        {
            List<Node> NeighboringNodes = new List<Node>();
            int xCheck, yCheck;

            //Right Side
            xCheck = a_Node.gridX + 1;
            yCheck = a_Node.gridY;
            if (xCheck >= 0 && xCheck < gridSizeX)
            {
                if (yCheck >= 0 && yCheck < gridSizeY)
                {
                    NeighboringNodes.Add(grid[xCheck, yCheck]);
                }
            }

            //Left Side
            xCheck = a_Node.gridX - 1;
            yCheck = a_Node.gridY;
            if (xCheck >= 0 && xCheck < gridSizeX)
            {
                if (yCheck >= 0 && yCheck < gridSizeY)
                {
                    NeighboringNodes.Add(grid[xCheck, yCheck]);
                }
            }

            //Top Side
            xCheck = a_Node.gridX;
            yCheck = a_Node.gridY + 1;
            if (xCheck >= 0 && xCheck < gridSizeX)
            {
                if (yCheck >= 0 && yCheck < gridSizeY)
                {
                    NeighboringNodes.Add(grid[xCheck, yCheck]);
                }
            }

            //Bottom Side
            xCheck = a_Node.gridX;
            yCheck = a_Node.gridY - 1;
            if (xCheck >= 0 && xCheck < gridSizeX)
            {
                if (yCheck >= 0 && yCheck < gridSizeY)
                {
                    NeighboringNodes.Add(grid[xCheck, yCheck]);
                }
            }

            return NeighboringNodes;
        }

        public void ClearGrid()
        {
            for (int i = 0; i < gridSizeX; i++)
            {
                for (int j = 0; j < gridSizeY; j++)
                {
                    grid[i, j].gCost = 0;
                    grid[i, j].hCost = 0;
                }
            }
        }
    }
}
