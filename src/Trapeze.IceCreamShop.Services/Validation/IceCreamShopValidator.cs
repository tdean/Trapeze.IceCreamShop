namespace Trapeze.IceCreamShop.Services.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Trapeze.IceCreamShop.Enums;
    using Trapeze.IceCreamShop.Models;

    public class IceCreamShopValidator : IIceCreamPurchaseValidator
    {
        public bool IsValidCost(decimal operatingCost, decimal purchaseAmount)
        {
            return purchaseAmount >= operatingCost;
        }

        public bool IsValidPurchase(IceCreamPurchasedRequest request)
        {
            if (request != null)
            {
                var validFlavourAndBase = IsValidFlavourAndBase(request);
                var validNumberOfScoops = IsValidNumberOfScoops(request.IceCreamBase, request.NumberOfScoops);

                return validFlavourAndBase && validNumberOfScoops;
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

        private static bool IsValidNumberOfScoops(string iceCreamBase, int numberOfScoops)
        {
            var baseType = (IceCreamBase)Enum.Parse(typeof(IceCreamBase), iceCreamBase);
            var maxNumberOfScoops = 4;

            if (baseType != IceCreamBase.Cup)
            {
                return numberOfScoops < maxNumberOfScoops;
            }

            return numberOfScoops <= maxNumberOfScoops;
        }

        private static bool IsValidFlavourAndBase(IceCreamPurchasedRequest request)
        {
            var validBase = IsValidIceCreamBase(request.IceCreamBase);
            var validFlavour = IsValidIceCreamFlavour(request.Flavours);

            if (validBase && validFlavour)
            {
                var hasCookieDoughAndSugarConeBase = IsCookieDoughFlavourInSugarCodeBase(request.IceCreamBase, request.Flavours);
                var hasStrawberryAndMintFlavours = IsStrawberryAndMintFlavours(request.Flavours);
                var hasCookiesAndCreamAndMooseTracksAndVanillFlavours = IsCookiesAndCreamAndMooseTracksAndVanilla(request.Flavours);

                return !hasCookieDoughAndSugarConeBase && !hasStrawberryAndMintFlavours && !hasCookiesAndCreamAndMooseTracksAndVanillFlavours;
            }

            return false;
        }

        private static bool IsCookieDoughFlavourInSugarCodeBase(string iceCreamBase, Collection<string> flavours)
        {
            var baseType = (IceCreamBase)Enum.Parse(typeof(IceCreamBase), iceCreamBase);
            var hasSugarConeBase = baseType == IceCreamBase.SugarCone;
            var hasCookeDoughFlavour = false;

            if (!hasSugarConeBase)
            {
                return false;
            }

            foreach (var f in flavours)
            {
                var flavour = (IceCreamFlavour)Enum.Parse(typeof(IceCreamFlavour), f);
                if (flavour == IceCreamFlavour.CookieDough)
                {
                    hasCookeDoughFlavour = true;
                }
            }

            return hasSugarConeBase && hasCookeDoughFlavour;
        }

        private static bool IsStrawberryAndMintFlavours(Collection<string> flavours)
        {
            var hasStrawberry = false;
            var hasMintChocolateChip = false;

            foreach (var f in flavours)
            {
                switch ((IceCreamFlavour)Enum.Parse(typeof(IceCreamFlavour), f))
                {
                    case IceCreamFlavour.Strawberry:
                        hasStrawberry = true;
                        break;
                    case IceCreamFlavour.MintChocolateChip:
                        hasMintChocolateChip = true;
                        break;
                }
            }

            return hasStrawberry && hasMintChocolateChip;
        }

        private static bool IsCookiesAndCreamAndMooseTracksAndVanilla(Collection<string> flavours)
        {
            var hasCookiesAndCream = false;
            var hasMooseTracks = false;
            var hasVanilla = false;

            foreach (var f in flavours)
            {
                switch ((IceCreamFlavour)Enum.Parse(typeof(IceCreamFlavour), f))
                {
                    case IceCreamFlavour.CookiesAndCream:
                        hasCookiesAndCream = true;
                        break;
                    case IceCreamFlavour.MooseTracks:
                        hasMooseTracks = true;
                        break;
                    case IceCreamFlavour.Vanilla:
                        hasVanilla = true;
                        break;
                }
            }

            return hasCookiesAndCream && hasMooseTracks && hasVanilla;
        }
    }
}
