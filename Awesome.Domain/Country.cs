namespace Awesome.Domain
{
    public class Country : BaseEntity
    {
        public Country(string name = "", string code = "")
        {
            Name = name;
            Code = code;
        }

        public string Name { get; set; }

        public string Code { get; set; }

        public List<City>? Cities { get; set; } 
    }
}
