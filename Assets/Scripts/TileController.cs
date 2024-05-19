using UnityEngine;

public class TileController : MonoBehaviour
{
    [field: SerializeField]
    public Transform SelectorDestination { get; private set; }
    [field: SerializeField]
    private MeshRenderer BoundMeshRenderer { get; set; }

    [field: SerializeField]
    private Color NeutralTileColor { get; set; }
    [field: SerializeField]
    private Color ObstacleTileColor { get; set; }
    [field: SerializeField]
    private Color CalculatedOpenColor { get; set; }
    [field: SerializeField]
    private Color CalculatedClosedColor { get; set; }

    private TileData BoundTileData { get; set; } = new TileData();

    public void SetTileSelectState(bool setSelected)
    {
        BoundTileData.SetTileSelectedState(setSelected);
    }

    public void SetTileAsObstacle()
    {
        BoundTileData.ChangeTileState(TileData.TileState.OBSTACLE);
        SetColorForCurrentTileState(BoundTileData.CurrentTileState);
    }

    private void SetColorForCurrentTileState(TileData.TileState tileState)
    {
        switch (tileState)
        {
            case TileData.TileState.NEUTRAL:
                ChangeRendererColor(NeutralTileColor);
                break;
            case TileData.TileState.CALCULATED_OPEN:
                ChangeRendererColor(CalculatedOpenColor);
                break;
            case TileData.TileState.CALCULATED_CLOSED:
                ChangeRendererColor(CalculatedClosedColor);
                break;
            case TileData.TileState.OBSTACLE:
                ChangeRendererColor(ObstacleTileColor);
                break;
            default:
                Debug.LogError($"Unhandled tile state {tileState}");
                break;
        }
    }

    private void ChangeRendererColor(Color newColor)
    {
        BoundMeshRenderer.material.color = newColor;
    }
}

public class TileData
{
    public TileState CurrentTileState { get; private set; } = TileState.NEUTRAL;
    public bool IsTileSelected { get; private set; } = false;

    public void ChangeTileState(TileState newTileState)
    {
        CurrentTileState = newTileState;
    }

    public void SetTileSelectedState(bool setSelected)
    {
        IsTileSelected = setSelected;
    }

    public enum TileState
    {
        NEUTRAL,
        CALCULATED_OPEN,
        CALCULATED_CLOSED,
        OBSTACLE
    }
}
