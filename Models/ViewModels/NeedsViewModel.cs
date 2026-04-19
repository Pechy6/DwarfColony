using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DwarfColony.Models.Entities;

namespace DwarfColony.Models.ViewModels;

public class NeedsViewModel
{
    [DisplayName("Time to sleep (hours)")]
    [Range(1, 12, ErrorMessage = "Time to sleep must be between 1 and 12 hours")]
    public int TimeToSleep { get; set; }
    
    public List<DwarfSleepRowViewModel> Dwarves { get; set; } = new();
    public List<int> SelectedDwarvesIds { get; set; } = new();
    
}