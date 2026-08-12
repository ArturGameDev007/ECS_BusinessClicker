using UnityEngine;

namespace _Project.Scripts._Configs
{
    [CreateAssetMenu(fileName = "BarIncome", menuName = "Config/Bar", order = 51)]
    public class BarIncomeConfig : ScriptableObject
    {
        [field: SerializeField] public float[] IncomeDuration { get; private set; }
    }
}