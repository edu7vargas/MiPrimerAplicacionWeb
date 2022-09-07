using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiPrimerAplicacionWeb.Models;

namespace MiPrimerAplicacionWeb.Controllers
{
    public class ViajeController : Controller
    {
        // GET: Viaje
        public ActionResult Index()
        {
            List<ViajeCLS> listaViaje = null;
            using (var bd = new BDPasajeEntities())
            {


                listaViaje = (from viaje in bd.Viaje
                            join lugarOrigen in bd.Lugar
                            on viaje.IIDLUGARORIGEN equals lugarOrigen.IIDLUGAR
                            join lugarDestino in bd.Lugar
                            on viaje.IIDLUGARDESTINO equals lugarDestino.IIDLUGAR
                              join bus in bd.Bus
                            on viaje.IIDBUS equals bus.IIDBUS

                              select new ViajeCLS
                            {
                                iidViaje = viaje.IIDVIAJE,
                                // precio = viaje.PRECIO,
                                // fechaViaje = bus.FECHAVIAJE,
                                nombreBus = bus.PLACA,
                                nombreLugarOrigen = lugarOrigen.NOMBRE,
                                nombreLugarDestino = lugarDestino.NOMBRE
                               

                            }).ToList();
            }

            return View(listaViaje);


        }



        public ActionResult Agregar()
        {
            listarCombos();
            return View();
        }


        public ActionResult Editar()
        {
            return View();
        }






        List<SelectListItem> lista;
        List<SelectListItem> listaLugar;
        private void listarComboLugar()
        {

            using (var bd = new BDPasajeEntities())
            {
                lista = (from lugar in bd.Lugar
                         where lugar.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = lugar.NOMBRE,
                             Value = lugar.IIDLUGAR.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione --", Value = "" });
                ViewBag.listaLugar = lista;
            }


        }




        List<SelectListItem> listaBus;
        private void listarComboBus()
        {

            using (var bd = new BDPasajeEntities())
            {
                lista = (from bus in bd.Bus
                         where bus.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = bus.PLACA,
                             Value = bus.IIDBUS.ToString()
                         }).ToList();
                lista.Insert(0, new SelectListItem { Text = "--Seleccione --", Value = "" });
                ViewBag.listaBus = lista;
            }


        }


        public void listarCombos()
        {
            listarComboLugar();
            listarComboBus();
        }


    }
}