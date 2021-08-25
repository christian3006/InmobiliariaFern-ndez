
using MongoDB.Driver.Core.Configuration;
using System.Data;
using System.Data.SqlClient;
using InmobiliariaFernández.Controllers;

namespace InmobiliariaFernández.Models;

public class RepoInmuebles
{

    public IList<Inmueble> ObtenerTodos()
    {
        IList<Inmueble> res = new List <Inmueble>();

        
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string sql = "SELECT id, direccion, ambientes, superficie, cantidadPisos, IdPropietario" +
                    "p.Nombre, p.Apellido" +
                    "FROM Inmuebles i JOIN Propietario p ON i.IdPropietario = p.IdPropietario";
            using (SqlCommand comm = new SqlCommand(sql, conn))
            {
                comm.CommandType = CommandType.Text;
                conn.Open();
                var lector = comm.ExecuteReader();
                while (lector.Read())
                {
                    Inmueble entity = new Inmueble
                    {
                        id = lector.GetInt32(0),
                        Direccion = lector.GetString(1),
                        Ambientes = lector.GetInt32(2),
                        Superficie = lector.GetInt32(3),
                        cantidadPisos = lector.GetInt32(4),
                        IdPropietario = lector.GetInt32(5),
                        Duenio = new Propietario
                        {
                            IdPropietario = lector.GetInt32(6),
                            Nombre = lector.GetString(7),
                            Apellido = lector.GetString(8),
                        }
                    };
                    res.Add(entity);
                }
                conn.Close();
            }

        }
    }
}
