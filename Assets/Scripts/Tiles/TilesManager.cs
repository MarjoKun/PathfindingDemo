using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour
{
    [field: SerializeField]
    private TileSelector BoundTileSelector { get; set; }
    [field: SerializeField]
    private PathfindingController BoundPathfindingController { get; set; }
    [field: SerializeField]
    private MapGeneratorController BoundMapGeneratorController { get; set; }

    public TileController CurrentStartDestinationTileController { get; private set; }
    public TileController CurrentEndDestinationTileController { get; private set; }
    private List<TileController> CurrentTileControllerCollection { get; set; } = new List<TileController>();

    private void Start()
    {
        AttachToEvents();
    }

    private void OnDestroy()
    {
        DetachFromEvents();
    }

    private void SetNewStartDestinationTile(TileController tileController)
    {
        if (CurrentStartDestinationTileController != null)
        {
            CurrentStartDestinationTileController.SetTileAsNeutral();
        }

        if (tileController.BoundTileData.CurrentTileState == TileData.TileState.OBSTACLE)
        {
            ChangePathfindingObstacleState(tileController);
        }

        CurrentStartDestinationTileController = tileController;
        CurrentStartDestinationTileController.SetTileAsStartDestination();
    }

    private void SetNewEndDestinationTile(TileController tileController)
    {
        if (CurrentEndDestinationTileController != null)
        {
            CurrentEndDestinationTileController.SetTileAsNeutral();
        }

        if (tileController.BoundTileData.CurrentTileState == TileData.TileState.OBSTACLE)
        {
            ChangePathfindingObstacleState(tileController);
        }

        CurrentEndDestinationTileController = tileController;
        CurrentEndDestinationTileController.SetTileAsEndDestination();
    }

    private void ChangePathfindingObstacleState(TileController tileController)
    {
        bool isObstacle = tileController.BoundTileData.CurrentTileState == TileData.TileState.OBSTACLE;
        BoundPathfindingController.SetPathNodeObstacle(tileController.BoundTileData.WidthPosition, tileController.BoundTileData.HeightPosition, isObstacle);
    }

    private void InitializeTileController(GameObject instantiatedGameObject, int columnIndex, int rowIndex)
    {
        TileController cachedTileController = instantiatedGameObject.GetComponent<TileController>();

        if (cachedTileController != null)
        {
            cachedTileController.InitializeTileData(columnIndex, rowIndex);
            CurrentTileControllerCollection.Add(cachedTileController);
        }
    }

    private void ClearTileControllerCollection()
    {
        CurrentTileControllerCollection.Clear();
    }

    private void SetCurrentPathVisual(List<PathNode> pathNodeCollection)
    {
        if (pathNodeCollection.Count > 0)
        {
            TileController pathTileController = null;
            ResetPathVisual();

            for (int i = 0; pathNodeCollection.Count > i; i++)
            {
                pathTileController = GetTileControllerByDimension(pathNodeCollection[i].nodeWidth, pathNodeCollection[i].nodeHeight);

                if (pathTileController != null)
                {
                    if (CheckIfTileIsStartOrEndPoint(pathTileController) == false)
                    {
                        pathTileController.SetTileAsPath();
                    }

                    pathTileController = null;
                }
            }
        }
    }

    private void ResetPathVisual()
    {
        for (int i = 0; CurrentTileControllerCollection.Count > i; i++)
        {
            if (CheckIfTileIsStartOrEndPoint(CurrentTileControllerCollection[i]) == false && CheckIfTileIsObstacle(CurrentTileControllerCollection[i]) == false)
            {
                CurrentTileControllerCollection[i].SetTileAsNeutral();
            }
        }
    }

    private TileController GetTileControllerByDimension(int width, int height)
    {
        for (int i = 0; CurrentTileControllerCollection.Count > i; i++)
        {
            if (CurrentTileControllerCollection[i].BoundTileData.WidthPosition == width && CurrentTileControllerCollection[i].BoundTileData.HeightPosition == height)
            {
                return CurrentTileControllerCollection[i];
            }
        }

        return null;
    }

    private bool CheckIfTileIsStartOrEndPoint(TileController tileController)
    {
        bool isStartPoint = tileController.BoundTileData.CurrentTileState == TileData.TileState.START_DESTINATION;
        bool isEndPoint = tileController.BoundTileData.CurrentTileState == TileData.TileState.END_DESTINATION;

        return isStartPoint || isEndPoint;
    }

    private bool CheckIfTileIsObstacle(TileController tileController)
    {
        return tileController.BoundTileData.CurrentTileState == TileData.TileState.OBSTACLE;
    }

    private void HandleOnStartTileSelected(TileController tileController)
    {
        SetNewStartDestinationTile(tileController);
    }

    private void HandleOnEndTileSelected(TileController tileController)
    {
        SetNewEndDestinationTile(tileController);
    }

    private void HandleOnObstacleTileStateChanged(TileController tileController)
    {
        ChangePathfindingObstacleState(tileController);
    }

    private void HandleOnTileInstantiated(GameObject instantiatedObject, int width, int height)
    {
        InitializeTileController(instantiatedObject, width, height);
    }

    private void HandleOnPathFound(List<PathNode> pathNodesCollection)
    {
        SetCurrentPathVisual(pathNodesCollection);
    }

    private void HandleOnMapDestroyed()
    {
        ClearTileControllerCollection();
    }

    private void AttachToEvents()
    {
        BoundTileSelector.OnStartTileSelected += HandleOnStartTileSelected;
        BoundTileSelector.OnEndTileSelected += HandleOnEndTileSelected;
        BoundTileSelector.OnObstacleTileStateChanged += HandleOnObstacleTileStateChanged;
        BoundMapGeneratorController.OnTileInstantiated += HandleOnTileInstantiated;
        BoundMapGeneratorController.OnMapDestroyed += HandleOnMapDestroyed;
        GameActionNotifier.OnPathFound += HandleOnPathFound;
    }

    private void DetachFromEvents()
    {
        BoundTileSelector.OnStartTileSelected -= HandleOnStartTileSelected;
        BoundTileSelector.OnEndTileSelected -= HandleOnEndTileSelected;
        BoundTileSelector.OnObstacleTileStateChanged -= HandleOnObstacleTileStateChanged;
        BoundMapGeneratorController.OnTileInstantiated -= HandleOnTileInstantiated;
        BoundMapGeneratorController.OnMapDestroyed -= HandleOnMapDestroyed;
        GameActionNotifier.OnPathFound -= HandleOnPathFound;
    }
}
