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
        private int CurrentGate = 2;
        public void CreateGates(GatewayContext context)
        {

            var courses = new List<Gate>
            {
            new Gate{GateID=1,Theme="Nintendo",Keys=5},
            new Gate{GateID=2,Theme="Indie Games",Keys=4},
            new Gate{GateID=3,Theme="Unknown",Keys=0},
            new Gate{GateID=4,Theme="Unknown",Keys=0},
            new Gate{GateID=5,Theme="Unknown",Keys=0},
            new Gate{GateID=6,Theme="Unknown",Keys=0},
            new Gate{GateID=7,Theme="Unknown",Keys=0},
            new Gate{GateID=8,Theme="BETA GATE 1",Keys=0},
            new Gate{GateID=9,Theme="BETA GATE 2",Keys=0},
            new Gate{GateID=10,Theme="BETA GATE 3",Keys=0},
            };
            courses.ForEach(s => context.Gates.AddOrUpdate(s));
            context.SaveChanges();
        }
        public void UpdateFromApi(GatewayContext context)
        {

            var searchPages = 100;

            for (int i = 0; i < searchPages; i++)
            {
                Console.WriteLine("Loading Page " + i);
                var usergates = new List<UserGate>();
                var users = new List<User>();
                searchPages = ProcessApiUsers(context, i, users, usergates);
                Console.WriteLine("All Data for Page " + i + " Loaded Successfully. Updating");
                users.ForEach(s => context.Users.AddOrUpdate(s));
                usergates.ForEach(s => context.UserGates.AddOrUpdate(s));
                context.SaveChanges();
                Console.WriteLine("Loaded page " + i + " successfully");
                searchPages = 100;
            }
            Console.WriteLine("Leaderboard update has successfully completed! There was about " + searchPages*100 + " users processed");
            

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
                    var amount = sevengates.total;
                    foreach (var result in sevengates.results)
                    {
                        int rank = 0;
                        if (int.TryParse(result.rank, out rank) == false)
                        {
                            rank = sevengates.total;
                        }

                        

                        var CollectiveTime = new TimeSpan(0, 0, 0, 0, result.total_time);
                        var TotalTime = CollectiveTime;
                        var TotalKeys = result.total_keys;
                        float PercentFinished = 0;
                        bool Participate = true;
                        PercentFinished = (float)rank / (float)numPages;
                        PercentFinished = (float)Math.Ceiling(PercentFinished);
                        string PrizeQM = "This user is not in the percentile for a physical prize.";
                        //if (PercentFinished <= 0.001)
                        //{
                        //    PercentFinished = 0.01;
                        //}
                        if (PercentFinished >= 101.0f)
                        {
                            PercentFinished = 100.0f;
                        }
                        if (PercentFinished <= 1.0f)
                        {
                            PrizeQM = "This user IS in the percentile for a 1% prize!";
                        }
                        if (PercentFinished > 1.0f && PercentFinished <= 5.0f)
                        {
                            PrizeQM = "This user IS in the percentile for a 5% prize";
                        }
                        for (int i = 1; i < CurrentGate; i++)
                        {
                            var PreviousGate = context.UserGates.Where(u => u.UserID == result.uuid && u.GateID == i).FirstOrDefault();
                            var PreviousKeys = context.Gates.Where(u => u.GateID == i && u.Keys >= 0).FirstOrDefault();
                            if (PreviousGate != null)
                            {
                                if (TotalTime == PreviousGate.Time)
                                {
                                    Participate = false;
                                }
                                TotalTime = TotalTime - PreviousGate.Time;
                            }
                            if (PreviousKeys != null && Participate == true)
                            {
                                TotalKeys = TotalKeys - PreviousKeys.Keys;
                            }

                        }
                        var user = new User
                        {
                            ID = result.uuid,
                            Username = result.username_raw,
                            Keys = result.total_keys,
                            Rank = rank,
                            TimeForAllGates = new TimeSpan(0, 0, 0, 0, result.total_time),
                            Percentile = PercentFinished,
                            PrizeStatus = PrizeQM
                        };
                        var userGate = new UserGate
                        {
                            GateID = CurrentGate,
                            Rank = rank,
                            UserID = result.uuid,
                            Time = TotalTime,
                            Keys = TotalKeys,
                            CollectiveTime = CollectiveTime,
                            Percentile = PercentFinished
                    };

                        users.Add(user);
                        usergates.Add(userGate);
                    }
                }
            }
            return numPages;
        }
    }
}