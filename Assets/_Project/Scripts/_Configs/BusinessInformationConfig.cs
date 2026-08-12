using UnityEngine;

namespace _Project.Scripts._Configs
{
    [CreateAssetMenu(fileName = "BasicInformation", menuName = "Config/BasicInformation", order = 51)]
    public class BusinessInformationConfig : ScriptableObject
    {
        [field: SerializeField] public int[] Level { get; private set; }
        [field: SerializeField] public double[] BasicIncome { get; private set; }
        // [field: SerializeField] public float[] BasicIncome { get; private set; }
    }
}