namespace DwarfColony.Models.Entities;

public enum AreaType
{
    Base,
    Forest,
    Desert,
    Mountain,
    Lake,
    Sea
}

public class Area
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public AreaType Type { get; set; }

    public int MaxWorkers { get; set; }

    public int DistanceFromBase { get; set; }

    public bool IsUnlocked { get; set; } = false;
    public bool CanRest { get; set; } = false;

    public ICollection<AreaResource> Resources { get; set; } = new List<AreaResource>();
    public ICollection<Dwarf> Dwarves { get; set; } = new List<Dwarf>();
}