using _Project.Scripts._Configs;

namespace _Project.Scripts.UI.Gameplay.BusinessModel
{
    public class BusinessInformationPresenter
    {
        private readonly BusinessModelView  _businessModelView;
        private readonly BusinessInformationConfig _businessInformationConfig;

        public BusinessInformationPresenter(BusinessModelView businessModelView, BusinessInformationConfig businessInformationConfig)
        {
            _businessModelView = businessModelView;
            _businessInformationConfig = businessInformationConfig;
        }

        public void RefreshData()
        {
            _businessModelView?.SetLevel(_businessInformationConfig.Level);
            _businessModelView?.SetIncome(_businessInformationConfig.BasicIncome);
        }
    }
}