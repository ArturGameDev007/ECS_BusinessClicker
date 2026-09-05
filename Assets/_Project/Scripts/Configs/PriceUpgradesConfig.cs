using UnityEngine;

namespace _Project.Scripts.Configs
{
    [CreateAssetMenu(fileName = "ButtonPriceUpgrades", menuName = "Config/ButtonPriceUpgrades", order = 51)]
    public class PriceUpgradesConfig : ScriptableObject
    {
        [field: Header("Price Upgrades")]
        
        [field: SerializeField] public double[] PriceFirstUpgrade { get;  private set; }
        [field: SerializeField] public double[] PriceSecondUpgrade { get;  private set; }
        
        [field: Header("Percent Income")]
        
        [field:SerializeField] public double[] PercentFirstUpgrade { get;  private set; }
        [field:SerializeField] public double[] PercentSecondUpgrade { get;  private set; }
    }
}