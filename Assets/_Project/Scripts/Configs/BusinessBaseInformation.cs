using System;
using UnityEngine;

namespace _Project.Scripts.Configs
{
    [Serializable]
    public class BusinessBaseInformation
    {
        [field: SerializeField] public int ID { get;  private set; }
        [field: SerializeField] public string Name { get;  private set; }
        [field: SerializeField] public float IncomeDuration { get; private set; }
        [field: SerializeField] public double PriceLevelUp { get; private set; }
        [field: SerializeField] public UpgradeInformation[] Upgrade { get; private set; }
    }
}