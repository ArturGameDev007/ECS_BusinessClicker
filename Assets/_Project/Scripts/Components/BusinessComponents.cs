using _Project.Scripts.UI.Gameplay.BarIncome;

namespace _Project.Scripts.Components
{
    public struct BusinessComponents
    {
        public int ID;
        
        public float CurrentValueSlider;
        public float MaxValueSlider;
        public float CurrentTime;

        public int Level;
        
        // public float BaseIncome;
        public double BaseIncome;
        public double FirstUpgradeIncome;
        public double SecondUpgradeIncome;
        
        public float CountSliderStep;
        public float IncomeDuration;
        public double FinalReward;
        // public float ValueAmountBalance;
    }
}