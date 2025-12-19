using ScenarioBuilder.BusinessSteps;
using ScenarioBuilder.Core;
using ScenarioBuilder.Pipelines;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilder.BusinessBuilders
{
    public sealed class OrderDocumentProcessorBuilder
    {
        private readonly OrderModel _model;
        private Func<ScenarioContext, Task> _pipeline = _ => Task.CompletedTask;

        public OrderDocumentProcessorBuilder(OrderModel model)
        {
            _model = model;
        }

        public OrderDocumentProcessorBuilder WithDocument(string name)
        {
            _pipeline = async ctx =>
            {
                _model.Documents.Add(name);
                await _pipeline(ctx);
            };
            return this;
        }

        public OrderDocumentProcessorStep Build()
            => new OrderDocumentProcessorStep(_pipeline, _model);
    }
}