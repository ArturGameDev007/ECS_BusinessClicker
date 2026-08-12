namespace _Project.Scripts.UI.Gameplay.Balance
{
    public class BalancePresenter
    {
        private readonly BalanceView _balanceView;
        private readonly BalanceModel _balanceModel;

        public BalancePresenter(BalanceView balanceView,  BalanceModel balanceModel)
        {
            _balanceView = balanceView;
            _balanceModel = balanceModel;
        }
        
        public void UpdateBalancePlayer(double amount)
        {
            _balanceView.SetBalanceText(amount);
        }
    }
}