using DwarfColony.Models.Entities;

namespace DwarfColony.Models.ViewModels;

public class NeedsViewModel
{
    public int Id { get; set; }
    public List<Dwarf> Dwarves { get; set; } = new();
    public int FoodToUse { get; set; }
    // storage 
    public int FoodInStorage { get; set; }
    
}