using Microsoft.EntityFrameworkCore;
using CsvApi.Domain;

namespace CsvApi.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<Process> Processes { get; set; }
    public DbSet<Operation> Operations { get; set; }
    public DbSet<Result> Results { get; set; }

    public AppDbContext(Env env) : base(CreateOptions(env)) { }

    private static DbContextOptions<AppDbContext> CreateOptions(Env env)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql(env.GetPostgresConnectionString());
        return optionsBuilder.Options;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Process>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired();

            entity.ToTable("Processes");
        });


        modelBuilder.Entity<Operation>(entity =>
        {
            entity.HasKey(o => o.Id);
            entity.Property(o => o.ProcessId).IsRequired();
            entity.Property(o => o.StartDate)
                .IsRequired()
                .HasConversion(
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                );
            entity.Property(o => o.ExecutionTimeSeconds).IsRequired();
            entity.Property(o => o.Value).IsRequired();

            entity.HasOne<Process>()
                 .WithMany()
                 .HasForeignKey(o => o.ProcessId)
                 .OnDelete(DeleteBehavior.Cascade);

            entity.ToTable("Operations");
        });

        modelBuilder.Entity<Result>(entity =>
       {
           entity.HasKey(r => r.Id);

           entity.Property(r => r.ProcessId)
               .IsRequired();

           entity.Property(r => r.DeltaTime)
               .IsRequired()
               .HasColumnType("double precision");

           entity.Property(r => r.FirstOperationTime)
               .IsRequired()
               .HasConversion(
                   v => DateTime.SpecifyKind(v, DateTimeKind.Utc),
                   v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
               );

           entity.Property(r => r.AverageExecutionTime)
               .IsRequired()
               .HasColumnType("double precision");

           entity.Property(r => r.ValueMean)
               .IsRequired()
               .HasColumnType("double precision");

           entity.Property(r => r.ValueMedian)
               .IsRequired()
               .HasColumnType("double precision");

           entity.Property(r => r.ValueMin)
               .IsRequired()
               .HasColumnType("double precision");

           entity.Property(r => r.ValueMax)
               .IsRequired()
               .HasColumnType("double precision");

           entity.HasOne<Process>()
                 .WithMany()
                 .HasForeignKey(r => r.ProcessId)
                 .OnDelete(DeleteBehavior.Cascade);

           entity.ToTable("Results");
       });
    }
}
