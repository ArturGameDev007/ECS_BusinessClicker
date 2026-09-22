using System;
using UnityEngine;

namespace _Project.Scripts.Configs
{
    [Serializable]
    public class UpgradeInformation
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public double Price { get; private set; }
        [field: SerializeField] public double Percent { get; private set; }
        [field: SerializeField] public int Income { get; private set; }
    }
}