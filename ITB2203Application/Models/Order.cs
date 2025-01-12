using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITB2203Application.Models
{
    public record Order
    {
        public int Id { get; set; }
         [Required]
        public string? CustomerName { get; set; }
        [Required]
        public string? OrderContents { get; set; }
        [Required]
        public DateTime? StartDate { get; set;}
        public DateTime? CompletionDate { get; set;} = null;
        public int restaurantId{ get; set; }
        public int CourierId { get; set; }

    }
}