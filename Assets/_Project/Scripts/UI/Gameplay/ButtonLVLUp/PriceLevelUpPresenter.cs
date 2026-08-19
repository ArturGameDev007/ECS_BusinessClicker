using _Project.Scripts._Configs;

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
            // int index = 0;
            //
            // foreach (var price in _priceLevelUpConfig.Price)
            // {
            //     if (_priceView != null)
            //     {
            //         _priceView?.SetPrice(index, price);
            //         index++;
            //     }
            // }

            _priceView?.SetPrice(_priceLevelUpConfig.Price);

            // for (int i = 0; i < _priceLevelUpConfig.Price.Length; i++)
            // {
            //     if (_priceView != null)
            //     {
            //         int temp = _priceLevelUpConfig.Price[i];
            //         _priceView?.SetPrice(_priceLevelUpConfig.Price[]);
            //     }
            // }
            //
            // foreach (var price in _priceLevelUpConfig.Price)
            // {
            //     if (_priceView)
            //     {
            //         _priceView?.SetPrice(price);
            //     }
            // }
        }
    }
}