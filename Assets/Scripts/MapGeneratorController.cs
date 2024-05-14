using UnityEngine;
using UnityEngine.UI;

public class MapGeneratorController : MonoBehaviour
{
    [field: SerializeField]
    private GameObject CubeTilePrefab { get; set; }
    [field: SerializeField]
    private int MapSizeValue { get; set; }

    [field:SerializeField]
    private Button SpawnButton { get; set; }
    

    private GameObject MapTilesParent { get; set; }

    private const string MAP_PARENT_NAME = "MapParent";

    public void SpawnMap()
    {
        int tilesAmount = MapSizeValue * MapSizeValue;
        int columnIndex = 0;
        int rowIndex = 0;

        AttemptToDestroyMap();
        MapTilesParent = new GameObject(MAP_PARENT_NAME);

        for (int i = 0; i < tilesAmount; i++)
        {
            InstantiateTile(GetTilePosition(columnIndex, rowIndex));
            //Instantiate(CubeTilePrefab, new Vector3(0 + (columnIndex * CubeTilePrefab.transform.localScale.x), 0, rowIndex), Quaternion.identity, MapTilesParent.transform);
            
            if (columnIndex == MapSizeValue - 1)
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

    private void SetNewRow()
    {

    }

    private void InstantiateTile(Vector3 spawnPosition)
    {
        Instantiate(CubeTilePrefab, spawnPosition, Quaternion.identity, MapTilesParent.transform);
    }

    private Vector3 GetTilePosition(int columnIndex, int rowIndex)
    {
        return new Vector3(0 + (columnIndex * CubeTilePrefab.transform.localScale.x), 0, rowIndex * CubeTilePrefab.transform.localScale.z);
    }

    private void HandleOnSpawnMabButtonClicked()
    {
        SpawnMap();
    }

    private void AttachToEvents()
    {
        SpawnButton.onClick.AddListener(HandleOnSpawnMabButtonClicked);
    }

    private void DetachFromEvents()
    {
        SpawnButton.onClick.RemoveListener(HandleOnSpawnMabButtonClicked);
    }
}
