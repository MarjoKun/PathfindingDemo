using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileSelector : MonoBehaviour
{
    [field: SerializeField]
    private Camera MainCamera { get; set; }
    [field: SerializeField]
    private float MaxRaycastDistance { get; set; } = 200.0f;

    public event Action<TileController> OnStartTileSelected = delegate { };
    public event Action<TileController> OnEndTileSelected = delegate { };
    public event Action<TileController> OnObstacleTileStateChanged = delegate { };

    private DestinationSelectorType CurrentDestinationSelectorType { get; set; } = DestinationSelectorType.NONE;

    public void SetDestinationSelectorType(DestinationSelectorType newDestinationSelectorType)
    {
        CurrentDestinationSelectorType = newDestinationSelectorType;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                SetPathingDestinationByType();
            }
        }

        if (Input.GetMouseButtonDown(1) == true)
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                GameObject hitGameObject = RaycastToHitGameObject();

                if (hitGameObject != null)
                {
                    AttemptToChangeTileObstacleState(RaycastToHitGameObject());
                }
            }
        }
    }

    private void SetPathingDestinationByType()
    {
        switch (CurrentDestinationSelectorType)
        {
            case DestinationSelectorType.NONE:
                break;
            case DestinationSelectorType.START_POINT:
                AttemptToSetStartDestination();
                break;
            case DestinationSelectorType.END_POINT:
                AttemptToSetEndDestination();
                break;
        }
    }

    private void AttemptToChangeTileObstacleState(GameObject gameObject)
    {
        TileController cachedTileController = gameObject.GetComponent<TileController>();

        if (cachedTileController != null)
        {
            cachedTileController.ChangeTileObstacleState();
            OnObstacleTileStateChanged.Invoke(cachedTileController);
        }
    }

    private void AttemptToSetStartDestination()
    {
        GameObject hitGameObject = RaycastToHitGameObject();

        if (hitGameObject != null)
        {
            TileController cachedTileController = hitGameObject.GetComponent<TileController>();

            if (cachedTileController != null)
            {
                OnStartTileSelected.Invoke(cachedTileController);
            }
        }
    }

    private void AttemptToSetEndDestination()
    {
        GameObject hitGameObject = RaycastToHitGameObject();

        if (hitGameObject != null)
        {
            TileController cachedTileController = hitGameObject.GetComponent<TileController>();

            if (cachedTileController != null)
            {
                OnEndTileSelected.Invoke(cachedTileController);
            }
        }
    }

    private GameObject RaycastToHitGameObject()
    {
        RaycastHit raycastHit;
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out raycastHit, MaxRaycastDistance))
        {
            if (raycastHit.transform != null)
            {
                return raycastHit.transform.gameObject;
            }
        }

        return null;
    }

    public enum DestinationSelectorType
    {
        NONE,
        START_POINT,
        END_POINT
    }
}
