using MongoDB.Driver.Core.Configuration;
using System.Data;
using System.Data.SqlClient;
using InmobiliariaFernández.Controllers;

namespace InmobiliariaFernández.Models;

public class RepoContrato : RepoBase
{
    public RepoContrato(IConfiguration configuration) : base(configuration)
    {

    }

    public IList<Contrato> ObtenerContratos()
    {
        IList<Contrato> res = new List<Contrato>();


        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string sql = "SELECT id, montoAPagar, Garante id" + "inq.Nombre, inq.Apellido" + "idInquilino" + "idInmueble.id" + "IdPropietario" +
                    "p.Nombre, p.Apellido" +
                    "FROM Inmuebles i JOIN Propietario p, Inquilinos inq ON i.IdPropietario = p.IdPropietario";
            using (SqlCommand comm = new SqlCommand(sql, conn))
            {
                comm.CommandType = CommandType.Text;
                conn.Open();
                var lector = comm.ExecuteReader();
                while (lector.Read())
                {
                    Contrato contract = new Contrato
                    {
                        id = lector.GetInt32(0),
                        montoAPagar = lector.GetInt32(1),
                        Garante = lector.GetString(2),
                        idInquilino = lector.GetInt32(3),
                        inquilino = new Inquilino 
                        { 
                        id = lector.GetInt32(4),
                        Nombre = lector.GetString(5),
                        Apellido = lector.GetString(6),
                        Telefono = lector.GetString(7),
                        },
                        idInmueble = lector.GetInt32(8),
                        inmueble = new Inmueble
                        { 
                        id = lector.GetInt32(9),    
                        },
                        IdPropietario = lector.GetInt32(10),
                        Duenio = new Propietario
                        {
                            IdPropietario = lector.GetInt32(11),
                            Nombre = lector.GetString(12),
                            Apellido = lector.GetString(13),
                        }
                           
                    };
                    res.Add(contract);
                }
                conn.Close();
            }
            return res;
        }
    }

    public int Alta(Contrato contract)
    {

        int res = -1;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {

            string sql = @"INSERT INTO Contrato (montoAPagar, Garante, IdPropietario, idInquilino, idInmueble) VALUES (@montoAPagar, @garante, @idPropietario, @idInquilino, @idInmueble)
SELECT SCOPE_IDENTITY();";
            using (var comm = new SqlCommand(sql, conn))
            {
                comm.CommandType = CommandType.Text;
                comm.Parameters.AddWithValue("@montoAPagar", contract.montoAPagar);
                comm.Parameters.AddWithValue("@garante", contract.Garante);
                comm.Parameters.AddWithValue("@idPropietario", contract.IdPropietario);
                comm.Parameters.AddWithValue("@idInquilino", contract.idInquilino);
                comm.Parameters.AddWithValue("@idInmueble", contract.idInmueble);
                conn.Open();
                res = Convert.ToInt32(comm.ExecuteScalar());
                conn.Close();
                contract.id = res;
            }

        }
        return res;
    }

    public int Modificar(Contrato contract)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {

            SqlCommand comando = new SqlCommand("UPDATE Inmuebles SET @montoAPagar, @garante, @idPropietario, @idInquilino, @idInmueble WHERE id=@id", conn);
            comando.Parameters.Add("@id", SqlDbType.Int);
            comando.Parameters["@id"].Value = contract.id;
            comando.Parameters.Add("@montoAPagar", SqlDbType.VarChar);
            comando.Parameters["@montoAPagar"].Value = contract.montoAPagar;
            comando.Parameters.Add("@garante", SqlDbType.VarChar);
            comando.Parameters["@garante"].Value = contract.Garante;
            comando.Parameters.Add("@idPropietario", SqlDbType.VarChar);
            comando.Parameters["@idPropietario"].Value = contract.IdPropietario;
            comando.Parameters.Add("@idInquilino", SqlDbType.VarChar);
            comando.Parameters["@idInquilino"].Value = contract.idInquilino;
            comando.Parameters.Add("@idInmueble", SqlDbType.VarChar);
            comando.Parameters["@idInmueble"].Value = contract.idInmueble;
            conn.Open();
            int e = comando.ExecuteNonQuery();
            conn.Close();
            return e;
        }
    }

    public int Baja(int id)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand comando = new SqlCommand("DELETE FROM Contrato WHERE id=@id", conn);
            comando.Parameters.Add("@id", SqlDbType.Int);
            comando.Parameters["@id"].Value = id;
            conn.Open();
            int i = comando.ExecuteNonQuery();
            conn.Close();
            return i;
        }
    }
}
