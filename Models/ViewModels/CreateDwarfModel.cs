using System.ComponentModel.DataAnnotations;

namespace DwarfColony.Models.ViewModels;

public class CreateDwarfModel
{
    /// <summary>
    /// Gets or sets the name of the dwarf.
    /// Must be between 4 and 20 characters.
    /// </summary>
    [Required]
    [StringLength(20, MinimumLength = 4, ErrorMessage = "Name must be between 4 and 20 characters")]
    public string Name { get; set; } = string.Empty;
}