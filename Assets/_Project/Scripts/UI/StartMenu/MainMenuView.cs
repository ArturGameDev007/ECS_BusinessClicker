using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.StartMenu
{
    public class MainMenuView : MonoBehaviour
    {
        [field: SerializeField] public Button StartButton { get; private set; }
        [field: SerializeField] public Button ExitButton { get; private set; }
    }
}