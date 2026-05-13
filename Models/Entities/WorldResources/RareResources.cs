using DwarfColony.Models.Entities.World;

namespace DwarfColony.Models.Entities.WorldResources;

public class RareResources
{
    public int Id { get; set; }
    
    public Area Area { get; set; }
    public int AreaId { get; set; }
    
    public ResourceType? ResourceType { get; set; }
    public int ResourceTypeId { get; set; }
    
    public int Amount { get; set; }
    
    public double ChanceToMine { get; set; }
    
}