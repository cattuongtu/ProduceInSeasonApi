namespace ProduceInSeasonApi.Models
{
    public class Product
    {
        public long Id{ get; set; }
        public string? Name { get; set; }
        public bool? IsFruit { get; set; }
        public string? Description { get; set; }
        public string? Season { get; set; }
        public string? Secret { get; set; }
    }
}