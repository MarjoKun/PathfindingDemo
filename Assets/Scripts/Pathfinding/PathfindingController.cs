using System.Collections.Generic;
using UnityEngine;

public class PathfindingController : MonoBehaviour
{
    public List<PathNode> CurrentPathingNodesCollection { get; private set; } = new List<PathNode>();
    private Pathfinding CachedPathfinding { get; set; }

    public void FindNewPath(int startWidth, int startHeight, int endWidth, int endHeight)
    {
        CurrentPathingNodesCollection.Clear();
        CurrentPathingNodesCollection.AddRange(CachedPathfinding.FindPath(startWidth, startHeight, endWidth, endHeight));
        GameActionNotifier.NotifyOnPathFound(CurrentPathingNodesCollection);
    }

    public void SetPathNodeObstacle(int nodeWidth, int nodeHeight, bool isObstacle)
    {
        CachedPathfinding.GetNode(nodeWidth, nodeHeight).IsObstacle = isObstacle;
    }

    private void Start()
    {
        AttachToEvents();
    }

    private void OnDestroy()
    {
        DetachFromEvents();
    }

    private void CreateCurrentPathfinding(int mapWidth, int mapHeight)
    {
        CachedPathfinding = new Pathfinding(mapWidth, mapHeight);
    }

    private void HandleOnSpawnMapCompleted(int mapWidth, int mapHeight)
    {
        CreateCurrentPathfinding(mapWidth, mapHeight);
    }

    private void AttachToEvents()
    {
        GameActionNotifier.OnSpawnMapCompleted += HandleOnSpawnMapCompleted;
    }

    private void DetachFromEvents()
    {
        GameActionNotifier.OnSpawnMapCompleted -= HandleOnSpawnMapCompleted;
    }
}
