using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI.Gameplay.IncomeUpgrades
{
    public class IncomeUpgradeView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] _firstIncomeUpgrade;
        [SerializeField] private TextMeshProUGUI[] _secondIncomeUpgrade;

        public void SetFirstIncomeUpgrades(int[] firstIncomeUpgrades)
        {
            if (_firstIncomeUpgrade == null)
                return;

            for (int i = 0; i < _firstIncomeUpgrade.Length; i++)
            {
                _firstIncomeUpgrade[i].text = $"Income: + {firstIncomeUpgrades[i]}%";
            }
        }

        public void SetSecondIncomeUpgrades(int[] secondIncomeUpgrades)
        {
            if (_secondIncomeUpgrade == null)
                return;

            for (int i = 0; i < _secondIncomeUpgrade.Length; i++)
            {
                _secondIncomeUpgrade[i].text = $"Income: + {secondIncomeUpgrades[i]}%";
            }
        }
    }
}