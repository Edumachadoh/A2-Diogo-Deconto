﻿using EnzoSottomaior;
using Microsoft.EntityFrameworkCore;

//Implementar a herança da classe DbContext
public class AppDataContext : DbContext
{
    //Informar quais as classes serão tabelas no banco de dados
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Folha> Folhas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Edumachado_EnzoSottomaior.db");
    }

}
