namespace LovatoOpticalApp.Application.DTOs
{
    public class CustomerResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CiRuc { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Address { get; set; }
        public List<RecipeResponseDto> Recipes { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
