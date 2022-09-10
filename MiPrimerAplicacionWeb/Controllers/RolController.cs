using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiPrimerAplicacionWeb.Models;
namespace MiPrimerAplicacionWeb.Controllers
{
    public class RolController : Controller
    {
        // GET: Rol
        public ActionResult Index()
        {
            List <RolCLS> listaRol = new List<RolCLS>();
            using (var db = new BDPasajeEntities())
            {
                listaRol = (from rol in db.Rol
                            where rol.BHABILITADO == 1
                            select new RolCLS
                            {
                                iidRol = rol.IIDROL,
                                nombre = rol.NOMBRE,
                                descripcion = rol.DESCRIPCION
                            }).ToList();
            }
            return View(listaRol);
        }


        public ActionResult Filtro(string nombre)
        {
            List<RolCLS> listaRol = new List<RolCLS>();
            using (var db = new BDPasajeEntities())
            {
                if (nombre == null)
                {
                    listaRol = (from rol in db.Rol
                                where rol.BHABILITADO == 1
                                select new RolCLS
                                {
                                    iidRol = rol.IIDROL,
                                    nombre = rol.NOMBRE,
                                    descripcion = rol.DESCRIPCION
                                }).ToList();
                }
                else {

                    listaRol = (from rol in db.Rol
                                where rol.BHABILITADO == 1
                                && rol.NOMBRE.Contains(nombre)
                                select new RolCLS
                                {
                                    iidRol = rol.IIDROL,
                                    nombre = rol.NOMBRE,
                                    descripcion = rol.DESCRIPCION
                                }).ToList();
                }
            }
            return PartialView("_TablaRol",listaRol);
        }



    }
    
}