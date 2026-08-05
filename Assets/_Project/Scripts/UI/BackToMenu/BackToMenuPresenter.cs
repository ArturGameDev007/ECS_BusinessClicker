using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.UI.BackToMenu
{
    public class BackToMenuPresenter : MonoBehaviour
    {
        [SerializeField] private BackToMenuView _backToMenuView;

        private void Start()
        {
            if (_backToMenuView == null)
                return;

            _backToMenuView.BackToMenuButton.onClick.AddListener(OnClickButton);
        }

        private void OnDestroy()
        {
            if (_backToMenuView != null)
                _backToMenuView.BackToMenuButton.onClick.RemoveListener(OnClickButton);
        }

        private void OnClickButton()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}