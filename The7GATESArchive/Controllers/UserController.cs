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

        
        [HttpPost]
        public ActionResult Options()
        {
            var compareUser = Request["CompareUser"];
            var compareUser2 = Request["compare"];
            Guid User1 = new Guid(compareUser);
            Guid User2 = new Guid(compareUser2);

            if (compareUser == null || compareUser2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(User1);
            var usergates = db.UserGates.Where(u => u.UserID == User1);
            var user2 = db.Users.Find(User2);
            var usergates2 = db.UserGates.Where(u => u.UserID == User2);
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
            UserViewModel.GateError = user.GateError;
            var UserViewModel2 = new UserViewModel();
            UserViewModel2.ID = user2.ID;
            UserViewModel2.TimeForAllGates = user2.TimeForAllGates;
            UserViewModel2.Keys = user2.Keys;
            UserViewModel2.UserGates = usergates2.ToList();
            UserViewModel2.Username = user2.Username;
            UserViewModel2.Rank = user2.Rank;
            UserViewModel2.Percentile = user2.Percentile;
            UserViewModel2.PrizeStatus = user2.PrizeStatus;
            UserViewModel2.Insight1 = user2.Insight1;
            UserViewModel2.Insight2 = user2.Insight2;
            UserViewModel2.GateError = user2.GateError;
            var UserViewModelList = new List<UserViewModel>();
            UserViewModelList.Add(UserViewModel);
            UserViewModelList.Add(UserViewModel2);

            return View("Compare", UserViewModelList);
        }

        public ActionResult Options(string sortOrder, string currentFilter, string searchString, int? page, Guid? id)
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
            ViewBag.CompareUser = id;

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
            UserViewModel.GateError = user.GateError;

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
            var UserViewModel2 = new UserViewModel();
            UserViewModel2.ID = user2.ID;
            UserViewModel2.TimeForAllGates = user2.TimeForAllGates;
            UserViewModel2.Keys = user2.Keys;
            UserViewModel2.UserGates = usergates2.ToList();
            UserViewModel2.Username = user2.Username;
            UserViewModel2.Rank = user2.Rank;
            UserViewModel2.Percentile = user2.Percentile;
            UserViewModel2.PrizeStatus = user2.PrizeStatus;
            UserViewModel2.Insight1 = user2.Insight1;
            UserViewModel2.Insight2 = user2.Insight2;
            var UserViewModelList = new List<UserViewModel>();
            UserViewModelList.Add(UserViewModel);
            UserViewModelList.Add(UserViewModel2);

            return View(UserViewModelList);
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
