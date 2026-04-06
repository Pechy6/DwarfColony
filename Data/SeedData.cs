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
                Energy = 80,
                Hunger = 20,
                Thirst = 10,
                IsAlive = true
            });
            context.Dwarves.Add(new Dwarf()
            {
                Name = "Balin",
                Age = 178,
                Energy = 56,
                Hunger = 80,
                Thirst = 90,
                IsAlive = true
            });
            context.Dwarves.Add(new Dwarf()
            {
                Name = "Dwalin",
                Age = 340,
                Energy = 97,
                Hunger = 15,
                Thirst = 35,
                IsAlive = true
            });
            context.Dwarves.Add(new Dwarf()
            {
                Name = "Oin",
                Age = 250,
                Energy = 100,
                Hunger = 9,
                Thirst = 86,
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
                Water = 100,
                Coal = 35,
                Stone = 50,
                IronCore = 20,
                Wood = 100
            });
            context.SaveChanges();  
        }
    }
}