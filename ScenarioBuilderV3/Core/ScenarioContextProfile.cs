using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilder.Core
{
    using AutoMapper;
    using System.Linq;
    using System.Reflection;

    public class ScenarioToContextProfile : Profile
    {
        public ScenarioToContextProfile()
        {
            CreateMap<Scenario, ScenarioContext>()
                .AfterMap((src, dest) =>
                {
                    var props = src.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .Where(p => p.CanRead && p.GetIndexParameters().Length == 0);

                    foreach (var prop in props)
                    {
                        var value = prop.GetValue(src);
                        dest.Set(prop.Name, value);
                    }
                });
        }
    }
}