using SmartHouse.Models;
using SmartHouse.Models.DevicePropStates;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SmartHouse.DdAccess
{
    // CreateDatabaseIfNotExists
    // DropCreateDatabaseAlways
    public class DataBaseInitializer : DropCreateDatabaseAlways<DataBaseContext>
    {
        protected override void Seed(DataBaseContext context)
        {
            context.Cookers.Add(new Cooker { Id = 1, IsOn = true, Temp1 = 0, Temp2 = 200, Temp3 = 150, Temp4 = 100 });
            context.Cookers.Add(new Cooker { Id = 2, IsOn = true, Temp1 = 10, Temp2 = 100, Temp3 = 50, Temp4 = 200 });
            context.Fridges.Add(new Fridge { Id = 1, IsOn = true, Temp1 = -5, Temp2 = 4 });
            context.Fridges.Add(new Fridge { Id = 2, IsOn = true, Temp1 = -10, Temp2 = 10 });
            context.Lamps.Add(new Lamp { Id = 1, IsOn = true, Bright = 20 });
            context.Lamps.Add(new Lamp { Id = 2, IsOn = true, Bright = 80 });
            context.Microwaves.Add(new Microwave { Id = 1, IsOn = true, Grill = GrillStates.Off, Power = 150 });
            context.Microwaves.Add(new Microwave { Id = 2, IsOn = true, Grill = GrillStates.Max, Power = 450 });
            context.Tvs.Add(new Tv { Id = 1, IsOn = true, Channel = Channels.BBCNews, Volume = 2 });
            context.Tvs.Add(new Tv { Id = 2, IsOn = true, Channel = Channels.HitList, Volume = 10 });
            context.SaveChanges();
        }
    }
}