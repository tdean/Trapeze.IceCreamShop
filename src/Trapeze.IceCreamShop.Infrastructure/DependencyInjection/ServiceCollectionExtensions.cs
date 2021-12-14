// <copyright file="ServiceCollectionExtensions.cs" company="Trapeze Ice Cream">
// Copyright (c) Trapeze Ice Cream. All rights reserved.
// </copyright>

namespace Trapeze.IceCreamShop.Infrastructure.DependencyInjection
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Trapeze.IceCreamShop.Data.DependencyInjection;
    using Trapeze.IceCreamShop.Services.DependencyInjection;

    /// <summary>
    /// Extension methods for the <see cref="IServiceCollection"/> object.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the necessary services for the ice cream shop.
        /// </summary>
        /// <param name="services">A <see cref="IServiceCollection"/>.</param>
        /// <param name="configuration">A <see cref="IConfiguration"/>.</param>
        /// <returns>A modified <see cref="IServiceCollection"/> with the added services.</returns>
        public static IServiceCollection AddIceCreamShop(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServiceLayer()
                .AddDataLayer(configuration);

            return services;
        }
    }
}
