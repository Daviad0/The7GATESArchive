using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using The7GATESArchive.DAL;
using The7GATESArchive.Models;

namespace The7GATESArchive
{
    public class ApiUpdater
    {
        public void CreateGates(GatewayContext context)
        {

            var courses = new List<Gate>
            {
            new Gate{GateID=1,Theme="Nintendo"},
            new Gate{GateID=2,Theme="Indie Games"},
            new Gate{GateID=3,Theme="Unknown"},
            new Gate{GateID=4,Theme="Unknown"},
            new Gate{GateID=5,Theme="Unknown"},
            new Gate{GateID=6,Theme="Unknown"},
            new Gate{GateID=7,Theme="Unknown"},
            new Gate{GateID=8,Theme="BETA GATE 1"},
            new Gate{GateID=9,Theme="BETA GATE 2"},
            new Gate{GateID=10,Theme="BETA GATE 3"},
            };
            courses.ForEach(s => context.Gates.AddOrUpdate(s));
            context.SaveChanges();
        }
        public void UpdateFromApi(GatewayContext context)
        {
            var usergates = new List<UserGate>();
            var users = new List<User>();
            var searchPages = ProcessApiUsers(context, 0, users, usergates);

            for (int i = 1; i < searchPages; i++)
            {
                searchPages = ProcessApiUsers(context, i, users, usergates);
            }
            users.ForEach(s => context.Users.AddOrUpdate(s));
            usergates.ForEach(s => context.UserGates.AddOrUpdate(s));
            context.SaveChanges();

        }
        public int ProcessApiUsers(GatewayContext context, int page, ICollection<User> users, ICollection<UserGate> usergates)
        {
            int numPages = 0;
            using (var client = new HttpClient())
            {
                var response = client.GetAsync("https://api.thetheoristgateway.com/args/265048a8-8521-4e9c-84c7-1de081efe0a4/leaderboard?page=" + page).Result;

                if (response.IsSuccessStatusCode)
                {

                    var responseContent = response.Content;

                    // by calling .Result you are synchronously reading the result
                    string responseString = responseContent.ReadAsStringAsync().Result;

                    var sevengates = JsonConvert.DeserializeObject<apiresults>(responseString);
                    numPages = sevengates.total / 100;
                    foreach (var result in sevengates.results)
                    {
                        var user = new User { ID = result.uuid, Username = result.username_raw, Keys = result.total_keys };
                        int rank = 0;
                        if (int.TryParse(result.rank, out rank) == false)
                        {
                            rank = sevengates.total;
                        }
                        var userGate = new UserGate { GateID = 1, Rank = rank, UserID = result.uuid, Time = new TimeSpan(0, 0, 0, 0, result.total_time) };
                        users.Add(user);
                        usergates.Add(userGate);
                    }
                    

                }
            }
            return numPages;
        }
    }
}