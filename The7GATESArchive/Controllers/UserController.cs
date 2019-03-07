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
using PagedList;

namespace Gateway.Controllers
{
    public class UserController : Controller
    {
        private GatewayContext db = new GatewayContext();

        // GET: Students
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.KeySortParm = sortOrder == "Key" ? "key_desc" : "Key";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.RankSortParm = sortOrder == "Rank" ? "rank_desc" : "Rank";

            //First filter the results from the database
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            // See if there's any sort order.
            if (sortOrder != null)
            {
                sortOrder = sortOrder.ToLower();
            }

            IQueryable<User> users;
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();

                switch (sortOrder)
                {
                    case "key_desc":
                        users = from s in db.Users.Where(u => u.Username.ToLower().Contains(searchString))
                                .OrderByDescending(u => u.Keys).ThenBy(u => u.TimeForAllGates)
                                select s;
                        break;
                    case "key":
                        users = from s in db.Users.Where(u => u.Username.ToLower().Contains(searchString))
                                .OrderBy(u => u.Keys).ThenBy(u => u.TimeForAllGates)
                                select s;
                        break;
                    case "name_desc":
                        users = from s in db.Users.Where(u => u.Username.ToLower().Contains(searchString))
                                .OrderByDescending(u => u.Username)
                                select s;
                        break;
                    case "name":
                        users = from s in db.Users.Where(u => u.Username.ToLower().Contains(searchString))
                                .OrderBy(u => u.Username)
                                select s;
                        break;
                    case "rank_desc":
                        users = from s in db.Users.Where(u => u.Username.ToLower().Contains(searchString))
                                .OrderByDescending(u => u.Rank)
                                select s;
                        break;
                    default:
                        users = from s in db.Users.Where(u => u.Username.ToLower().Contains(searchString))
                                .OrderBy(u => u.Rank).ThenBy(u => u.TimeForAllGates)
                                select s;
                        break;
                }
            }
            else
            {
                switch (sortOrder)
                {
                    case "key_desc":
                        users = from s in db.Users
                                .OrderByDescending(u => u.Keys).ThenBy(u => u.TimeForAllGates)
                                select s;
                        break;
                    case "key":
                        users = from s in db.Users
                                .OrderBy(u => u.Keys).ThenBy(u => u.TimeForAllGates)
                                select s;
                        break;
                    case "name_desc":
                        users = from s in db.Users
                                .OrderByDescending(u => u.Username)
                                select s;
                        break;
                    case "name":
                        users = from s in db.Users
                                .OrderBy(u => u.Username)
                                select s;
                        break;
                    case "rank_desc":
                        users = from s in db.Users
                                .OrderByDescending(u => u.Rank)
                                select s;
                        break;
                    default:
                        users = from s in db.Users
                                .OrderBy(u => u.Rank).ThenBy(u => u.TimeForAllGates)
                                select s;
                        break;
                }
            }

            ViewBag.CurrentFilter = searchString;

