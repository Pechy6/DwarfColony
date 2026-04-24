using DwarfColony.Models.Entities;

namespace DwarfColony.Models.ViewModels;

public class IndexViewModel
{
    public List<Dwarf> Dwarves { get; set; } = new();

    public string? SortOrder { get; set; }
    public string? NameSort { get; set; }
    public string? AgeSort { get; set; }
    public string? EnergySort { get; set; }
    public string? HungerSort { get; set; }
    public string? ThirstSort { get; set; }
    public string? JobSort { get; set; }
    public string? StatusSort { get; set; }
    public string? StateSort { get; set; }
}