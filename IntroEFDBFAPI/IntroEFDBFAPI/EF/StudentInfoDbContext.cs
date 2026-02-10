using System;
using System.Collections.Generic;
using IntroEFDBFAPI.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace IntroEFDBFAPI.EF;

public partial class StudentInfoDbContext : DbContext
{
    public StudentInfoDbContext()
    {
    }

    public StudentInfoDbContext(DbContextOptions<StudentInfoDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DbConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
