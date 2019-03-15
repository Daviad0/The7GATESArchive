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
        //WARNING:
        //DO NOT CHANGE UNLESS GATE IS CHANGING
        //MAKE SURE TO CHANGE KEY AMOUNTS IN TOTAL KEY CALCULATIONS!
        private int CurrentGate = 4;
        //DANGER ZONE
        public void CreateGates(GatewayContext context)
        {

            var courses = new List<Gate>
            {
            new Gate{GateID=1,Theme="Nintendo",Keys=5},
            new Gate{GateID=2,Theme="Indie Games",Keys=4},
            new Gate{GateID=3,Theme="Unknown",Keys=5},
            new Gate{GateID=4,Theme="Unknown",Keys=4},
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

            var searchPages = 1000;

            for (int i = 0; i < searchPages; i++)
            {
                Console.WriteLine("Loading Page " + i);
                var usergates = new List<UserGate>();
                var users = new List<User>();
                Console.ResetColor();
                searchPages = ProcessApiUsers(context, i, users, usergates);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("All Data for Page " + i + " Loaded Successfully. Updating");
                users.ForEach(s => context.Users.AddOrUpdate(s));
                usergates.ForEach(s => context.UserGates.AddOrUpdate(s));
                context.SaveChanges();
                Console.WriteLine("Loaded page " + i + " successfully");

            }
            Console.WriteLine("Leaderboard update has successfully completed! There was about " + searchPages * 100 + " users processed");
            Console.Write("Finished");
            var ready = Console.ReadLine();

            

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

                        int I1 = 0;
                        int I2 = 0;
                        int I3 = 0;
                        if (result.gates_solved == 2 && result.total_keys < 14 && result.total_keys > 11)
                        {
                            I1 = 4;
                        }
                        if (result.gates_solved < 3 && I1 == 0){
                            if (result.gates_solved == 2)
                            {
                                I1 = 1;
                            }
                            if (result.gates_solved == 1)
                            {
                                I1 = 2;
                            }
                            if (result.gates_solved == 0)
                            {
                                I1 = 3;
                            }
                        }
                        if (!result.username_raw.ToLower().Contains('<') && !result.username_raw.ToLower().Contains('>') && rank > 150)
                        {
                            if (I1 == 0)
                            {
                                I1 = 5;
                            }
                            else
                            {
                                I2 = 5;
                            }
                        }
                        if (!result.username_raw.ToLower().Contains('<') && !result.username_raw.ToLower().Contains('>') && rank < 151)
                        {
                            if (I1 == 0)
                            {
                                I1 = 6;
                            }
                            else
                            {
                                I2 = 6;
                            }
                        }
                        if (result.gates_solved == 3)
                        {
                            if (rank > 500) { 
                                if (I1 == 0)
                                {
                                    I1 = 7;
                                }
                                else
                                {
                                    I2 = 7;
                                }
                            }
                            if (rank > 100 && rank < 501)
                            {
                                if (I1 == 0)
                                {
                                    I1 = 8;
                                }
                                else
                                {
                                    I2 = 8;
                                }
                            }
                            if (rank < 101)
                            {
                                if (I1 == 0)
                                {
                                    I1 = 0;
                                }
                                else
                                {
                                    I2 = 0;
                                }
                            }
                        }

                        var CollectiveTime = new TimeSpan(0, 0, 0, 0, result.total_time);
                        var TotalTime = CollectiveTime;
                        var TotalKeys = result.total_keys;
                        bool Complete = false;
                        float PercentFinished = 0;
                        bool Participate = true;
                        PercentFinished = (float)rank / (float)numPages;
                        PercentFinished = (float)Math.Ceiling(PercentFinished);
                        string PrizeQM = "0";
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
                            PrizeQM = "1";
                        }
                        if (PercentFinished > 1.0f && PercentFinished <= 5.0f)
                        {
                            PrizeQM = "2";
                        }
                        /*for (int i = 1; i < CurrentGate; i++)
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

                        }*/
                        var ThisGateKeys = 0;
                        var NewTimePrev = context.UserGates.Where(u => u.UserID == result.uuid && u.GateID == 3).FirstOrDefault();
                        var GoBackInfo = context.UserGates.Where(u => u.UserID == result.uuid && u.GateID == 4).FirstOrDefault();
                        var G1Complete = context.UserGates.Where(u => u.UserID == result.uuid && u.GateID == 1).FirstOrDefault();
                        var G2Complete = context.UserGates.Where(u => u.UserID == result.uuid && u.GateID == 2).FirstOrDefault();
                        var G3Complete = context.UserGates.Where(u => u.UserID == result.uuid && u.GateID == 3).FirstOrDefault();
                        var KeysG1 = context.UserGates.Where(u => u.UserID == result.uuid && u.GateID == 1 && u.Keys >= 0).FirstOrDefault();
                        var KeysG2 = context.UserGates.Where(u => u.UserID == result.uuid && u.GateID == 2 && u.Keys >= 0).FirstOrDefault();
                        var KeysG3 = context.UserGates.Where(u => u.UserID == result.uuid && u.GateID == 3 && u.Keys >= 0).FirstOrDefault();
                        var PrevAllKeys = 0;
                        var ErrorGate = false;
                        var G1STime = new TimeSpan(0, 0, 0);
                        var G2STime = new TimeSpan(0, 0, 0);
                        var G3STime = new TimeSpan(0, 0, 0);
                        var ChangePrevTime = 0;
                        bool FirstEntry = true;
                        var userGatePrev = new UserGate
                        {
                            GateID = 9,
                            Rank = 1,
                            UserID = result.uuid,
                            Time = new TimeSpan(0,0,0),
                            Keys = 0,
                            CollectiveTime = new TimeSpan(0, 0, 0),
                            Percentile = 4,
                            Finished = true,
                            FirstTime = true
                        };
                        //CHANGE ABOVE VALUE TO DEFAULT KEY SET
                        if (GoBackInfo != null && GoBackInfo.CollectiveTime == TotalTime && (G1Complete != null && G2Complete != null && G3Complete != null))
                        {
                            FirstEntry = false;
                            if (KeysG1 != null && KeysG2 != null && KeysG3 != null) { 
                                PrevAllKeys = KeysG1.Keys + KeysG2.Keys + KeysG3.Keys;
                            }
                            if (NewTimePrev != null) { 
                                if (TimeSpan.Compare(NewTimePrev.CollectiveTime, TotalTime) == 0)
                                {
                                    TotalTime = NewTimePrev.CollectiveTime - TotalTime;
                                }
                                else
                                {
                                    TotalTime = TotalTime - NewTimePrev.CollectiveTime;
                                }
                                //BE READY TO CHANGE ON GATE CHANGE
                                if (System.Math.Abs(TotalKeys - PrevAllKeys) > 3)
                                {
                                    ThisGateKeys = 4;
                                }
                                else if (System.Math.Abs(TotalKeys - PrevAllKeys) == 3)
                                {
                                    ThisGateKeys = 3;
                                }
                                else if (System.Math.Abs(TotalKeys - PrevAllKeys) == 2)
                                {
                                    ThisGateKeys = 2;
                                }
                                else if (System.Math.Abs(TotalKeys - PrevAllKeys) == 1)
                                {
                                    ThisGateKeys = 1;
                                }
                                else if (System.Math.Abs(TotalKeys - PrevAllKeys) == 0)
                                {
                                    ThisGateKeys = 0;
                                }
                            }
                        }
                        else if (GoBackInfo != null && TimeSpan.Compare(GoBackInfo.CollectiveTime, TotalTime) == -1)
                        {
                            if (G1Complete != null && G2Complete != null && G3Complete != null) { 
                                FirstEntry = false;
                                if (G1Complete.CollectiveTime == new TimeSpan(0, 0, 0) && G1Complete != null)
                                {
                                    ChangePrevTime = 1;
                                    G1STime = TotalTime - GoBackInfo.CollectiveTime;
                                }
                                else if (G2Complete.CollectiveTime == new TimeSpan(0, 0, 0) && G2Complete != null)
                                {
                                    ChangePrevTime = 2;
                                    G2STime = TotalTime - GoBackInfo.CollectiveTime;
                                }
                                else if (G3Complete.CollectiveTime == new TimeSpan(0, 0, 0) && G3Complete != null)
                                {
                                    ChangePrevTime = 3;
                                    G3STime = TotalTime - GoBackInfo.CollectiveTime;
                                }
                            }
                        }
                        else if (NewTimePrev != null)
                        {
                            if (KeysG1 != null && KeysG2 != null && KeysG3 != null)
                            {
                                PrevAllKeys = KeysG1.Keys + KeysG2.Keys + KeysG3.Keys;
                            }
                            if (NewTimePrev != null)
                            {
                                if (TimeSpan.Compare(NewTimePrev.CollectiveTime, TotalTime) == 0)
                                {
                                    TotalTime = NewTimePrev.CollectiveTime - TotalTime;
                                }
                                else
                                {
                                    TotalTime = TotalTime - NewTimePrev.CollectiveTime;
                                }
                                //BE READY TO CHANGE ON GATE CHANGE
                                if (System.Math.Abs(TotalKeys - PrevAllKeys) > 4)
                                {
                                    ThisGateKeys = 4;
                                }
                                else if (System.Math.Abs(TotalKeys - PrevAllKeys) == 3)
                                {
                                    ThisGateKeys = 3;
                                }
                                else if (System.Math.Abs(TotalKeys - PrevAllKeys) == 2)
                                {
                                    ThisGateKeys = 2;
                                }
                                else if (System.Math.Abs(TotalKeys - PrevAllKeys) == 1)
                                {
                                    ThisGateKeys = 1;
                                }
                                else if (System.Math.Abs(TotalKeys - PrevAllKeys) == 0)
                                {
                                    ThisGateKeys = 0;
                                }
                            }
                        }
                        else if (NewTimePrev == null)
                        {
                            if (KeysG1 != null && KeysG2 != null && KeysG3 != null)
                            {
                                PrevAllKeys = KeysG1.Keys + KeysG2.Keys + KeysG3.Keys;
                            }
                            TotalTime = CollectiveTime;
                            ThisGateKeys = result.total_keys;
                        }
                        if (TimeSpan.Compare(TotalTime, new TimeSpan(0,55,0)) == 1)
                        {
                            ErrorGate = true;
                        }
                        var user = new User
                        {
                            ID = result.uuid,
                            Username = result.username_raw,
                            Keys = result.total_keys,
                            Rank = rank,
                            TimeForAllGates = new TimeSpan(0, 0, 0, 0, result.total_time),
                            Percentile = PercentFinished,
                            PrizeStatus = PrizeQM,
                            Insight1 = I1,
                            Insight2 = I2,
                            Insight3 = I3,
                            GateError = ErrorGate
                        };
                        var userGate = new UserGate
                        {
                            GateID = CurrentGate,
                            Rank = rank,
                            UserID = result.uuid,
                            Time = TotalTime,
                            Keys = ThisGateKeys,
                            CollectiveTime = CollectiveTime,
                            Percentile = PercentFinished,
                            Finished = Complete,
                            FirstTime = FirstEntry,

                    };
                        if (ChangePrevTime != 0)
                        {
                            if (ChangePrevTime == 1 && G1Complete != null)
                            {
                                userGatePrev = new UserGate
                                {
                                    GateID = G1Complete.GateID,
                                    Rank = G1Complete.Rank,
                                    UserID = G1Complete.UserID,
                                    Time = G1STime,
                                    Keys = G1Complete.Keys,
                                    CollectiveTime = G1Complete.CollectiveTime + G1STime,
                                    Percentile = G1Complete.Percentile,
                                    Finished = G1Complete.Finished,
                                    FirstTime = G1Complete.FirstTime
                                };
                            }
                            if (ChangePrevTime == 2 && G2Complete != null)
                            {
                                userGatePrev = new UserGate
                                {
                                    GateID = G2Complete.GateID,
                                    Rank = G2Complete.Rank,
                                    UserID = G2Complete.UserID,
                                    Time = G2STime,
                                    Keys = G2Complete.Keys,
                                    CollectiveTime = G2Complete.CollectiveTime + G1STime,
                                    Percentile = G2Complete.Percentile,
                                    Finished = G2Complete.Finished,
                                    FirstTime = G2Complete.FirstTime
                                };
                            }
                            if (ChangePrevTime == 3 && G3Complete != null)
                            {
                                userGatePrev = new UserGate
                                {
                                    GateID = G3Complete.GateID,
                                    Rank = G3Complete.Rank,
                                    UserID = G3Complete.UserID,
                                    Time = G3STime,
                                    Keys = G3Complete.Keys,
                                    CollectiveTime = G3Complete.CollectiveTime + G1STime,
                                    Percentile = G3Complete.Percentile,
                                    Finished = G3Complete.Finished,
                                    FirstTime = G3Complete.FirstTime
                                };
                            }

                        }
                        Console.WriteLine("COMPLETE: " + result.username_raw);
                        if (ErrorGate == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("FAILURE: Gate specific data produced an error");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("NORMAL: Gate specific data successfully calculated");
                            Console.ResetColor();
                        }
                        users.Add(user);
                        usergates.Add(userGate);
                        usergates.Add(userGatePrev);
                    }
                }
            }
            return numPages;
        }
    }
}