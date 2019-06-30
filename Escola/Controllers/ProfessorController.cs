using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Escola.Models;
using X.PagedList;

namespace Escola.Controllers
{
    public class ProfessorController : Controller
    {
        private EscolaEntities db = new EscolaEntities();

        // GET: Professor
        public ActionResult Index(int? page)
        {
            return View();
        }

        public PartialViewResult Listar(int? page)
        {
            var professores = db.Professor.OrderBy(x => x.ProfessorId);

            var numPagina = page ?? 1;
            var paginaDeProfessores = professores.ToPagedList(numPagina, 1);
            ViewBag.PaginaDeProfessores = paginaDeProfessores;

            return PartialView("_Listar", paginaDeProfessores);
        }

        public ActionResult ListarProfessores(int? page)
        {            
            return View();
        }

        public PartialViewResult ListarProfMediaDeIdadeAlunos15a17(int? page)
        {
            var professores = new Filtro.FiltroProfessores().FiltraProfessoresCujaMediaIdadeDosAlunos(15, 17);

            var numPagina = page ?? 1;
            var paginaDeProfessores = professores.ToPagedList(numPagina, 1);
            ViewBag.PaginaDeProfessores = paginaDeProfessores;

            return PartialView("_Listar", paginaDeProfessores);
        }

        // GET: Professor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = db.Professor.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
        }

        // GET: Professor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Professor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProfessorId,Nome")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                db.Professor.Add(professor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(professor);
        }

        // GET: Professor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = db.Professor.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
        }

        // POST: Professor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProfessorId,Nome")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(professor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(professor);
        }

        // GET: Professor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = db.Professor.Find(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
        }

        // POST: Professor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Professor professor = db.Professor.Find(id);
            db.Professor.Remove(professor);
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
    }
}
