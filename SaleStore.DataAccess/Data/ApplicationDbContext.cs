using Microsoft.EntityFrameworkCore;
using SaleStore.Model;

namespace SaleStore.Data
{
    // The class which we use to access EF
    public class ApplicationDbContext : DbContext
    {
        //Basic Configuration
        // We inject/configure this dbcontext by getting the connection string as a parameter in the constructor in order to pass the connection 
        // string to this class as a dbcontext option and that we'll be passing on to the base class 
        // This way, whatever options we configured in this constructor will be passed on to our base class DbContext
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // To create tables in our database
        // First create a model
        // Set a related DbSet in this class
        // add migration from nuget package console : add-migration hellosomething
        // to remove a migration : remove-migration //(before update-database)
        // in same package manager, run : update-database
        // yay magic... not really
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        // Overriding our data model by adding default values to our db
        // Every first time our application gets implemented data columns that defined here will be implemented to our database as well
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { ID = 1, Name = "Action", DisplayOrder = 1 },
                new Category { ID = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { ID = 3, Name = "History", DisplayOrder = 3 }
            );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ID = 1,
                    Title = "Lorem",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    ISBN = "ABCDEF1234",
                    Author = "John Smith",
                    ListPrice = 100,
                    Price = 80,
                    Price50 = 65,
                    Price100 = 60,
                    CategoryID = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    ID = 2,
                    Title = "Ipsum",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit sed do eiusmod.",
                    ISBN = "ZXCVB0987Y",
                    Author = "Sarah Johnson",
                    ListPrice = 120,
                    Price = 90,
                    Price50 = 75,
                    Price100 = 70,
                    CategoryID = 1,
                    ImageUrl = ""
                },
                new Product
                {
                    ID = 3,
                    Title = "Dolor",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit sed do eiusmod tempor.",
                    ISBN = "QWERT5678P",
                    Author = "David Brown",
                    ListPrice = 80,
                    Price = 60,
                    Price50 = 55,
                    Price100 = 52,
                    CategoryID = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    ID = 4,
                    Title = "Sit",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit sed.",
                    ISBN = "ASDFG1234H",
                    Author = "Emily Davis",
                    ListPrice = 140,
                    Price = 110,
                    Price50 = 100,
                    Price100 = 95,
                    CategoryID = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    ID = 5,
                    Title = "Amet",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit sed do eiusmod tempor incididunt.",
                    ISBN = "JKLPO6789I",
                    Author = "Michael Wilson",
                    ListPrice = 70,
                    Price = 55,
                    Price50 = 50,
                    Price100 = 48,
                    CategoryID = 3,
                    ImageUrl = ""
                },
                new Product
                {
                    ID = 6,
                    Title = "Consectetur",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ISBN = "MNBVC4321U",
                    Author = "Jessica Thompson",
                    ListPrice = 130,
                    Price = 100,
                    Price50 = 90,
                    Price100 = 85,
                    CategoryID = 3,
                    ImageUrl = ""
                }
            );
        }
    }
}
