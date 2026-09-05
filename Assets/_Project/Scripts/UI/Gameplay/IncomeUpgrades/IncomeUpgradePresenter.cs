using _Project.Scripts.Configs;

namespace _Project.Scripts.UI.Gameplay.IncomeUpgrades
{
    public class IncomeUpgradePresenter
    {
        private readonly IncomeUpgradeView _incomeUpgradeView;
        private readonly IncomeUpgradesConfig _incomeUpgradesConfig;

        public IncomeUpgradePresenter(IncomeUpgradeView incomeUpgradeView, IncomeUpgradesConfig incomeUpgradesConfig)
        {
            _incomeUpgradeView = incomeUpgradeView;
            _incomeUpgradesConfig = incomeUpgradesConfig;
        }

        public void ShowIncomeUpgrades()
        {
            _incomeUpgradeView?.SetFirstIncomeUpgrades(_incomeUpgradesConfig.FirstIncomeUpgrades);
            _incomeUpgradeView?.SetSecondIncomeUpgrades(_incomeUpgradesConfig.SecondIncomeUpgrades);
        }
    }
}