using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using The7GATESArchive.DAL;
using The7GATESArchive.Models;

namespace Gateway.Controllers
{
    public class UserController : Controller
    {
        private GatewayContext db = new GatewayContext();

        // GET: Students
        public ActionResult Index(string sortOrder)
        {
            ICollection<UserViewModel> userTimes = new List<UserViewModel>();
            IEnumerable<UserViewModel> filteredUsers;
            ViewBag.KeySortParm = sortOrder == "Key" ? "key_desc" : "Key";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.RankSortParm = sortOrder == "Rank" ? "rank_desc" : "Rank";
            var users = from s in db.Users
                           select s;

            foreach (User user in users.ToList())
            {
                var userModel = new UserViewModel()
                {
                    ID = user.ID,
                    Username = user.Username,
                    Keys = user.Keys,
                    TimeForAllGates = user.UserGates.Sum(ug => ug.Time.TotalSeconds)
                };
                userTimes.Add(userModel);
            }
            filteredUsers = userTimes.OrderByDescending(s => s.Keys).ThenBy(s => s.TimeForAllGates);
            int rank = 0;
            foreach(var user in filteredUsers)
            {
                rank++;
                user.Rank = rank;
            }
            if (sortOrder != null)
            {
                sortOrder = sortOrder.ToLower();
            }
            switch (sortOrder)
            {
                case "key_desc":
                    filteredUsers = userTimes.OrderByDescending(s => s.Keys).ThenBy(s => s.TimeForAllGates);
                    break;
                case "key":
                    filteredUsers = userTimes.OrderBy(s => s.Keys).ThenBy(s => s.TimeForAllGates);
                    break;
                case "name_desc":
                    filteredUsers = userTimes.OrderByDescending(s => s.Username);
                    break;
                case "name":
                    filteredUsers = userTimes.OrderBy(s => s.Username);
                    break;
                case "rank":
                    filteredUsers = userTimes.OrderBy(s => s.Rank);
                    break;
                case "rank_desc":
                    filteredUsers = userTimes.OrderByDescending(s => s.Rank);
                    break;
                default:
                    filteredUsers = userTimes.OrderBy(s => s.Rank);
                    break;
            }
           
            return View(filteredUsers.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(Guid? id)
        {
            ICollection<UserViewModel> userTimes = new List<UserViewModel>();
            IEnumerable<UserViewModel> filteredUsers;
            var users = from s in db.Users
                        select s;

            foreach (User user in users.ToList())
            {
                var userModel = new UserViewModel()
                {
                    ID = user.ID,
                    Username = user.Username,
                    Keys = user.Keys,
                    TimeForAllGates = user.UserGates.Sum(ug => ug.Time.TotalSeconds)
                };
                userTimes.Add(userModel);
            }
            filteredUsers = userTimes.OrderByDescending(s => s.Keys).ThenBy(s => s.TimeForAllGates);
            int rank = 0;
            foreach (var user in filteredUsers)
            {
                rank++;
                user.Rank = rank;
            }

            return View(filteredUsers.Where(s => s.ID == id).FirstOrDefault());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
