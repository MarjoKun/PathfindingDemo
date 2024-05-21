using System.Collections.Generic;
using UnityEngine;

public class Pathfinding 
{
    private Grid<PathNode> PathNodeGrid { get; set; }
    private List<PathNode> OpenPathNodeCollection { get; set; }
    private List<PathNode> CloseePathNodeCollection { get; set; }

    private const int MOVE_STRAIGHT_COST = 10;

    public Pathfinding(int width, int height)
    {
        PathNodeGrid = new Grid<PathNode>(width, height, (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));
    }

    public PathNode GetNode(int width, int height)
    {
        return PathNodeGrid.GetGridObject(width, height);
    }

    public List<PathNode> FindPath(int startWidth, int startHeight, int endWidth, int endHeight)
    {
        PathNode startNode = PathNodeGrid.GetGridObject(startWidth, startHeight);
        PathNode endNode = PathNodeGrid.GetGridObject(endWidth, endHeight);

        OpenPathNodeCollection = new List<PathNode> { startNode };
        CloseePathNodeCollection = new List<PathNode>();

        for(int x = 0; x < PathNodeGrid.GetWidth(); x++)
        {
            for (int y = 0; y < PathNodeGrid.GetHeight(); y++)
            {
                PathNode pathNode = PathNodeGrid.GetGridObject(x, y);
                pathNode.GCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.CameFromNode = null;
            }
        }

        startNode.GCost = 0;
        startNode.HCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (OpenPathNodeCollection.Count > 0)
        {
            PathNode currentPathNode = GetLowestFCostNode(OpenPathNodeCollection);

            if (currentPathNode == endNode)
            {
                return CalculatePath(endNode);
            }

            OpenPathNodeCollection.Remove(currentPathNode);
            CloseePathNodeCollection.Add(currentPathNode);

            foreach (PathNode neighbourPathNode in GetNeighbourList(currentPathNode))
            {
                if (CloseePathNodeCollection.Contains(neighbourPathNode) == true)
                {
                    continue;
                }

                if (neighbourPathNode.IsObstacle == true)
                {
                    CloseePathNodeCollection.Add(neighbourPathNode);
                    continue;
                }

                int tentativeGCost = currentPathNode.GCost + CalculateDistanceCost(currentPathNode, neighbourPathNode);

                if (tentativeGCost < neighbourPathNode.GCost)
                {
                    neighbourPathNode.CameFromNode = currentPathNode;
                    neighbourPathNode.GCost = tentativeGCost;
                    neighbourPathNode.HCost = CalculateDistanceCost(neighbourPathNode, endNode);
                    neighbourPathNode.CalculateFCost();

                    if (OpenPathNodeCollection.Contains(neighbourPathNode) == false)
                    {
                        OpenPathNodeCollection.Add(neighbourPathNode);
                    }
                }
            }
        }

        return null;
    }

    private List<PathNode> GetNeighbourList(PathNode pathNode)
    {
        List<PathNode> neighbourPathNodeCollection = new List<PathNode>();

        if (pathNode.nodeWidth - 1 >= 0)
        {
            neighbourPathNodeCollection.Add(GetNode(pathNode.nodeWidth - 1, pathNode.nodeHeight));;
        }

        if (pathNode.nodeWidth + 1 < PathNodeGrid.GetWidth())
        {
            neighbourPathNodeCollection.Add(GetNode(pathNode.nodeWidth + 1, pathNode.nodeHeight));
        }

        if (pathNode.nodeHeight - 1 >= 0)
        {
            neighbourPathNodeCollection.Add(GetNode(pathNode.nodeWidth, pathNode.nodeHeight - 1));
        }

        if (pathNode.nodeHeight + 1 < PathNodeGrid.GetHeight())
        {
            neighbourPathNodeCollection.Add(GetNode(pathNode.nodeWidth, pathNode.nodeHeight + 1));
        }

        return neighbourPathNodeCollection;
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> pathNodeCollection = new List<PathNode>();
        pathNodeCollection.Add(endNode);
        PathNode currentNode = endNode;

        while (currentNode.CameFromNode != null)
        {
            pathNodeCollection.Add(currentNode.CameFromNode);
            currentNode = currentNode.CameFromNode;
        }

        pathNodeCollection.Reverse();

        return pathNodeCollection;
    }

    private int CalculateDistanceCost(PathNode firstPathNode,  PathNode secondPathNode)
    {
        int widthDistance = Mathf.Abs(firstPathNode.nodeWidth - secondPathNode.nodeWidth);
        int heightDistance = Mathf.Abs(firstPathNode.nodeHeight - secondPathNode.nodeHeight);
        int remainingDistance = Mathf.Abs(widthDistance - heightDistance);

        return MOVE_STRAIGHT_COST * remainingDistance;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeCollection)
    {
        PathNode lowestFCostNode = pathNodeCollection[0];

        for (int i = 1; i < pathNodeCollection.Count; i++)
        {
            if (pathNodeCollection[i].FCost < lowestFCostNode.FCost)
            {
                lowestFCostNode = pathNodeCollection[i];
            }
        }

        return lowestFCostNode;
    }
}
