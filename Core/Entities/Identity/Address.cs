using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.Entities.Identity
{
    public class Address
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(100)]
        public string Street { get; set; }
        [Required]
        [MaxLength(100)]
        public string City { get; set; }
        [Required]
        [MaxLength(100)]
        public string State { get; set; }
        [Required]
        [MaxLength(100)]
        public string ZipCode { get; set; }
        [MaxLength(100)]
        [JsonIgnore]
        public string AppUserId { get; set; }
        [JsonIgnore]
        public AppUser AppUser { get; set; }

    }
}