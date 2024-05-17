using UI;
using UnityEngine;

public class MainUIModel : Model<MainUIView>
{
    private void OnEnable()
    {
        AttachToEvents();
    }

    private void OnDisable()
    {
        DetachFromEvents();
    }

    private void HandleOnSpawnMapButtonClicked()
    {
        int mapSize;

        if (int.TryParse(CurrentView.MapSizeInputField.text, out mapSize) == true)
        {
            GameActionNotifier.NotifyOnSpawnMapRequested(mapSize);
        }
        else
        {
            Debug.Log($"Couldn't parse map size value: {CurrentView.MapSizeInputField.text}");
        }
    }

    private void HandleOnResetCameraButtonClicked()
    {
        GameActionNotifier.NotifyOnCameraResetRequested();
    }

    private void AttachToEvents()
    {
        CurrentView.SpawnMapButton.onClick.AddListener(HandleOnSpawnMapButtonClicked);
        CurrentView.ResetCameraButton.onClick.AddListener(HandleOnResetCameraButtonClicked);
    }

    private void DetachFromEvents()
    {
        if (CurrentView != null)
        {
            CurrentView.SpawnMapButton.onClick.RemoveListener(HandleOnSpawnMapButtonClicked);
            CurrentView.ResetCameraButton.onClick.RemoveListener(HandleOnResetCameraButtonClicked);
        }
    }
}

