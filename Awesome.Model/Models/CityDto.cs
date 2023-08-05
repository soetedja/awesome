namespace Awesome.Model
{
    public class CityDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int CountryId { get; set; }

        public CountryDto? Country { get; set; }
    }
}
