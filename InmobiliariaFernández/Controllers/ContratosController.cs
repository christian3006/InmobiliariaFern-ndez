using InmobiliariaFernández.Models;
using Microsoft.AspNetCore.Mvc;

namespace InmobiliariaFernández.Controllers;
public class ContratosController : Controller
{
    private readonly RepoContrato repoC;
    private readonly RepoInmuebles repoI;
    private readonly RepoPropietario repoP;
    private readonly RepoInquilino repoInq;
    protected readonly IConfiguration configuration;

    public ContratosController(IConfiguration configuration)
    {
        this.repoI = repoI;
        this.repoP = repoP;
        this.repoInq=repoInq;
        this.configuration = configuration;
        repoC = new RepoContrato(configuration);
    }

    // GET: ContratosController
    public ActionResult Index()
    {
        var listar = repoC.ObtenerContratos();
        if (TempData.ContainsKey("id"))
            ViewBag.id = TempData["id"];
        if (TempData.ContainsKey("mensaje"))
            ViewBag.mensaje = TempData["mensaje"];
        return View(listar);
    }

    // GET: ContratosController/Details/5
    public ActionResult Details(int id)
    {
        var entity = repoC.ObtenerContratos();
        return View(id);
    }

    // GET: InmueblesController/Create
    public ActionResult Create()
    {
        ViewBag.Contratos = repoC.ObtenerContratos();
        return View();
    }

    // POST: ContratosController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Contrato contrato)
    {
        try
        {
            if (ModelState.IsValid)
            {
                repoC.Alta(contrato);
                TempData["id"] = contrato.id;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Propietario = repoP.ObtenerPropietarios();
                return View(contrato);
            }
        }
        catch (Exception ex)
        {
            ViewBag.Error = ex.Message;
            ViewBag.StackTrace = ex.StackTrace;
            return View(contrato);
        }
    }



    // GET: ContratosController/Edit/5
    public ActionResult Edit(int id)
    {
        var contrato = repoC.ObtenerContratos();
        ViewBag.Contratos = repoC.ObtenerContratos();
        if (TempData.ContainsKey("mensaje"))
            ViewBag.mensaje = TempData["mensaje"];
        if (TempData.ContainsKey("error"))
            ViewBag.error = TempData["error"];
        return View(contrato);
    }

    // POST: ContratosController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, Contrato contrato)
    {
        try
        {
            contrato.id = id;
            repoC.Modificar(contrato);
            TempData["mensaje"] = "Datos modificados correctamente";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ViewBag.Contratos = repoC.ObtenerContratos();
            ViewBag.Error = ex.Message;
            ViewBag.StackTrace = ex.StackTrace;
            return View(contrato);
        }
    }

    // GET: ContratosController/Delete/5
    public ActionResult Delete(int id)
    {
        var inmueble = repoC.ObtenerContratos();
        if (TempData.ContainsKey("mensaje"))
            ViewBag.mensaje = TempData["mensaje"];
        if (TempData.ContainsKey("error"))
            ViewBag.Error = TempData["error"];
        return View(id);
    }

    // POST: ContratosController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, Contrato contrato)
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
            return View(contrato);
        }
    }
}