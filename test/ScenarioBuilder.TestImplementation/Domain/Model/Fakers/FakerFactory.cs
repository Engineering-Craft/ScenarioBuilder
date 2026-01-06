using Bogus;
using System;

namespace ScenarioBuilder.Tests.ScenarioBuilder.TestImplementation.Domain.Model.Fakers
{
    public static class FakerFactory
    {
        public static Faker<T> GetFaker<T>() where T : class, new()
        {
            return new Faker<T>();
        }
    }
}