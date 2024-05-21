using UnityEngine;

namespace Utilities
{
    public class ExitGame : MonoBehaviour
    {
        private void Update()
        {
            AttemptToCloseApplication();
        }

        private void AttemptToCloseApplication()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
