using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DwarfColony.Models.Entities;

namespace DwarfColony.Models.ViewModels;

public class NeedsViewModel
{
    public int Id { get; set; }
    public List<Dwarf> Dwarves { get; set; } = new();
    
    [Display(Name = "Food to use")]
    [Range(0, 5, ErrorMessage = "Food to use must be between 1 and 5")]
    public int FoodToUse { get; private set; }
    // storage 
    public int FoodInStorage { get; set; }
    
}