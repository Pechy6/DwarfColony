using DwarfColony.Models.Entities;

namespace DwarfColony.Data;

public class SeedData
{
    public static void Initialize(ApplicationDbContext context)
    {
        // Areas
        if (!context.Areas.Any())
        {
            context.Areas.Add(new Area
            {
                Name = "Dwarf basement",
                Description = "The basement of the dwarves",
                Type = AreaType.Base,
                DistanceFromBase = 0,
                MaxWorkers = 999,
                IsUnlocked = true,
                CanRest = true
            });

            context.Areas.Add(new Area
            {
                Name = "North forest",
                Description =
                    "The Northern Forest is a dense woodland filled with wildlife," +
                    " trees, and scattered stones. A small stream with clean water flows" +
                    " through the area, making it a safe and resource-rich location for beginners.",
                Type = AreaType.Forest,
                DistanceFromBase = 2,
                MaxWorkers = 10,
                IsUnlocked = true,
                CanRest = false
            });
            context.SaveChanges();
        }

        // Storage
        if (!context.Storages.Any())
        {
            context.Storages.Add(new Storage
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

        // Dwarves
        if (!context.Dwarves.Any())
        {
            var homeArea = context.Areas.FirstOrDefault(a => a.Type == AreaType.Base);

            if (homeArea != null)
            {
                context.Dwarves.Add(new Dwarf
                {
                    Name = "Thorin",
                    Age = 195,
                    Energy = 100,
                    Hunger = 100,
                    Thirst = 100,
                    IsAlive = true,
                    CurrentAreaId = homeArea.Id
                });
                context.Dwarves.Add(new Dwarf
                {
                    Name = "Balin",
                    Age = 178,
                    Energy = 100,
                    Hunger = 100,
                    Thirst = 100,
                    IsAlive = true,
                    CurrentAreaId = homeArea.Id
                });
                context.Dwarves.Add(new Dwarf
                {
                    Name = "Dwalin",
                    Age = 340,
                    Energy = 100,
                    Hunger = 100,
                    Thirst = 100,
                    IsAlive = true,
                    CurrentAreaId = homeArea.Id
                });
                context.Dwarves.Add(new Dwarf
                {
                    Name = "Oin",
                    Age = 250,
                    Energy = 100,
                    Hunger = 100,
                    Thirst = 100,
                    IsAlive = true,
                    CurrentAreaId = homeArea.Id
                });
            }

            context.SaveChanges();
        }
    }
}