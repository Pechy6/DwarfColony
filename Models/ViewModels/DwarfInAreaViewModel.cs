namespace DwarfColony.Models.ViewModels;

public class DwarfInAreaViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Job { get; set; } = "";
    public string State { get; set; } = "";
    public int TravelRemainingTicks { get; set; } 
}