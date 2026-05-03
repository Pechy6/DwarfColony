using DwarfColony.Models.Entities.Dwarfs;

namespace DwarfColony.Models.ViewModels;

public class DwarfOrderViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Job { get; set; } = "";
    public string CurrentArea { get; set; } = "";
    public string Status { get; set; } = "";
    public string TargetArea { get; set; } = "";
    public int TravelRemainingTicks { get; set; }
}