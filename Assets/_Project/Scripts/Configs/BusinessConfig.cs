using System;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts.Configs
{
    [CreateAssetMenu(fileName = "BusinessesConfig", menuName = "Config/BusinessesConfig", order = 51)]
    public class BusinessConfig : ScriptableObject
    {
        [field: SerializeField] public BusinessBaseInformation[] BaseInformation { get; private set; }

        public BusinessBaseInformation GetById(int id)
        {
            foreach (var business in BaseInformation)
            {
                if (business.ID == id)
                {
                    return business;
                }
            }
            
            throw new ArgumentOutOfRangeException(nameof(id), id, "ID not found");
        }

        public string[] GetAllBusinessNames()
        {
            var name = BaseInformation.Select(b => b.Name).ToArray();
            return name;
        }

        public double[] GetPriceLevelUp()
        {
            var level = BaseInformation.Select(b => b.PriceLevelUp).ToArray();
            return level;
        }

        public string[] GetAllFirstUpgradeNames()
        {
            var name = BaseInformation.Select(b => b.Upgrade[0].Name).ToArray();
            return name;
        }
        
        public string[] GetAllSecondUpgradeNames()
        {
            var name = BaseInformation.Select(b => b.Upgrade[1].Name).ToArray();
            return name;
        }

        public int[] GetFirstIncomeUpgrades()
        {
            var income = BaseInformation.Select(b => b.Upgrade[0].Income).ToArray();
            return income;
        }
        
        public int[] GetSecondIncomeUpgrades()
        {
            var income = BaseInformation.Select(b => b.Upgrade[1].Income).ToArray();
            return income;
        }

        public double[] GetAllFirstPriceUpgrades()
        {
            var price = BaseInformation.Select(b => b.Upgrade[0].Price).ToArray();
            return price;
        }
        
        public double[] GetAllSecondPriceUpgrades()
        {
            var price = BaseInformation.Select(b => b.Upgrade[1].Price).ToArray();
            return price;
        }
    }
}