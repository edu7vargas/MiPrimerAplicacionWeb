using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiPrimerAplicacionWeb.Models;

namespace MiPrimerAplicacionWeb.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        public ActionResult Index(EmpleadoCLS oEmpleadoCLS)
        {

            int idTipoUsuario = oEmpleadoCLS.iidtipoUsuario;
            List<EmpleadoCLS> listaEmpleado = null;
            
            listarCombos();
            using (var bd = new BDPasajeEntities())
            {

                if (idTipoUsuario==0) {
                    listaEmpleado = (from empleado in bd.Empleado
                                     join tipousuario in bd.TipoUsuario
                                     on empleado.IIDTIPOUSUARIO equals tipousuario.IIDTIPOUSUARIO
                                     join tipoContrato in bd.TipoContrato
                                     on empleado.IIDTIPOCONTRATO equals tipoContrato.IIDTIPOCONTRATO
                                     where empleado.BHABILITADO == 1
                                     select new EmpleadoCLS
                                     {
                                         iidEmpleado = empleado.IIDEMPLEADO,
                                         nombre = empleado.NOMBRE,
                                         appaterno = empleado.APPATERNO,
                                         apmaterno = empleado.APMATERNO,
                                         nombreTipoUsuario = tipousuario.NOMBRE,
                                         nombreTipoContrato = tipoContrato.NOMBRE

                                     }).ToList();

                }
                else {
                    listaEmpleado = (from empleado in bd.Empleado
                                     join tipousuario in bd.TipoUsuario
                                     on empleado.IIDTIPOUSUARIO equals tipousuario.IIDTIPOUSUARIO
                                     join tipoContrato in bd.TipoContrato
                                     on empleado.IIDTIPOCONTRATO equals tipoContrato.IIDTIPOCONTRATO
                                     where empleado.BHABILITADO == 1 &&
                                     empleado.IIDTIPOUSUARIO == idTipoUsuario
                                     select new EmpleadoCLS
                                     {
                                         iidEmpleado = empleado.IIDEMPLEADO,
                                         nombre = empleado.NOMBRE,
                                         appaterno = empleado.APPATERNO,
                                         apmaterno = empleado.APMATERNO,
                                         nombreTipoUsuario = tipousuario.NOMBRE,
                                         nombreTipoContrato = tipoContrato.NOMBRE

                                     }).ToList();

                }



            }

                return View(listaEmpleado);
        }




        public ActionResult Agregar()
        {

            listarCombos();
            return View();
        }


        public ActionResult Editar(int id)
        {

            
            EmpleadoCLS oEmpleadoCLS = new EmpleadoCLS();

            using (var bd = new BDPasajeEntities())
            {
                listarCombos();
                // con el "First" te devuelve un Objeto
                Empleado oEmpleado = bd.Empleado.Where(p => p.IIDEMPLEADO.Equals(id)).First();

                oEmpleadoCLS.iidEmpleado = oEmpleado.IIDEMPLEADO;
                oEmpleadoCLS.nombre = oEmpleado.NOMBRE;
                oEmpleadoCLS.appaterno = oEmpleado.APPATERNO;
                oEmpleadoCLS.apmaterno = oEmpleado.APMATERNO;
                oEmpleadoCLS.fechaContrato = (DateTime)oEmpleado.FECHACONTRATO;
                oEmpleadoCLS.sueldo = (decimal)oEmpleado.SUELDO;
                oEmpleadoCLS.iidSexo = (int)oEmpleado.IIDSEXO;
                oEmpleadoCLS.iidtipoContrato  = (int)oEmpleado.IIDTIPOCONTRATO;
                oEmpleadoCLS.iidtipoUsuario = (int)oEmpleado.IIDTIPOUSUARIO;

            }
            return View(oEmpleadoCLS);
        }




        [HttpPost]
        public ActionResult Agregar(EmpleadoCLS oEmpleadoCLS)
        {


            int nroRegistrosEncontrados = 0;
            string nombreEmpleado = oEmpleadoCLS.nombre;
            string appaternoEmpleado = oEmpleadoCLS.appaterno;
            string apmaternoEmpleado = oEmpleadoCLS.apmaterno;

            using (var bd = new BDPasajeEntities())
            {
                nroRegistrosEncontrados = bd.Empleado.Where(p => p.NOMBRE.Equals(nombreEmpleado) && p.APPATERNO.Equals(appaternoEmpleado) && p.APMATERNO.Equals(apmaternoEmpleado)).Count();
            }


            if (!ModelState.IsValid || nroRegistrosEncontrados >= 1)
            {
                if (nroRegistrosEncontrados >= 1) oEmpleadoCLS.mensajeError2 = "El empleado con ese nombre y apellidos ya existe.";
                listarCombos();
                return View(oEmpleadoCLS);
            }
            else
            {
                using (var bd = new BDPasajeEntities())
                {
                    Empleado oEmpleado = new Empleado();
                    oEmpleado.NOMBRE = oEmpleadoCLS.nombre;
                    oEmpleado.APPATERNO = oEmpleadoCLS.appaterno;
                    oEmpleado.APMATERNO = oEmpleadoCLS.apmaterno;
                    oEmpleado.FECHACONTRATO = oEmpleadoCLS.fechaContrato;
                    oEmpleado.SUELDO = oEmpleadoCLS.sueldo;
                    oEmpleado.IIDSEXO = oEmpleadoCLS.iidSexo;
                    oEmpleado.IIDTIPOCONTRATO = oEmpleadoCLS.iidtipoContrato;
                    oEmpleado.IIDTIPOUSUARIO = oEmpleadoCLS.iidtipoUsuario;

                    oEmpleado.BHABILITADO = 1;

                    bd.Empleado.Add(oEmpleado);
                    bd.SaveChanges();

                    return RedirectToAction("Index");
                }

            }
            

        }





        [HttpPost]
        public ActionResult Editar(EmpleadoCLS oEmpleadoCLS)
        {


            int nroRegistrosEncontrados = 0;
            int iidEmpleado = oEmpleadoCLS.iidEmpleado;
            string nombreEmpleado = oEmpleadoCLS.nombre;
            string appaternoEmpleado = oEmpleadoCLS.appaterno;
            string apmaternoEmpleado = oEmpleadoCLS.apmaterno;

            using (var bd = new BDPasajeEntities())
            {
                nroRegistrosEncontrados = bd.Empleado.Where(p => p.NOMBRE.Equals(nombreEmpleado) && p.APPATERNO.Equals(appaternoEmpleado) && p.APMATERNO.Equals(apmaternoEmpleado) && !p.IIDEMPLEADO.Equals(iidEmpleado)).Count();
            }


            if (!ModelState.IsValid || nroRegistrosEncontrados >= 1)
            {
                if (nroRegistrosEncontrados >= 1) oEmpleadoCLS.mensajeError2 = "El empleado con ese nombre y apellidos ya existe.";
                listarCombos();
                return View(oEmpleadoCLS);
            }
            else
            {
                using (var bd = new BDPasajeEntities())
                {
                    Empleado oEmpleado = bd.Empleado.Where(p => p.IIDEMPLEADO.Equals(iidEmpleado)).First();
                    oEmpleado.NOMBRE = oEmpleadoCLS.nombre;
                    oEmpleado.APPATERNO = oEmpleadoCLS.appaterno;
                    oEmpleado.APMATERNO = oEmpleadoCLS.apmaterno;
                    oEmpleado.FECHACONTRATO = oEmpleadoCLS.fechaContrato;
                    oEmpleado.SUELDO = oEmpleadoCLS.sueldo;
                    oEmpleado.IIDSEXO = oEmpleadoCLS.iidSexo;
                    oEmpleado.IIDTIPOCONTRATO = oEmpleadoCLS.iidtipoContrato;
                    oEmpleado.IIDTIPOUSUARIO = oEmpleadoCLS.iidtipoUsuario;
                    bd.SaveChanges();

                    return RedirectToAction("Index");
                }

            }


        }











        [HttpPost]
        public ActionResult Eliminar(int iidEmpleado) {

            using (var bd = new BDPasajeEntities())
            {
                Empleado oEmpleado = bd.Empleado.Where(p => p.IIDEMPLEADO.Equals(iidEmpleado)).First();
                oEmpleado.BHABILITADO = 0;
                bd.SaveChanges();

                return RedirectToAction("Index");
            }



        }






        List<SelectListItem> lista;
        private void listarComboSexo()
        {

            using (var bd = new BDPasajeEntities())
            {
                lista = (from sexo in bd.Sexo
                             where sexo.BHABILITADO == 1
                             select new SelectListItem
                             {
                                 Text = sexo.NOMBRE,
                                 Value = sexo.IIDSEXO.ToString()
                             }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione --", Value = "" });
                ViewBag.listaSexo = lista;
            }


        }





        List<SelectListItem> listaTipoContrato;
        private void listarComboTipoContrato()
        {

            using (var bd = new BDPasajeEntities())
            {
                lista = (from tipoContrato in bd.TipoContrato
                             where tipoContrato.BHABILITADO == 1
                             select new SelectListItem
                             {
                                 Text = tipoContrato.NOMBRE,
                                 Value = tipoContrato.IIDTIPOCONTRATO.ToString()
                             }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione --", Value = "" });
                ViewBag.listaTipoContrato = lista;
            }


        }



        List<SelectListItem> listaTipoUsuario;
        private void listarComboTipoUsuario()
        {

            using (var bd = new BDPasajeEntities())
            {
                lista = (from tipoUsuario in bd.TipoUsuario
                                     where tipoUsuario.BHABILITADO == 1
                                     select new SelectListItem
                                     {
                                         Text = tipoUsuario.NOMBRE,
                                         Value = tipoUsuario.IIDTIPOUSUARIO.ToString()
                                     }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione --", Value = "" });
                ViewBag.listaTipoUsuario = lista;
            }


        }


        public void listarCombos()
        {
            listarComboSexo();
            listarComboTipoContrato();
            listarComboTipoUsuario();
        }




    }

}