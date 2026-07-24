namespace LovatoOpticalApp.Application.DTOs
{
    public class CustomerResquestDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CiRuc { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Address { get; set; }
    }
}
