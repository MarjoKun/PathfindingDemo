using UnityEngine;

public class MapGeneratorController : MonoBehaviour
{
    [field: SerializeField]
    private GameObject CubeTilePrefab { get; set; }
    
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
            InstantiateTile(GetTilePosition(columnIndex, rowIndex));
            
            if (columnIndex == mapSize - 1)
            {
                rowIndex ++;
                columnIndex = 0;
            }
            else
            {
                columnIndex++;
            }
        }
    }

    public void AttemptToDestroyMap()
    {
        if (MapTilesParent != null)
        {
            Destroy(MapTilesParent);
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

    private void InstantiateTile(Vector3 spawnPosition)
    {
        Instantiate(CubeTilePrefab, spawnPosition, Quaternion.identity, MapTilesParent.transform);
    }

    private Vector3 GetTilePosition(int columnIndex, int rowIndex)
    {
        return new Vector3(0 + (columnIndex * CubeTilePrefab.transform.localScale.x), 0, rowIndex * CubeTilePrefab.transform.localScale.z);
    }

    private void HandleOnSpawnMabButtonClicked(int mapSize)
    {
        SpawnMap(mapSize);
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
