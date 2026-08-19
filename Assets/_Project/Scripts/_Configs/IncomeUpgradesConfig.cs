using TMPro;
using UnityEngine;

namespace _Project.Scripts._Configs
{
    [CreateAssetMenu(fileName = "IncomeUpgrade", menuName = "Config/IncomeUpgrade", order = 51)]
    public class IncomeUpgradesConfig : ScriptableObject
    {
        [field: SerializeField] public int[] FirstIncomeUpgrades { get; private set; }
        [field: SerializeField] public int[] SecondIncomeUpgrades { get; private set; }
    }
}