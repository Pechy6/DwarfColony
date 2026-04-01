using System.ComponentModel.DataAnnotations;
using DwarfColony.Models.Entities;

namespace DwarfColony.Models.ViewModels;

public class EditDwarfModel
{
    public int Id { get; set; }
    [Required]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Name must be between 4 and 20 characters")]
    public string Name { get; set; } = string.Empty;
    
    public DwarfJob Job { get; set; } = DwarfJob.None;
}