namespace PM_CQRS.Commands
{
    public class DeleteParkingPricingIntervalCommand : ICommand
    {
        public Guid CommandId { get; set; } = Guid.NewGuid();
        public Int64 PricingIntervalId { get; set; }
    }
}