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
    public class firmController : Controller
    {
        private FirmService _firmService;

        public firmController()
        {
            _firmService = new FirmService();
        }

        // GET: firm
        public ActionResult Index()
        {
            return View(_firmService.GetAll().ToList());
        }

        // GET: firm/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: firm/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,OIB,phone,adress,since,email")] firm firm)
        {
            if (ModelState.IsValid)
            {
                _firmService.Create(firm);
                return RedirectToAction("Index");
            }

            return View(firm);
        }

        // GET: firm/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            firm firm = _firmService.FindById(id.Value);
            if (firm == null)
            {
                return HttpNotFound();
            }
            return View(firm);
        }

        // POST: firm/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,OIB,phone,adress,since,email")] firm firm)
        {
            if (ModelState.IsValid)
            {
                _firmService.Update(firm);
                return RedirectToAction("Index");
            }
            return View(firm);
        }

        // GET: firm/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            firm firm = _firmService.FindById(id.Value);
            if (firm == null)
            {
                return HttpNotFound();
            }
            return View(firm);
        }

        // POST: firm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            firm firm = _firmService.FindById(id);
            _firmService.Delete(firm);
            return RedirectToAction("Index");
        }
    }
}
