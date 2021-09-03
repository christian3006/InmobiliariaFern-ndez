using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InmobiliariaFernández.Models;

public class Contrato
{
    [Display(Name = "Número de Contrato")]
    public int id { get; set; }

    public int montoAPagar { get; set; }

    public String Garante { get; set; }

    public int idInmueble {  get; set; }    
    [ForeignKey(nameof(idInmueble))]

    public Inmueble inmueble { get; set; }

    public int idInquilino { get; set; }
    [ForeignKey(nameof(idInquilino))]

    public Inquilino inquilino { get; set; }

    public int IdPropietario { get; set; }
    [ForeignKey(nameof(IdPropietario))]

    public Propietario Duenio { get; set; }
 

    

    
}
