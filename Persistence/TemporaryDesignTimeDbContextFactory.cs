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
            var optionsBuilder = new DbContextOptionsBuilder<StratosphereContext>();
            optionsBuilder.UseMySql(
                "Server=localhost;Database=stratosphere;Uid=root;Pwd=12345;");

            return new StratosphereContext(optionsBuilder.Options);
        }

    }
}
