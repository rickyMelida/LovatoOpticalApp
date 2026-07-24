namespace LovatoOpticalApp.Core.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CiRuc { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; }
        public string Email { get; set; }
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
        public DateTime CreationDate { get; set; }

        protected Customer() { }

        public Customer(string name, string ciRuc, string phone, string email, DateTime? birthDay, string address)
        {
            Id = Guid.NewGuid();
            Name = name;
            CiRuc = ciRuc;
            Phone = phone;
            Email = email;
            BirthDay = birthDay;
            Address = address;
            CreationDate = DateTime.Now;
        }

    }
}
