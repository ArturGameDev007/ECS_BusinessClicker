using UnityEngine;

namespace _Project.Scripts.Configs
{
    [CreateAssetMenu(fileName = "PriceLVLUp", menuName = "Config/LVLUp", order = 51)]
    public class PriceLevelUpConfig : ScriptableObject
    {
        [field: SerializeField] public double[] Price { get; private set; }
    }
}