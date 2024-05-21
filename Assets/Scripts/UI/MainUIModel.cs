using System;
using UnityEngine;

namespace UI
{
    public class MainUIModel : Model<MainUIView>
    {
        public event Action OnNoneToggleValueChanged = delegate { };
        public event Action OnStartToggleValueChanged = delegate { };
        public event Action OnEndToggleValueChanged = delegate { };
        public event Action OnFindPathButtonClicked = delegate { };
        public event Action OnMovePlayerButtonClicked = delegate { };

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
                if (mapSize > 0)
                {
                    GameActionNotifier.NotifyOnSpawnMapRequested(mapSize);
                }
                else
                {
                    Debug.Log($"Map size value cannot be negative!");
                }
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


        private void HandleOnNoneToggleValueChanged(bool toggleValue)
        {
            if (toggleValue == true)
            {
                OnNoneToggleValueChanged.Invoke();
            }
        }

        private void HandleOnStartToggleValueChanged(bool toggleValue)
        {
            if (toggleValue == true)
            {
                OnStartToggleValueChanged.Invoke();
            }
        }

        private void HandleOnEndToggleValueChanged(bool toggleValue)
        {
            if (toggleValue == true)
            {
                OnEndToggleValueChanged.Invoke();
            }
        }

        private void HandleOnFindPathButtonClicked()
        {
            OnFindPathButtonClicked.Invoke();
        }

        private void HandleOnMovePlayerButtonClicked()
        {
            OnMovePlayerButtonClicked.Invoke();
        }

        private void HandleOnInstructionButtonClicked()
        {
            CurrentView.SetInstructionPopupVisibility(true);
        }

        private void HandleOnCloseInstructionButtonClicked()
        {
            CurrentView.SetInstructionPopupVisibility(false);
        }

        private void AttachToEvents()
        {
            CurrentView.SpawnMapButton.onClick.AddListener(HandleOnSpawnMapButtonClicked);
            CurrentView.ResetCameraButton.onClick.AddListener(HandleOnResetCameraButtonClicked);
            CurrentView.NoneToggle.onValueChanged.AddListener(HandleOnNoneToggleValueChanged);
            CurrentView.StartToggle.onValueChanged.AddListener(HandleOnStartToggleValueChanged);
            CurrentView.EndToggle.onValueChanged.AddListener(HandleOnEndToggleValueChanged);
            CurrentView.FindPathButton.onClick.AddListener(HandleOnFindPathButtonClicked);
            CurrentView.MovePlayerButton.onClick.AddListener(HandleOnMovePlayerButtonClicked);
            CurrentView.InstructionButton.onClick.AddListener(HandleOnInstructionButtonClicked);
            CurrentView.CloseInstructionButton.onClick.AddListener(HandleOnCloseInstructionButtonClicked);
        }

        private void DetachFromEvents()
        {
            if (CurrentView != null)
            {
                CurrentView.SpawnMapButton.onClick.RemoveListener(HandleOnSpawnMapButtonClicked);
                CurrentView.ResetCameraButton.onClick.RemoveListener(HandleOnResetCameraButtonClicked);
                CurrentView.NoneToggle.onValueChanged.RemoveListener(HandleOnNoneToggleValueChanged);
                CurrentView.StartToggle.onValueChanged.RemoveListener(HandleOnStartToggleValueChanged);
                CurrentView.EndToggle.onValueChanged.RemoveListener(HandleOnEndToggleValueChanged);
                CurrentView.FindPathButton.onClick.AddListener(HandleOnFindPathButtonClicked);
                CurrentView.MovePlayerButton.onClick.AddListener(HandleOnMovePlayerButtonClicked);
                CurrentView.InstructionButton.onClick.RemoveListener(HandleOnInstructionButtonClicked);
                CurrentView.CloseInstructionButton.onClick.RemoveListener(HandleOnCloseInstructionButtonClicked);
            }
        }
    }
}

