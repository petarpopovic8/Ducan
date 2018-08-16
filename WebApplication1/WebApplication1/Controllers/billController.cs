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
using WebApplication1.Models;
using PagedList;

namespace WebApplication1.Controllers
{
    public class billController : Controller
    {
        private BillService _billService;
        private ArticleService _articleService;

        public billController()
        {
            _billService = new BillService();
            _articleService = new ArticleService();
        }

        // GET: bill
        public ActionResult Index(int? page)
        {
            ViewBag.Page = page;
            ViewBag.id_article = new SelectList(_articleService.GetAll(), "id", "name");
            var bill = _billService.GetAll().Select(x => new BillViewModel(x)).OrderByDescending(x => x.id);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(bill.ToPagedList(pageNumber, pageSize));
        }

        // GET: bill/Details/5
        public ActionResult Details(int? id, int? page)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.URL = "/bill/Index/?page=" + page;
            bill bill = _billService.FindById(id.Value);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(new BillViewModel(bill));
        }

        [HttpPost]
        public ActionResult Create(int num, int user_id, bill[] order)
        {

            return View();
        }        

        // GET: bill/Delete/5
        public ActionResult Delete(int? id, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Page = page;
            bill bill = _billService.FindById(id.Value);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(new BillViewModel(bill));
        }

        // POST: bill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int page)
        {
            bill bill = _billService.FindById(id);
            _billService.Delete(bill);
            return RedirectToAction("Index/?page=" + page);
        }
    }
}
