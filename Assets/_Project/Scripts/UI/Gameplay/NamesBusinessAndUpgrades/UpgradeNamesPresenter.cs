namespace _Project.Scripts.UI.Gameplay.NamesBusinessAndUpgrades
{
    public class UpgradeNamesPresenter
    {
        private readonly UpgradeNamesView _upgradeNamesView;

        public UpgradeNamesPresenter(UpgradeNamesView upgradeNamesView)
        {
            _upgradeNamesView = upgradeNamesView;
        }

        public void ShowUpgradeNames(string[] upgradeNameFirst, string[] upgradeNameSecond)
        {
            _upgradeNamesView?.SetNameUpgrades(upgradeNameFirst, upgradeNameSecond);
        }
    }
}