using Trapeze.IceCreamShop.Models;

namespace Trapeze.IceCreamShop.Validation
{
    public interface IIceCreamPurchaseValidator
    {
        bool IsValidPurchase(IceCreamPurchasedRequest request);

    }
}