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

        // GET: Students/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
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
