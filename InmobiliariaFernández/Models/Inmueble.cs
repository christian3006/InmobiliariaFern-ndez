
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InmobiliariaFernández.Models;
public class Inmueble
{
    [Display(Name = "Código")]

    public int id { get; set; }

    public string Direccion { get; set; }

    public string Ambiente { get; set; }

    public int Ambientes { get; set; }

    public int Superficie { get; set; }

    public int cantidadPisos { get; set; }

    [Display(Name = "Dueño")]

    public int IdPropietario { get; set; }
    [ForeignKey(nameof(IdPropietario))]

    public Propietario Duenio { get; set; }
}
