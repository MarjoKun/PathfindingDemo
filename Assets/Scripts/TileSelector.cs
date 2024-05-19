using UnityEngine;

public class TileSelector : MonoBehaviour
{
    [field: SerializeField]
    private GameObject SelectorObject { get; set; }

    private TileController CurrentlySelectedTileController { get; set; }
    private DestinationSelectorType CurrentDestinationSelectorType { get; set; }

    private void Awake()
    {
        SetSelectorObjectVisibilityState(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) == true)
        {
            AttemptToSelectTile(RaycastToHitGameObject());
        }

        if (Input.GetMouseButtonDown(1) == true)
        {
            AttemptToSetTileAsObstacle(RaycastToHitGameObject());
        }
    }

    public void AttemptToSelectTile(GameObject gameObjectToSelect)
    {
        TileController cachedTileController = gameObject.GetComponent<TileController>();

        if (cachedTileController != null)
        {
            DeselectCurrentTile();
            SelectNewTile(cachedTileController);
        }
    }

    public void AttemptToSetTileAsObstacle(GameObject gameObject)
    {
        TileController cachedTileController = gameObject.GetComponent<TileController>();

        if (cachedTileController != null)
        {
            cachedTileController.SetTileAsObstacle();
        }
    }

    private void SelectNewTile(TileController tileControllerToSelect)
    {
        CurrentlySelectedTileController = tileControllerToSelect;
        CurrentlySelectedTileController.SetTileSelectState(true);
        MoveSelectorObject();
        SetSelectorObjectVisibilityState(true);
    }

    private void DeselectCurrentTile()
    {
        if (CurrentlySelectedTileController != null)
        {
            CurrentlySelectedTileController.SetTileSelectState(false);
            MoveSelectorObject();
            SetSelectorObjectVisibilityState(false);
        }
    }

    private void SetTileAsObstacle()
    {

    }

    private void MoveSelectorObject()
    {
        SelectorObject.transform.position = CurrentlySelectedTileController.SelectorDestination.transform.position;
        SelectorObject.transform.rotation = CurrentlySelectedTileController.SelectorDestination.transform.rotation;
    }

    private void SetSelectorObjectVisibilityState(bool setVisibility)
    {
        SelectorObject.SetActive(setVisibility);
    }

    private GameObject RaycastToHitGameObject()
    {
        RaycastHit raycastHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out raycastHit, 100f))
        {
            if (raycastHit.transform != null)
            {
                return raycastHit.transform.gameObject;
                //CurrentClickedGameObject(raycastHit.transform.gameObject);
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
