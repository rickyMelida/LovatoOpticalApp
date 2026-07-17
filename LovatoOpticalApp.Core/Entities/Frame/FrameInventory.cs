namespace  LovatoOpticalApp.Core.Entities
{
	public class FrameInventory
	{
		public Guid Id { get; set; }
		public Guid Frame { get; private set; }
		public int Quantity { get; private set; }
		public DateTime UpdatedAt { get; set; }
		public Guid UpdatedBy { get; set; }

		public FrameInventory(Guid frame, int quantity)
		{
			Frame = frame;
			Quantity = quantity;
		}
	}
}