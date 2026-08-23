using UnityEngine;

namespace _Project.Scripts.Components
{
    public struct BusinessComponents
    {
        public int ID;
        
        [Header("Slider Settings")]
        public float CurrentValueSlider;
        public float MaxValueSlider;
        public float CurrentTime;
        public float CountSliderStep;

        [Header("Basic Settings")]
        public int Level;
        public double BaseIncome;
        
        public double FirstUpgradeIncome;
        public double SecondUpgradeIncome;
        
        public float IncomeDuration;
        public double FinalReward;
    }
}