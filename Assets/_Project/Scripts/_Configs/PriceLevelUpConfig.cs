using UnityEngine;

namespace _Project.Scripts._Configs
{
    [CreateAssetMenu(fileName = "PriceLVLUp", menuName = "Config/LVLUp", order = 51)]
    public class PriceLevelUpConfig : ScriptableObject
    {
        [field: SerializeField] public int[] Price { get; private set; }
    }
}