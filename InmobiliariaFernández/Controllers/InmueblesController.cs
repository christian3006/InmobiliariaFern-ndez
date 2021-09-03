using InmobiliariaFernández.Models;
using Microsoft.AspNetCore.Mvc;

namespace InmobiliariaFernández.Controllers;
public class InmueblesController : Controller
{
    private readonly RepoInmuebles repoI;
    private readonly RepoPropietario repoP;
    
    protected readonly IConfiguration configuration;

    public InmueblesController(IConfiguration configuration)
    {
        this.repoI = repoI;
        this.repoP = repoP;
        this.configuration = configuration;
        repoI = new RepoInmuebles(configuration);
    }

    // GET: InmueblesController
    public ActionResult Index()
    {
        var listar = repoI.ObtenerTodos();
        if (TempData.ContainsKey("id"))
            ViewBag.id = TempData["id"];
        if (TempData.ContainsKey("mensaje"))
            ViewBag.mensaje = TempData["mensaje"];
        return View(listar);
    }

    // GET: InmueblesController/Details/5
    public ActionResult Details(int id)
    {
        var entity = repoI.ObtenerTodos();
        return View(id);
    }

    // GET: InmueblesController/Create
    public ActionResult Create()
    {
        ViewBag.Propietarios = repoP.ObtenerPropietarios();
        return View();
    }

    // POST: InmueblesController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Inmueble inmueble)
    {
        try
        {
            if (ModelState.IsValid)
            {
                repoI.Alta(inmueble);
                TempData["id"] = inmueble.id;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Propietario = repoP.ObtenerPropietarios();
                return View(inmueble);
            }
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            ViewBag.StackTrace = ex.StackTrace;
            return View(inmueble);
        }
    }



    // GET: InmueblesController/Edit/5
    public ActionResult Edit(int id)
    {
        var inmueble = repoI.ObtenerTodos();
        ViewBag.Propietarios = repoP.ObtenerPropietarios();
        if (TempData.ContainsKey("mensaje"))
            ViewBag.mensaje = TempData["mensaje"];
        if (TempData.ContainsKey("error"))
            ViewBag.error = TempData["error"];
        return View(inmueble);
    }

    // POST: InmueblesController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, Inmueble inmueble)
    {
        try
        {
            inmueble.id = id;
            repoI.Modificar(inmueble);
            TempData["mensaje"] = "Datos modificados correctamente";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Propietario = repoP.ObtenerPropietarios();
            ViewBag.Error = ex.Message;
            ViewBag.StackTrace = ex.StackTrace; 
            return View(inmueble);
        }
    }

    // GET: InmueblesController/Delete/5
    public ActionResult Delete(int id)
    {
        var inmueble = repoI.ObtenerTodos();
        if (TempData.ContainsKey("mensaje"))
            ViewBag.mensaje = TempData["mensaje"];
        if(TempData.ContainsKey("error"))
            ViewBag.Error = TempData["error"];  
        return View(inmueble);
    }

    // POST: InmueblesController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, Inmueble inmueble)
    {
        try
        {
            repoI.Baja(id);
            TempData["mensaje"] = "Se ha eliminado correctamente";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.error = ex.Message;
            ViewBag.StackTrace = ex.StackTrace;
            return View(inmueble);
        }
    }
}
