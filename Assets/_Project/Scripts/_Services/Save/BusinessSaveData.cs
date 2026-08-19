using System;

namespace _Project.Scripts._Services.Save
{
    [Serializable]
    public class BusinessSaveData
    {
        public int ID;
        public int Level;
        public double CurrentIncome;
        public double FirstUpgradeIncome;
        public double SecondUpgradeIncome;
    }
}