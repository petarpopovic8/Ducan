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
    [Authorize]
    public class articleController : Controller
    {
        private ArticleService _articleService;
        private FirmService _firmService;

        public articleController()
        {
            _articleService = new ArticleService();
            _firmService = new FirmService();
        }
        // GET: article
        public ActionResult Index(string sortOrder, int? page)
        {
            var articles = _articleService.GetAll().Select(x => new ArticleViewModel(x));

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.FirmSortParm = sortOrder == "firm" ? "firm_desc" : "firm";
            ViewBag.AmountSortParm = sortOrder == "amount" ? "amount_desc" : "amount";
            ViewBag.PriceSortParm = sortOrder == "price" ? "price_desc" : "price";

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            //return View(students.ToPagedList(pageNumber, pageSize));  OBRISI AKO RADI

            return View(_articleService.GetSorted(sortOrder).Select(x => new ArticleViewModel(x)).ToPagedList(pageNumber, pageSize));
        }

        // GET: article/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            article article = _articleService.FindById(id.Value);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: article/Create
        public ActionResult Create()
        {
            ViewBag.firm_id = new SelectList(_firmService.GetAll(), "id", "name");
            return View();
        }

        // POST: article/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,firm_id,name,amount,price,tax")] article article)
        {
            if (ModelState.IsValid)
            {
                _articleService.Create(article);
                return RedirectToAction("Index");
            }

            ViewBag.firm_id = new SelectList(_firmService.GetAll(), "id", "name");
            return View(article);
        }

        // GET: article/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            article article = _articleService.FindById(id.Value);
            if (article == null)
            {
                return HttpNotFound();
            }
            //ViewBag.firm_id = new SelectList(db.firm, "id", "name", article.firm_id);
            return PartialView(article);
        }

        // POST: article/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,firm_id,name,amount,price,sumvalue,tax")] article article)
        {
            if (ModelState.IsValid)
            {
                _articleService.Update(article);
                return RedirectToAction("Index");
            }
           // ViewBag.firm_id = new SelectList(db.firm, "id", "name", article.firm_id);
            return View(article);
        }

        [HttpGet]
        public ActionResult EditAjax(int id)
        {
            article article = _articleService.FindById(id);
            ViewBag.firm_id = new SelectList(_firmService.GetAll(), "id", "name", article.firm_id);
            return PartialView(new ArticleViewModel(article));
        }

        [HttpPost]
        public ActionResult EditAjax(article article)
        {
            //article article = model.ToArticle();
            article original = _articleService.FindById(article.id);  //nađi iz baze po id-u (u bazi je jos spremljena "stara" verzija)
            try
            {
                original.name = article.name;
                original.amount = article.amount;                     //postavi varijable nove verzije te ih updejtaj
                original.price = article.price;
                original.tax = article.tax;
                //original.firm_id = article.firm_id;
                // ModelState.ValidateBusinessObject(original);
                if (ModelState.IsValid)
                {
                    _articleService.Update(original);
                    return RedirectToAction("Show", new { id = original.id});
                }
            }
            catch (Exception exc)
            {
                TempData["Pogreska"] = exc.Message;
            }

            return PartialView(original);
        }

        public ActionResult Show(int id)
        {
            article artikl = _articleService.FindById(id);
            return PartialView(new ArticleViewModel(artikl));
        }


        // GET: article/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            article article = _articleService.FindById(id.Value);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: article/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            article article = _articleService.FindById(id);
            _articleService.Delete(article);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult DeleteAjax(int id)
        {
            var result = new { Successful = true, ErrorMessage = string.Empty };
            try
            {
                article article = _articleService.FindById(id);
                _articleService.Delete(article);
            }
            catch (Exception exc)
            {
                result = new { Successful = false, ErrorMessage = exc.Message };
            }
            return Json(result);
        }
    }
}
