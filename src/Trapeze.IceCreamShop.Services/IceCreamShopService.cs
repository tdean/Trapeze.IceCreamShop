// <copyright file="IceCreamShopService.cs" company="Trapeze Ice Cream">
// Copyright (c) Trapeze Ice Cream. All rights reserved.
// </copyright>

namespace Trapeze.IceCreamShop.Services
{
    using System;
    using System.Threading.Tasks;
    using Trapeze.IceCreamShop.Abstractions;
    using Trapeze.IceCreamShop.Data;
    using Trapeze.IceCreamShop.Data.Entities;
    using Trapeze.IceCreamShop.Enums;
    using Trapeze.IceCreamShop.Models;
    using Trapeze.IceCreamShop.Services.Data;
    using Trapeze.IceCreamShop.Services.Validation;

    /// <summary>
    /// An implementation of methods usable by the ice cream shop service layer.
    /// </summary>
    public class IceCreamShopService : IIceCreamShopService
    {
        private readonly IceCreamDbContext _dbContext;

        public IceCreamShopService(IIceCreamPurchaseValidator validator, IceCreamDbContext dbContext)
        {
            Validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IIceCreamPurchaseValidator Validator { get; set; }

        public async Task<decimal?> ProcessRequest(IceCreamPurchasedRequest purchaseDetails)
        {
            var isValidPurchaseAmount = ValidPurchase(purchaseDetails);
            var isValidCost = ValidCost(purchaseDetails);

            if (isValidPurchaseAmount && isValidCost)
            {
                return SaveData(purchaseDetails);
            }
            else
            {
                return null;
            }
        }

        private static decimal CalculateCost(IceCreamPurchasedRequest purchaseDetails)
        {
            var baseCost = CalculateBaseCost(purchaseDetails.IceCreamBase);
            var scoopCost = CalculateScoopCost(purchaseDetails.NumberOfScoops);

            purchaseDetails.OperatingCost = baseCost + scoopCost;

            return baseCost + scoopCost;
        }

        private static decimal CalculateBaseCost(string iceCreamBase)
        {
            var iceCreamBaseEnum = (IceCreamBase)Enum.Parse(typeof(IceCreamBase), iceCreamBase, true);

            return IceCreamCostData.GetIceCreamBaseCost(iceCreamBaseEnum);
        }

        private static decimal CalculateScoopCost(int numberOfScoops)
        {
            return IceCreamCostData.GetIceCreamScoopCost(numberOfScoops);
        }

        private decimal SaveData(IceCreamPurchasedRequest purchaseDetails)
        {
            var iceCreamPurchase = new IceCreamInformation
            {
                PurchaserName = purchaseDetails.Purchaser.FirstName + purchaseDetails.Purchaser.LastName,
                PurchaseAmount = purchaseDetails.AmountPaid - purchaseDetails.OperatingCost,
                Base = new BaseInformation
                {
                    IceCreamBase = (IceCreamBase)Enum.Parse(typeof(IceCreamBase), purchaseDetails.IceCreamBase, true)
                },
            };

            foreach (var f in purchaseDetails.Flavours)
            {
                iceCreamPurchase.Flavours.Add(new FlavourInformation
                {
                    IceCreamFlavour = (IceCreamFlavour)Enum.Parse(typeof(IceCreamFlavour), f, true),
                });
            }

            _dbContext.IceCreams.Add(iceCreamPurchase);
            _dbContext.SaveChanges();


            return purchaseDetails.AmountPaid - purchaseDetails.OperatingCost;
        }



        private bool ValidPurchase(IceCreamPurchasedRequest purchaseDetails)
        {
            return Validator.IsValidPurchase(purchaseDetails);
        }

        private bool ValidCost(IceCreamPurchasedRequest purchaseDetails)
        {
            var operatingCost = CalculateCost(purchaseDetails);
            return Validator.IsValidCost(operatingCost, purchaseDetails.AmountPaid);
        }
    }
}
