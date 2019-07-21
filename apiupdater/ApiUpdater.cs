using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Data.Entity;
using System.Diagnostics;
using System.Threading;
using System.Data.Entity.Migrations;
using The7GATESArchive.DAL;
using The7GATESArchive.Models;

namespace The7GATESArchive
{
    public class ApiUpdater
    {
        bool Realtime = true;
        //WARNING:
        //DO NOT CHANGE UNLESS GATE IS CHANGING
        //MAKE SURE TO CHANGE KEY AMOUNTS IN TOTAL KEY CALCULATIONS!
        private int CurrentGate = 6;
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

            var searchPages = 1;
            
            var StartTime = DateTime.Now.ToString("h:mm:ss tt");
            Console.WriteLine("DB Update at " + StartTime);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Loading Page " + i);
                var usergates = new List<UserGate>();
                var users = new List<User>();
                Console.ResetColor();
                Stopwatch pageLoad = new Stopwatch();
                pageLoad.Start();
                searchPages = ProcessApiUsers(context, i, users, usergates);
                pageLoad.Stop();
                int Wait = 15 - pageLoad.Elapsed.Seconds;
                if (Wait < 0)
                {
                    Wait = 0;
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("All Data for Page " + i + " Loaded Successfully. Updating");
                users.ForEach(s => context.Users.AddOrUpdate(s));
                usergates.ForEach(s => context.UserGates.AddOrUpdate(s));
                context.SaveChanges();
                Console.WriteLine("Loaded page " + i + " successfully");
                if(Realtime == true)
                {
                    Console.WriteLine("REALTIME: Waiting " + Wait + " seconds until next page load...");
                    Thread.Sleep(Wait * 1000);
                }

            }
            var EndTime = DateTime.Now.ToString("h:mm:ss tt");
            Console.WriteLine("DB Update from " + StartTime + " to " + EndTime + " completed!");
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
                    if (Realtime == false)
                    {
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
                            if (result.gates_solved < 3 && I1 == 0)
                            {
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
                                if (rank > 500)
                                {
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
                            var NewTimePrev = context.UserGates.Where(u => u.UserID == result.uuid && u.GateID == 5).FirstOrDefault();
                            var GoBackInfo = context.UserGates.Where(u => u.UserID == result.uuid && u.GateID == 6).FirstOrDefault();
                            var G1Complete = context.UserGates.Where(u => u.UserID == result.uuid && u.GateID == 1).FirstOrDefault();
                            var G2Complete = context.UserGates.Where(u => u.UserID == result.uuid && u.GateID == 2).FirstOrDefault();
                            var G3Complete = context.UserGates.Where(u => u.UserID == result.uuid && u.GateID == 3).FirstOrDefault();
                            var G4Complete = context.UserGates.Where(u => u.UserID == result.uuid && u.GateID == 4).FirstOrDefault();
                            var G5Complete = context.UserGates.Where(u => u.UserID == result.uuid && u.GateID == 5).FirstOrDefault();
                            var KeysG1 = context.UserGates.Where(u => u.UserID == result.uuid && u.GateID == 1 && u.Keys >= 0).FirstOrDefault();
                            var KeysG2 = context.UserGates.Where(u => u.UserID == result.uuid && u.GateID == 2 && u.Keys >= 0).FirstOrDefault();
                            var KeysG3 = context.UserGates.Where(u => u.UserID == result.uuid && u.GateID == 3 && u.Keys >= 0).FirstOrDefault();
                            var KeysG4 = context.UserGates.Where(u => u.UserID == result.uuid && u.GateID == 4 && u.Keys >= 0).FirstOrDefault();
                            var KeysG5 = context.UserGates.Where(u => u.UserID == result.uuid && u.GateID == 5 && u.Keys >= 0).FirstOrDefault();
                            var PrevAllKeys = 0;
                            var ErrorGate = false;
                            var G1STime = new TimeSpan(0, 0, 0);
                            var G2STime = new TimeSpan(0, 0, 0);
                            var G3STime = new TimeSpan(0, 0, 0);
                            var G4STime = new TimeSpan(0, 0, 0);
                            var G5STime = new TimeSpan(0, 0, 0);
                            var ChangePrevTime = 0;
                            bool FirstEntry = true;
                            int Eliminate = 0;
                            bool Unsupported = false;
                            bool Stacked = false;
                            if (result.gates_solved < 4)
                            {
                                Eliminate = 5;
                            }
                            else
                            {
                                if (G1Complete != null && G1Complete.Time > new TimeSpan(1, 30, 0))
                                {
                                    Eliminate++;
                                }
                                if (G2Complete != null && G2Complete.Time > new TimeSpan(5, 1, 0))
                                {
                                    Eliminate++;
                                }
                                if (G3Complete != null && G3Complete.Time > new TimeSpan(4, 1, 0))
                                {
                                    Eliminate++;
                                }
                                if (G4Complete != null && G4Complete.Time > new TimeSpan(0, 55, 0))
                                {
                                    Eliminate++;
                                }
                                if (G5Complete != null && G5Complete.Time > new TimeSpan(3, 52, 0))
                                {
                                    Eliminate++;
                                }
                                if (GoBackInfo != null && GoBackInfo.Time > new TimeSpan(3, 52, 0))
                                {
                                    Eliminate++;
                                }
                                if (G1Complete != null && G1Complete.Time == new TimeSpan(0, 0, 0))
                                {
                                    Eliminate++;
                                }
                                if (G2Complete != null && G2Complete.Time == new TimeSpan(0, 0, 0))
                                {
                                    Eliminate++;
                                }
                                if (G3Complete != null && G3Complete.Time == new TimeSpan(0, 0, 0))
                                {
                                    Eliminate++;
                                }
                                if (G4Complete != null && G4Complete.Time == new TimeSpan(0, 0, 0))
                                {
                                    Eliminate++;
                                }
                                if (G5Complete != null && G5Complete.Time == new TimeSpan(0, 0, 0))
                                {
                                    Eliminate++;
                                }
                                if (GoBackInfo != null && GoBackInfo.Time == new TimeSpan(0, 0, 0))
                                {
                                    Eliminate++;
                                }
                            }
                            if (Eliminate >= 3)
                            {
                                Unsupported = true;
                            }
                            var userGatePrev = new UserGate
                            {
                                GateID = 9,
                                Rank = 1,
                                UserID = result.uuid,
                                Time = new TimeSpan(0, 0, 0),
                                Keys = 0,
                                CollectiveTime = new TimeSpan(0, 0, 0),
                                Percentile = 4,
                                Finished = true,
                                FirstTime = true
                            };
                            //CHANGE ABOVE VALUE TO DEFAULT KEY SET
                            if (GoBackInfo != null && GoBackInfo.CollectiveTime == TotalTime && (G1Complete != null && G2Complete != null && G3Complete != null && G4Complete != null))
                            {
                                FirstEntry = false;
                                if (KeysG1 != null && KeysG2 != null && KeysG3 != null && KeysG4 != null && KeysG5 != null)
                                {
                                    PrevAllKeys = KeysG1.Keys + KeysG2.Keys + KeysG3.Keys + KeysG4.Keys + KeysG5.Keys;
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
                                    if (System.Math.Abs(TotalKeys - PrevAllKeys) > 6)
                                    {
                                        if (result.total_keys == 29)
                                        {
                                            //OVERRRIDE OF STRICT SYSTEM
                                            ThisGateKeys = 6;
                                        }
                                        else
                                        {
                                            ThisGateKeys = 6;
                                            Stacked = true;
                                        }
                                    }
                                    if (System.Math.Abs(TotalKeys - PrevAllKeys) == 6)
                                    {
                                        ThisGateKeys = 6;
                                    }
                                    if (System.Math.Abs(TotalKeys - PrevAllKeys) == 5)
                                    {
                                        ThisGateKeys = 5;
                                    }
                                    if (System.Math.Abs(TotalKeys - PrevAllKeys) == 4)
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
                                if (G1Complete != null && G2Complete != null && G3Complete != null)
                                {
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
                                    else if (G4Complete.CollectiveTime == new TimeSpan(0, 0, 0) && G4Complete != null)
                                    {
                                        ChangePrevTime = 4;
                                        G4STime = TotalTime - GoBackInfo.CollectiveTime;
                                    }
                                    else if (G5Complete.CollectiveTime == new TimeSpan(0, 0, 0) && G5Complete != null)
                                    {
                                        ChangePrevTime = 5;
                                        G5STime = TotalTime - GoBackInfo.CollectiveTime;
                                    }
                                    TotalTime = TotalTime - GoBackInfo.CollectiveTime;
                                    //REMOVE AFTER FIRST OR SECOND USAGE!
                                }
                            }
                            else if (NewTimePrev != null)
                            {
                                if (KeysG1 != null && KeysG2 != null && KeysG3 != null && KeysG4 != null && KeysG5 != null)
                                {
                                    PrevAllKeys = KeysG1.Keys + KeysG2.Keys + KeysG3.Keys + KeysG4.Keys + KeysG5.Keys;
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
                                    if (System.Math.Abs(TotalKeys - PrevAllKeys) > 6)
                                    {
                                        if (result.total_keys == 29)
                                        {
                                            //OVERRRIDE OF STRICT SYSTEM
                                            ThisGateKeys = 6;
                                        }
                                        else
                                        {
                                            ThisGateKeys = 6;
                                            Stacked = true;
                                        }
                                    }
                                    if (System.Math.Abs(TotalKeys - PrevAllKeys) == 6)
                                    {
                                        ThisGateKeys = 6;
                                    }
                                    if (System.Math.Abs(TotalKeys - PrevAllKeys) == 5)
                                    {
                                        ThisGateKeys = 5;
                                    }
                                    if (System.Math.Abs(TotalKeys - PrevAllKeys) == 4)
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
                                if (KeysG1 != null && KeysG2 != null && KeysG3 != null && KeysG4 != null && KeysG5 != null)
                                {
                                    PrevAllKeys = KeysG1.Keys + KeysG2.Keys + KeysG3.Keys + KeysG4.Keys + KeysG5.Keys;
                                }
                                TotalTime = CollectiveTime;
                                ThisGateKeys = result.total_keys;
                            }
                            if (TimeSpan.Compare(TotalTime, new TimeSpan(3, 52, 0)) == 1)
                            {
                                ErrorGate = true;
                            }
                            if (GoBackInfo == null)
                            {
                                if (TotalTime > new TimeSpan(3, 52, 0))
                                {
                                    Stacked = true;
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
                                PrizeStatus = PrizeQM,
                                Insight1 = I1,
                                Insight2 = I2,
                                Insight3 = I3,
                                GateError = ErrorGate,
                                Unsupported = Unsupported
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
                                Stacked = Stacked

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
                                if (ChangePrevTime == 4 && G4Complete != null)
                                {
                                    userGatePrev = new UserGate
                                    {
                                        GateID = G4Complete.GateID,
                                        Rank = G4Complete.Rank,
                                        UserID = G4Complete.UserID,
                                        Time = G4STime,
                                        Keys = G4Complete.Keys,
                                        CollectiveTime = G4Complete.CollectiveTime + G1STime,
                                        Percentile = G4Complete.Percentile,
                                        Finished = G4Complete.Finished,
                                        FirstTime = G4Complete.FirstTime
                                    };
                                }
                                if (ChangePrevTime == 5 && G5Complete != null)
                                {
                                    userGatePrev = new UserGate
                                    {
                                        GateID = G5Complete.GateID,
                                        Rank = G5Complete.Rank,
                                        UserID = G5Complete.UserID,
                                        Time = G5STime,
                                        Keys = G5Complete.Keys,
                                        CollectiveTime = G5Complete.CollectiveTime + G1STime,
                                        Percentile = G5Complete.Percentile,
                                        Finished = G5Complete.Finished,
                                        FirstTime = G5Complete.FirstTime
                                    };
                                }

                            }
                            Console.WriteLine("COMPLETE: " + result.username_raw + ". (Rank #" + result.rank + ")");
                            if (Stacked == true)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("DEBUG: Stacked boolean is TRUE");
                                Console.ResetColor();
                            }
                            if (Unsupported == true)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("UNSUPPORTED: This user cannot be supported on The7GATESArchive.");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("SUPPORTED: This user has supportable data!");
                                Console.ResetColor();
                            }
                            users.Add(user);
                            usergates.Add(userGate);
                            usergates.Add(userGatePrev);
                        }
                    }
                    if (Realtime == true)
                    {
                        foreach (var result in sevengates.results)
                        {
                            var User = context.Users.Where(u => u.ID == result.uuid).FirstOrDefault();
                            if (User != null) {
                                int rank = 0;
                                if (int.TryParse(result.rank, out rank) == false)
                                {
                                    rank = sevengates.total;
                                }
                                int ThisGateKeys = 0;
                                TimeSpan ThisGateTime = new TimeSpan(0, 0, 0);
                                if (User.Realtime == false)
                                {
                                    ThisGateKeys = result.total_keys - User.Keys;
                                    ThisGateTime = new TimeSpan(0,0,0,0,result.total_time) - User.TimeForAllGates;
                                }
                                else { 
                                    var Gate1 = context.UserGates.Where(u => u.UserID == result.uuid && u.GateID == 1).FirstOrDefault();
                                    var Gate2 = context.UserGates.Where(u => u.UserID == result.uuid && u.GateID == 2).FirstOrDefault();
                                    var Gate3 = context.UserGates.Where(u => u.UserID == result.uuid && u.GateID == 3).FirstOrDefault();
                                    var Gate4 = context.UserGates.Where(u => u.UserID == result.uuid && u.GateID == 4).FirstOrDefault();
                                    var Gate5 = context.UserGates.Where(u => u.UserID == result.uuid && u.GateID == 5).FirstOrDefault();
                                    var Gate6 = context.UserGates.Where(u => u.UserID == result.uuid && u.GateID == 6).FirstOrDefault();
                                    var Gate7 = context.UserGates.Where(u => u.UserID == result.uuid && u.GateID == 7).FirstOrDefault();
                                    ThisGateKeys = Gate1.Keys + Gate2.Keys + Gate3.Keys + Gate4.Keys + Gate5.Keys + Gate6.Keys + Gate7.Keys;
                                    ThisGateTime = Gate1.Time + Gate2.Time + Gate3.Time + Gate4.Time + Gate5.Time + Gate6.Time + Gate7.Time;
                                }
                                var Gate = new UserGate
                                {
                                    GateID = 12,
                                    //GATE 12 FOR TESTING PURPOSES ONLY
                                    Rank = rank,
                                    UserID = result.uuid,
                                    Time = ThisGateTime,
                                    Keys = ThisGateKeys,
                                    CollectiveTime = new TimeSpan(0, 0, 0, 0, result.total_time),
                                };
                            }
                        }

                    }
                }
            }
            return numPages;
        }
    }
}