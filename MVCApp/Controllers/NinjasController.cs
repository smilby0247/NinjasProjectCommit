using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NinjaDomain.Classes;
using NinjaDomain.DataModel;

namespace MVCApp.Controllers
{
    public class NinjasController : Controller
    {
        private NinjaContext db = new NinjaContext();

        // GET: Ninjas
        public ActionResult Index()
        {
            var ninjas = db.Ninjas.Include(n => n.Clan);
            return View(ninjas.ToList());
        }

        public ActionResult ClanGrid()
        {
            var clans = db.Clans;
            return View(clans.ToList());
        }

        public ActionResult NinjaGrid()
        {
            var ninjas = db.Ninjas.Include(n => n.Clan);
            return View(ninjas.ToList());
        }


        // GET: Ninjas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ninja ninja = db.Ninjas.Find(id);
            if (ninja == null)
            {
                return HttpNotFound();
            }
            return View(ninja);
        }

        // GET: Ninjas/Details/
        public ActionResult ClanDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clan clans = db.Clans.Find(id);
            if (clans == null)
            {
                return HttpNotFound();
            }
            return View(clans);
        }

        // GET: Ninjas/Create
        public ActionResult Create()
        {
            ViewBag.ClanId = new SelectList(db.Clans, "Id", "ClanName");
            return View();
        }

        public ActionResult CreateClan()
        {
            ViewBag.ClanId = new SelectList(db.Clans, "Id", "ClanName");
            return View();
        }



        // POST: Ninjas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ServedInOniwaban,ClanId,DateOfBirth,DateCreated,DateModified")] Ninja ninja)
        {
            if (ModelState.IsValid)
            {
                db.Ninjas.Add(ninja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClanId = new SelectList(db.Clans, "Id", "ClanName", ninja.ClanId);
            return View(ninja);
        }


        // POST: Ninjas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateClan([Bind(Include = "ClanName,DateCreated,DateModified")] Clan clan)
        {
            if (ModelState.IsValid)
            {
                db.Clans.Add(clan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClanId = new SelectList(db.Clans, "Id", "ClanName");
            return View(clan);
        }

        // GET: Clan/Edit/5
        public ActionResult EditClanName(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clan clan = db.Clans.Find(id);
            if (clan == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClanName = new SelectList(db.Clans, "ClanName", clan.ClanName);

            return View(clan);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ninja ninja = db.Ninjas.Find(id);
            if (ninja == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClanId = new SelectList(db.Clans, "Id", "ClanName", ninja.ClanId);

            return View(ninja);
        }

        // POST: Ninjas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ServedInOniwaban,ClanId,DateOfBirth,DateCreated,DateModified")] Ninja ninja)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ninja).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClanId = new SelectList(db.Clans, "Id", "ClanName", ninja.ClanId);
            return View(ninja);
        }

        // GET: Ninjas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ninja ninja = db.Ninjas.Find(id);
            if (ninja == null)
            {
                return HttpNotFound();
            }
            return View(ninja);
        }


        // POST: Ninjas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ninja ninja = db.Ninjas.Find(id);
            db.Ninjas.Remove(ninja);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        // GET: Ninjas/ClanDelete/5
        public ActionResult ClanDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clan clans = db.Clans.Find(id);
            if (clans == null)
            {
                return HttpNotFound();
            }
            return View(clans);
        }


        // POST: Ninjas/ClanDelete/5
        [HttpPost, ActionName("ClanDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult ClanDeleteConfirmed(int id)
        {
            Clan clans = db.Clans.Find(id);
            db.Clans.Remove(clans);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}

