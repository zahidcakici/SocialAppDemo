using System;
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data
{
    public class SocialContext:DbContext
    {
        public SocialContext()
        {

        }

        public SocialContext(DbContextOptions<SocialContext> options):base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
