using DwarfColony.Models.Entities.WorldResources;

namespace DwarfColony.Models.Entities.World;

public class AreaResource
{
    public int Id { get; set; }
    
    public int AreaId { get; set; }
    public Area Area { get; set; }
    
    public int ResourceTypeId { get; set; }
    public ResourceType ResourceType { get; set; }
    
    public int Amount { get; set; }
}