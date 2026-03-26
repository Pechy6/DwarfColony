using Microsoft.EntityFrameworkCore;
using DwarfColony.Models.Entities;

namespace DwarfColony.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Dwarf> Dwarves { get; set; }
}