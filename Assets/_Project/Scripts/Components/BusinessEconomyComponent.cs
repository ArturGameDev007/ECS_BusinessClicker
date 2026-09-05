using System;

namespace _Project.Scripts.Components
{
    public struct BusinessEconomyComponent
    {
        public Action<int> OnBuyLevelSuccess;
        public Action<int> OnBuyFirstUpgradeSuccess;
        public Action<int> OnBuySecondUpgradeSuccess;
        
        public int ID;

        public int Level;
        public double BaseIncome;
        public double FirstUpgradeIncome;
        public double SecondUpgradeIncome;
        public double FinalReward;
    }
}