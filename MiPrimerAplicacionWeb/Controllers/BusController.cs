using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiPrimerAplicacionWeb.Models;

namespace MiPrimerAplicacionWeb.Controllers
{
    public class BusController : Controller
    {
        // GET: Bus
        public ActionResult Index()
        {
            List<BusCLS> listaBus = null;
            using (var bd = new BDPasajeEntities())
            {


                listaBus = (from bus in bd.Bus
                                 join sucursal in bd.Sucursal
                                 on bus.IIDSUCURSAL equals sucursal.IIDSUCURSAL
                                 join tipobus in bd.TipoBus
                                 on bus.IIDTIPOBUS equals tipobus.IIDTIPOBUS
                                 join modelo in bd.Modelo
                                 on bus.IIDMODELO equals modelo.IIDMODELO
                                 join marca in bd.Marca
                                 on bus.IIDMARCA equals marca.IIDMARCA
                            where bus.BHABILITADO == 1
                            select new BusCLS
                                 {
                                     iidbus = bus.IIDBUS,
                                     placa = bus.PLACA,
                                     // fechacompra = bus.FECHACOMPRA,
                                     // numerocolumnas = bus.NUMEROCOLUMNAS,
                                     // numerofilas = bus.NUMEROFILAS,
                                     descripcion = bus.DESCRIPCION,
                                     observacion = bus.OBSERVACION,
                                    nombreTipoBus = tipobus.NOMBRE,
                                    nombreSucursal = sucursal.NOMBRE,
                                    nombreModelo = modelo.NOMBRE,
                                    nombreMarca = marca.NOMBRE

                                 }).ToList();
            }

            return View(listaBus);
        
        }






        public ActionResult Agregar()
        {
            listarCombos();
            return View();
        }



        [HttpPost]
        public ActionResult Agregar(BusCLS oBusCls)
        {


            int nroRegistrosEncontrados = 0;
            string placaBus = oBusCls.placa;

            using (var bd = new BDPasajeEntities())
            {
                nroRegistrosEncontrados = bd.Bus.Where(p => p.PLACA.Equals(placaBus)).Count();
            }

            
            if (!ModelState.IsValid || nroRegistrosEncontrados >= 1) {
                if (nroRegistrosEncontrados >= 1) oBusCls.mensajeError = "La placa del Bus ya existe.";
                listarCombos();
                return View(oBusCls);
            }
            else
            { 

                        using (var db = new BDPasajeEntities())
                        {
                            Bus oBus = new Bus();
                            oBus.IIDSUCURSAL = oBusCls.iidsucursal;
                            oBus.IIDMARCA = oBusCls.iidmarca;
                            oBus.IIDMODELO = oBusCls.iidmodelo;
                            oBus.IIDTIPOBUS = oBusCls.iidtipobus;
                            oBus.NUMEROFILAS = oBusCls.numerofilas;
                            oBus.NUMEROCOLUMNAS = oBusCls.numerocolumnas;
                            oBus.FECHACOMPRA = oBusCls.fechacompra;
                            oBus.PLACA = oBusCls.placa;
                            oBus.DESCRIPCION = oBusCls.descripcion;
                            oBus.OBSERVACION = oBusCls.observacion;
                            oBus.BHABILITADO = 1;

                            db.Bus.Add(oBus);
                            db.SaveChanges();
    }
                
            }
            return RedirectToAction("Index");



        }



        public ActionResult Editar(int id)
        {


            BusCLS oBusCLS = new BusCLS();

            using (var bd = new BDPasajeEntities())
            {
                listarCombos();
                // con el "First" te devuelve un Objeto
                Bus oBus = bd.Bus.Where(p => p.IIDBUS.Equals(id)).First();

                oBusCLS.iidbus = id;
                oBusCLS.iidsucursal = (int)oBus.IIDSUCURSAL;
                oBusCLS.iidmarca = (int)oBus.IIDMARCA;
                oBusCLS.iidmodelo = (int)oBus.IIDMODELO;
                oBusCLS.iidtipobus = (int)oBus.IIDTIPOBUS;
                oBusCLS.numerofilas = (int)oBus.NUMEROFILAS;
                oBusCLS.numerocolumnas = (int)oBus.NUMEROCOLUMNAS;
                oBusCLS.fechacompra = (DateTime)oBus.FECHACOMPRA;
                oBusCLS.placa = oBus.PLACA;
                oBusCLS.descripcion = oBus.DESCRIPCION;
                oBusCLS.observacion = oBus.OBSERVACION;
                
            }
            return View(oBusCLS);
        }

