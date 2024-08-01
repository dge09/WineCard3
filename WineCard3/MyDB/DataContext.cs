using Microsoft.EntityFrameworkCore;
using WineCard3.MyDB.Enities;

namespace WineCard3.MyDB
{
    public class DataContext : DbContext
    {
        private readonly string conStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WineCard3;Integrated Security=True;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(conStr);
        }

        public virtual DbSet<Wine> Wines  { get; set; }
        public virtual DbSet<Card> Cards  { get; set; }
        public virtual DbSet<Origin> Origins  { get; set; }
        public virtual DbSet<Style> Styles  { get; set; }
    }
}
