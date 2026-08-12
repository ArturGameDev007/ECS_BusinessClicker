using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI.Gameplay.NamesBusinessAndUpgrades
{
    public class UpgradeNamesView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] _firstUpgrade;
        [SerializeField] private TextMeshProUGUI[] _secondUpgrade;
        
        public void SetNameUpgrades(string[] upgradeNameFirst, string[] upgradeNameSecond)
        {
            if (_firstUpgrade == null || _secondUpgrade == null)
                return;

            for (int i = 0; i < _firstUpgrade.Length && i < _secondUpgrade.Length; i++)
            {
                _firstUpgrade[i].text = upgradeNameFirst[i];
                _secondUpgrade[i].text = upgradeNameSecond[i];
            }
        }
    }
}