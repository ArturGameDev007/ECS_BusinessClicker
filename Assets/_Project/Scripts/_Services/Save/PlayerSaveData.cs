using System;

namespace _Project.Scripts._Services.Save
{
    [Serializable]
    public class PlayerSaveData
    {
        public float Balance { get; private set; }
        // public BusinessSaveData[] BusinessSaveData;

        public PlayerSaveData(float balance)
        {
            Balance = balance;
        }
    }
}