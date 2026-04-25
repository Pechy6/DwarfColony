using DwarfColony.Models.Entities;

namespace DwarfColony.Data;

public class SeedData
{
    public static void Initialize(ApplicationDbContext context)
    {
        if (!context.Dwarves.Any())
        {
            context.Dwarves.Add(new Dwarf
            { Name = "Thorin",
                Age = 195,
                Energy = 100,
                Hunger = 100,
                Thirst = 100,
                IsAlive = true
            });
            context.Dwarves.Add(new Dwarf()
            {
                Name = "Balin",
                Age = 178,
                Energy = 100,
                Hunger = 100,
                Thirst = 100,
                IsAlive = true
            });
            context.Dwarves.Add(new Dwarf()
            {
                Name = "Dwalin",
                Age = 340,
                Energy = 100,
                Hunger = 100,
                Thirst = 100,
                IsAlive = true
            });
            context.Dwarves.Add(new Dwarf()
            {
                Name = "Oin",
                Age = 250,
                Energy = 100,
                Hunger = 100,
                Thirst = 100,
                IsAlive = true
            });
        
            context.SaveChanges();   
        }
        
        if (!context.Storages.Any())
        {
            context.Storages.Add(new Storage()
            {
                RawFood = 100,
                Food = 50,
                Water = 200,
                Coal = 35,
                Stone = 50,
                IronCore = 20,
                Wood = 100
            });
            context.SaveChanges();  
        }
    }
}