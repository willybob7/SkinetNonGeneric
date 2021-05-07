using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class ProductBrand : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}