namespace LovatoOpticalApp.Core.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CiRuc { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime CreationDate { get; set; }

        public Customer(string name, string ciRuc, DateTime? birthDay, string address, string phone)
        {
            Id = Guid.NewGuid();
            Name = name;
            CiRuc = ciRuc;
            BirthDay = birthDay;
            Address = address;
            Phone = phone;
            CreationDate = DateTime.Now;
        }

    }
}
