using AdoNetExamples.EF.Es1.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AdoNetExamples.EF.Es1.Data
{
    public class PizzeriaDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.LogTo(s => Debug.WriteLine(s));
            builder.UseMySQL("Server=localhost;database=pizzeria;username=root;password=123456abc!;port=3306");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Composition>().HasKey(x => new {x.CodePizza,x.CodeIngredient});
        }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Composition> Compositions { get; set; }
    }
}
