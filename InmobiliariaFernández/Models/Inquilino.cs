
using System.ComponentModel.DataAnnotations;

namespace InmobiliariaFernández.Models;
public class Inquilino
{
    [Display(Name = "Código")]
    public int id { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }

    [Display(Name = "N° de Departamento")]
    public string NroDpto { get; set; }

    public string DNI { get; set; }

    public string Telefono { get; set; }



}
