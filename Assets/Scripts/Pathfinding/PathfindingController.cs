using System.Collections.Generic;
using UnityEngine;

namespace Logic.Pathfinding
{
    public class PathfindingController : MonoBehaviour
    {
        public List<PathNode> CurrentPathingNodesCollection { get; private set; } = new List<PathNode>();
        private Pathfinding CachedPathfinding { get; set; }

        public void FindNewPath(int startWidth, int startHeight, int endWidth, int endHeight)
        {
            List<PathNode> cachedPathNodeCollection = CachedPathfinding.FindPath(startWidth, startHeight, endWidth, endHeight);

            if (cachedPathNodeCollection != null)
            {
                ClearCurrentPathing();
                CurrentPathingNodesCollection.AddRange(CachedPathfinding.FindPath(startWidth, startHeight, endWidth, endHeight));
                GameActionNotifier.NotifyOnPathChanged(CurrentPathingNodesCollection);
            }
            else
            {
                Debug.Log("Path couldn't be found!");
            }
        }

        public void SetPathNodeObstacle(int nodeWidth, int nodeHeight, bool isObstacle)
        {
            ClearCurrentPathing();
            CachedPathfinding.GetNode(nodeWidth, nodeHeight).IsObstacle = isObstacle;
        }

        public void ClearCurrentPathing()
        {
            CurrentPathingNodesCollection.Clear();
            GameActionNotifier.NotifyOnPathChanged(CurrentPathingNodesCollection);
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
}
