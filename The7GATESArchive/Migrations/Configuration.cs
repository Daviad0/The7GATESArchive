namespace The7GATESArchive.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using The7GATESArchive.Models;
    using Newtonsoft;
    using Newtonsoft.Json;
    using System.Net.Http;
    using The7GATESArchive.DAL;

    internal sealed class Configuration : DbMigrationsConfiguration<The7GATESArchive.DAL.GatewayContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;

            bool shouldUpdate = true;

            if (shouldUpdate)
            {
                /*var updater = new ApiUpdater();
                using (var context = new GatewayContext())
                {
                    updater.CreateGates(context);
                    updater.UpdateFromApi(context);
                }*/
                
            }
        }



        protected override void Seed(The7GATESArchive.DAL.GatewayContext context)
        {
            var courses = new List<Gate>
            {
            new Gate{GateID=1,Theme="Nintendo"},
            new Gate{GateID=2,Theme="Indie Games"},
            new Gate{GateID=3,Theme="Unknown"},
            new Gate{GateID=4,Theme="Unknown"},
            new Gate{GateID=5,Theme="Unknown"},
            new Gate{GateID=6,Theme="Unknown"},
            new Gate{GateID=7,Theme="Unknown"}
            };
            courses.ForEach(s => context.Gates.AddOrUpdate(s));
            context.SaveChanges();
        }
    }
}
