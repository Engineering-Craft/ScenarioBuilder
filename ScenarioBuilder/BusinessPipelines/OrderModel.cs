namespace ScenarioBuilder.Pipelines
{
    public class OrderModel
    {
        public string OrderId { get; internal set; }
        public string? Status { get; internal set; }
        public bool Paid { get; internal set; }
        public bool Shipped { get; internal set; }
        public List<string> Documents { get; set; }
    }
}