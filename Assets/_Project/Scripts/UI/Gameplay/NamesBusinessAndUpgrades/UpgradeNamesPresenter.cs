using _Project.Scripts.Configs;

namespace _Project.Scripts.UI.Gameplay.NamesBusinessAndUpgrades
{
    public class UpgradeNamesPresenter
    {
        private readonly UpgradeNamesView _upgradeNamesView;
        private readonly BusinessConfig _businessConfig;

        public UpgradeNamesPresenter(UpgradeNamesView upgradeNamesView,  BusinessConfig businessConfig)
        {
            _upgradeNamesView = upgradeNamesView;
            _businessConfig = businessConfig;
        }

        public void ShowUpgradeNames()
        {
            _upgradeNamesView?.SetNameUpgrades(_businessConfig.GetAllFirstUpgradeNames(), _businessConfig.GetAllSecondUpgradeNames());
        }
    }
}