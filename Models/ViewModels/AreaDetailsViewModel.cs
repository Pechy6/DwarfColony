namespace DwarfColony.Models.ViewModels;

public class AreaDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = "";

    public List<DwarfInAreaViewModel> CurrentDwarves { get; set; } = [];
    public List<DwarfInAreaViewModel> IncomingDwarves { get; set; } = [];
}