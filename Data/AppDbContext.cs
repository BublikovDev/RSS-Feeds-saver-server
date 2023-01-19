using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;
using TestTask.Models;

namespace TestTask.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<Channel> Channels { get; set; }
        public DbSet<Enclosure> Enclosures { get; set; }
        public DbSet<Models.Guid> Guids { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Rss> Rsses { get; set; }
        public DbSet<Source> Sources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rss>()
                .HasOne(r => r.Channel)
                .WithOne(c => c.Rss)
                .HasForeignKey<Channel>(c => c.RssId);

            modelBuilder.Entity<Channel>()
                .HasMany(c => c.Items)
                .WithOne(i => i.Channel)
                .HasForeignKey(i => i.ChannelId);

            modelBuilder.Entity<Item>()
                .HasOne(i => i.Guid)
                .WithOne(g => g.Item)
                .HasForeignKey<Models.Guid>(g => g.ItemId);

            modelBuilder.Entity<Item>()
                .HasOne(i => i.Source)
                .WithOne(s => s.Item)
                .HasForeignKey<Source>(s => s.ItemId);

            modelBuilder.Entity<Item>()
                .HasOne(i => i.Enclosure)
                .WithOne(e => e.Item)
                .HasForeignKey<Enclosure>(e => e.ItemId);

        }
    }
}