using System.ComponentModel.DataAnnotations;

namespace RManagementMVC.Models;

public class ReservationViewModel
{
    public Guid? Id { get; set; }

    [Required]
    [Display(Name = "Date")]
    public string At { get; set; } = default!;

    [Required]
    [Display(Name = "Time")]
    public string TimeSlot { get; set; } = default!;

    [Required]
    [Display(Name = "Name")]
    public string Name { get; set; } = default!;

    [EmailAddress]
    [Display(Name = "Email")]
    public string? Email { get; set; }

    [Required]
    [Range(1, 10, ErrorMessage = "Please select between 1 and 10 people")]
    [Display(Name = "Number of People")]
    public int Quantity { get; set; }

    public string FormattedDateTime => $"{At} {TimeSlot}";
}
