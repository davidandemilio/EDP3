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
namespace ED1L3.Controllers
{
    public class PartidoController : Controller
    {


        DefaultConnection<Partido,DateTime> db = DefaultConnection<Partido, DateTime>.getInstance;
        // GET: Partido
        public ActionResult Index()
        {
            return View(db.datos.ToList());
        }

        // GET: Partido/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Partido/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Partido/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "Nopartido,FechaPartido,Grupo,Pais_1,Pais_2,Estadio ")]Partido partido)
        {
            try
            {
                // TODO: Add insert logic here
                Nodo<Partido,    DateTime> nueva_pais = new Nodo<Partido,DateTime>(partido, null);
                nueva_pais.valor = partido;
                nueva_pais.llave = partido.FechaPartido;
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

        public void pasar_a_lista(Nodo<Partido,DateTime> actual)
        {
            db.datos.Add(actual.valor);
        }

        public int comparador_fechas(DateTime actual, DateTime nuevo)
        {
            return actual.CompareTo(nuevo);

        }

        public void asignar_comparacion(Nodo<Partido,DateTime> actual)
        {
            actual.comparador = comparador_fechas;
        }

        // GET: Partido/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Partido/Edit/5
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

        // GET: Partido/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Partido/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
