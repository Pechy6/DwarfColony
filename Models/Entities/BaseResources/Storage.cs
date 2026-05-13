namespace DwarfColony.Models.Entities.BaseResources;

public class Storage
{
    public int Id { get; set; }
    
    // Essentials needs
    public int RawFood { get; set; }
    public int Food { get; set; }
    public int Water { get; set; }
    
    // Resources from mining
    public int Stone { get; set; }
    public int IronOre { get; set; }
    public int Coal { get; set; }
    public int GoldOre { get; set; }
    
    // Resources from woodcutting
    public int Wood { get; set; }
    public int Charcoal { get; set; }
}