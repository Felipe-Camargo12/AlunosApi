﻿using AlunosApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlunosApi.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }

        public DbSet<Aluno> Alunos { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Aluno>().HasData(
        //        new Aluno
        //        {
        //            Id = 1,
        //            Name = "Maria da Penha",
        //            Email = "mariapenha@yahoo.com",
        //            Idade = 23
        //        },
        //        new Aluno
        //        {
        //            Id = 2,
        //            Name = "João Pedro",
        //            Email = "joaopedro@gmail.com",
        //            Idade = 20
        //        }
        //        );
        //}
    }
}
