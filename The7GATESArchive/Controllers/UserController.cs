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
        public ActionResult Options(Guid?[] compare)
        {
            var compareUser = Request["CompareUser"];
            var BaseUser = db.Users.Find(new Guid(compareUser));
            var BaseUserGates = db.UserGates.Where(u => u.UserID == new Guid(compareUser));
            var UserViewModelList = new List<UserViewModel>();
            foreach (Guid g in compare)
            {
                var user = db.Users.Find(g);
                var usergates = db.UserGates.Where(u => u.UserID == g);
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
                UserViewModelList.Add(UserViewModel);
            }
            var UserViewModel2 = new UserViewModel();
            UserViewModel2.ID = BaseUser.ID;
            UserViewModel2.TimeForAllGates = BaseUser.TimeForAllGates;
            UserViewModel2.Keys = BaseUser.Keys;
            UserViewModel2.UserGates = BaseUserGates.ToList();
            UserViewModel2.Username = BaseUser.Username;
            UserViewModel2.Rank = BaseUser.Rank;
            UserViewModel2.Percentile = BaseUser.Percentile;
            UserViewModel2.PrizeStatus = BaseUser.PrizeStatus;
            UserViewModel2.Insight1 = BaseUser.Insight1;
            UserViewModel2.Insight2 = BaseUser.Insight2;
            UserViewModel2.GateError = BaseUser.GateError;
            UserViewModelList.Add(UserViewModel2);
            return View("Group", UserViewModelList);
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
        public ActionResult Gate1()
        {
            var gateTop = (from ug in db.UserGates
                           join u in db.Users on ug.UserID equals u.ID
                           where ug.GateID == 1
                           where ug.Keys == 5
                           select new GateTopModel
                           {
                               Time = ug.Time,
                               Keys = ug.Keys,
                               ID = ug.UserID,
                               Username = u.Username,
                               Rank = ug.Rank
                           });
            return View(gateTop.OrderBy(ug => ug.Rank).ToList());
        }
        public ActionResult Gate2()
        {
            var gateTop = (from ug in db.UserGates
                           join u in db.Users on ug.UserID equals u.ID
                           where ug.GateID == 2
                           where ug.Keys == 4
                           where ug.Time > new TimeSpan(1, 0, 0)
                           where ug.Time < new TimeSpan(5, 1, 0)
                           select new GateTopModel
                           {
                               Time = ug.Time,
                               Keys = ug.Keys,
                               ID = ug.UserID,
                               Username = u.Username
                           });
            return View(gateTop.OrderByDescending(ug => ug.Keys).ThenBy(ug => ug.Time).ToList());
        }
        public ActionResult Gate3()
        {
            var gateTop = (from ug in db.UserGates
                           join u in db.Users on ug.UserID equals u.ID
                           where ug.GateID == 3
                           where ug.Keys == 5
                           where ug.Time > new TimeSpan(0, 30, 0)
                           where ug.Time < new TimeSpan(3, 1, 0)
                           select new GateTopModel
                           {
                               Time = ug.Time,
                               Keys = ug.Keys,
                               ID = ug.UserID,
                               Username = u.Username
                           });
            return View(gateTop.OrderByDescending(ug => ug.Keys).ThenBy(ug => ug.Time).ToList());
        }
        public ActionResult Gate4()
        {
            var gateTop = (from ug in db.UserGates
                           join u in db.Users on ug.UserID equals u.ID
                           where ug.GateID == 4
                           where ug.Keys == 4
                           where ug.Time > new TimeSpan(0, 20, 0)
                           where ug.Time < new TimeSpan(0, 55, 0)
                           select new GateTopModel
                           {
                               Time = ug.Time,
                               Keys = ug.Keys,
                               ID = ug.UserID,
                               Username = u.Username
                           });
            return View(gateTop.OrderByDescending(ug => ug.Keys).ThenBy(ug => ug.Time).ToList());
        }
        public ActionResult Gate5()
        {
            var gateTop = (from ug in db.UserGates
                           join u in db.Users on ug.UserID equals u.ID
                           where ug.GateID == 5
                           where ug.Keys == 5
                           where ug.Time > new TimeSpan(1, 30, 0)
                           where ug.Time < new TimeSpan(3, 52, 0)
                           where ug.Stacked == false
                           select new GateTopModel
                           {
                               Time = ug.Time,
                               Keys = ug.Keys,
                               ID = ug.UserID,
                               Username = u.Username
                           });
            return View(gateTop.OrderByDescending(ug => ug.Keys).ThenBy(ug => ug.Time).ToList());
        }
        public ActionResult Gate6()
        {
            var gateTop = (from ug in db.UserGates
                           join u in db.Users on ug.UserID equals u.ID
                           where ug.GateID == 6
                           where ug.Keys == 6
                           where ug.Time > new TimeSpan(2, 10, 0)
                           where ug.Time < new TimeSpan(4, 45, 0)
                           where ug.Stacked == false
                           select new GateTopModel
                           {
                               Time = ug.Time,
                               Keys = ug.Keys,
                               ID = ug.UserID,
                               Username = u.Username
                           });
            return View(gateTop.OrderByDescending(ug => ug.Keys).ThenBy(ug => ug.Time).ToList());
            
        }
        public ActionResult Group(string identifier)
        {
            bool comuser0b = false;
            bool comuser1b = false;
            bool comuser2b = false;
            bool comuser3b = false;
            bool comuser4b = false;
            bool comuser5b = false;
            string[] comuser = identifier.Split('-');
            if (comuser[0] == "X")
            {
                comuser0b = true;
            }
            if (comuser[1] == "X")
            {
                comuser1b = true;
            }
            if (comuser[2] == "X")
            {
                comuser2b = true;
            }
            if (comuser[3] == "X")
            {
                comuser3b = true;
            }
            if (comuser[4] == "X")
            {
                comuser4b = true;
            }
            if (comuser[5] == "X")
            {
                comuser5b = true;
            }
            var TestValues = new TestValuesModel
            {
                TV0 = comuser[0],
                TV1 = comuser[1],
                TV2 = comuser[2],
                TV3 = comuser[3],
                TV4 = comuser[4],
                TV5 = comuser[5],
                TV0EX = comuser0b,
                TV1EX = comuser1b,
                TV2EX = comuser2b,
                TV3EX = comuser3b,
                TV4EX = comuser4b,
                TV5EX = comuser5b,
            };
            return View("TestValues", TestValues);
        }
        public ActionResult TestValues()
        {
            return View("TestValues");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        /*public ActionResult Group(Guid? id, Guid? id2, Guid? id3, Guid? id4, Guid? id5)
        {
            if (id == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            var usergates = db.UserGates.Where(u => u.UserID == id);
            var user2 = db.Users.Find(id2);
            var usergates2 = db.UserGates.Where(u => u.UserID == id2);
            var user3 = db.Users.Find(id3);
            var usergates3 = db.UserGates.Where(u => u.UserID == id3);
            var user4 = db.Users.Find(id4);
            var usergates4 = db.UserGates.Where(u => u.UserID == id4);
            var user5 = db.Users.Find(id5);
            var usergates5 = db.UserGates.Where(u => u.UserID == id5);
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
            var UserViewModel3 = new UserViewModel();
            UserViewModel3.ID = user3.ID;
            UserViewModel3.TimeForAllGates = user3.TimeForAllGates;
            UserViewModel3.Keys = user3.Keys;
            UserViewModel3.UserGates = usergates3.ToList();
            UserViewModel3.Username = user3.Username;
            UserViewModel3.Rank = user3.Rank;
            UserViewModel3.Percentile = user3.Percentile;
            UserViewModel3.PrizeStatus = user3.PrizeStatus;
            UserViewModel3.Insight1 = user3.Insight1;
            UserViewModel3.Insight2 = user3.Insight2;
            var UserViewModel4 = new UserViewModel();
            UserViewModel4.ID = user4.ID;
            UserViewModel4.TimeForAllGates = user4.TimeForAllGates;
            UserViewModel4.Keys = user4.Keys;
            UserViewModel4.UserGates = usergates4.ToList();
            UserViewModel4.Username = user4.Username;
            UserViewModel4.Rank = user4.Rank;
            UserViewModel4.Percentile = user4.Percentile;
            UserViewModel4.PrizeStatus = user4.PrizeStatus;
            UserViewModel4.Insight1 = user4.Insight1;
            UserViewModel4.Insight2 = user4.Insight2;
            var UserViewModel5 = new UserViewModel();
            UserViewModel5.ID = user5.ID;
            UserViewModel5.TimeForAllGates = user5.TimeForAllGates;
            UserViewModel5.Keys = user5.Keys;
            UserViewModel5.UserGates = usergates5.ToList();
            UserViewModel5.Username = user5.Username;
            UserViewModel5.Rank = user5.Rank;
            UserViewModel5.Percentile = user5.Percentile;
            UserViewModel5.PrizeStatus = user5.PrizeStatus;
            UserViewModel5.Insight1 = user5.Insight1;
            UserViewModel5.Insight2 = user5.Insight2;
            var UserViewModelList = new List<UserViewModel>();
            UserViewModelList.Add(UserViewModel);
            UserViewModelList.Add(UserViewModel2);
            UserViewModelList.Add(UserViewModel3);
            UserViewModelList.Add(UserViewModel4);
            UserViewModelList.Add(UserViewModel5);

            return View(UserViewModelList);
        }*/
    }
}
