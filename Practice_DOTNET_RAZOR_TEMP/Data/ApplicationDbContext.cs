﻿using Microsoft.EntityFrameworkCore;
using Practice_DOTNET_RAZOR_TEMP.Models;

namespace Practice_DOTNET_RAZOR_TEMP.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(

                  new Category { Id = 1, Name = "Books", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Electronics", DisplayOrder = 2 }
            );
        }
    }
    }

