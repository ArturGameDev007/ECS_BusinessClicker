using _Project.Scripts._Configs;

namespace _Project.Scripts.UI.Gameplay.ButtonUpBaseIncome
{
    public class PriceUpgradePresenter
    {
        private readonly PriceUpgradesView _priceUpgradesView;
        private readonly PriceUpgradesConfig _priceUpgradesConfig;
        
        public PriceUpgradePresenter(PriceUpgradesView priceUpgradesView,  PriceUpgradesConfig priceUpgradesConfig)
        {
            _priceUpgradesView = priceUpgradesView;
            _priceUpgradesConfig = priceUpgradesConfig;
        }

        public void ShowPriceFirstUpgrade()
        {
            if (_priceUpgradesView != null)
            {
                _priceUpgradesView.SetPriceFirstUpgrade(_priceUpgradesConfig.PriceFirstUpgrade);
            }
        }

        public void ShowPriceSecondUpgrade()
        {
            if (_priceUpgradesView != null)
            {
                _priceUpgradesView.SetPriceSecondUpgrade(_priceUpgradesConfig.PriceSecondUpgrade);
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