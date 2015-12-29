using SmartHouse.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SmartHouse.DdAccess
{
    public class DataBaseContext : DbContext
    {
        static DataBaseContext()
        {
            Database.SetInitializer<DataBaseContext>(new DataBaseInitializer());
        }

        public DbSet<Cooker> Cookers { get; set; }
        public DbSet<Fridge> Fridges { get; set; }
        public DbSet<Lamp> Lamps { get; set; }
        public DbSet<Microwave> Microwaves { get; set; }
        public DbSet<Tv> Tvs { get; set; }
    }
}