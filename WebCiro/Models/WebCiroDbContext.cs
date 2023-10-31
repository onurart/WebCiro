﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebCiro.Models.Authentication;

namespace WebCiro.Models
{
    public class WebCiroDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public WebCiroDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<NET_001_CIRO> NET_001_CIRO { get; set; }
    }
}