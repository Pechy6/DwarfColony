namespace DwarfColony.Models.Entities;

public enum AreaResourcesType
{
    Water,
    Wood,
    Stone,
    IronOre,
    Coal,
    Food
}

public class AreaResource
{
    public int Id { get; set; }
    
    public int AreaId { get; set; }
    public Area Area { get; set; }
    
    public AreaResourcesType Type { get; set; }
    
    public int Amount { get; set; }
}