            int pageSize = 100;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Options(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.KeySortParm = sortOrder == "Key" ? "key_desc" : "Key";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.RankSortParm = sortOrder == "Rank" ? "rank_desc" : "Rank";

            //First filter the results from the database
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            // See if there's any sort order.
            if (sortOrder != null)
            {
                sortOrder = sortOrder.ToLower();
            }

            IQueryable<User> users;
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();

                switch (sortOrder)
                {
                    case "key_desc":
                        users = from s in db.Users.Where(u => u.Username.ToLower().Contains(searchString))
                                .OrderByDescending(u => u.Keys).ThenBy(u => u.TimeForAllGates)
                                select s;
                        break;
                    case "key":
                        users = from s in db.Users.Where(u => u.Username.ToLower().Contains(searchString))
                                .OrderBy(u => u.Keys).ThenBy(u => u.TimeForAllGates)
                                select s;
                        break;
                    case "name_desc":
                        users = from s in db.Users.Where(u => u.Username.ToLower().Contains(searchString))
                                .OrderByDescending(u => u.Username)
                                select s;
                        break;
                    case "name":
                        users = from s in db.Users.Where(u => u.Username.ToLower().Contains(searchString))
                                .OrderBy(u => u.Username)
                                select s;
                        break;
                    case "rank_desc":
                        users = from s in db.Users.Where(u => u.Username.ToLower().Contains(searchString))
                                .OrderByDescending(u => u.Rank)
                                select s;
                        break;
                    default:
                        users = from s in db.Users.Where(u => u.Username.ToLower().Contains(searchString))
                                .OrderBy(u => u.Rank).ThenBy(u => u.TimeForAllGates)
                                select s;
                        break;
                }
            }
            else
            {
                switch (sortOrder)
                {
                    case "key_desc":
                        users = from s in db.Users
                                .OrderByDescending(u => u.Keys).ThenBy(u => u.TimeForAllGates)
                                select s;
                        break;
                    case "key":
                        users = from s in db.Users
                                .OrderBy(u => u.Keys).ThenBy(u => u.TimeForAllGates)
                                select s;
                        break;
                    case "name_desc":
                        users = from s in db.Users
                                .OrderByDescending(u => u.Username)
                                select s;
                        break;
                    case "name":
                        users = from s in db.Users
                                .OrderBy(u => u.Username)
                                select s;
                        break;
                    case "rank_desc":
                        users = from s in db.Users
                                .OrderByDescending(u => u.Rank)
                                select s;
                        break;
                    default:
                        users = from s in db.Users
                                .OrderBy(u => u.Rank).ThenBy(u => u.TimeForAllGates)
                                select s;
                        break;
                }
            }

            ViewBag.CurrentFilter = searchString;

