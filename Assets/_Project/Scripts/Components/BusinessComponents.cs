using _Project.Scripts.UI.Gameplay.BarIncome;
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

        public int Level;
        public double BaseIncome;
        
        public double FirstUpgradeIncome;
        public double SecondUpgradeIncome;
        
        public float IncomeDuration;
        public double FinalReward;
    }
}