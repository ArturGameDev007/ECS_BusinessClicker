using System;

namespace _Project.Scripts.Components
{
    public struct PlayerBalanceComponent
    {
        public Action<double> OnBalanceChanged;
        
        public double Amount;
    }
}