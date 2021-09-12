using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Parse3.Models;
using System.Text.RegularExpressions;

namespace Parse3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Price> Prices { get; set; }
        public DbSet<CodeRem> CodeRems { get; set; }
        public DbSet<CostRem> CostRems { get; set; }
        public DbSet<MoveDB> MoveDBs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           
        }

    }
}
