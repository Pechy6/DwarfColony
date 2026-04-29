using DwarfColony.Models.Entities.World;

namespace DwarfColony.Models.Entities.WorldResources;

public class ResourceType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<AreaResource> AreaResources { get; set; } = new List<AreaResource>();
}