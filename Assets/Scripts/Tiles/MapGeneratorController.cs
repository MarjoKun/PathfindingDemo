using System;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    public class MapGeneratorController : MonoBehaviour
    {
        public event Action<GameObject, int, int> OnTileInstantiated = delegate { };
        public event Action OnMapDestroyed = delegate { };

        [field: SerializeField]
        private GameObject CubeTilePrefab { get; set; }

        private List<TileController> CurrentTileControllerCollection { get; set; } = new List<TileController>();
        private GameObject MapTilesParent { get; set; }

        private const string MAP_PARENT_NAME = "MapParent";

        public void SpawnMap(int mapSize)
        {
            int tilesAmount = mapSize * mapSize;
            int columnIndex = 0;
            int rowIndex = 0;

            AttemptToDestroyMap();
            MapTilesParent = new GameObject(MAP_PARENT_NAME);

            for (int i = 0; i < tilesAmount; i++)
            {
                GameObject instantiatedTile = InstantiateTile(GetTilePosition(columnIndex, rowIndex));
                OnTileInstantiated.Invoke(instantiatedTile, columnIndex, rowIndex);

                if (columnIndex == mapSize - 1)
                {
                    rowIndex++;
                    columnIndex = 0;
                }
                else
                {
                    columnIndex++;
                }
            }

            GameActionNotifier.NotifyOnSpawnMapCompleted(mapSize, mapSize);
        }

        public void AttemptToDestroyMap()
        {
            if (MapTilesParent != null)
            {
                CurrentTileControllerCollection.Clear();
                Destroy(MapTilesParent);
                OnMapDestroyed.Invoke();
            }
        }

        private void OnEnable()
        {
            AttachToEvents();
        }

        private void OnDisable()
        {
            DetachFromEvents();
        }

        private GameObject InstantiateTile(Vector3 spawnPosition)
        {
            return Instantiate(CubeTilePrefab, spawnPosition, Quaternion.identity, MapTilesParent.transform);
        }

        private Vector3 GetTilePosition(int columnIndex, int rowIndex)
        {
            return new Vector3(0 + (columnIndex * CubeTilePrefab.transform.localScale.x), 0, rowIndex * CubeTilePrefab.transform.localScale.z);
        }

        private void HandleOnSpawnMabButtonClicked(int mapSize)
        {
            if (mapSize <= 400)
            {
                SpawnMap(mapSize);
            }
            else
            {
                Debug.Log("400 is the limit. Isn't it enough?");
            }
        }

        private void AttachToEvents()
        {
            GameActionNotifier.OnSpawnMapRequested += HandleOnSpawnMabButtonClicked;
        }

        private void DetachFromEvents()
        {
            GameActionNotifier.OnSpawnMapRequested -= HandleOnSpawnMabButtonClicked;
        }
    }
}
