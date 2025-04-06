using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace service_api.Models;

public class services
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id { get; set; }
    public string name { get; set; } = string.Empty;
    public string? description { get; set; }
    public decimal price { get; set; }
}
