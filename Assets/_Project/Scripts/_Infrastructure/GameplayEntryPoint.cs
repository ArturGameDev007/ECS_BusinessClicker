using UnityEngine;

namespace _Project.Scripts._Infrastructure
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private GameplayCompositionRoot _gameplayCompositionRoot;
        
        private EcsManager  _ecsManager;

        private void Awake()
        {
            _ecsManager = _gameplayCompositionRoot.Compose();
        }

        private void Start()
        {
            _ecsManager?.Init();
        }

        private void Update()
        {
            _ecsManager?.Tick();
        }

        private void OnDestroy()
        {
            _ecsManager?.Destroy();
        }
    }
}