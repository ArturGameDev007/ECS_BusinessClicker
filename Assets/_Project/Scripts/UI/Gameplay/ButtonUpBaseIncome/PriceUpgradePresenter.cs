using _Project.Scripts.Configs;

namespace _Project.Scripts.UI.Gameplay.ButtonUpBaseIncome
{
    public class PriceUpgradePresenter
    {
        private readonly PriceUpgradesView _priceUpgradesView;
        private readonly BusinessConfig _businessConfig;
        
        public PriceUpgradePresenter(PriceUpgradesView priceUpgradesView,  BusinessConfig businessConfig)
        {
            _priceUpgradesView = priceUpgradesView;
            _businessConfig = businessConfig;
        }
        
        public void ShowPriceFirstUpgrade()
        {
            if (_priceUpgradesView != null)
            {
                _priceUpgradesView.SetPriceFirstUpgrade(_businessConfig.GetAllFirstPriceUpgrades());
            }
        }

        public void ShowPriceSecondUpgrade()
        {
            if (_priceUpgradesView != null)
            {
                _priceUpgradesView.SetPriceSecondUpgrade(_businessConfig.GetAllSecondPriceUpgrades());
            }
        }
        
        public void ShowMessageForFirstUpgrade(int index)
        {
            if (_priceUpgradesView != null)
            {
                _priceUpgradesView?.ChangeMessageForFirstUpgrade(index);
            }
        }
        
        public void ShowMessageForSecondUpgrade(int index)
        {
            if (_priceUpgradesView != null)
            {
                _priceUpgradesView?.ChangeMessageForSecondUpgrade(index);
            }
        }
    }
}