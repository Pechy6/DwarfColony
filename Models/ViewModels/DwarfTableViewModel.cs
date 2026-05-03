using DwarfColony.Models.Entities;
using DwarfColony.Models.Entities.Dwarfs;

namespace DwarfColony.Models.ViewModels;

public class DwarfTableViewModel
{
    public List<Dwarf> Dwarves { get; set; } = new();

    public string? SortOrder { get; set; }
    public string NameSort  => SortOrder == "name_desc" ? "name" : "name_desc";
    public string AgeSort => SortOrder == "age_desc" ? "age" : "age_desc";
    public string EnergySort => SortOrder == "energy_desc" ? "energy" : "energy_desc";
    public string HungerSort => SortOrder == "hunger_desc" ? "hunger" : "hunger_desc";
    public string ThirstSort => SortOrder == "thirst_desc" ? "thirst" : "thirst_desc";
    public string JobSort => SortOrder == "job_desc" ? "job" : "job_desc";
    public string StatusSort => SortOrder == "status_desc" ? "status" : "status_desc";
    public string StateSort => SortOrder == "state_desc" ? "state" : "state_desc";
}