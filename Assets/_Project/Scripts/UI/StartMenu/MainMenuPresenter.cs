using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.UI.StartMenu
{
    public class MainMenuPresenter: MonoBehaviour
    {
        [SerializeField] private MainMenuView _mainMenuView;

        private void Start()
        {
            if (_mainMenuView == null)
                return;

            _mainMenuView.StartButton.onClick.AddListener(OnStartButtonClick);
            _mainMenuView.ExitButton.onClick.AddListener(OnExitButtonClick);
        }

        private void OnDestroy()
        {
            if (_mainMenuView != null)
            {
                _mainMenuView.StartButton.onClick.RemoveListener(OnStartButtonClick);
                _mainMenuView.ExitButton.onClick.RemoveListener(OnExitButtonClick);
            }
        }

        private void OnStartButtonClick()
        {
            _mainMenuView.gameObject.SetActive(false);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private void OnExitButtonClick()
        {
            Application.Quit();
            
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}