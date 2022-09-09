using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiPrimerAplicacionWeb.Models;
namespace MiPrimerAplicacionWeb.Controllers
{
    public class TipoUsuarioController : Controller
    {

        private TipoUsuarioCLS oTipoVal;
        private bool buscarTipousuario(TipoUsuarioCLS oTipoUsuarioCLS)
        {
            bool busquedaId = true;
            bool busquedaNombre = true;
            bool busquedaDescripcion = true;

            if (oTipoVal.iidtipousuario > 0)
                busquedaId = oTipoUsuarioCLS.iidtipousuario.ToString().Contains(oTipoVal.iidtipousuario.ToString());

            if (oTipoVal.nombre != null)
                busquedaNombre = oTipoUsuarioCLS.nombre.ToString().Contains(oTipoVal.nombre.ToString());

            if (oTipoVal.descripcion != null)
                busquedaDescripcion = oTipoUsuarioCLS.descripcion.ToString().Contains(oTipoVal.descripcion.ToString());


            return (busquedaId && busquedaNombre && busquedaDescripcion);
        }

        // GET: TipoUsuario
        public ActionResult Index(TipoUsuarioCLS oTipoUsuarioCLS)
        {

            oTipoVal = oTipoUsuarioCLS;

            List <TipoUsuarioCLS> listaFiltrado = null;

            //Varible filtrado
            List<TipoUsuarioCLS> listaTipoUsuario = null;

            using (var bd = new BDPasajeEntities()) {


                listaTipoUsuario = (from tipoUsuario in bd.TipoUsuario
                                    where tipoUsuario.BHABILITADO==1
                                    select new TipoUsuarioCLS
                                    {
                                        iidtipousuario = tipoUsuario.IIDTIPOUSUARIO,
                                        nombre = tipoUsuario.NOMBRE,
                                        descripcion = tipoUsuario.DESCRIPCION
                                    }
                                    ).ToList();


                if (oTipoUsuarioCLS.iidtipousuario == 0 && oTipoUsuarioCLS.nombre == null && oTipoUsuarioCLS.descripcion == null)
                {
                    listaFiltrado = listaTipoUsuario;
                }
                else {
                    Predicate<TipoUsuarioCLS> pred = new Predicate<TipoUsuarioCLS>(buscarTipousuario);
                    listaFiltrado = listaTipoUsuario.FindAll(pred);
                    
                }



            }


                return View(listaFiltrado);
        }
    }
}