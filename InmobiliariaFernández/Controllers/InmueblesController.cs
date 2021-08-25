using InmobiliariaFernández.Models;
using Microsoft.AspNetCore.Mvc;

namespace InmobiliariaFernández.Controllers;
public class InmueblesController : Controller
{
    private readonly RepoInmuebles repoI;
    private readonly RepoPropietario repoP;
    private readonly RepoBase repoB;

    public InmueblesController()
    {
        this.repoI = repoI;
        this.repoP = repoP;
        this.repoB = repoB;
    }

    // GET: InmueblesController
    public ActionResult Index()
    {
        var listar = repoI.ObtenerTodos();
        if(TempData.ContainsKey("id"))
            ViewBag.id = TempData["id"];
        if (TempData.ContainsKey("mensaje"))
            ViewBag.mensaje = TempData["mensaje"];
        return View(listar);
    }

    // GET: InmueblesController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: InmueblesController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: InmueblesController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: InmueblesController/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: InmueblesController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: InmueblesController/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: InmueblesController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
