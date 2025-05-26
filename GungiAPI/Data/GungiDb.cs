using GungiAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GungiAPI.Data;

public class GungiDb : DbContext
{
    public GungiDb(DbContextOptions<GungiDb> options) : base(options)
    {
    }

    public DbSet<Gasto> Gastos => Set<Gasto>();
}