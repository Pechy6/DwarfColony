namespace DwarfColony.Models.ViewModels;

public class DwarfSleepRowViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Job { get; set; } = string.Empty;
    public int Hunger { get; set; }
    public int Thirst { get; set; }
    public int Energy { get; set; }
    public string Status { get; set; }
    public string State { get; set; }
    public bool CanSleep { get; set; }
}