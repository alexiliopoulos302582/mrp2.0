using Microsoft.EntityFrameworkCore;
using Mrp2.Models;

namespace Mrp2.Data
{
    public class DataContextEF:DbContext
    {

        private string _connectionString= "Data Source=.;Initial Catalog=MRP;Integrated Security=True;Encrypt=False;Trust Server Certificate=True";
        public DataContextEF(DbContextOptions<DataContextEF> options):base(options)
        {


        }





        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString, x =>
                {
                    x.EnableRetryOnFailure();
                });
            }
        }


        public DbSet<Mrp2.Models.Units> Units { get; set; } = default!;
        public DbSet<Mrp2.Models.Suppliers> Suppliers { get; set; } = default!;
        public DbSet<Mrp2.Models.Status> Status { get; set; } = default!;
        public DbSet<Mrp2.Models.Category> Category { get; set; } = default!;
        public DbSet<RawMaterial> RawMaterial { get; set; }
        public DbSet<RecipeRawMaterial> recipeRawMaterials{ get; set; }
        public DbSet<Recipe> Recipe{ get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderRawMaterial> OrderRawMaterials { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
    modelBuilder.Entity<OrderRawMaterial>().HasKey(x => new { x.OrderId, x.RawMaterialId });
    modelBuilder.Entity<OrderRawMaterial>().HasOne(x=>x.Order).WithMany(x=>x.OrderRawMaterials).HasForeignKey(x=>x.OrderId);
    modelBuilder.Entity<OrderRawMaterial>().HasOne(x=>x.RawMaterial).WithMany(x=>x.OrderRawMaterials).HasForeignKey(x=>x.RawMaterialId);






            modelBuilder.Entity<OrderRawMaterial>(entity => {
                entity.Property(x => x.Quantity).HasColumnType("decimal(18,4)");
                entity.Property(x => x.UnitCost).HasColumnType("decimal(18,4)");
               
            });

            modelBuilder.Entity<Order>()
           .HasMany(o => o.OrderRawMaterials)
           .WithOne(orm => orm.Order)
           .HasForeignKey(orm => orm.OrderId);
            modelBuilder.Entity<Order>()
          .HasOne(o => o.Recipe)
          .WithMany()
          .HasForeignKey(o => o.RecipeId);


            modelBuilder.Entity<Order>(entity => {
                entity.Property(x => x.Quantity).HasColumnType("decimal(18,4)");
                entity.Property(x => x.OrderTotalCost).HasColumnType("decimal(18,4)");
                entity.Property(x => x.RawMaterialCost).HasColumnType("decimal(18,4)");
                entity.Property(x => x.RecipeCost).HasColumnType("decimal(18,4)");
            });

            modelBuilder.Entity<RecipeRawMaterial>().HasKey(rr => new
            {
                rr.RecipeId,
                rr.RawMaterialId
            });

            modelBuilder.Entity<RecipeRawMaterial>().HasOne(r => r.Recipe)
                .WithMany(r => r.recipeRawMaterials).HasForeignKey(r => r.RecipeId);

            modelBuilder.Entity<RecipeRawMaterial>().HasOne(rr => rr.RawMaterial)
                .WithMany(rr => rr.RecipeRawMaterials).HasForeignKey(rr => rr.RawMaterialId);
            modelBuilder.Entity<RecipeRawMaterial>(entity=>
            {
            entity.Property(x => x.Quantity).HasColumnType("decimal(18,4)");
            entity.Property(x => x.Cost).HasColumnType("decimal(18,4)");

            }
                );
            modelBuilder.Entity<Recipe>(entity => {
            entity.Property(x=>x.TotalCost).HasColumnType("decimal(18,4)");
            entity.Property(x=>x.Quantity).HasColumnType("decimal(18,4)");
            });


            modelBuilder.Entity<Units>(x=>x.ToTable("Units"));
            modelBuilder.Entity<Category>(x =>
            {  x.ToTable("Category");
                x.HasKey(c => c.Id);
                x.Property(c => c.Id).ValueGeneratedOnAdd(); });

            modelBuilder.Entity<Suppliers>(x => x.ToTable("Suppliers"));
            modelBuilder.Entity<Status>(x => x.ToTable("Status"));



            modelBuilder.Entity<RawMaterial>(entity =>
            {
                entity.ToTable("RawMaterial");
                entity.HasKey(c => c.Id);
                entity.Property(x => x.Quantity).HasColumnType("decimal(18,4)");
                entity.Property(x => x.BoundQTY).HasColumnType("decimal(18,4)");
            entity.Property(x => x.ReorderPoint).HasColumnType("decimal(18,4)");
                entity.Property(x => x.SafetyStock).HasColumnType("decimal(18,4)");
                entity.Property(x => x.UnitCost).HasColumnType("decimal(18,4)");
                entity.Property(x => x.Eoq).HasColumnType("decimal(18,4)");


            });
        }


    }



   
    
}
