using UnityEngine;

namespace _Project.Scripts._Infrastructure
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private GameplayCompositionRoot _gameplayCompositionRoot;
        
        private GameManager  _gameManager;

        private void Awake()
        {
            _gameManager = _gameplayCompositionRoot.Compose();
        }

        private void Start()
        {
            _gameManager?.Init();
        }

        private void Update()
        {
            _gameManager?.Tick();
        }

        private void OnDestroy()
        {
            _gameManager?.Destroy();
        }
    }
}