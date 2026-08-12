using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI.Gameplay.NamesBusinessAndUpgrades
{
    public class BusinessNameView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] _businessNameText;
        
        public void SetBusinessName(string[] businessName)
        {
            if (businessName == null)
                return;

            for (int i = 0; i < _businessNameText.Length && i < businessName.Length; i++)
                _businessNameText[i].text = businessName[i];
        }
    }
}