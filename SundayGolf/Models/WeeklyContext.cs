using System.Data.Entity;

namespace SundayGolf.Models
{
    public class WeeklyContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<SundayGolf.Models.WeeklyContext>());

        public WeeklyContext() : base("SunGolfDB")
        {
        }

        public DbSet<Weekly>   Weeklies  { get; set; }
        public DbSet<Course>   Courses   { get; set; }
        public DbSet<Golfer>   Golfers   { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
    }
}
