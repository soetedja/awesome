namespace Awesome.Domain
{
    public class City : BaseEntity
    {
        public City(string name = "")
        {
            Name = name;
        }

        public string Name { get; set; }

        public int CountryId { get; set; }

        public Country? Country { get; set; }
    }
}
