using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    /// <summary>
    /// Context: Db tabloları ile proje classlarını bağlamak.
    /// </summary>
    public class GradingSystemDbContext : DbContext
    {
        /// <summary>
        /// projenin hangi db ile ilişkilendirileceğinin bulunduğu yer.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=GradingSystemDb;Trusted_Connection=true");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<UserSection> UserSections { get; set; }
    }
}
