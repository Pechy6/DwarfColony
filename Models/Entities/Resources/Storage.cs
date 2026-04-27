namespace DwarfColony.Models.Entities.Resources;

public class Storage
{
    public int Id { get; set; }
    
    // Essentials needs
    public int RawFood { get; set; }
    public int Food { get; set; }
    public int Water { get; set; }
    
    // Resources from mining
    public int Stone { get; set; }
    public int IronCore { get; set; }
    public int Coal { get; set; }
    
    // Resources from woodcutting
    public int Wood { get; set; }
}