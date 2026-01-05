namespace ScenarioBuilder.Domain
{
    public interface IPaymentService
    {
        public void Pay();
    }

    public class PaymentService : IPaymentService
    {
        public void Pay()
        {
        }
    }
}