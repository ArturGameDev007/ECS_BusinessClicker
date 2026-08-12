using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI.Gameplay.ButtonLVLUp
{
    public class PriceView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] _priceText;

        public void SetPrice(int[] price)
        {
            if (_priceText == null) 
                return;

            for (int i = 0; i < _priceText.Length; i++)
            {
                _priceText[i].text = $"LVL UP\nPrice: {price[i]}$";
            }
        }
    }
}