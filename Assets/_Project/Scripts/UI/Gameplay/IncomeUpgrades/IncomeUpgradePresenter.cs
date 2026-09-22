using _Project.Scripts.Configs;

namespace _Project.Scripts.UI.Gameplay.IncomeUpgrades
{
    public class IncomeUpgradePresenter
    {
        private readonly IncomeUpgradeView _incomeUpgradeView;
        private readonly BusinessConfig _businessConfig;

        public IncomeUpgradePresenter(IncomeUpgradeView incomeUpgradeView, BusinessConfig businessConfig)
        {
            _incomeUpgradeView = incomeUpgradeView;
            _businessConfig = businessConfig;
        }

        public void ShowIncomeUpgrades()
        {
            _incomeUpgradeView?.SetFirstIncomeUpgrades(_businessConfig.GetFirstIncomeUpgrades());
            _incomeUpgradeView?.SetSecondIncomeUpgrades(_businessConfig.GetSecondIncomeUpgrades());
        }
    }
}