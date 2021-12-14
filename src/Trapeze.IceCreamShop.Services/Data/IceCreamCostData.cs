namespace Trapeze.IceCreamShop.Services.Data
{
    using System.Collections.Generic;
    using Trapeze.IceCreamShop.Enums;

    public static class IceCreamCostData
    {
        public static decimal GetIceCreamBaseCost(IceCreamBase iceCreamBase)
        {
            var baseData = IceCreamBaseData();
            return baseData[iceCreamBase];
        }

        public static decimal GetIceCreamScoopCost(int numberOfScoops)
        {
            var scoopData = IceCreamScoopCostData();
            return scoopData[numberOfScoops];
        }

        private static Dictionary<IceCreamBase, decimal> IceCreamBaseData()
        {
            return new Dictionary<IceCreamBase, decimal>()
            {
                { IceCreamBase.CakeCone, 3 },
                { IceCreamBase.Cup, 3 },
                { IceCreamBase.SugarCone, 3 },
                { IceCreamBase.WaffleCone, 4 }
            };
        }

        private static Dictionary<int, decimal> IceCreamScoopCostData()
        {
            return new Dictionary<int, decimal>()
            {
                { 1, 2m },
                { 2, 3m },
                { 3, 3.50m },
                { 4, 3.80m }
            };
        }
    }
}
