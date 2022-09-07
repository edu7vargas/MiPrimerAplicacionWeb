using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiPrimerAplicacionWeb.Models;

namespace MiPrimerAplicacionWeb.Controllers
{
    public class MarcaController : Controller
    {
        // GET: Marca
        public ActionResult Index(MarcaCLS omarcaCLS)
        {

            // recepcion del campo de buscar
            string nombreMarca = omarcaCLS.nombre;

            List<MarcaCLS> listaMarca = null;
            using (var bd = new BDPasajeEntities())
            {
                if (omarcaCLS.nombre == null)
                {
                    listaMarca = (from marca in bd.Marca
                                  where marca.BHABILITADO == 1
                                  select new MarcaCLS
                                  {
                                      iidmarca = marca.IIDMARCA,
                                      nombre = marca.NOMBRE,
                                      descripcion = marca.DESCRIPCION
                                  }).ToList();

                }
                else {
                    listaMarca = (from marca in bd.Marca
                                  where marca.BHABILITADO == 1
                                  && marca.NOMBRE.Contains(nombreMarca)
                                  select new MarcaCLS
                                  {
                                      iidmarca = marca.IIDMARCA,
                                      nombre = marca.NOMBRE,
                                      descripcion = marca.DESCRIPCION
                                  }).ToList();
                }
            }                   
            return View(listaMarca);
        }







        public ActionResult Agregar()
        {
            return View();
        }




        public ActionResult Editar(int id)
        {

            MarcaCLS oMarcaCLS = new MarcaCLS();

            using (var bd = new BDPasajeEntities())
            {
                // con el "First" te devuelve un Objeto
                Marca oMarca = bd.Marca.Where(p => p.IIDMARCA.Equals(id)).First();
                oMarcaCLS.iidmarca = oMarca.IIDMARCA;
                oMarcaCLS.nombre = oMarca.NOMBRE;
                oMarcaCLS.descripcion = oMarca.DESCRIPCION;
            }
            return View(oMarcaCLS);
        }



        
        [HttpPost]
        public ActionResult Agregar(MarcaCLS oMarcaCLS)
        {
            int nroRegistrosEncontrados = 0;
            string nombreMarca = oMarcaCLS.nombre;
            using (var bd = new BDPasajeEntities())
            {
                nroRegistrosEncontrados = bd.Marca.Where(p => p.NOMBRE.Equals(nombreMarca)).Count();
            }

            if (!ModelState.IsValid || nroRegistrosEncontrados >= 1)
            {
                if (nroRegistrosEncontrados >= 1) oMarcaCLS.mensajeError = "La marca ya existe";
                return View(oMarcaCLS);
            }
            else {
                using (var bd = new BDPasajeEntities())
                { 
                    Marca oMarca = new Marca();
                    oMarca.NOMBRE = oMarcaCLS.nombre;
                    oMarca.DESCRIPCION = oMarcaCLS.descripcion;
                    oMarca.BHABILITADO = 1;
                    bd.Marca.Add(oMarca);
                    bd.SaveChanges();
                }

            }
            return RedirectToAction("Index");

        }




        [HttpPost]
        public ActionResult Editar(MarcaCLS oMarcaCLS)
        {
            int nroRegistrosEncontrados = 0;
            string nombreMarca = oMarcaCLS.nombre;
            int iidMarca = oMarcaCLS.iidmarca;
            using (var bd = new BDPasajeEntities())
            {
                nroRegistrosEncontrados = bd.Marca.Where(p => p.NOMBRE.Equals(nombreMarca) && !p.IIDMARCA.Equals(iidMarca)).Count();
            }

            if (!ModelState.IsValid|| nroRegistrosEncontrados>=1)
            {
                if (nroRegistrosEncontrados >= 1) oMarcaCLS.mensajeError = "La marca ya existe";
                return View(oMarcaCLS);
            }
            else
            {
                int idMarca = oMarcaCLS.iidmarca;
                using (var bd = new BDPasajeEntities())
                {
                    Marca oMarca = bd.Marca.Where(p => p.IIDMARCA.Equals(idMarca)).First();
                    oMarca.NOMBRE = oMarcaCLS.nombre;
                    oMarca.DESCRIPCION = oMarcaCLS.descripcion;
                    bd.SaveChanges();
                }

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Eliminar(int iidmarca)
        {

                using (var bd = new BDPasajeEntities())
                {
                    Marca oMarca = bd.Marca.Where(p => p.IIDMARCA.Equals(iidmarca)).First();
                    oMarca.BHABILITADO = 0;
                    bd.SaveChanges();
                    return RedirectToAction("Index");
                }
        }









    }
}