using ScenarioBuilder.BusinessBuilders;
using ScenarioBuilder.Pipelines;
using ScenarioBuilder.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilder.BusinessFactories
{
    public sealed class CreateOrderFactory
    {
        private readonly IPaymentService _paymentService;
        private readonly OrderModel orderModel;

        public CreateOrderFactory(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        /// <summary>
        /// Creates a standard order builder with default items.
        /// </summary>
        public CreateOrderBuilder Instance()
        {
            return new CreateOrderBuilder(_paymentService, orderModel);
        }
    }
}