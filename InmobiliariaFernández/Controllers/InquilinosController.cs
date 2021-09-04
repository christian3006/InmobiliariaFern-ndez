using InmobiliariaFernández.Models;
using Microsoft.AspNetCore.Mvc;

public class InquilinosController : Controller
{
    RepoInquilino repositorio;

    public InquilinosController(IConfiguration configuration)
    {
        repositorio = new RepoInquilino(configuration);
    }

    // GET: InquilinosController
    public ActionResult Index()
    {
        var list = repositorio.ObtenerTodos();
        return View(list);
    }

    // GET: InquilinosController/Details/5
    public ActionResult Details(int id)
    {
        var i = repositorio.ObtenerPorId(id);
        return View(i);
    }

    // GET: InquilinosController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: InquilinosController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Inquilino i)
    {
        try
        {
            repositorio.Alta(i);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: InquilinosController/Edit/5
    public ActionResult Edit(int id)
    {
        var i = repositorio.ObtenerPorId(id);
        return View(i);
    }

    // POST: InquilinosController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, Inquilino i)
    {
        try
        {
            i.id = id;
            repositorio.Modificar(i);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View(i);
        }
    }

    // GET: InquilinosController/Delete/5
    public ActionResult Delete(int id)
    {
        var i = repositorio.ObtenerPorId(id);
        return View(i);
    }

    // POST: InquilinosController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, Inquilino i)
    {
        try
        {
            repositorio.Baja(id);
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View(i);
        }
    }
}
