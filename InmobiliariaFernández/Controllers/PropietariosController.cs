using InmobiliariaFernández.Models;
using Microsoft.AspNetCore.Mvc;

namespace InmobiliariaFernández.Controllers
{
    public class PropietariosController : Controller
    {
        RepoPropietario repoP;

        public PropietariosController()
        {
            repoP = new RepoPropietario();
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
            return View();
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
            return View();
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
                return View();
            }
        }

        // GET: PropietariosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
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
                return View();
            }
        }
    }
}