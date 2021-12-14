using System.Collections.ObjectModel;

namespace Trapeze.IceCreamShop.Models
{
    public class IceCreamPurchasedRequest
    {
        public Person Purchaser { get; set; }

        public decimal AmountPaid { get; set; }

        public string IceCreamBase { get; set; }

        public int NumberOfScoops { get; set; }

        public Collection<string> Flavours { get; set; }

        public decimal OperatingCost { get; set; }

    }
}
