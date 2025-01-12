using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITB2203Application.Models
{
    public record Restaurant
    {
        public int Id { get; set; }
        
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Location { get; set; }
    }
}