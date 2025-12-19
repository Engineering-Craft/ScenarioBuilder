using ScenarioBuilder.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilder.Pipelines
{
    internal delegate Task OrderCreationStep(ScenarioContext context);
}