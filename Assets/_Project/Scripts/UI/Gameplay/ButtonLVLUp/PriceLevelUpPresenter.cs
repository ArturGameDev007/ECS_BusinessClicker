using _Project.Scripts.Configs;

namespace _Project.Scripts.UI.Gameplay.ButtonLVLUp
{
    public class PriceLevelUpPresenter
    {
        private readonly PriceView _priceView;
        private readonly PriceLevelUpConfig _priceLevelUpConfig;

        public PriceLevelUpPresenter(PriceView priceView, PriceLevelUpConfig priceLevelUpConfig)
        {
            _priceView = priceView;
            _priceLevelUpConfig = priceLevelUpConfig;
        }

        public void ShowPriceLevelUp()
        {
            _priceView?.SetPrice(_priceLevelUpConfig.Price);
        }
    }
}