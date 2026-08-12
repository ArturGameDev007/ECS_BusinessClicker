using UnityEngine;

namespace _Project.Scripts._Configs
{
    [CreateAssetMenu(fileName = "UpgradeNames", menuName = "Config/UpgradeNames", order = 51)]
    public class UpgradeNamesConfig : ScriptableObject
    {
        [field: SerializeField] public string[] FirstUpgradeName { get; private set; }
        [field: SerializeField] public string[] SecondUpgradeName { get; private set; }
    }
}