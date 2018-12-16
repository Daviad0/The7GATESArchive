using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using The7GATESArchive.Models;

namespace The7GATESArchive.DAL
{
    public class GatewayInitializer : System.Data.Entity.DropCreateDatabaseAlways<GatewayContext>
    {
        protected override void Seed(GatewayContext context)
        {
            /*
            var students = new List<User>
            {
            new User{ID=new Guid("9b7a6663-009d-4563-a6ef-8934976547dd"), Username="This could be you <and your team>",Keys=3},
            new User{ID=new Guid("ab442949-a45a-4d28-9944-f6aab7deb63c"), Username="Steph",Keys=4},
            new User{ID=new Guid("4d69215d-5c82-4304-818a-bbe196d94a50"), Username="MatPat <Why Am I Here?>",Keys=1},
            new User{ID=new Guid("51107863-4216-40ab-a61c-dd1384cd9dad"), Username="Jason <Blame Jason>",Keys=5},
            new User{ID=new Guid("13dcdc49-1296-4c08-b4cd-befd457a5067"), Username="Chris",Keys=1},
            new User{ID=new Guid("d8be0f78-395c-444c-96a3-36ed2f27e360"), Username="Dan <Cybert>",Keys=2},
            new User{ID=new Guid("70d78475-8ced-4f71-9c43-d6f99c7f1cd9"), Username="Daviado <Yes even in the mock i make myself 7th>",Keys=4},
            new User{ID=new Guid("164fdae4-5f69-455d-9ddd-bcaeaaa8b0a5"), Username="Retro <He's cool>",Keys=3},
            new User{ID=new Guid("b13f1525-00d3-4400-9f7a-3a23e64cf6be"), Username="Cara <Nice>",Keys=3},
            new User{ID=new Guid("e2a14a15-335f-4d9b-ba3c-cd562f01fbec"), Username="Snape <Now im running out of ideas>",Keys=1},

            };

            students.ForEach(s => context.Users.Add(s));
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
            courses.ForEach(s => context.Gates.Add(s));
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
            new UserGate{UserGateID=9,  GateID=1, UserID=new Guid("b13f1525-00d3-4400-9f7a-3a23e64cf6be"), Time=new TimeSpan(5, 2, 43)},
            new UserGate{UserGateID=10,  GateID=1, UserID=new Guid("e2a14a15-335f-4d9b-ba3c-cd562f01fbec"), Time=new TimeSpan(0, 3, 12)},
            };
            enrollments.ForEach(s => context.UserGates.Add(s));
            context.SaveChanges();
            */
        }
    }
}