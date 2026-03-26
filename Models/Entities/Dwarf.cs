namespace DwarfColony.Models.Entities;

public class Dwarf
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public int Energy { get; set; }
    public int Hunger { get; set; }
    public int Thirst { get; set; }
    public bool IsAlive { get; set; }
}