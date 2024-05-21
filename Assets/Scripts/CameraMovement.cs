using Logic;
using UnityEngine;

namespace Player
{
    public class CameraMovement : MonoBehaviour
    {
        [field: SerializeField]
        private float ZoomSensitivity { get; set; } = 5.0f;
        [field: SerializeField]
        private Camera MainCamera { get; set; }

        private Vector3 CachedCameraStartPosition { get; set; }
        private Vector3 MouseClickStartPosition { get; set; }

        private const string MOUSE_WHEEL_AXIS_NAME = "Mouse ScrollWheel";

        private void Awake()
        {
            CachedCameraStartPosition = MainCamera.transform.position;
        }

        private void OnEnable()
        {
            AttachToEvents();
        }

        private void OnDisable()
        {
            DetachFromEvents();
        }

        private void Update()
        {
            AttemptToCacheMousePosition();
            AttemptToChangeCameraPosition();
            ZoomCamera();
        }

        private void AttemptToCacheMousePosition()
        {
            if (Input.GetMouseButtonDown(0) == true)
            {
                MouseClickStartPosition = GetWorldPosition();
            }
        }

        private void AttemptToChangeCameraPosition()
        {
            if (Input.GetMouseButton(0) == true)
            {
                Vector3 direction = MouseClickStartPosition - GetWorldPosition();
                MainCamera.transform.position += direction;
            }
        }

        private void ZoomCamera()
        {
            float zoomValue = MainCamera.transform.position.y + Input.GetAxis(MOUSE_WHEEL_AXIS_NAME) * ZoomSensitivity * -1.0f;
            MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, zoomValue, MainCamera.transform.position.z);
        }

        private void ResetCameraPosition()
        {
            MainCamera.transform.position = CachedCameraStartPosition;
        }

        private Vector3 GetWorldPosition()
        {
            float distanceValue;
            Ray mousePositionRay = MainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(MainCamera.transform.forward, new Vector3(0, 0, 0));

            groundPlane.Raycast(mousePositionRay, out distanceValue);

            return mousePositionRay.GetPoint(distanceValue);
        }

        private void HandleOnCameraResetRequested()
        {
            ResetCameraPosition();
        }

        private void AttachToEvents()
        {
            GameActionNotifier.OnCameraResetRequested += HandleOnCameraResetRequested;
        }

        private void DetachFromEvents()
        {
            GameActionNotifier.OnCameraResetRequested -= HandleOnCameraResetRequested;
        }
    }
}
