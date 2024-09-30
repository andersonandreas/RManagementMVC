using System.ComponentModel.DataAnnotations;

namespace RManagementMVC.Models;

public class CreateReservation
{

    [Required]
    public string At { get; set; } = default!;

    [Required]
    [Length(3, 75)]
    public string Name { get; set; } = default!;

    public string? Email { get; set; }

    [Required]
    public int Quantity { get; set; }
}

