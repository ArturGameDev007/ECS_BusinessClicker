using UnityEngine;

namespace _Project.Scripts._Configs
{
    [CreateAssetMenu(fileName = "NamesBusiness", menuName = "Config/NamesBusiness", order = 51)]
    public class BusinessNamesConfig : ScriptableObject
    {
        [field: SerializeField] public string[] Names { get; private set; }
        
    }
}