using Logic.Pathfinding;
using System;
using System.Collections.Generic;

namespace Logic
{
    public static class GameActionNotifier
    {
        public static event Action<int> OnSpawnMapRequested = delegate { };
        public static event Action OnCameraResetRequested = delegate { };
        public static event Action<int, int> OnSpawnMapCompleted = delegate { };
        public static event Action<List<PathNode>> OnPathChanged = delegate { };
        public static event Action OnPlayerStoppedMoving = delegate { };

        public static void NotifyOnSpawnMapRequested(int mapSize)
        {
            OnSpawnMapRequested.Invoke(mapSize);
        }

        public static void NotifyOnCameraResetRequested()
        {
            OnCameraResetRequested.Invoke();
        }

        public static void NotifyOnSpawnMapCompleted(int mapWidth, int mapHeight)
        {
            OnSpawnMapCompleted.Invoke(mapWidth, mapHeight);
        }

        public static void NotifyOnPathChanged(List<PathNode> pathNodesCollection)
        {
            OnPathChanged.Invoke(pathNodesCollection);
        }

        public static void NotifyOnPlayerStoppedMoving()
        {
            OnPlayerStoppedMoving.Invoke();
        }
    }
}
