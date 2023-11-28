namespace sacremap_web_api.Models
{
    public class Church
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string HouseNumber { get; set; }
        public required string Street { get; set; }
        public required string PostalCode { get; set; }
        public required string City { get; set; }


    }
}
