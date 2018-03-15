using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.IO;
using directorios = System.IO;
using TDA;
using ED1L3.Models;
using ED1L3.DBContest;
using System.Net;
namespace ED1L3.Models
{
    public class PartidonoController : Controller
    {
        DefaultConnection<Partido, int> db = DefaultConnection<Partido, int>.getInstance;
        // GET: Partidono
        public ActionResult Index()
        {
            return View(db.datos.ToList());
        }

        // GET: Partidono/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Partidono/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Partidono/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Nopartido,FechaPartido,Grupo,Pais_1,Pais_2,Estadio ")]Partido partido)
        {
            try
            {
                // TODO: Add insert logic here
                Nodo<Partido, int> nueva_pais = new Nodo<Partido, int>(partido, null);
                nueva_pais.valor = partido;
                nueva_pais.llave = partido.Nopartido;
                db.datos.Clear();
                db.AB.Insertar(nueva_pais);
                db.AB.EnOrden(asignar_comparacion);
                db.AB.EnOrden(pasar_a_lista);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public void pasar_a_lista(Nodo<Partido, int> actual)
        {
            db.datos.Add(actual.valor);
        }

        public int comparador_no(int actual, int nuevo)
        {
            return actual.CompareTo(nuevo);

        }

        public void asignar_comparacion(Nodo<Partido, int> actual)
        {
            actual.comparador = comparador_no;
        }

        // GET: Partidono/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Partidono/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Partidono/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Partido partido_buscado = db.datos.Find(x => x.Nopartido == id);

            if (partido_buscado == null)
            {

                return HttpNotFound();
            }
            return View(partido_buscado);
        }

        // POST: Partido/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                db.AB.Eliminar(db.datos.First(x => x.Nopartido == id).Nopartido);
                db.datos.Clear();
                db.AB.EnOrden(asignar_comparacion);
                db.AB.EnOrden(pasar_a_lista);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
