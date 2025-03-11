using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Models
{
    public class SerialNumber
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        public Item? Item { get; set; }
    }
}
