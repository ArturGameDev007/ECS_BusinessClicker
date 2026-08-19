using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Gameplay.ButtonUpBaseIncome
{
    public class ButtonUpIncomeView : MonoBehaviour
    {
        [field: SerializeField] public Button[] ButtonFirstUpgrade { get; private set; }
        [field: SerializeField] public Button[] ButtonSecondUpgrade { get; private set; }
    }
}