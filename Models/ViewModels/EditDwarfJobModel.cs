using System.ComponentModel.DataAnnotations;
using DwarfColony.Models.Entities;
using DwarfColony.Models.Entities.Dwarfs;

namespace DwarfColony.Models.ViewModels;

public class EditDwarfJobModel
{
    public int Id { get; set; }
    public string Name { get; init; } = string.Empty;
    
    public DwarfJob Job { get; set; } = DwarfJob.None;
}