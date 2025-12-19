using System;
using System.Collections.Generic;
using System.Text;

namespace ScenarioBuilder.Services
{
    public interface IPaymentService
    {
        Task ProcessAsync();
    }

    public class PaymentService : IPaymentService
    {
        public async Task ProcessAsync()
        {
            await Task.Delay(1000);
        }
    }
}