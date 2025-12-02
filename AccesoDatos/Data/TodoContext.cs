using Microsoft.EntityFrameworkCore;
using Dominio.Entidades;

namespace AccesoDatos.Data;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options)
        : base(options) { }

    public DbSet<List> TodoList { get; set; } = default!;
    public DbSet<Item> TodoItems { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<List>(entity =>
        {
            entity.ToTable("List");
            entity.HasKey(l => l.Id); 
            entity.Property(l => l.Id)
                  .HasColumnName("id")
                  .HasColumnType("bigint");

            entity.Property(l => l.Name)
                  .HasColumnName("name")
                  .HasColumnType("character varying")
                  .IsRequired();
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.ToTable("Item");
            entity.HasKey(i => i.Id);
            entity.Property(i => i.Id)
                  .HasColumnName("id")
                  .HasColumnType("bigint");

            entity.Property(i => i.Title)
                  .HasColumnName("Name") 
                  .HasColumnType("character varying")
                  .IsRequired();

            entity.Property(i => i.Description)
                  .HasColumnName("Description")
                  .HasColumnType("character varying");

            entity.Property(i => i.IsComplete)
                  .HasColumnName("IsComplete")
                  .HasColumnType("boolean")
                  .HasDefaultValue(false);

            entity.Property(i => i.ListId)
                  .HasColumnName("ListId")
                  .HasColumnType("bigint");

            entity.HasOne(i => i.List)
                  .WithMany(l => l.Items)
                  .HasForeignKey(i => i.ListId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
