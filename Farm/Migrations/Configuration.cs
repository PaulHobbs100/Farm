namespace Farm.Migrations
{
    using System.Data.Entity.Migrations;
    
    internal sealed class Configuration : DbMigrationsConfiguration<Farm.Models.Context.FarmContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Farm.Models.Context.FarmContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
