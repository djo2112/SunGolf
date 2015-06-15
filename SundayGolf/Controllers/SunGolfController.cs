using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SundayGolf.Models;

namespace SundayGolf.Controllers
{
    public class SunGolfController : Controller
    {
        private WeeklyContext _db = new WeeklyContext();

        /// <summary>
        /// Fill the golfers list in the ViewBag.SelectedGolfer object
        /// </summary>
        private void PopulateGolferDropDown(int? SelectedGolfer)
        {
            ViewBag.SelectedGolfer = new SelectList(_db.Golfers, "GolferID", "GolferName", SelectedGolfer);
        }

        /// <summary>
        /// Fill the course list in the ViewBag.SelectedCourse object
        /// </summary>
        private void PopulateCourseDropDown(int? SelectedCourse)
        {
            ViewBag.SelectedCourse = new SelectList(_db.Courses, "CourseID", "CourseName", SelectedCourse);
        }

        /// <summary>
        /// Fill the year list in the ViewBag.SelectedYear object
        /// </summary>
        private void PopulateYearListDropDown(string SelectedYear)
        {
            // TODO: Read the weekly table and find the distinct years - for now, just default to this static list
            List<SelectListItem> _years = new List<SelectListItem>();
            _years.Add(new SelectListItem { Text = "2013", Value = "2013" });
            _years.Add(new SelectListItem { Text = "2014", Value = "2014" });
            _years.Add(new SelectListItem { Text = "2015", Value = "2015", Selected = true });
            if (string.IsNullOrEmpty(SelectedYear))
                ViewBag.SelectedYear = _years;
            else
                ViewBag.SelectedYear = new SelectList(_years, "Text", "Value", SelectedYear);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }

        //
        // GET: /SunGolf/Details/5

        public ActionResult Details(int id = 0)
        {
            Weekly weekly = _db.Weeklies.Find(id);
            if (weekly == null)
            {
                return HttpNotFound();
            }
            return View(weekly);
        }

        //
        // GET: /SunGolf/Create

        public ActionResult Create()
        {
            ViewBag.CourseID = new SelectList(_db.Courses, "CourseID", "CourseName");
            ViewBag.GolferID = new SelectList(_db.Golfers, "GolferID", "GolferName");
            return View();
        }

        //
        // GET: /SunGolf/AddCourses

        public ActionResult AddCourses()
        {
            ViewBag.CourseID = new SelectList(_db.Courses, "CourseID", "CourseName");
            return View();
        }

        //
        // GET: /SunGolf/AddCourses

        [HttpPost]
        public ActionResult AddCourses(Course course)
        {
            if (ModelState.IsValid)
            {
                _db.Courses.Add(course);
                _db.SaveChanges();
                return RedirectToAction("AddCourses");
            }

            ViewBag.CourseID = new SelectList(_db.Courses, "CourseID", "CourseName");
            return View(course);
        }

        //
        // GET: /SunGolf/AddGolfers

        public ActionResult AddGolfers()
        {
            ViewBag.GolferID = new SelectList(_db.Golfers, "GolferID", "GolferName");
            return View();
        }

        //
        // GET: /SunGolf/AddGolfers

        [HttpPost]
        public ActionResult AddGolfers(Golfer golfer)
        {
            if (ModelState.IsValid)
            {
                _db.Golfers.Add(golfer);
                _db.SaveChanges();
                return RedirectToAction("AddGolfers");
            }

            ViewBag.GolferID = new SelectList(_db.Golfers, "GolferID", "GolferName");
            return View(golfer);
        }

        //
        // POST: /SunGolf/Create

        [HttpPost]
        public ActionResult Create(Weekly weekly)
        {
            if (ModelState.IsValid)
            {
                _db.Weeklies.Add(weekly);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(_db.Courses, "CourseID", "CourseName", weekly.CourseID);
            ViewBag.GolferID = new SelectList(_db.Golfers, "GolferID", "GolferName", weekly.GolferID);
            return View(weekly);
        }

        //
        // GET: /SunGolf/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Weekly weekly = _db.Weeklies.Find(id);
            if (weekly == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseID = new SelectList(_db.Courses, "CourseID", "CourseName", weekly.CourseID);
            ViewBag.GolferID = new SelectList(_db.Golfers, "GolferID", "GolferName", weekly.GolferID);
            return View(weekly);
        }

        //
        // POST: /SunGolf/Edit/5

        [HttpPost]
        public ActionResult Edit(Weekly weekly)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(weekly).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseID = new SelectList(_db.Courses, "CourseID", "CourseName", weekly.CourseID);
            ViewBag.GolferID = new SelectList(_db.Golfers, "GolferID", "GolferName", weekly.GolferID);
            return View(weekly);
        }

