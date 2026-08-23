using System;
using UnityEngine;

namespace _Project.Scripts._Services.Save
{
    [Serializable]
    public class BusinessSaveData
    {
        [field: SerializeField] public int ID { get; private set; }
        [field: SerializeField] public int Level { get; private set; }
        [field: SerializeField] public double CurrentIncome { get; private set; }
        [field: SerializeField] public double FirstUpgradeIncome { get; private set; }
        [field: SerializeField] public double SecondUpgradeIncome { get; private set; }

        public BusinessSaveData(int id, int level, double currentIncome, double firstUpgradeIncome, double secondUpgradeIncome)
        {
            ID = id;
            Level = level;
            CurrentIncome = currentIncome;
            FirstUpgradeIncome = firstUpgradeIncome;
            SecondUpgradeIncome = secondUpgradeIncome;
        }
    }
}