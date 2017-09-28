// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.JsonLocalization
{
    /// <summary>
    /// Extension methods for setting up localization services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class JsonLocalizationServiceCollectionExtensions
    {
        /// <summary>
        /// Adds services required for application localization.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddJsonLocalization(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddOptions();

            AddJsonLocalizationServices(services);

            return services;
        }

        /// <summary>
        /// Adds services required for application localization.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="setupAction">
        /// An <see cref="Action{LocalizationOptions}"/> to configure the <see cref="LocalizationOptions"/>.
        /// </param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddJsonLocalization(
            this IServiceCollection services,
            Action<LocalizationOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            AddJsonLocalizationServices(services, setupAction);

            return services;
        }

        // To enable unit testing
        internal static void AddJsonLocalizationServices(IServiceCollection services)
        {
            services.TryAddSingleton<IStringLocalizerFactory, JsonResourceStringLocalizerFactory>();
            services.TryAddTransient(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));
        }

        internal static void AddJsonLocalizationServices(
            IServiceCollection services,
            Action<LocalizationOptions> setupAction)
        {
            AddJsonLocalizationServices(services);
            services.Configure(setupAction);
        }
    }
}