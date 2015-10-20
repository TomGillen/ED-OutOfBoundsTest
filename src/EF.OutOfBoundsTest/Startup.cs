using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace EF.OutOfBoundsTest
{
    public class Startup
    {
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework()
                .AddInMemoryDatabase()
                .AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase());
        }

        public void Configure(IApplicationBuilder app, DatabaseContext db)
        {
            var count = db.A.Count();
        }
    }

    public class DatabaseContext : DbContext
    {
        public DbSet<A> A { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<A>();
            modelBuilder.Entity<B>();
            modelBuilder.Entity<C>();
        }
    }

    public abstract class A
    {
        [Key]
        public int Key { get; set; }
    }

    public class B : A
    {
        public int DataB { get; set; }
    }

    public class C : A
    {
        public int DataC { get; set; }
    }
}
