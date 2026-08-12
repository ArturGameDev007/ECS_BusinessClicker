namespace _Project.Scripts.UI.Gameplay.BarIncome
{
    public class BarIncomePresenter
    {
        private readonly BarIncomeView _barIncomeView;

        public BarIncomePresenter(BarIncomeView barIncomeView)
        {
            _barIncomeView = barIncomeView;
        }

        public void RefreshBarSlider(int index, float currentValue,  float maxValue)
        {
            _barIncomeView?.UpdateBarIncome(index, currentValue, maxValue);
        }
    }
}