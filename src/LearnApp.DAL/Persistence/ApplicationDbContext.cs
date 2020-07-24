using LearnApp.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LearnApp.DAL.Persistence
{
    /// <summary>
    /// Application database context.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Person entities
        /// </summary>
        public DbSet<ApplicationUser> Users { get; set; }

        /// <summary>
        /// Card entities.
        /// </summary>
        public DbSet<Card> Cards { get; set; }

        /// <summary>
        /// Define application DB context.
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }    
    }
}
