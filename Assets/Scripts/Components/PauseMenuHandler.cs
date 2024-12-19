using UnityEngine;

namespace Components
{
    public class PauseMenuHandler : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;

        private bool _isMenuShown;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_isMenuShown)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Time.timeScale = 1.0f;
                    pauseMenu.SetActive(false);
                    _isMenuShown = false;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Confined;
                    Time.timeScale = 0.0f;
                    pauseMenu.SetActive(true);
                    _isMenuShown = true;
                }
            }
        }

        public void OnContinueButtonClick()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);
            _isMenuShown = false;
        }
        
        public void OnExitButtonClick()
        {
            Application.Quit();
        }
    }
}