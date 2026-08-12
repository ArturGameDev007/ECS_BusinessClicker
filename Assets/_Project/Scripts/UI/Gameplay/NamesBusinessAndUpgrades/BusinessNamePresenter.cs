namespace _Project.Scripts.UI.Gameplay.NamesBusinessAndUpgrades
{
    public class BusinessNamePresenter
    {
        private readonly BusinessNameView _businessNameView;

        public BusinessNamePresenter(BusinessNameView businessNameView)
        {
            _businessNameView = businessNameView;
        }

        public void ShowBusinessName(string[] names)
        {
            _businessNameView.SetBusinessName(names);
        }
    }
}