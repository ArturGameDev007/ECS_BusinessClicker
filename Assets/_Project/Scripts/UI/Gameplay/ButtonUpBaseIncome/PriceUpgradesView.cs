using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI.Gameplay.ButtonUpBaseIncome
{
    public class PriceUpgradesView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] _firstUpgrade;
        [SerializeField] private TextMeshProUGUI[] _secondUpgrade;

        public void SetPriceFirstUpgrade(double[] price)
        {
            if (_firstUpgrade == null)
                return;

            for (int i = 0; i < _firstUpgrade.Length; i++)
            {
                _firstUpgrade[i].text = $"Price: {price[i]}$";
            }
        }

        public void SetPriceSecondUpgrade(double[] price)
        {
            if (_secondUpgrade == null)
                return;
            
            for (int i = 0; i < _secondUpgrade.Length; i++)
            {
                _secondUpgrade[i].text = $"Price: {price[i]}$";
            }
        }

        public void ChangeMessageForFirstUpgrade(int index)
        {
            if (_firstUpgrade == null)
                return;
            
            if (index >= 0 && index < _firstUpgrade.Length && _firstUpgrade[index] != null)
                _firstUpgrade[index].text = "Purchased";
        }

        public void ChangeMessageForSecondUpgrade(int index)
        {
            if (_secondUpgrade == null)
                return;

            if (index >= 0 && index < _secondUpgrade.Length && _secondUpgrade[index] != null)
                _secondUpgrade[index].text = "Purchased";
        }
    }
}