using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebApplication1.ReferenciaNegocio;

namespace WebApplication1.Controllers
{
    public class NegociosController : Controller
    {

        Service1Client servicio = new Service1Client();
        int paginacion = 10;

        public ActionResult Index(int? pag = null)
        {
            //recupero la cantidad de registro y almaceno el numero de registro
            int cantidad = servicio.ListadoCliente().Count();
            ViewBag.paginacion = cantidad % paginacion != 0 ? cantidad / paginacion + 1 : cantidad / paginacion;
            //definimos la paginacion actual y el registro de inicio y registro final
            int pagPrincipal = pag == null ? 0 : (int)pag;
            int registroInicial = pagPrincipal * paginacion;
            int registroFinal = registroInicial + paginacion;
            //variable que almacenara los clientes para la paginacion
            List<Cliente> listaPag = new List<Cliente>();
            for (int i = registroInicial; i < registroFinal; i++)
            {
                if (i == cantidad) break; // si i es igual a numero de registro saldra
                listaPag.Add(servicio.ListadoCliente().ToList()[i]);
            }

            //return View(servicio.ListadoCliente());
            return View(listaPag);
        }

        public ActionResult Create()
        {
            ViewBag.paises = new SelectList(servicio.ListadoPais(),"idpais","nombrepais");
            return View(new Cliente());
        }

        [HttpPost] public ActionResult Create(Cliente reg)
        {
            
            TempData["msg"] = servicio.AgregarCliente(reg);
            ViewBag.paises = new SelectList(servicio.ListadoPais(), "idpais", "nombrepais", reg.idpais);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id = null)
        {
            Cliente reg = servicio.ListadoCliente().Where(a => a.idcliente == id).FirstOrDefault();
            if (reg == null) { return RedirectToAction("Index"); }
            ViewBag.paises = new SelectList(servicio.ListadoPais(), "idpais", "nombrepais", reg.idpais);       
            return View(reg);
        }

        [HttpPost]
        public ActionResult Edit(Cliente reg)
        {

            TempData["msg"] = servicio.ActualizarCliente(reg);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(string id = null)
        {
            ViewBag.msg = servicio.EliminarCliente(id).ToString();
            return RedirectToAction("Index");
        }

    }

    
}