using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilder.Domain.Extensions
{
    using Microsoft.Extensions.DependencyInjection;
    using ScenarioBuilder.Core.Interfaces;
    using System;
    using System.Linq;
    using System.Reflection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAllScenarioEvents(this IServiceCollection services, Assembly assembly)
        {
            var eventTypes = assembly.GetTypes()
                .Where(t => typeof(IScenarioEvent).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var type in eventTypes)
            {
                services.AddTransient(type);
            }

            return services;
        }
    }
}