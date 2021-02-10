namespace DAL.Context
{
    using Microsoft.EntityFrameworkCore;
    using Models.DataModels;

    public class WeatherContext : DbContext
    {
        public DbSet<Region> Regions { get; set; }

        public DbSet<Weather> Weathers { get; set; }

        public WeatherContext(DbContextOptions<WeatherContext> options)
            : base(options) { }
    }
}
