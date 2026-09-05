using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI.Gameplay.BusinessModel
{
    public class BusinessModelView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] _levelText;
        [SerializeField] private TextMeshProUGUI[] _incomeText;

        public void SetLevel(int index, int level)
        {
            if (_levelText == null || index < 0 || index >= _levelText.Length)
                return;

            _levelText[index].text = $"LVL\n{level}";
        }

        public void SetIncome(int index, double income)
        {
            if (_incomeText == null ||  index < 0 || index >= _incomeText.Length)
                return;

            _incomeText[index].text = $"Income\n{income}$";
        }
    }
}