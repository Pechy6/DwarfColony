using DwarfColony.Models.Entities.World;

namespace DwarfColony.Models.ViewModels;

public class SetAreaViewModel
{
    public string DwarfName { get; set; }
    public string CurrentArea { get; set; }
    public List<Area> TargetAreas { get; set; }
    public int Distance { get; set; }
}