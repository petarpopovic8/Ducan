using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using ServiceLayer;

namespace WebApplication1.Controllers
{
    public class billController : Controller
    {
        private BillService _billService;

        public billController()
        {
            _billService = new BillService();
        }

        // GET: bill
        public ActionResult Index()
        {
            var bill = _billService.GetAll();
            return View(bill.ToList());
        }

        // GET: bill/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bill bill = _billService.FindById(id.Value);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // GET: bill/Create
        public ActionResult Create()
        {
            //ViewBag.article_id = new SelectList(db.article, "id", "name");
            // ViewBag.user_id = new SelectList(db.user, "id", "full_name");
            return View();
        }

        // POST: bill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,user_id,article_id,amount,num")] bill bill)
        {
            if (ModelState.IsValid)
            {
                //db.bill.Add(bill);
                //db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.article_id = new SelectList(db.article, "id", "name", bill.article_id);
            //ViewBag.user_id = new SelectList(db.user, "id", "full_name", bill.user_id);
            return View(bill);
        }

        // GET: bill/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bill bill = _billService.FindById(id.Value);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        // POST: bill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            bill bill = _billService.FindById(id);
            _billService.Delete(bill);
            return RedirectToAction("Index");
        }
    }
}
