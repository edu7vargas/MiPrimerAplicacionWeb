using MiPrimerAplicacionWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiPrimerAplicacionWeb.Controllers
{
    public class SucursalController : Controller
    {
        // GET: Sucursal
        public ActionResult Index(SucursalCLS oSucursalCls)
        {
            List<SucursalCLS> listaSucursal =  null;
            string nombreSucursal = oSucursalCls.nombre;

            using (var bd = new BDPasajeEntities())
            { 
                if (oSucursalCls.nombre == null)
                {
              
                        listaSucursal = (from sucursal in bd.Sucursal
                                         where sucursal.BHABILITADO == 1
                                         select new SucursalCLS
                                         {
                                             iidsucursal = sucursal.IIDSUCURSAL,
                                             nombre = sucursal.NOMBRE,
                                             direccion = sucursal.DIRECCION,
                                             telefono = sucursal.TELEFONO,
                                             email = sucursal.EMAIL
                                         }).ToList();
              
                }
                else {

                    listaSucursal = (from sucursal in bd.Sucursal
                                     where sucursal.BHABILITADO == 1
                                     && sucursal.NOMBRE.Contains(nombreSucursal)
                                     select new SucursalCLS
                                     {
                                         iidsucursal = sucursal.IIDSUCURSAL,
                                         nombre = sucursal.NOMBRE,
                                         direccion = sucursal.DIRECCION,
                                         telefono = sucursal.TELEFONO,
                                         email = sucursal.EMAIL
                                     }).ToList();

                }
                
            }




            return View(listaSucursal);
        }





        public ActionResult Agregar()
        {

            return View();

        }


        public ActionResult Editar(int id)
        {
            SucursalCLS oSucursalCLS = new SucursalCLS();

            using (var bd = new BDPasajeEntities())
            {
                // con el "First" te devuelve un Objeto
                Sucursal oSucursal = bd.Sucursal.Where(p => p.IIDSUCURSAL.Equals(id)).First();
                oSucursalCLS.iidsucursal = oSucursal.IIDSUCURSAL;
                oSucursalCLS.nombre = oSucursal.NOMBRE;
                oSucursalCLS.direccion = oSucursal.DIRECCION;
                oSucursalCLS.telefono = oSucursal.TELEFONO;
                oSucursalCLS.email = oSucursal.EMAIL;
                oSucursalCLS.fechaapertura = (DateTime)oSucursal.FECHAAPERTURA;
            }
            return View(oSucursalCLS);

        }


        [HttpPost]
        public ActionResult Agregar(SucursalCLS oSucursalCLS)
        {
            int nroRegistrosEncontrados = 0;
            string nombreSucursal = oSucursalCLS.nombre;
            using (var bd = new BDPasajeEntities())
            {
                nroRegistrosEncontrados = bd.Sucursal.Where(p => p.NOMBRE.Equals(nombreSucursal)).Count();
            }

            if (!ModelState.IsValid || nroRegistrosEncontrados >= 1)
            {
                if (nroRegistrosEncontrados >= 1) oSucursalCLS.mensajeError = "La sucursal ya existe.";
                return View(oSucursalCLS);
            }
            else
            {
                using (var bd = new BDPasajeEntities())
                {
                    Sucursal oSucursal = new Sucursal();
                    oSucursal.NOMBRE = oSucursalCLS.nombre;
                    oSucursal.DIRECCION = oSucursalCLS.direccion;
                    oSucursal.TELEFONO = oSucursalCLS.telefono;
                    oSucursal.EMAIL = oSucursalCLS.email;
                    oSucursal.FECHAAPERTURA = oSucursalCLS.fechaapertura;
                    oSucursal.BHABILITADO = 1;
                    bd.Sucursal.Add(oSucursal);
                    bd.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Editar(SucursalCLS oSucursalCLS)
        {
            int nroRegistrosEncontrados = 0;
            string nombreSucursal = oSucursalCLS.nombre;
            int idSucursal = oSucursalCLS.iidsucursal;
            using (var bd = new BDPasajeEntities())
            {
                nroRegistrosEncontrados = bd.Sucursal.Where(p=> p.NOMBRE.Equals(nombreSucursal) && !p.IIDSUCURSAL.Equals(idSucursal)).Count();
            }


                if (!ModelState.IsValid || nroRegistrosEncontrados >= 1)
                {
                    if (nroRegistrosEncontrados >= 1) oSucursalCLS.mensajeError = "La sucursal ya existe.";
                    return View(oSucursalCLS);
                }
                else
                {
                    using (var bd = new BDPasajeEntities())
                    {
                        Sucursal oSucursal = bd.Sucursal.Where(p => p.IIDSUCURSAL.Equals(idSucursal)).First();
                        oSucursal.NOMBRE = oSucursalCLS.nombre;
                        oSucursal.DIRECCION = oSucursalCLS.direccion;
                        oSucursal.TELEFONO = oSucursalCLS.telefono;
                        oSucursal.EMAIL = oSucursalCLS.email;
                        oSucursal.FECHAAPERTURA = oSucursalCLS.fechaapertura;
                        bd.SaveChanges();
                    }

                }
            return RedirectToAction("Index");

        }


        [HttpPost]
        public ActionResult Eliminar(int iidsucursal)
        {
            SucursalCLS oSucursalCLS = new SucursalCLS();


            using (var bd = new BDPasajeEntities())
            {
                Sucursal oSucursal = bd.Sucursal.Where(p => p.IIDSUCURSAL.Equals(iidsucursal)).First();
                oSucursal.BHABILITADO = 0;
                bd.SaveChanges();
            }

            return RedirectToAction("Index");


        }
    }
}