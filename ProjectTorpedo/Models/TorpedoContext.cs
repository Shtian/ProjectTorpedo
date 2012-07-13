using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CodeFirstMembershipSharp;

namespace ProjectTorpedo.Models
{
    public class TorpedoContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Platform> Platforms { get; set; } 
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<GamePicture> Pictures { get; set; }
        public DbSet<Loan> Loans { get; set; } 
    }
}