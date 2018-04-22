using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Persistence
{
    public class TemporaryDesignTimeDbContextFactory : IDesignTimeDbContextFactory<StratosphereContext>
    {
        public StratosphereContext CreateDbContext(string[] args)
        {
            var conString = Environment.GetEnvironmentVariable("StratosphereConnectionString");
            var optionsBuilder = new DbContextOptionsBuilder<StratosphereContext>();
            optionsBuilder.UseMySql(
                conString);

            return new StratosphereContext(optionsBuilder.Options);
        }

    }
}
