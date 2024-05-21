using Logic;
using Logic.Pathfinding;
using Player;
using UnityEngine;

namespace UI
{
    public class MainUIController : Controller<MainUIView, MainUIModel>
    {
        [field: SerializeField]
        private TileSelector BoundTileSelector { get; set; }
        [field: SerializeField]
        private TilesManager BoundTilesManager { get; set; }
        [field: SerializeField]
        private PlayerPathfindingController BoundPlayerPathfindingController { get; set; }
        [field: SerializeField]
        private PathfindingController BoundPathfindingController { get; set; }

        private void OnEnable()
        {
            AttachToEvents();
        }

        private void OnDisable()
        {
            DetachFromEvents();
        }

        private void HandleOnNoneToggleValueChanged()
        {
            BoundTileSelector.SetDestinationSelectorType(TileSelector.DestinationSelectorType.NONE);
        }

        private void HandleOnStartToggleValueChanged()
        {
            BoundTileSelector.SetDestinationSelectorType(TileSelector.DestinationSelectorType.START_POINT);
        }

        private void HandleOnEndToggleValueChanged()
        {
            BoundTileSelector.SetDestinationSelectorType(TileSelector.DestinationSelectorType.END_POINT);
        }

        private void HandleOnFindPathButtonClicked()
        {
            if (BoundTilesManager.CurrentStartDestinationTileController != null && BoundTilesManager.CurrentEndDestinationTileController != null)
            {
                int startWidth = BoundTilesManager.CurrentStartDestinationTileController.BoundTileData.WidthPosition;
                int startHeight = BoundTilesManager.CurrentStartDestinationTileController.BoundTileData.HeightPosition;
                int endWidth = BoundTilesManager.CurrentEndDestinationTileController.BoundTileData.WidthPosition;
                int endHeight = BoundTilesManager.CurrentEndDestinationTileController.BoundTileData.HeightPosition;

                BoundPathfindingController.FindNewPath(startWidth, startHeight, endWidth, endHeight);
            }
            else
            {
                Debug.Log("Missing Start or End destination");
            }
        }

        private void HandleOnMovePlayerButtonClicked()
        {
            BoundPlayerPathfindingController.StartPlayerMovement();
        }

        private void AttachToEvents()
        {
            CurrentModel.OnNoneToggleValueChanged += HandleOnNoneToggleValueChanged;
            CurrentModel.OnStartToggleValueChanged += HandleOnStartToggleValueChanged;
            CurrentModel.OnEndToggleValueChanged += HandleOnEndToggleValueChanged;
            CurrentModel.OnFindPathButtonClicked += HandleOnFindPathButtonClicked;
            CurrentModel.OnMovePlayerButtonClicked += HandleOnMovePlayerButtonClicked;
        }

        private void DetachFromEvents()
        {
            CurrentModel.OnNoneToggleValueChanged -= HandleOnNoneToggleValueChanged;
            CurrentModel.OnStartToggleValueChanged -= HandleOnStartToggleValueChanged;
            CurrentModel.OnEndToggleValueChanged -= HandleOnEndToggleValueChanged;
            CurrentModel.OnFindPathButtonClicked += HandleOnFindPathButtonClicked;
            CurrentModel.OnMovePlayerButtonClicked += HandleOnMovePlayerButtonClicked;
        }
    }
}