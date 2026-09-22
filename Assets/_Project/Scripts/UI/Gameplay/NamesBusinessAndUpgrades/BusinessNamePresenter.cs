using _Project.Scripts.Configs;

namespace _Project.Scripts.UI.Gameplay.NamesBusinessAndUpgrades
{
    public class BusinessNamePresenter
    {
        private readonly BusinessNameView _businessNameView;
        private readonly BusinessConfig _businessConfig;

        public BusinessNamePresenter(BusinessNameView businessNameView,  BusinessConfig businessConfig)
        {
            _businessNameView = businessNameView;
            _businessConfig = businessConfig;
        }

        public void ShowBusinessName()
        {
            _businessNameView.SetBusinessName(_businessConfig.GetAllBusinessNames());
        }
    }
}