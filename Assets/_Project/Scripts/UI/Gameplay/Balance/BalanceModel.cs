namespace _Project.Scripts.UI.Gameplay.Balance
{
    public class BalanceModel
    {
        public float Amount { get; set; }

        // private float value = 3f;

        public BalanceModel(float amount)
        {
            Amount = amount;
        }

        // public void AddAmount()
        // {
        //     Amount += value;
        // }
        //
        
        public void AddAmountIncome(float value)
        {
            Amount += value;
        }
    }
}