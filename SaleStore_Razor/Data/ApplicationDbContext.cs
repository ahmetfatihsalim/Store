﻿using Microsoft.EntityFrameworkCore;
using SaleStore_Razor.Models;

namespace SaleStore_Razor.Data
{
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
        // in same package manager, run : update-database
        // yay magic... not really
        public DbSet<Category> Categories { get; set; }

        // Overriding our data model by adding default values to our db
        // Every first time our application gets implemented data columns that defined here will be implemented to our database as well
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { ID = 1, Name = "Action", DisplayOrder = 1 },
                new Category { ID = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { ID = 3, Name = "History", DisplayOrder = 3 }
            );
        }
    }
}
