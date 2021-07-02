using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using PublicApiExercise.Models;

namespace PublicApiExercise.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PublicApiExercise.Models.Deneme> Deneme { get; set; }
        public DbSet<PublicApiExercise.Models.Genre> Genre { get; set; }
        public DbSet<PublicApiExercise.Models.Movie> Movie { get; set; }
        public DbSet<PublicApiExercise.Models.Film> Film { get; set; }
    }
}
