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
    }
}