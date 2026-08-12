using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Gameplay.BarIncome
{
    public class BarIncomeView : MonoBehaviour
    {
        [SerializeField] private Slider[] _slider;

        private float _minValue = 0f;

        private void Start()
        {
            if (_slider == null)
                return;

            for (int i = 0; i < _slider.Length; i++)
            {
                _slider[i].value = _minValue;
            }
        }

        public void UpdateBarIncome(int index, float currentValue, float maxValue)
        {
            if (index >= 0 && index < _slider.Length)
            {
                if (_slider[index] != null)
                {
                    _slider[index].maxValue = maxValue;
                    _slider[index].value = currentValue;
                }
            }
        }
    }
}