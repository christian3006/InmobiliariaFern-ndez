using InmobiliariaFernández.Models;
using Microsoft.AspNetCore.Mvc;

namespace InmobiliariaFernández.Controllers
{
    public class PropietariosController : Controller
    {
        RepoPropietario repoP;

        public PropietariosController(IConfiguration configuration)
        {
            repoP = new RepoPropietario(configuration);
        }

        // GET: PropietariosController
        public ActionResult Index()
        {
            var lista = repoP.ObtenerPropietarios();
            return View(lista);
        }

        // GET: PropietariosController/Details/5
        public ActionResult Details(int id)
        {
            var p = repoP.ObtenerPPorId(id);
            return View(p);
        }

        // GET: PropietariosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PropietariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Propietario P)
        {
            try
            {
                repoP.AltaP(P);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PropietariosController/Edit/5
        public ActionResult Edit(int id)
        {
            var p= repoP.ObtenerPPorId(id); 
            return View(p);
        }

        // POST: PropietariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Propietario pr)
        {
            try
            {
                pr.IdPropietario = id;
                int res = repoP.ModificarP(pr);
                if (res > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View(pr);
            }
        }

        // GET: PropietariosController/Delete/5
        public ActionResult Delete(int id)
        {
            var p = repoP.ObtenerPPorId(id);
            return View(p);
        }

        // POST: PropietariosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Propietario P)
        {
            try
            {
                repoP.BajaP(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(P);
            }
        }
    }
}