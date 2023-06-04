using Microsoft.EntityFrameworkCore;

namespace OurProject2
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        { }

        public DbSet<MyModel> MyModels { get; set; }
    }

    public class MyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Other properties...
    }
}
