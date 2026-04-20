using System.ComponentModel.DataAnnotations;
using DwarfColony.Models.Entities;

namespace DwarfColony.Models.ViewModels;

public class EditDwarfJobModel
{
    public int Id { get; set; }
    public string Name { get; init; } = string.Empty;
    
    public DwarfJob Job { get; set; } = DwarfJob.None;
}