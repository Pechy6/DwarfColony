using DwarfColony.Models.Entities.World;
using DwarfColony.Services;

namespace DwarfColony.Models.Entities.Dwarfs;

public enum DwarfJob
{
    None, 
    Cook,
    Woodcutter,
    Miner
}

public enum DwarfStatus
{
    Fit,
    Strained,
    Exhausted
}

public enum DwarfState
{
    Idle,
    Working,
    Sleeping,
    Traveling,
    Dead
}

public class Dwarf
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public int Energy { get; set; }
    public int Hunger { get; set; }
    public int Thirst { get; set; }
    public bool IsAlive { get; set; }
    
    public DwarfJob Job { get; set; } = DwarfJob.None;

    public DwarfStatus Status { get; set; } = DwarfStatus.Fit;
    
    public DwarfState State { get; set; } = DwarfState.Idle;
    
    // čas pro spánek
    public int ActionRemainingTime { get; set; }
    
    // Area kde se nachazi
    public int CurrentAreaId { get; set; }
    public Area CurrentArea { get; set; } = null!;
    
    // Area kam se chce posunout
    public int? TargetAreaId { get; set; }
    // Cas za jak dlouho dojde do te target area 
    public int TravelRemainingTicks { get; set; }
}