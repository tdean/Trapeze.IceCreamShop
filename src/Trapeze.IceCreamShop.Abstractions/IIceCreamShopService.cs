// <copyright file="IIceCreamShopService.cs" company="Trapeze Ice Cream">
// Copyright (c) Trapeze Ice Cream. All rights reserved.
// </copyright>

namespace Trapeze.IceCreamShop.Abstractions
{
    using System.Threading.Tasks;
    using Trapeze.IceCreamShop.Models;

    /// <summary>
    /// An interface for methods usable by the ice cream shop service layer.
    /// </summary>
    public interface IIceCreamShopService
    {
        Task<decimal?> ProcessRequest(IceCreamPurchasedRequest purchaseDetails);
    }
}
