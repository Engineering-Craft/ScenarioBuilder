using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilder.Core
{
    public static class ScenarioContextExtensions
    {
        public static T Require<T>(this ScenarioContext context)
        {
            if (!context.TryGet<T>(out var value))
                throw new InvalidOperationException(
                    $"Required context value {typeof(T).Name} not previously executed as expected."
                );

            return value;
        }
    }
}