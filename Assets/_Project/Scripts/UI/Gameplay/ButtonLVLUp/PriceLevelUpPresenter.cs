using _Project.Scripts.Configs;

namespace _Project.Scripts.UI.Gameplay.ButtonLVLUp
{
    public class PriceLevelUpPresenter
    {
        private readonly PriceView _priceView;
        private readonly BusinessConfig _businessConfig;
        
        private double _priceLevelUp;

        public PriceLevelUpPresenter(PriceView priceView, BusinessConfig businessConfig)
        {
            _priceView = priceView;
            _businessConfig = businessConfig;
        }

        public void ShowPriceLevelUp()
        {
            _priceView?.SetPrice(_businessConfig.GetPriceLevelUp());
        }
    }
}