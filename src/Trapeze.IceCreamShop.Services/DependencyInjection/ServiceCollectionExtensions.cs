// <copyright file="ServiceCollectionExtensions.cs" company="Trapeze Ice Cream">
// Copyright (c) Trapeze Ice Cream. All rights reserved.
// </copyright>

namespace Trapeze.IceCreamShop.Services.DependencyInjection
{
    using Microsoft.Extensions.DependencyInjection;
    using Trapeze.IceCreamShop.Abstractions;
    using Trapeze.IceCreamShop.Services;

    /// <summary>
    /// Extension methods for the <see cref="IServiceCollection"/> object.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the necessary service layer services for the ice cream shop.
        /// </summary>
        /// <param name="services">A <see cref="IServiceCollection"/>.</param>
        /// <returns>A modified <see cref="IServiceCollection"/> with the added services.</returns>
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            services.AddTransient<IIceCreamShopService, IceCreamShopService>();

            return services;
        }
    }
}
