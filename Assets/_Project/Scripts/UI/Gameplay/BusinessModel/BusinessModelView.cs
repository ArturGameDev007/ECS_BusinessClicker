using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI.Gameplay.BusinessModel
{
    public class BusinessModelView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] _levelText;
        [SerializeField] private TextMeshProUGUI[] _incomeText;

        public void SetLevel(int[] level)
        {
            if (_levelText ==  null)
                return;

            for (int i = 0; i < _levelText.Length; i++)
            {
                _levelText[i].text = $"LVL\n{level[i]}";
            }
        }

        public void SetIncome(double[] income)
        {
            if (_incomeText ==  null)
                return;
            
            for (int i = 0; i < _incomeText.Length; i++)
            {
                _incomeText[i].text = $"Income\n{income[i]}$";
            }
        }
    }
}