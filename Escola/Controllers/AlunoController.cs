using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Escola.Filtro;
using Escola.Models;
using X.PagedList;

namespace Escola.Controllers
{
    public class AlunoController : Controller
    {
        private EscolaEntities db = new EscolaEntities();

        // GET: Aluno
        public ActionResult Index(int? page)
        {                        
            return View();
        }

        public PartialViewResult Listar(int? page)
        {
            var alunos = db.Aluno.Include(a => a.Professor).OrderBy(x => x.AlunoId);

            var numPagina = page ?? 1;
            var paginaDeAlunos = alunos.ToPagedList(numPagina, 5);
            ViewBag.PaginaDeAlunos = paginaDeAlunos;

            return PartialView("_Listar", paginaDeAlunos);
        }

        public ActionResult ListaMaiores(int? page)
        {
            var b = new FiltroAlunos().FiltraAlunosMaioresDe(16);

            var numPagina = page ?? 1;
            var paginaDeAlunos = b.ToPagedList(numPagina, 5);
            ViewBag.PaginaDeAlunos = paginaDeAlunos;

            return View(paginaDeAlunos);
        }

        // GET: Aluno/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluno aluno = db.Aluno.Find(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        // GET: Aluno/Create
        public ActionResult Create()
        {
            ViewBag.ProfessorId = new SelectList(db.Professor, "ProfessorId", "Nome");
            return View();
        }

        // POST: Aluno/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlunoId,Nome,DataNascimento,ProfessorId")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                db.Aluno.Add(aluno);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProfessorId = new SelectList(db.Professor, "ProfessorId", "Nome", aluno.ProfessorId);
            return View(aluno);
        }

        // GET: Aluno/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluno aluno = db.Aluno.Find(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfessorId = new SelectList(db.Professor, "ProfessorId", "Nome", aluno.ProfessorId);
            return View(aluno);
        }

        // POST: Aluno/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlunoId,Nome,DataNascimento,ProfessorId")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aluno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProfessorId = new SelectList(db.Professor, "ProfessorId", "Nome", aluno.ProfessorId);
            return View(aluno);
        }

        // GET: Aluno/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluno aluno = db.Aluno.Find(id);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        // POST: Aluno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Aluno aluno = db.Aluno.Find(id);
            db.Aluno.Remove(aluno);
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
