﻿using Desafio.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Identity;

public class IdentityContext : IdentityDbContext<User>
{
    public DbSet<User> Users => Set<User>();

    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {

    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<User>().HasKey(x => x.Id);

    }
}
