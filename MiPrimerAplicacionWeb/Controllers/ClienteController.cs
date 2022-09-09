using MiPrimerAplicacionWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiPrimerAplicacionWeb.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index(ClienteCLS oClienteCLS)
        {

            int idSexo = oClienteCLS.iidsexo;

            List<ClienteCLS> listaCliente;
            using (var bd = new BDPasajeEntities())
            {

                if (idSexo == 0)
                {
                    listaCliente = (from cliente in bd.Cliente
                                    where cliente.BHABILITADO == 1
                                    select new ClienteCLS
                                    {
                                        iidcliente = cliente.IIDCLIENTE,
                                        nombre = cliente.NOMBRE,
                                        appaterno = cliente.APPATERNO,
                                        apmaterno = cliente.APMATERNO,
                                        email = cliente.EMAIL,
                                        direccion = cliente.DIRECCION,
                                        telefonocelular = cliente.TELEFONOCELULAR,
                                        telefonofijo = cliente.TELEFONOFIJO

                                    }).ToList();
                }
                else {
                    listaCliente = (from cliente in bd.Cliente
                                    where cliente.BHABILITADO == 1
                                    && cliente.IIDSEXO == idSexo
                                    select new ClienteCLS
                                    {
                                        iidcliente = cliente.IIDCLIENTE,
                                        nombre = cliente.NOMBRE,
                                        appaterno = cliente.APPATERNO,
                                        apmaterno = cliente.APMATERNO,
                                        email = cliente.EMAIL,
                                        direccion = cliente.DIRECCION,
                                        telefonocelular = cliente.TELEFONOCELULAR,
                                        telefonofijo = cliente.TELEFONOFIJO

                                    }).ToList();
                }

            }

            llenarSexo();
            ViewBag.lista = listaSexo;


            return View(listaCliente);
        }



        public ActionResult Agregar() {

            llenarSexo();
            ViewBag.lista = listaSexo;
            return View();
        }




        public ActionResult Editar(int id)
        {

            ClienteCLS oClienteCLS = new ClienteCLS();

            using (var bd = new BDPasajeEntities())
            {
                llenarSexo();
                ViewBag.lista = listaSexo;
                // con el "First" te devuelve un Objeto
                Cliente oCliente = bd.Cliente.Where(p => p.IIDCLIENTE.Equals(id)).First();


                oClienteCLS.iidcliente = oCliente.IIDCLIENTE;
                oClienteCLS.nombre = oCliente.NOMBRE;
                oClienteCLS.appaterno = oCliente.APPATERNO;
                oClienteCLS.apmaterno = oCliente.APMATERNO;
                oClienteCLS.email = oCliente.EMAIL;
                oClienteCLS.iidsexo = (int)oCliente.IIDSEXO;
                oClienteCLS.direccion = oCliente.DIRECCION;
                oClienteCLS.telefonocelular = oCliente.TELEFONOCELULAR;
                oClienteCLS.telefonofijo = oCliente.TELEFONOFIJO;

            }
            return View(oClienteCLS);
        }





        [HttpPost]
        public ActionResult Agregar(ClienteCLS oClienteCLS)
        {

            int nroRegistrosEncontrados = 0;
            string nombreCliente = oClienteCLS.nombre;
            string appaternoCliente = oClienteCLS.appaterno;
            string apmaternoCliente = oClienteCLS.apmaterno;

            using (var bd = new BDPasajeEntities())
            {
                nroRegistrosEncontrados = bd.Cliente.Where(p => p.NOMBRE.Equals(nombreCliente) && p.APPATERNO.Equals(appaternoCliente) && p.APMATERNO.Equals(apmaternoCliente)).Count();
            }



            if (!ModelState.IsValid || nroRegistrosEncontrados >= 1)
            {
                if (nroRegistrosEncontrados >= 1) oClienteCLS.mensajeError = "El cliente con ese nombre y apellidos ya existe.";
                llenarSexo();
                ViewBag.lista = listaSexo;

                return View(oClienteCLS);
            }
            else
            {
                using (var bd = new BDPasajeEntities())
                {
                    Cliente oCliente = new Cliente();
                    oCliente.NOMBRE = oClienteCLS.nombre;
                    oCliente.APPATERNO = oClienteCLS.appaterno;
                    oCliente.APMATERNO = oClienteCLS.apmaterno;
                    oCliente.EMAIL = oClienteCLS.email;
                    oCliente.DIRECCION = oClienteCLS.direccion;
                    oCliente.IIDSEXO = oClienteCLS.iidsexo;
                    oCliente.TELEFONOFIJO = oClienteCLS.telefonofijo;
                    oCliente.TELEFONOCELULAR = oClienteCLS.telefonocelular;
                    oCliente.BHABILITADO = 1;

                    bd.Cliente.Add(oCliente);
                    bd.SaveChanges();
                }

            }
            return RedirectToAction("Index");



        }



        [HttpPost]
        public ActionResult Editar(ClienteCLS oClienteCLS)
        {


            int nroRegistrosEncontrados = 0;
            int iidcliente = oClienteCLS.iidcliente;
            string nombreCliente = oClienteCLS.nombre;
            string appaternoCliente = oClienteCLS.appaterno;
            string apmaternoCliente = oClienteCLS.apmaterno;

            using (var bd = new BDPasajeEntities())
            {
                nroRegistrosEncontrados = bd.Cliente.Where(p => p.NOMBRE.Equals(nombreCliente) && p.APPATERNO.Equals(appaternoCliente) && p.APMATERNO.Equals(apmaternoCliente) && !p.IIDCLIENTE.Equals(iidcliente)).Count();
            }


            if (!ModelState.IsValid || nroRegistrosEncontrados >= 1)
            {
                if (nroRegistrosEncontrados >= 1) oClienteCLS.mensajeError = "El cliente con ese nombre y apellidos ya existe.";
                llenarSexo();
                ViewBag.lista = listaSexo;

                return View(oClienteCLS);
            }
            else
            {
                int idCliente = oClienteCLS.iidcliente;
                using (var bd = new BDPasajeEntities())
                {
                    Cliente oCliente = bd.Cliente.Where(p => p.IIDCLIENTE.Equals(idCliente)).First();
                    oCliente.NOMBRE = oClienteCLS.nombre;
                    oCliente.APPATERNO = oClienteCLS.appaterno;
                    oCliente.APMATERNO = oClienteCLS.apmaterno;
                    oCliente.EMAIL = oClienteCLS.email;
                    oCliente.DIRECCION = oClienteCLS.direccion;
                    oCliente.IIDSEXO = oClienteCLS.iidsexo;
                    oCliente.TELEFONOFIJO = oClienteCLS.telefonofijo;
                    oCliente.TELEFONOCELULAR = oClienteCLS.telefonocelular;
                    bd.SaveChanges();
                }

            }
            return RedirectToAction("Index");

        }


        [HttpPost]
        public ActionResult Eliminar(int iidcliente)
        {
            using (var bd = new BDPasajeEntities())
            {
                Cliente oCliente = bd.Cliente.Where(p => p.IIDCLIENTE.Equals(iidcliente)).First();
                oCliente.BHABILITADO = 0;
                bd.SaveChanges();
                return RedirectToAction("Index");
            }

        }




        List<SelectListItem> listaSexo;
        private void llenarSexo()
        {

            using (var bd = new BDPasajeEntities())
            {
                listaSexo = (from sexo in bd.Sexo
                             where sexo.BHABILITADO == 1
                             select new SelectListItem
                             {
                                 Text = sexo.NOMBRE,
                                 Value = sexo.IIDSEXO.ToString()
                             }).ToList();
                listaSexo.Insert(0, new SelectListItem { Text = "--Seleccione --", Value = "" });
            }
        }



    }
}