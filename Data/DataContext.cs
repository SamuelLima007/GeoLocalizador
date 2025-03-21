using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoLocalizador.app.models;
using Microsoft.EntityFrameworkCore;

namespace GeoLocalizador.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Cep> Ceps  { get; set; }


         protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(@"Data Source=banquinho");
        }
      
    }

   
}