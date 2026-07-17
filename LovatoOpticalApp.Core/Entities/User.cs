namespace LovatoOpticalApp.Core.Entities
{
	public class User
	{
		public Guid Id { get; set; }
		public string Name { get; private set; }
		public string Role { get; set; }
		public string Email { get; private set; }
		public string Password { get; private set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		public User(string name, string role, string email, string password)
		{
			Name = name;
			Role = role;
			Email = email;
			Password = password;
			CreatedAt = DateTime.UtcNow;
			UpdatedAt = DateTime.UtcNow;
		}
	}
}