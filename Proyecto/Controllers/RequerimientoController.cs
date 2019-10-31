﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;
using Newtonsoft.Json;



namespace Proyecto.Controllers
{
    
    public class RequerimientoController : Controller
    {
        //Variables para saber quien está logeado y a que proyecto pertenece
        public string usuario = "";
        public string cedula = "";
        public string proy = "";

        Proyecto.Controllers.ProyectoController proyController = new Proyecto.Controllers.ProyectoController();
        Proyecto.Controllers.ModuloeController moduloController = new Proyecto.Controllers.ModuloeController();

        // GET: Requerimiento
        public async Task<ActionResult> Index()
        {
            string usuario = System.Web.HttpContext.Current.Session["rol"] as string;
            ViewBag.user = usuario;
            string proy = System.Web.HttpContext.Current.Session["proyecto"] as string;
            string cedula = System.Web.HttpContext.Current.Session["cedula"] as string;

            using (Gr02Proy3Entities db = new Gr02Proy3Entities())
            {
                var requerimiento = db.Requerimiento.Include(r => r.EmpleadoDesarrollador).Include(r => r.Modulo);
                return View(await requerimiento.ToListAsync());
            }
        }




        [HttpPost]
        public ActionResult Index(string nombreProyecto, string nombreModulo)
        {
            string usuario = System.Web.HttpContext.Current.Session["rol"] as string;
            ViewBag.user = usuario;
            string proy = System.Web.HttpContext.Current.Session["proyecto"] as string;
            string cedula = System.Web.HttpContext.Current.Session["cedula"] as string;

            


            using (Gr02Proy3Entities db = new Gr02Proy3Entities())
            {
<<<<<<< HEAD
=======
                string usuario = System.Web.HttpContext.Current.Session["rol"] as string;
                ViewBag.user = usuario;
                string proy = System.Web.HttpContext.Current.Session["proyecto"] as string;
                string cedula = System.Web.HttpContext.Current.Session["cedula"] as string;
                ViewBag.proyectos = getProyectos(usuario, cedula);

              
                
>>>>>>> 4e86fa6f7e27be256334e4a3041615e744bf1adb

                var queryMod = from a in db.Modulo
                               where a.NombreProy.Equals(nombreProyecto) && (a.Nombre.Equals(nombreModulo))
                               select a.Id;
<<<<<<< HEAD
                if (queryMod != null)
                {
                    var queryReq = from a in db.Requerimiento
                                   where a.nombreProyecto_FK == nombreProyecto && a.idModulo_FK == queryMod.FirstOrDefault()
                                   select a;
                    return View(queryReq.ToList());
                }
                else
                {
                    var queryReq = from a in db.Requerimiento
                                   where a.nombreProyecto_FK == nombreProyecto
                                   select a;
=======

>>>>>>> 4e86fa6f7e27be256334e4a3041615e744bf1adb

                var queryReq = from a in db.Requerimiento
                               where a.nombreProyecto_FK == nombreProyecto && a.idModulo_FK == queryMod.FirstOrDefault()
                               select a;


<<<<<<< HEAD
                /*if (usuario != "Jefe")
                {
                    if (usuario == "Cliente"  || usuario == "Lider")
                    {//Para cliente y lider ven los requerimientos de su proyecto
                        var obj = from a in db.Requerimiento
                                  from b in db.Cliente
                                  from c in db.Equipo
                                  from d in db.Proyecto
                                  from e in db.EmpleadoDesarrollador
                                  where d.cedulaCliente == b.cedula
                                  where c.rol == true
                                  where c.cedulaEM_FK == e.cedulaED
                                  where c.nombreProy_FK == d.nombre
                                  where a.nombreProyecto_FK == d.nombre
                                  select a;


                        return View(obj.ToList());
                    }
                    else
                    {//Para desarrollador solo puede ver los requerimientos en los que está asignado 
                        var obj = from a in db.Requerimiento
                                  from c in db.Equipo
                                  from d in db.Proyecto
                                  from e in db.EmpleadoDesarrollador
                                  where c.cedulaEM_FK == e.cedulaED
                                  where c.nombreProy_FK == d.nombre
                                  where a.nombreProyecto_FK == d.nombre
                                  where a.cedulaResponsable_FK == e.cedulaED
                                  select a;

                        return View(obj.ToList());
                    }
                }
                else
                {
                    return View(db.Requerimiento.ToList());
                }*/




=======
                ViewBag.Modulo = nombreModulo;

                return View(queryReq.ToList());




            }
        }





        public ActionResult consulReq(int modID,string nombreModulo, string nombreProy)
        {
            using (Gr02Proy3Entities db = new Gr02Proy3Entities())
            {
                string usuario = System.Web.HttpContext.Current.Session["rol"] as string;
                ViewBag.user = usuario;
                string proy = System.Web.HttpContext.Current.Session["proyecto"] as string;
                string cedula = System.Web.HttpContext.Current.Session["cedula"] as string;


                ViewBag.Proyecto = nombreProy;
                ViewBag.Modulo = nombreModulo;

                // var requerimiento = db.Requerimiento.Include(r => r.EmpleadoDesarrollador).Include(r => r.Modulo);
                //var requerimiento = reqController.getRequerimientos(modID, nombreProy, "BackLog");
                var requerimiento = from Req in db.Requerimiento
                            where Req.idModulo_FK == modID && Req.nombreProyecto_FK == nombreProy
                            select Req;




                return View(requerimiento.ToList());

>>>>>>> 4e86fa6f7e27be256334e4a3041615e744bf1adb
            }

        }

        


        // GET: Requerimiento/Details/5
        public async Task<ActionResult> Details(string id)
        {
            using (Gr02Proy3Entities db = new Gr02Proy3Entities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Requerimiento requerimiento = await db.Requerimiento.FindAsync(id);
                if (requerimiento == null)
                {
                    return HttpNotFound();
                }
                return View(requerimiento);
            }
        }

        // GET: Requerimiento/Create
        public ActionResult Create()
        {
            using (Gr02Proy3Entities db = new Gr02Proy3Entities())
            {
                ViewBag.cedulaResponsable_FK = new SelectList(db.EmpleadoDesarrollador, "cedulaED", "nombreED");
                ViewBag.nombreProyecto_FK = new SelectList(db.Modulo, "NombreProy", "Nombre");
                return View();
            }
        }

        // POST: Requerimiento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Proyecto, string Modulo, [Bind(Include = "nombreProyecto_FK,idModulo_FK,nombre,complejidad,duracionEstimada,duracionReal,cedulaResponsable_FK,estado")] Requerimiento requerimiento)
        {
            using (Gr02Proy3Entities db = new Gr02Proy3Entities())
            {
                if (ModelState.IsValid)
                {
                    var queryMod = from a in db.Modulo
                                   where a.NombreProy.Equals(Proyecto) && (a.Nombre.Equals(Modulo))
                                   select a.Id;
                    string input = requerimiento.nombre;
                    string output = input.Replace("requerimiento", "");
                    requerimiento.nombre = output;
                    requerimiento.nombreProyecto_FK = Proyecto;
                    requerimiento.idModulo_FK = queryMod.FirstOrDefault();
                    requerimiento.cedulaResponsable_FK = "302250355";
                    db.Requerimiento.Add(requerimiento);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.cedulaResponsable_FK = new SelectList(db.EmpleadoDesarrollador, "cedulaED", "nombreED", requerimiento.cedulaResponsable_FK);
                ViewBag.nombreProyecto_FK = new SelectList(db.Modulo, "NombreProy", "Nombre", requerimiento.nombreProyecto_FK);
                return View(requerimiento);
            }
        }

        // GET: Requerimiento/Edit/5
        public async Task<ActionResult> Edit(string nombreProyecto,int modID , string nombreReq)
        {

            using (Gr02Proy3Entities db = new Gr02Proy3Entities())
            {

                Requerimiento requerimiento = await db.Requerimiento.FindAsync(modID,nombreProyecto);
                if (requerimiento == null)
                {
                    return HttpNotFound();
                }

                ViewBag.cedulaResponsable_FK = new SelectList(db.EmpleadoDesarrollador, "cedulaED", "nombreED", requerimiento.cedulaResponsable_FK);
                ViewBag.nombreProyecto_FK = new SelectList(db.Modulo, "NombreProy", "Nombre", requerimiento.nombreProyecto_FK);
                return View(requerimiento);
            }
        }

        // POST: Requerimiento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "nombreProyecto_FK,idModulo_FK,nombre,complejidad,duracionEstimada,duracionReal,cedulaResponsable_FK,estado")] Requerimiento requerimiento)
        {
            using (Gr02Proy3Entities db = new Gr02Proy3Entities())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(requerimiento).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                ViewBag.cedulaResponsable_FK = new SelectList(db.EmpleadoDesarrollador, "cedulaED", "nombreED", requerimiento.cedulaResponsable_FK);
                ViewBag.nombreProyecto_FK = new SelectList(db.Modulo, "NombreProy", "Nombre", requerimiento.nombreProyecto_FK);
                return View(requerimiento);
            }
        }

        // GET: Requerimiento/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            using (Gr02Proy3Entities db = new Gr02Proy3Entities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Requerimiento requerimiento = await db.Requerimiento.FindAsync(id);
                if (requerimiento == null)
                {
                    return HttpNotFound();
                }
                return View(requerimiento);
            }
        }

