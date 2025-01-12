using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITB2203Application.Models
{
   public record Courier {
    public int Id { get; init; }

    [Required]
    public string? Name { get; init; }
    [Required]
    public string? VehicleType { get; init; }
}

}