        [HttpPost]
        public ActionResult Editar(BusCLS oBusCLS)
        {

            int nroRegistrosEncontrados = 0;
            int iidbus = oBusCLS.iidbus;
            string placaBus = oBusCLS.placa;

            using (var bd = new BDPasajeEntities())
            {
                nroRegistrosEncontrados = bd.Bus.Where(p => p.PLACA.Equals(placaBus) && !p.IIDBUS.Equals(iidbus)).Count();
            }


            if (!ModelState.IsValid || nroRegistrosEncontrados >= 1)
            {
                if (nroRegistrosEncontrados >= 1) oBusCLS.mensajeError = "La placa del Bus ya existe.";
                listarCombos();
                return View(oBusCLS);
            }
            else
            {

                int idBus = oBusCLS.iidbus;
                using (var db = new BDPasajeEntities())
                {
                    Bus oBus = db.Bus.Where(p => p.IIDBUS.Equals(idBus)).First();
                    oBus.IIDSUCURSAL = oBusCLS.iidsucursal;
                    oBus.IIDMARCA = oBusCLS.iidmarca;
                    oBus.IIDMODELO = oBusCLS.iidmodelo;
                    oBus.IIDTIPOBUS = oBusCLS.iidtipobus;
                    oBus.NUMEROFILAS = oBusCLS.numerofilas;
                    oBus.NUMEROCOLUMNAS = oBusCLS.numerocolumnas;
                    oBus.FECHACOMPRA = oBusCLS.fechacompra;
                    oBus.PLACA = oBusCLS.placa;
                    oBus.DESCRIPCION = oBusCLS.descripcion;
                    oBus.OBSERVACION = oBusCLS.observacion;
                    db.SaveChanges();

                }
                
            }
            return RedirectToAction("Index");
        }





        public ActionResult Eliminar(int iidbus)
        {
            using (var db = new BDPasajeEntities())
            {
                Bus oBus = db.Bus.Where(p => p.IIDBUS.Equals(iidbus)).First();
                oBus.BHABILITADO = 0;
                db.SaveChanges();
                return RedirectToAction("Index");
            }


        }



        List<SelectListItem> lista;
        List<SelectListItem> listaTipoBus;
        private void listarComboTipoBus()
        {

            using (var bd = new BDPasajeEntities())
            {
                lista = (from tipobus in bd.TipoBus
                         where tipobus.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = tipobus.NOMBRE,
                             Value = tipobus.IIDTIPOBUS.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione --", Value = "" });
                ViewBag.listaTipoBus = lista;
            }


        }





        List<SelectListItem> listaMarca;
        private void listarComboMarca()
        {

            using (var bd = new BDPasajeEntities())
            {
                lista = (from marca in bd.Marca
                         where marca.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = marca.NOMBRE,
                             Value = marca.IIDMARCA.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione --", Value = "" });
                ViewBag.listaMarca = lista;
            }


        }



        List<SelectListItem> listaModelo;
        private void listarComboModelo()
        {

            using (var bd = new BDPasajeEntities())
            {
                lista = (from modelo in bd.Modelo
                         where modelo.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = modelo.NOMBRE,
                             Value = modelo.IIDMODELO.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione --", Value = "" });
                ViewBag.listaModelo = lista;
            }


        }


        List<SelectListItem> listaSucursal;
        private void listarComboSucursal()
        {

            using (var bd = new BDPasajeEntities())
            {
                lista = (from sucursal in bd.Sucursal
                         where sucursal.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = sucursal.NOMBRE,
                             Value = sucursal.IIDSUCURSAL.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione --", Value = "" });
                ViewBag.listaSucursal = lista;
            }


        }










        public void listarCombos()
        {
            listarComboTipoBus();
            listarComboMarca();
            listarComboModelo();
            listarComboSucursal();
        }















    }
}