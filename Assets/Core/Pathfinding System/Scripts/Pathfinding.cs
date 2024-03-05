//Nicholas Tvaroha

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace T02.PathSystem
{
    public class Pathfinding : MonoBehaviour
    {
        WorldGrid grid;

        private void Awake()
        {
            grid = GetComponent<WorldGrid>();
        }

        /// <summary>
        /// Uses A* method to find the shortest walkable path to the target position from the starting position
        /// </summary>
        public void FindPath(Vector3 a_StartPos, Vector3 a_TargetPos)
        {
            Node StartNode = grid.NodeFromWorldPosition(a_StartPos);
            Node TargetNode = grid.NodeFromWorldPosition(a_TargetPos);

            List<Node> OpenList = new List<Node>(); //Current assessing nodes
            HashSet<Node> ClosedList = new HashSet<Node>(); //All nodes

            OpenList.Add(StartNode);
            StartNode.gCost = 0;

            //Iterates through currently assessed nodes
            while (OpenList.Count > 0)
            {
                Node CurrentNode = OpenList[0];

                //Finds the best movement options and removes others
                for (int i = 0; i < OpenList.Count; i++)
                {
                    if (OpenList[i].FCost < CurrentNode.FCost || OpenList[i].FCost == CurrentNode.FCost && OpenList[i].hCost < CurrentNode.hCost)
                    {
                        CurrentNode = OpenList[i];
                    }
                }
                OpenList.Remove(CurrentNode);
                ClosedList.Add(CurrentNode);

                //If the node is where the path is leading, creates the final path
                if (CurrentNode == TargetNode)
                {
                    GetFinalPath(StartNode, TargetNode);
                    break;
                }

                //Adds each of the current nodes neighbors to the open list with updated g and h costs
                foreach (Node NeighborNode in grid.GetNeighboringNodes(CurrentNode))
                {
                    if (!NeighborNode.walkable || ClosedList.Contains(NeighborNode))
                        continue;

                    int MoveCost = CurrentNode.gCost + GetManhattenDistance(CurrentNode, NeighborNode);

                    if (MoveCost < NeighborNode.gCost || !OpenList.Contains(NeighborNode))
                    {
                        NeighborNode.gCost = MoveCost;
                        NeighborNode.hCost = GetManhattenDistance(NeighborNode, TargetNode);
                        NeighborNode.parent = CurrentNode;

                        if(NeighborNode.condition.conditionType == ConditionType.SANDY)
                            NeighborNode.gCost += 1;

                        if (!OpenList.Contains(NeighborNode))
                            OpenList.Add(NeighborNode);
                    }
                }
            }
        }

        /// <summary>
        /// Takes available nodes and checks back through parents to find path, then reverses it to have forward path
        /// </summary>
        void GetFinalPath(Node a_StartNode, Node a_EndNode)
        {
            List<Node> finalPath = new List<Node>();
            Node CurrentNode = a_EndNode;

            while (CurrentNode != a_StartNode)
            {
                finalPath.Add(CurrentNode);
                CurrentNode = CurrentNode.parent;
            }

            finalPath.Reverse();

            grid.FinalPath = finalPath;
        }

        /// <summary>
        /// Finds distance along node paths
        /// </summary>
        int GetManhattenDistance(Node a_node, Node b_node)
        {
            int ix = Mathf.Abs(a_node.gridX - b_node.gridX);
            int iy = Mathf.Abs(a_node.gridY - b_node.gridY);

            return ix + iy;
        }
    }
}
