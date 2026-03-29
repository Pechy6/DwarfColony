using DwarfColony.Models.Entities;
using DwarfColony.Models.ViewModels;

namespace DwarfColony.Services;

public class DwarfFactory
{
    /// <summary>
    /// Random number generator.
    /// </summary>
    private readonly Random _random = new Random();
    
    /// <summary>
    /// The core (Start) values of a dwarf. Like energy, hunger, and thirst.
    /// </summary>
    private readonly int _startValue = 100;

    /// <summary>
    /// Creates a new Dwarf with default starting values..
    /// </summary>
    /// <param name="createDwarfModel">
    /// Input data used to initialize the dwarf.
    /// </param>
    /// <returns>
    /// A newly created Dwarf entity.
    /// </returns>
    public Dwarf Create(CreateDwarfModel createDwarfModel)
    {
        Dwarf newDwarf = new Dwarf();
        newDwarf.Name = createDwarfModel.Name;
        newDwarf.Age = GetDwarfAge();
        newDwarf.Energy = _startValue;
        newDwarf.Hunger = _startValue;
        newDwarf.Thirst = _startValue;
        newDwarf.IsAlive = true;
        return newDwarf;
    }
    
    /// <summary>
    /// Generates a random age for the dwarf.
    /// </summary>
    /// <returns>A random age between 30 and 150</returns>
    private int GetDwarfAge()
    {
        return _random.Next(30, 151);
    }
    
}