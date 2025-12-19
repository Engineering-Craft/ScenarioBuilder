using ScenarioBuilder.Core;
using ScenarioBuilder.Pipelines;
using System.Net.NetworkInformation;

namespace ScenarioBuilder.BusinessSteps
{
    public class OrderDocumentProcessorStep : IStep<ScenarioContext>
    {
        private bool _status;
        private Func<ScenarioContext, Task> pipeline;
        private OrderModel model;

        public OrderDocumentProcessorStep(Func<ScenarioContext, Task> pipeline, OrderModel model)
        {
            this.pipeline = pipeline;
            this.model = model;
        }

        public string Name => "Order Processing";

        public void ByProcessingDocuments(bool process)
        {
            _status = process;
        }

        public async Task ExecuteAsync(ScenarioContext context)
        {
            if (_status)
            {
                await Task.Delay(100);
            }
        }

        public bool PreConditionsSatisfied(ScenarioContext ctx)
        {
            return true;
        }
    }
}