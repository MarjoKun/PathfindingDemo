using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainUIView : View
    {
        [field: SerializeField]
        public Button SpawnMapButton { get; private set; }
        [field: SerializeField]
        public Button ResetCameraButton { get; private set; }
        [field: SerializeField]
        public InputField MapSizeInputField { get; private set; }
        [field: SerializeField]
        public Toggle NoneToggle { get; private set; }
        [field: SerializeField]
        public Toggle StartToggle { get; private set; }
        [field: SerializeField]
        public Toggle EndToggle { get; private set; }
        [field: SerializeField]
        public Button FindPathButton { get; private set; }
        [field: SerializeField]
        public Button MovePlayerButton { get; private set; }
        [field: SerializeField]
        public Button InstructionButton { get; private set; }
        [field: SerializeField]
        public Button CloseInstructionButton { get; private set; }

        [field: SerializeField]
        private GameObject InstructionPanel { get; set; }

        public void SetInstructionPopupVisibility(bool setVisible)
        {
            InstructionPanel.SetActive(setVisible);
        }

        private void Awake()
        {
            SetInstructionPopupVisibility(false);
        }
    }
}