using System.ComponentModel.DataAnnotations;

namespace DwarfColony.Models.ViewModels;

public class EditDwarfModel
{
    public int Id { get; set; }
    [Required]
    [StringLength(20, MinimumLength = 4, ErrorMessage = "Name must be between 4 and 20 characters")]
    public string Name { get; set; } = string.Empty;
}