using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.Gameplay.ButtonLVLUp
{
    public class PriceView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] _priceText;

        public void SetPrice(double[] price)
        {
            if (_priceText == null)
                return;

            int count = Mathf.Min(_priceText.Length, price.Length);

            for (int i = 0; i < count; i++)
                if (_priceText[i] != null)
                    _priceText[i].text = $"LVL UP\nPrice: {price[i]}$";
        }
    }
}