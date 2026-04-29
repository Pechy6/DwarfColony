using DwarfColony.Models.Entities;
using DwarfColony.Models.Entities.Dwarfs;
using DwarfColony.Models.Entities.World;
using DwarfColony.Models.Entities.BaseResources;
using DwarfColony.Models.Entities.WorldResources;

namespace DwarfColony.Data;

public class SeedData
{
    public static void Initialize(ApplicationDbContext context)
    {
        // Resources 
        if (!context.ResourceTypes.Any())
        {
            context.ResourceTypes.AddRange(
                new ResourceType { Name = "RawFood" },
                new ResourceType { Name = "Food" },
                new ResourceType { Name = "Water" },
                new ResourceType { Name = "Stone" },
                new ResourceType { Name = "IronCore" },
                new ResourceType { Name = "Coal" },
                new ResourceType { Name = "Wood" });

            context.SaveChanges();
        }

        // Areas
        if (!context.Areas.Any())
        {
            context.Areas.Add(new Area
            {
                Name = "Stone heart base",
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

            context.Areas.Add(new Area
            {
                Name = "East desert",
                Description =
                    "The East desert is a vast, arid region with sand dunes, sparse vegetation, and occasional oasis. It is a challenging environment for exploration and resource gathering.",
                Type = AreaType.Desert,
                DistanceFromBase = 6,
                MaxWorkers = 35,
                IsUnlocked = false,
                CanRest = false
            });
            context.SaveChanges();
        }

        //Area resources 
        if (!context.AreaResources.Any())
        {
            // Areas
            var dwarfBase = context.Areas.First(a => a.Name == "Stone heart base");
            var northForrest = context.Areas.First(a => a.Name == "North forest");
            var eastDesert = context.Areas.First(a => a.Name == "East desert");

            // Resources
            var water = context.ResourceTypes.First(r => r.Name == "Water");
            var wood = context.ResourceTypes.First(r => r.Name == "Wood");
            var stone = context.ResourceTypes.First(r => r.Name == "Stone");
            var food = context.ResourceTypes.First(r => r.Name == "Food");
            var rawFood = context.ResourceTypes.First(r => r.Name == "RawFood");
            var ironCore = context.ResourceTypes.First(r => r.Name == "IronCore");
            var coal = context.ResourceTypes.First(r => r.Name == "Coal");

            context.AreaResources.AddRange(
                // Stone heart base
                new AreaResource { AreaId = dwarfBase.Id, ResourceTypeId = wood.Id, Amount = 50 },
                new AreaResource { AreaId = dwarfBase.Id, ResourceTypeId = water.Id, Amount = 100 },
                new AreaResource { AreaId = dwarfBase.Id, ResourceTypeId = stone.Id, Amount = 400 },
                new AreaResource { AreaId = dwarfBase.Id, ResourceTypeId = ironCore.Id, Amount = 150 },
                new AreaResource { AreaId = dwarfBase.Id, ResourceTypeId = coal.Id, Amount = 200 },
                new AreaResource { AreaId = dwarfBase.Id, ResourceTypeId = food.Id, Amount = 100 },
                new AreaResource { AreaId = dwarfBase.Id, ResourceTypeId = rawFood.Id, Amount = 180 },
                
                // North forrest
                new AreaResource { AreaId = northForrest.Id, ResourceTypeId = wood.Id, Amount = 600 },
                new AreaResource { AreaId = northForrest.Id, ResourceTypeId = water.Id, Amount = 999 },
                new AreaResource { AreaId = northForrest.Id, ResourceTypeId = stone.Id, Amount = 30 },
                new AreaResource { AreaId = northForrest.Id, ResourceTypeId = food.Id, Amount = 180 },
                new AreaResource { AreaId = northForrest.Id, ResourceTypeId = rawFood.Id, Amount = 300 },
                
                //East desert
                new AreaResource { AreaId = eastDesert.Id, ResourceTypeId = wood.Id, Amount = 5 },
                new AreaResource { AreaId = eastDesert.Id, ResourceTypeId = water.Id, Amount = 15 },
                new AreaResource { AreaId = eastDesert.Id, ResourceTypeId = stone.Id, Amount = 10 },
                new AreaResource { AreaId = eastDesert.Id, ResourceTypeId = food.Id, Amount = 10 },
                new AreaResource { AreaId = eastDesert.Id, ResourceTypeId = rawFood.Id, Amount = 25 }
            );
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