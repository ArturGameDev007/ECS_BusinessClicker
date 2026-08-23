using System;
using System.Collections.Generic;

namespace _Project.Scripts._Services.Save
{
    [Serializable]
    public class PlayerSaveData
    {
        public double Balance;

        public List<BusinessSaveData> BusinessSave = new();

        public PlayerSaveData(double balance)
        {
            Balance = balance;
        }
    }
}