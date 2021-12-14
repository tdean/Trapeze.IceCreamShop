using Trapeze.IceCreamShop.Models;

namespace Trapeze.IceCreamShop.Services.Validation
{
    public interface IIceCreamPurchaseValidator
    {
        bool IsValidPurchase(IceCreamPurchasedRequest request);

        bool IsValidCost(decimal operatingCost, decimal purchaseAmount);

    }
}