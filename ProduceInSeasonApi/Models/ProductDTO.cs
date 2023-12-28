using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProduceInSeasonApi.Models;

public class ProductDTO {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string? Name { get; set; }
    public bool? IsFruit { get; set; }
    public string? Description { get; set; }
    public string? Season { get; set; }
}
