using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class CustomerBasket
    {
        public CustomerBasket()
        {
        }

        public CustomerBasket(string id)
        {
            Id = id;
        }

        [Required]
        [MaxLength(100)]
        public string Id { get; set; }
        [Required]
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
    }
}
