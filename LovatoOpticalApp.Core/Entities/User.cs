namespace LovatoOpticalApp.Core.Entities
{
	public class User
	{
		public Guid Id { get; set; }
		public Guid RoleId { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public DateTime CreateAt { get; set; } = DateTime.UtcNow;
	}
}