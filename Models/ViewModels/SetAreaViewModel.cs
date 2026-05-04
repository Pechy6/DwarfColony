using DwarfColony.Models.Entities.World;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DwarfColony.Models.ViewModels;

public class SetAreaViewModel
{
    public int DwarfId { get; set; }
    public string DwarfName { get; set; }
    public string CurrentArea { get; set; }
    public int TargetAreaId { get; set; }
    public List<SelectListItem> TargetAreas { get; set; }
    public string Description { get; set; }
    public AreaType AreaType { get; set; }
    public int Distance { get; set; }
}