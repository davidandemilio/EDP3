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
using ED1L3.Controllers;

namespace ED1L3.Models
{
    public class PartidonoController : Controller
    {
        DefaultConnection<Partido, int> db = DefaultConnection<Partido, int>.getInstance;

        public ActionResult CargaArchivo(HttpPostedFileBase archivo)
        {
            List<Nodo<Partido, int>> nodos_a_insertar = new List<Nodo<Partido, int>>();
            Nodo<Partido, int> nuevo;

            db.datos.Clear();
            Carga_de_Archivo<Partido, int> archivo_json = new Carga_de_Archivo<Partido, int>();
            nodos_a_insertar = archivo_json.Cargajsoninterna(archivo, Server);
            for (int i = 0; i < nodos_a_insertar.Count; i++)
            {
                nuevo = nodos_a_insertar[i];
                nuevo.llave = nuevo.valor.Nopartido;

                db.datos.Clear();
                db.AB.Insertar(nuevo);
                db.AB.EnOrden(asignar_comparacion);
                db.AB.EnOrden(pasar_a_lista);
            }

            return RedirectToAction("Index");
        }

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
        public ActionResult Create([Bind(Include = "Nopartido,FechaPartido,Grupo,Pais1,Pais2,Estadio ")]Partido partido)
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

        [HttpPost]
        public void Search(int target)
        {
            bool seleccion = false;
            Nodo<Partido, int> node = db.AB.Raiz;
            if (node != null)
            {
                while (node != null)
                {
                    if (comparador_no(node.valor.Nopartido, target) > 0)
                    {
                        node = node.izquierdo;
                    }
                    else if (comparador_no(node.valor.Nopartido, target) < 0)
                    {
                        node = node.derecho;
                    }
                    else if (comparador_no(node.valor.Nopartido, target) == 0)
                    {
                        break;
                    }
                    else
                    {
                        seleccion = true;
                    }
                }
                List<string> lista = new List<string>();
                lista.Add(node.valor.Nopartido.ToString());
                lista.Add(node.valor.FechaPartido.ToString());
                lista.Add(node.valor.Grupo);
                lista.Add(node.valor.Pais_1);
                lista.Add(node.valor.Pais_2);
                lista.Add(node.valor.Estadio);
                if (seleccion == false)
                {
                    Response.Write("Equipo encontrado: " + " " + "No. Partido: " + " " + lista[0].ToString() + " " + "Fecha de partido: " + " " + lista[1].ToString() + " " + "Grupo: " + " " + lista[2].ToString() + " " + "Pais_1" + " " + lista[3].ToString() + " " + "Pais_2" + " " + lista[4].ToString() + " " + "Estadio: " + " " + lista[5].ToString());
                }
                else
                {
                    Response.Write("Equipo no encontrado");
                }
            }
        }
        // GET: Partidono/Delete/5
        public ActionResult Delete(int? id)
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
