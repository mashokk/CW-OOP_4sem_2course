﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FoodApp
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DbRecipes : DbContext
    {
        private static DbRecipes _context;
        public DbRecipes()
            : base("name=DbRecipes")
        {
        }

        public static DbRecipes GetContext()
        {
            if (_context == null)
                _context = new DbRecipes();
            return _context;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Dish_Composition> Dish_Composition { get; set; }
        public DbSet<Dishes> Dishes { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; }
        public DbSet<Photos> Photos { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
