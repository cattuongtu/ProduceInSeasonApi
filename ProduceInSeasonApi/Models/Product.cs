using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProduceInSeasonApi.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id{ get; set; }
        public string? Name { get; set; }
        public bool? IsFruit { get; set; }
        public string? Description { get; set; }
        public string? Season { get; set; }
        public string? Secret { get; set; }
    }
}