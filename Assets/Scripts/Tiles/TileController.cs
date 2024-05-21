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
    private Color StartDestinationColor { get; set; }
    [field: SerializeField]
    private Color EndDestinationColor { get; set; }
    [field: SerializeField]
    private Color PathColor { get; set; }

    public TileData BoundTileData { get; private set; } = new TileData();

    public void InitializeTileData(int widthPosition, int heightPosition)
    {
        BoundTileData.SetTileCoordinates(widthPosition, heightPosition);
    }

    public void ChangeTileObstacleState()
    {
        if (BoundTileData.CurrentTileState == TileData.TileState.OBSTACLE)
        {
            BoundTileData.ChangeTileState(TileData.TileState.NEUTRAL);
        }
        else
        {
            BoundTileData.ChangeTileState(TileData.TileState.OBSTACLE);
        }

        SetColorForCurrentTileState(BoundTileData.CurrentTileState);
    }

    public void SetTileAsNeutral()
    {
        SetTileState(TileData.TileState.NEUTRAL);
        SetColorForCurrentTileState(TileData.TileState.NEUTRAL);
    }

    public void SetTileAsStartDestination()
    {
        SetTileState(TileData.TileState.START_DESTINATION);
        SetColorForCurrentTileState(TileData.TileState.START_DESTINATION);
    }

    public void SetTileAsEndDestination()
    {
        SetTileState(TileData.TileState.END_DESTINATION);
        SetColorForCurrentTileState(TileData.TileState.END_DESTINATION);
    }

    public void SetTileAsPath()
    {
        SetTileState(TileData.TileState.PATH);
        SetColorForCurrentTileState(TileData.TileState.PATH);
    }

    private void SetTileState(TileData.TileState newTileState)
    {
        BoundTileData.ChangeTileState(newTileState);
    }

    private void SetColorForCurrentTileState(TileData.TileState tileState)
    {
        switch (tileState)
        {
            case TileData.TileState.NEUTRAL:
                ChangeRendererColor(NeutralTileColor);
                break;
            case TileData.TileState.START_DESTINATION:
                ChangeRendererColor(StartDestinationColor);
                break;
            case TileData.TileState.END_DESTINATION:
                ChangeRendererColor(EndDestinationColor);
                break;
            case TileData.TileState.PATH:
                ChangeRendererColor(PathColor);
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
    public int WidthPosition { get; private set; }
    public int HeightPosition { get; private set; }
    public TileState CurrentTileState { get; private set; } = TileState.NEUTRAL;

    public void SetTileCoordinates(int width, int height)
    {
        WidthPosition = width; 
        HeightPosition = height;
    }

    public void ChangeTileState(TileState newTileState)
    {
        CurrentTileState = newTileState;
    }

    public enum TileState
    {
        NEUTRAL,
        START_DESTINATION,
        END_DESTINATION,
        PATH,
        OBSTACLE
    }
}
