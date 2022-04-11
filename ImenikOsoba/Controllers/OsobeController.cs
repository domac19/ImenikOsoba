using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ImenikOsoba;

namespace ImenikOsoba.Controllers
{
    public class OsobeController : Controller
    {
        private ImenikDbContext _dbContext = new ImenikDbContext();

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();    
        }

        public ActionResult Index()
        {
            return View(_dbContext.Osobes.ToList());
        }

        public ActionResult Details(int? id)
        {
            var osobe = _dbContext.Osobes.SingleOrDefault(i => i.Id == id);
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (osobe == null)
            {
                return HttpNotFound();
            }
            return View(osobe);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Osobe osobe)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Osobes.Add(osobe);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(osobe);
        }

        public ActionResult Edit(int? id)
        {
            var osobe = _dbContext.Osobes.SingleOrDefault(i => i.Id == id);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            if (osobe == null)
            {
                return HttpNotFound();
            }
            return View(osobe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Osobe osobe)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Entry(osobe).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(osobe);
        }

        public ActionResult Delete(int? id)
        {
            var osobe = _dbContext.Osobes.SingleOrDefault(i => i.Id == id);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (osobe == null)
            {
                return HttpNotFound();
            }
            return View(osobe);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var osobe = _dbContext.Osobes.SingleOrDefault(i => i.Id == id);
            _dbContext.Osobes.Remove(osobe);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
