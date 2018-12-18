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

    internal sealed class Configuration : DbMigrationsConfiguration<The7GATESArchive.DAL.GatewayContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(The7GATESArchive.DAL.GatewayContext context)
        {
            /*
            int currentgate = 2;
            string json = @"";

            sevengates m = JsonConvert.DeserializeObject<sevengates>(json);

            string name = m.username_raw;
            */
            var students = new List<User>
            {
            new User{ID=new Guid("9b7a6663-009d-4563-a6ef-8934976547dd"), Username="This could be you <and your team>",Keys=3},
            new User{ID=new Guid("ab442949-a45a-4d28-9944-f6aab7deb63c"), Username="Steph",Keys=4},
            new User{ID=new Guid("4d69215d-5c82-4304-818a-bbe196d94a50"), Username="MatPat <Why Am I Here?>",Keys=1},
            new User{ID=new Guid("51107863-4216-40ab-a61c-dd1384cd9dad"), Username="Jason <Blame Jason>",Keys=5},
            new User{ID=new Guid("13dcdc49-1296-4c08-b4cd-befd457a5067"), Username="Chris",Keys=1},
            new User{ID=new Guid("d8be0f78-395c-444c-96a3-36ed2f27e360"), Username="Dan <Cybert>",Keys=2},
            new User{ID=new Guid("70d78475-8ced-4f71-9c43-d6f99c7f1cd9"), Username="Daviado <Yes even in the mock i make myself this>",Keys=4},
            new User{ID=new Guid("164fdae4-5f69-455d-9ddd-bcaeaaa8b0a5"), Username="Retro <He's cool>",Keys=3},
            new User{ID=new Guid("b13f1525-00d3-4400-9f7a-3a23e64cf6be"), Username="Cara <Nice>",Keys=3},
            new User{ID=new Guid("e2a14a15-335f-4d9b-ba3c-cd562f01fbec"), Username="The Lion",Keys=4},            
            new User{ID=new Guid("77727fb1-eb54-4967-9f16-8363705eceed"), Username="Silox <Gotta Put Him In>",Keys=2},
            new User{ID=new Guid("07951aac-53da-4ce0-abe0-b50b6788e077"), Username="CreatorINK <Fix thetheoristgateway plz>",Keys=4},
            new User{ID=new Guid("34d7b06d-f400-4966-9f74-1cda1e19074b"), Username="ARGTheorists <Wait I mixed this up>",Keys=1},
            new User{ID=new Guid("8f529ddf-c5d7-4f3a-ba05-53374c47aa8d"), Username="The Riddler",Keys=1},
            new User{ID=new Guid("99a82d48-75cf-4290-a20b-7dbb4286f703"), Username="Konto <Konto Gang>",Keys=5},
            new User{ID=new Guid("543109d4-5ac6-4435-b639-c69e5853d2c7"), Username="Peridot <Fastest Photoshop in the West>",Keys=4},
            new User{ID=new Guid("519d880f-ce27-4f95-9360-61e1ff76607c"), Username="EMOD <Mod?>",Keys=3},
            new User{ID=new Guid("166d30b8-85fc-47dd-9b88-6aef2c865a63"), Username="Sam",Keys=2},
            new User{ID=new Guid("b4b8c196-fe50-4751-a77e-44afb60fc843"), Username="Duarpeto",Keys=4},
            new User{ID=new Guid("8d0fafb1-5e0c-40d1-ba15-73f60511dbe3"), Username="Blanners <Anime Weeb>",Keys=1},
            new User{ID=new Guid("1026b568-8cd6-456a-87aa-71f5fb290591"), Username="Boterle <A robot>",Keys=2},
            new User{ID=new Guid("38357448-8e2d-4289-8433-64313221e7f1"), Username="Chronic <Mom>",Keys=5},
            new User{ID=new Guid("97377ae7-94d8-4b55-aadd-cbb529759288"), Username="Xilas <The King Furry>",Keys=5},
            new User{ID=new Guid("fa07b031-9b46-463a-9c53-1a53a9e30e18"), Username="Cazards",Keys=3},
            new User{ID=new Guid("fa6d36b2-66c1-4acb-8da9-2ca009ee3622"), Username="Debbles",Keys=1},
            new User{ID=new Guid("247f4e54-9b5c-4993-8acf-26dbce8b0e4c"), Username="Obleguy",Keys=3},
            new User{ID=new Guid("0f0bb886-4881-41fe-91a0-b820ae4bbf9b"), Username="Vossy <Part of the AT Team>",Keys=2},
            new User{ID=new Guid("59efc77f-297d-4d7f-8885-69a1b7157ade"), Username="ChaoticKoala <The one GP that has Get Help>",Keys=3},
            new User{ID=new Guid("2de94b9b-8aa8-4363-8374-6b7479d3aeba"), Username="Approven't",Keys=2},
            new User{ID=new Guid("ae566cbe-75a3-45c8-871d-b5ec1bf7c8df"), Username="santa",Keys=2},
            new User{ID=new Guid("b1b333c7-8c9e-431d-aabb-0110db68ed45"), Username="Shroom",Keys=4},
            new User{ID=new Guid("7fa8ce25-c28d-414a-8755-90c154328399"), Username="Jacob Alexander",Keys=3},
            new User{ID=new Guid("4b3b05df-b36a-4aff-83de-f63634814469"), Username="BIRB",Keys=5},
            new User{ID=new Guid("833d7378-1b44-445b-ad28-f4f58db9c931"), Username="Mee6 <Oh yeah im back>",Keys=2},
            new User{ID=new Guid("d1092c37-fda6-4934-8ae7-7c399bd427e6"), Username="Tatsukami <Hey ignore my friend mee6 uwu>",Keys=4},
            new User{ID=new Guid("ffaf4954-5763-4b24-9202-6dc0c344d06f"), Username="Hey look a new API token for everyone",Keys=5},
            new User{ID=new Guid("091fb91b-cca1-4711-a60b-f7072b5e7280"), Username="Zapdos26 <He's also cool>",Keys=2},
            new User{ID=new Guid("d7899c57-100f-4978-aae9-f57792658c69"), Username="Octo <Worked so hard on the MT>",Keys=3},
            new User{ID=new Guid("a5407ee2-21a6-47ec-83bb-f0a956e3647c"), Username="Get Game Theory Merch <SO COMFY>",Keys=5},
            new User{ID=new Guid("b8f6e6b2-9606-4b72-a5be-938ebc987dcd"), Username="I just noticed I have to make the keys and stuff",Keys=4},


            };

            
            students.ForEach(s => context.Users.AddOrUpdate(s));
            context.SaveChanges();
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
            
            
            var enrollments = new List<UserGate>
            {
            new UserGate{UserGateID=1,  GateID=1, UserID=new Guid("9b7a6663-009d-4563-a6ef-8934976547dd"), Time=new TimeSpan(0, 25, 30)},
            new UserGate{UserGateID=2,  GateID=1, UserID=new Guid("ab442949-a45a-4d28-9944-f6aab7deb63c"), Time=new TimeSpan(0, 10, 5)},
            new UserGate{UserGateID=3,  GateID=1, UserID=new Guid("4d69215d-5c82-4304-818a-bbe196d94a50"), Time=new TimeSpan(1, 1, 3)},
            new UserGate{UserGateID=4,  GateID=1, UserID=new Guid("51107863-4216-40ab-a61c-dd1384cd9dad"), Time=new TimeSpan(0, 30, 29)},
            new UserGate{UserGateID=5,  GateID=1, UserID=new Guid("13dcdc49-1296-4c08-b4cd-befd457a5067"), Time=new TimeSpan(0, 30, 30)},
            new UserGate{UserGateID=6,  GateID=1, UserID=new Guid("d8be0f78-395c-444c-96a3-36ed2f27e360"), Time=new TimeSpan(23, 59, 59)},
            new UserGate{UserGateID=7,  GateID=1, UserID=new Guid("70d78475-8ced-4f71-9c43-d6f99c7f1cd9"), Time=new TimeSpan(0, 5, 1)},
            new UserGate{UserGateID=8,  GateID=1, UserID=new Guid("164fdae4-5f69-455d-9ddd-bcaeaaa8b0a5"), Time=new TimeSpan(2, 7, 1)},
            new UserGate{UserGateID=9,  GateID=1, UserID=new Guid("b13f1525-00d3-4400-9f7a-3a23e64cf6be"), Time=new TimeSpan(5, 2, 43)},/*Lion */
            new UserGate{UserGateID=10,  GateID=1, UserID=new Guid("e2a14a15-335f-4d9b-ba3c-cd562f01fbec"), Time=new TimeSpan(0, 3, 12)},
            new UserGate{UserGateID=11,  GateID=1, UserID=new Guid("77727fb1-eb54-4967-9f16-8363705eceed"),Time=new TimeSpan(0, 21, 32)},
            new UserGate{UserGateID=12,  GateID=1, UserID=new Guid("07951aac-53da-4ce0-abe0-b50b6788e077"),Time=new TimeSpan(1, 4, 3)},
            new UserGate{UserGateID=13,  GateID=1, UserID=new Guid("34d7b06d-f400-4966-9f74-1cda1e19074b"),Time=new TimeSpan(0, 45, 21)},
            new UserGate{UserGateID=14,  GateID=1, UserID=new Guid("8f529ddf-c5d7-4f3a-ba05-53374c47aa8d"),Time=new TimeSpan(0, 53, 30)},
            new UserGate{UserGateID=15,  GateID=1, UserID=new Guid("99a82d48-75cf-4290-a20b-7dbb4286f703"),Time=new TimeSpan(0, 21, 20)},
            new UserGate{UserGateID=16,  GateID=1, UserID=new Guid("543109d4-5ac6-4435-b639-c69e5853d2c7"),Time=new TimeSpan(0, 23, 48)},
            new UserGate{UserGateID=17,  GateID=1, UserID=new Guid("519d880f-ce27-4f95-9360-61e1ff76607c"),Time=new TimeSpan(0, 59, 23)},
            new UserGate{UserGateID=18,  GateID=1, UserID=new Guid("166d30b8-85fc-47dd-9b88-6aef2c865a63"),Time=new TimeSpan(1, 11, 45)},
            new UserGate{UserGateID=19,  GateID=1, UserID=new Guid("b4b8c196-fe50-4751-a77e-44afb60fc843"),Time=new TimeSpan(0, 21, 58)},
            new UserGate{UserGateID=20,  GateID=1, UserID=new Guid("8d0fafb1-5e0c-40d1-ba15-73f60511dbe3"),Time=new TimeSpan(0, 34, 27)},
            new UserGate{UserGateID=21,  GateID=1, UserID=new Guid("1026b568-8cd6-456a-87aa-71f5fb290591"),Time=new TimeSpan(2, 12, 23)},
            new UserGate{UserGateID=22,  GateID=1, UserID=new Guid("38357448-8e2d-4289-8433-64313221e7f1"),Time=new TimeSpan(0, 56, 37)},
            new UserGate{UserGateID=23,  GateID=1, UserID=new Guid("97377ae7-94d8-4b55-aadd-cbb529759288"),Time=new TimeSpan(0, 45, 27)},
            new UserGate{UserGateID=24,  GateID=1, UserID=new Guid("fa07b031-9b46-463a-9c53-1a53a9e30e18"),Time=new TimeSpan(0, 24, 18)},
            new UserGate{UserGateID=25,  GateID=1, UserID=new Guid("fa6d36b2-66c1-4acb-8da9-2ca009ee3622"),Time=new TimeSpan(2, 10, 5)},
            new UserGate{UserGateID=26,  GateID=1, UserID=new Guid("247f4e54-9b5c-4993-8acf-26dbce8b0e4c"),Time=new TimeSpan(0, 40, 40)},
            new UserGate{UserGateID=27,  GateID=1, UserID=new Guid("0f0bb886-4881-41fe-91a0-b820ae4bbf9b"),Time=new TimeSpan(0, 2, 0)},
            new UserGate{UserGateID=28,  GateID=1, UserID=new Guid("59efc77f-297d-4d7f-8885-69a1b7157ade"),Time=new TimeSpan(1, 23, 20)},
            new UserGate{UserGateID=29,  GateID=1, UserID=new Guid("2de94b9b-8aa8-4363-8374-6b7479d3aeba"),Time=new TimeSpan(0, 53, 55)},
            new UserGate{UserGateID=30,  GateID=1, UserID=new Guid("ae566cbe-75a3-45c8-871d-b5ec1bf7c8df"),Time=new TimeSpan(0, 33, 21)},
            new UserGate{UserGateID=31,  GateID=1, UserID=new Guid("b1b333c7-8c9e-431d-aabb-0110db68ed45"),Time=new TimeSpan(0, 24, 51)},
            new UserGate{UserGateID=32,  GateID=1, UserID=new Guid("7fa8ce25-c28d-414a-8755-90c154328399"),Time=new TimeSpan(1, 18, 23)},
            new UserGate{UserGateID=33,  GateID=1, UserID=new Guid("4b3b05df-b36a-4aff-83de-f63634814469"),Time=new TimeSpan(0, 58, 43)},
            new UserGate{UserGateID=34,  GateID=1, UserID=new Guid("833d7378-1b44-445b-ad28-f4f58db9c931"),Time=new TimeSpan(0, 47, 41)},
            new UserGate{UserGateID=35,  GateID=1, UserID=new Guid("d1092c37-fda6-4934-8ae7-7c399bd427e6"),Time=new TimeSpan(0, 29, 47)},
            new UserGate{UserGateID=36,  GateID=1, UserID=new Guid("ffaf4954-5763-4b24-9202-6dc0c344d06f"),Time=new TimeSpan(1, 5, 39)},
            new UserGate{UserGateID=37,  GateID=1, UserID=new Guid("091fb91b-cca1-4711-a60b-f7072b5e7280"),Time=new TimeSpan(0, 5, 32)},
            new UserGate{UserGateID=38,  GateID=1, UserID=new Guid("d7899c57-100f-4978-aae9-f57792658c69"),Time=new TimeSpan(0, 10, 50)},
            new UserGate{UserGateID=39,  GateID=1, UserID=new Guid("a5407ee2-21a6-47ec-83bb-f0a956e3647c"),Time=new TimeSpan(0, 30, 0)},
            new UserGate{UserGateID=40,  GateID=1, UserID=new Guid("b8f6e6b2-9606-4b72-a5be-938ebc987dcd"),Time=new TimeSpan(0, 54, 10)}
            };
            enrollments.ForEach(s => context.UserGates.AddOrUpdate(s));
            
            context.SaveChanges();
        }
    }
}
