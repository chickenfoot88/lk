namespace WebAppAuth.Migrations
{
    using System.Data.Entity.Migrations;
    using Context;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
        }
    }
}