        // POST: Requerimiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            using (Gr02Proy3Entities db = new Gr02Proy3Entities())
            {
                Requerimiento requerimiento = await db.Requerimiento.FindAsync(id);
                db.Requerimiento.Remove(requerimiento);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }


        protected override void Dispose(bool disposing)
        {
            using (Gr02Proy3Entities db = new Gr02Proy3Entities())
            {
                if (disposing)
                {
                    db.Dispose();
                }
                base.Dispose(disposing);
            }
        }




<<<<<<< HEAD
        //Obtiene proyectos de controlador
=======

>>>>>>> 4e86fa6f7e27be256334e4a3041615e744bf1adb
        public SelectList getProyectos(string rol, string cedula)
        {
            using (Gr02Proy3Entities db = new Gr02Proy3Entities())
            {
<<<<<<< HEAD
                return this.proyController.getProyectos(rol, cedula);
=======
                var listaproyectos = this.proyController.getProyectos(rol, cedula);
                return listaproyectos;
>>>>>>> 4e86fa6f7e27be256334e4a3041615e744bf1adb
            }
        }

        
        public class Proyectito
        {
            public string nombreProyecto { get; set; }
        }
        
        [HttpPost]
        public JsonResult getModulos(string nombreproyecto)
        {
                       
            using (Gr02Proy3Entities db = new Gr02Proy3Entities())
            {
             

                
               // Proyectito jsonData = JsonConvert.DeserializeObject<RequerimientoController.Proyectito>(nombreproyecto);

                // return Json(this.moduloController.getModulos(jsonData.nombreProyecto));
                return Json(this.moduloController.getModulos(nombreproyecto));
            }
        }



    }
}
