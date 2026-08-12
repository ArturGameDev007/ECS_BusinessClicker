using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI.Gameplay.Balance
{
    public class BalanceView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _balanceText;
        
        public void SetBalanceText(double text)
        {
            _balanceText.text = $"Balance: {text}$";
        }
    }
}