        //
        // GET: /SunGolf/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Weekly weekly = _db.Weeklies.Find(id);
            if (weekly == null)
            {
                return HttpNotFound();
            }
            return View(weekly);
        }

        //
        // POST: /SunGolf/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Weekly weekly = _db.Weeklies.Find(id);
            _db.Weeklies.Remove(weekly);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        //
        // GET: /SunGolf/Summary

        public ActionResult Summary(string SelectedYear)
        {
            // The result of the query is a descending list of golfers by dollar amounts
            int computedYear;
            PopulateYearListDropDown(SelectedYear);  // Shore-up our drop-down

            if (!string.IsNullOrEmpty(SelectedYear))
                computedYear = Convert.ToInt32(SelectedYear);
            else
                computedYear = DateTime.Now.Year;

            // We assume we have a computedYear for this LINQ to object query
            var query = from t in _db.Weeklies.Include(w => w.Golfer).AsQueryable()
                        where t.Date.Year == computedYear
                        group t by t.GolferID into g
                        let totalAmount = g.Sum(x => x.Total)
                        let totSkins = g.Sum(x => x.Skins)
                        let totPins = g.Sum(x => x.Pins)
                        let numEvents = g.Count()
                        orderby totalAmount descending
                        select new { g.Key, totalAmount, numEvents, totPins, totSkins };

            List<Weekly> weeklyList = new List<Weekly>();
            foreach (var row in query)
            {
                Weekly weekly = new Weekly();
                weekly.Golfer = new Golfer();
                weekly.GolferID = row.Key;
                Golfer selectedGolfer = _db.Golfers.Find(weekly.GolferID);
                weekly.Golfer.GolferName = selectedGolfer.GolferName;
                weekly.Total = row.totalAmount;
                weekly.Pins = row.totPins;
                weekly.Skins = row.totSkins;
                weekly.Score = row.numEvents;
                weeklyList.Add(weekly);
            }

            return View(weeklyList.ToList());
        }

        //
        // GET: /SunGolf/Index

        public ActionResult Index(string SelectedYear, int? SelectedGolfer)
        {
            // The result of the query is a list of golfer details
            int computedYear;
            PopulateYearListDropDown(SelectedYear);
            PopulateGolferDropDown(SelectedGolfer);

            if (!string.IsNullOrEmpty(SelectedYear))
                computedYear = Convert.ToInt32(SelectedYear);
            else
                computedYear = DateTime.Now.Year;

            // We assume we have a computedYear
            // Setup Fluent Syntax
            var weeklies = _db.Weeklies
                .Include(w => w.Course)
                .Include(w => w.Golfer)
                .OrderByDescending(w => w.Date).ThenBy(w => w.Net)
                .Where(w => w.Date.Year.Equals(computedYear));

            // Do we have a golfer selected
            if (SelectedGolfer != null)
                weeklies = weeklies.Where(w => w.GolferID == SelectedGolfer);

            return View(weeklies.ToList());
        }

        //
        // GET: /SunGolf/Schedule

        public ActionResult Schedule()
        {
            // This will populate the main and particl views

            //IQueryable<Schedule> schedule = _db.Schedules.OrderBy(s => s.Golfer.GolferName);
            //ViewBag.GolferID = new SelectList(_db.Golfers, "GolferID", "GolferName");
            ViewBag.SelectedCourse = new SelectList(_db.Courses, "CourseID", "CourseName");
            ViewBag.SelectedDate = new SelectList(_db.Schedules, "Date", "Date");

            return View(new ScheduleModel { scheduleModel = Samplegolfers() });
        }

        private static List<ScheduleModel> Samplegolfers()
        {
            List<ScheduleModel> model = new List<ScheduleModel>();
            model.Add(new ScheduleModel { GolferName = "Fred", CourseID = 20, GolferID = 15 });
            model.Add(new ScheduleModel { GolferName = "Oz", CourseID = 20, GolferID = 16 });
            return model;
        }

        //
        // POST: /SunGolf/Schedule

        [HttpPost]
        public ActionResult Schedule(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(schedule).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseID = new SelectList(_db.Courses, "CourseID", "CourseName", schedule.CourseID);
            return View();
        }

    }
}