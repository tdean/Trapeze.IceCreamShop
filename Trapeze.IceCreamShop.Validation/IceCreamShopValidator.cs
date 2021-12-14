using System;
using System.Collections.Generic;
using Trapeze.IceCreamShop.Enums;
using Trapeze.IceCreamShop.Models;

namespace Trapeze.IceCreamShop.Validation
{
    public class IceCreamShopValidator : IIceCreamPurchaseValidator
    {
        public bool IsValidPurchase(IceCreamPurchasedRequest request)
        {
            if (request != null)
            {
                var validBase = IsValidIceCreamBase(request.IceCreamBase);
                var validFlavour = IsValidIceCreamFlavour(request.Flavours);

                return validBase && validFlavour;
            }

            return false;
        }

        private static bool IsValidIceCreamBase(string iceCreamBase)
        {
            return Enum.IsDefined(typeof(IceCreamBase), iceCreamBase);
        }

        private static bool IsValidIceCreamFlavour(ICollection<string> flavours)
        {
            foreach (var f in flavours)
            {
                if (!Enum.IsDefined(typeof(IceCreamFlavour), f))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