            int pageSize = 100;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize));
        }
        // GET: Students/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            var usergates = db.UserGates.Where(u => u.UserID == id);
            var UserViewModel = new UserViewModel();
            UserViewModel.ID = user.ID;
            UserViewModel.TimeForAllGates = user.TimeForAllGates;
            UserViewModel.Keys = user.Keys;
            UserViewModel.UserGates = usergates.ToList();
            UserViewModel.Username = user.Username;
            UserViewModel.Rank = user.Rank;
            UserViewModel.Percentile = user.Percentile;
            UserViewModel.PrizeStatus = user.PrizeStatus;
            UserViewModel.Insight1 = user.Insight1;
            UserViewModel.Insight2 = user.Insight2;

            return View(UserViewModel);
        }
        public ActionResult Compare(Guid? id, Guid? id2)
        {
            if (id == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            var usergates = db.UserGates.Where(u => u.UserID == id);
            var user2 = db.Users.Find(id2);
            var usergates2 = db.UserGates.Where(u => u.UserID == id2);
            var UserViewModel = new UserViewModel();
            UserViewModel.ID = user.ID;
            UserViewModel.TimeForAllGates = user.TimeForAllGates;
            UserViewModel.Keys = user.Keys;
            UserViewModel.UserGates = usergates.ToList();
            UserViewModel.Username = user.Username;
            UserViewModel.Rank = user.Rank;
            UserViewModel.Percentile = user.Percentile;
            UserViewModel.PrizeStatus = user.PrizeStatus;
            UserViewModel.Insight1 = user.Insight1;
            UserViewModel.Insight2 = user.Insight2;
            UserViewModel.ID2 = user2.ID;
            UserViewModel.TimeForAllGates2 = user2.TimeForAllGates;
            UserViewModel.Keys2 = user2.Keys;
            UserViewModel.UserGates2 = usergates2.ToList();
            UserViewModel.Username2 = user2.Username;
            UserViewModel.Rank2 = user2.Rank;
            UserViewModel.Percentile2 = user2.Percentile;
            UserViewModel.PrizeStatus2 = user2.PrizeStatus;
            UserViewModel.Insight12 = user2.Insight1;
            UserViewModel.Insight22 = user2.Insight2;

            return View(UserViewModel);
        }
        public ActionResult Insights(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            var usergates = db.UserGates.Where(u => u.UserID == id);
            var UserViewModel = new UserViewModel();
            UserViewModel.ID = user.ID;
            UserViewModel.TimeForAllGates = user.TimeForAllGates;
            UserViewModel.Keys = user.Keys;
            UserViewModel.UserGates = usergates.ToList();
            UserViewModel.Username = user.Username;
            UserViewModel.Rank = user.Rank;
            UserViewModel.Percentile = user.Percentile;
            UserViewModel.PrizeStatus = user.PrizeStatus;
            UserViewModel.Insight1 = user.Insight1;
            UserViewModel.Insight2 = user.Insight2;

            return View(UserViewModel);
        }
        public ActionResult DetailsPlain(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            var usergates = db.UserGates.Where(u => u.UserID == id);
            var UserViewModel = new UserViewModel();
            UserViewModel.ID = user.ID;
            UserViewModel.TimeForAllGates = user.TimeForAllGates;
            UserViewModel.Keys = user.Keys;
            UserViewModel.UserGates = usergates.ToList();
            UserViewModel.Username = user.Username;
            UserViewModel.Rank = user.Rank;
            UserViewModel.Percentile = user.Percentile;
            UserViewModel.PrizeStatus = user.PrizeStatus;

            return View(UserViewModel);
        }
        public ActionResult Gate1(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.KeySortParm = sortOrder == "Key" ? "key_desc" : "Key";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.RankSortParm = sortOrder == "Rank" ? "rank_desc" : "Rank";

            //First filter the results from the database
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            // See if there's any sort order.
            if (sortOrder != null)
            {
                sortOrder = sortOrder.ToLower();
            }

            IQueryable<User> users;
            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();

                switch (sortOrder)
                {
                    case "key_desc":
                        users = from s in db.Users.Where(u => u.Username.ToLower().Contains(searchString))
                                .OrderByDescending(u => u.Keys).ThenBy(u => u.TimeForAllGates)
                                select s;
                        break;
                    case "key":
                        users = from s in db.Users.Where(u => u.Username.ToLower().Contains(searchString))
                                .OrderBy(u => u.Keys).ThenBy(u => u.TimeForAllGates)
                                select s;
                        break;
                    case "name_desc":
                        users = from s in db.Users.Where(u => u.Username.ToLower().Contains(searchString))
                                .OrderByDescending(u => u.Username)
                                select s;
                        break;
                    case "name":
                        users = from s in db.Users.Where(u => u.Username.ToLower().Contains(searchString))
                                .OrderBy(u => u.Username)
                                select s;
                        break;
                    case "rank_desc":
                        users = from s in db.Users.Where(u => u.Username.ToLower().Contains(searchString))
                                .OrderByDescending(u => u.Rank)
                                select s;
                        break;
                    default:
                        users = from s in db.Users.Where(u => u.Username.ToLower().Contains(searchString))
                                .OrderBy(u => u.Rank).ThenBy(u => u.TimeForAllGates)
                                select s;
                        break;
                }
            }
            else
            {
                switch (sortOrder)
                {
                    case "key_desc":
                        users = from s in db.Users
                                .OrderByDescending(u => u.Keys).ThenBy(u => u.TimeForAllGates)
                                select s;
                        break;
                    case "key":
                        users = from s in db.Users
                                .OrderBy(u => u.Keys).ThenBy(u => u.TimeForAllGates)
                                select s;
                        break;
                    case "name_desc":
                        users = from s in db.Users
                                .OrderByDescending(u => u.Username)
                                select s;
                        break;
                    case "name":
                        users = from s in db.Users
                                .OrderBy(u => u.Username)
                                select s;
                        break;
                    case "rank_desc":
                        users = from s in db.Users
                                .OrderByDescending(u => u.Rank)
                                select s;
                        break;
                    default:
                        users = from s in db.Users
                                .OrderBy(u => u.Rank).ThenBy(u => u.TimeForAllGates)
                                select s;
                        break;
                }
            }

            ViewBag.CurrentFilter = searchString;

            int pageSize = 200;
            int pageNumber = (page ?? 1);
            return View(users.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Gate2()
        {

            return View();
        }
        public ActionResult Gate3()
        {

            return View();
        }
        public ActionResult Gate4()
        {

            return View();
        }
        public ActionResult Gate5()
        {

            return View();
        }
        public ActionResult Gate6()
        {

            return View();
        }
        public ActionResult Gate7()
        {

            return View();
        }
        public ActionResult BGate1()
        {

            return View();
        }
        public ActionResult BGate2()
        {

            return View();
        }
        public ActionResult BGate3()
        {

            return View();
        }
        public ActionResult Report()
        {

            return View();
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
