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
                //Consumables
                new ResourceType { Name = "Raw Food" },
                new ResourceType { Name = "Food" },
                new ResourceType { Name = "Water" },
                // Mining
                new ResourceType { Name = "Stone" },
                new ResourceType { Name = "Iron ore" },
                new ResourceType { Name = "Coal" },
                new ResourceType { Name = "Gold ore" },
                // Woodcutter
                new ResourceType { Name = "Wood" },
                //Crafting materials
                new ResourceType { Name = "Charcoal" }
            );

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
            var northForest = context.Areas.First(a => a.Name == "North forest");
            var eastDesert = context.Areas.First(a => a.Name == "East desert");

            // Resources
            var water = context.ResourceTypes.First(r => r.Name == "Water");
            var wood = context.ResourceTypes.First(r => r.Name == "Wood");
            var stone = context.ResourceTypes.First(r => r.Name == "Stone");
            var food = context.ResourceTypes.First(r => r.Name == "Food");
            var rawFood = context.ResourceTypes.First(r => r.Name == "Raw Food");

            context.AreaResources.AddRange(
                // Stone heart base
                new AreaResource { AreaId = dwarfBase.Id, ResourceTypeId = wood.Id, Amount = 50 },
                new AreaResource { AreaId = dwarfBase.Id, ResourceTypeId = water.Id, Amount = 999 },
                new AreaResource { AreaId = dwarfBase.Id, ResourceTypeId = stone.Id, Amount = 800 },
                new AreaResource { AreaId = dwarfBase.Id, ResourceTypeId = food.Id, Amount = 100 },
                new AreaResource { AreaId = dwarfBase.Id, ResourceTypeId = rawFood.Id, Amount = 180 },

                // North forest
                new AreaResource { AreaId = northForest.Id, ResourceTypeId = wood.Id, Amount = 600 },
                new AreaResource { AreaId = northForest.Id, ResourceTypeId = water.Id, Amount = 999 },
                new AreaResource { AreaId = northForest.Id, ResourceTypeId = stone.Id, Amount = 30 },
                new AreaResource { AreaId = northForest.Id, ResourceTypeId = food.Id, Amount = 180 },
                new AreaResource { AreaId = northForest.Id, ResourceTypeId = rawFood.Id, Amount = 300 },

                //East desert
                new AreaResource { AreaId = eastDesert.Id, ResourceTypeId = wood.Id, Amount = 5 },
                new AreaResource { AreaId = eastDesert.Id, ResourceTypeId = water.Id, Amount = 15 },
                new AreaResource { AreaId = eastDesert.Id, ResourceTypeId = stone.Id, Amount = 10 },
                new AreaResource { AreaId = eastDesert.Id, ResourceTypeId = food.Id, Amount = 10 },
                new AreaResource { AreaId = eastDesert.Id, ResourceTypeId = rawFood.Id, Amount = 25 }
            );
            context.SaveChanges();
        }

        if (!context.RareResources.Any())
        {
            // Areas
            var dwarfBase = context.Areas.First(a => a.Name == "Stone heart base");
            var northForest = context.Areas.First(a => a.Name == "North forest");
            var eastDesert = context.Areas.First(a => a.Name == "East desert");

            // Resources
            var ironOre = context.ResourceTypes.First(r => r.Name == "Iron ore");
            var goldOre = context.ResourceTypes.First(r => r.Name == "Gold ore");
            var coal = context.ResourceTypes.First(r => r.Name == "Coal");

            context.RareResources.AddRange(
                // Stone heart base    
                new RareResources { AreaId = dwarfBase.Id, ResourceTypeId = ironOre.Id, Amount = 100, ChanceToMine = 0.01},
                new RareResources { AreaId = dwarfBase.Id, ResourceTypeId = goldOre.Id, Amount = 20, ChanceToMine = 0.005},
                new RareResources { AreaId = dwarfBase.Id, ResourceTypeId = coal.Id, Amount = 120, ChanceToMine = 0.02},

                // North forest
                new RareResources { AreaId = northForest.Id, ResourceTypeId = ironOre.Id, Amount = 20, ChanceToMine = 0.005},
                
                // East desert
                new RareResources { AreaId = eastDesert.Id, ResourceTypeId = ironOre.Id, Amount = 50, ChanceToMine = 0.01},
                new RareResources { AreaId = eastDesert.Id, ResourceTypeId = goldOre.Id, Amount = 45, ChanceToMine = 0.005},
                new RareResources { AreaId = eastDesert.Id, ResourceTypeId = coal.Id, Amount = 60, ChanceToMine = 0.015}
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
                IronOre = 20,
                GoldOre = 10,
                Wood = 100,
                Charcoal = 0
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