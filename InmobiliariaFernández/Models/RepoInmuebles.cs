
using MongoDB.Driver.Core.Configuration;
using System.Data;
using System.Data.SqlClient;
using InmobiliariaFernández.Controllers;

namespace InmobiliariaFernández.Models;

public class RepoInmuebles : RepoBase
{
    public RepoInmuebles(IConfiguration configuration) : base(configuration)
    {

    }

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
                            Apellido = lector.GetString(8)
                        }
                    };
                    res.Add(entity);
                }
                conn.Close();
            }
            return res;
        }
    }

    public int Alta(Inmueble inmueble) {

        int res = -1;

        using (SqlConnection conn = new SqlConnection(connectionString)) { 
        
            string sql = @"INSERT INTO Inmuebles (Direccion, Ambientes, Superficie, cantidadPisos, IdPropietario) VALUES (@direccion, @ambientes, @superficie
,@cantidadPisos, @idPropietario)
SELECT SCOPE_IDENTITY();";
            using (var comm = new SqlCommand(sql, conn)) 
            { 
            comm.CommandType = CommandType.Text;
                comm.Parameters.AddWithValue("@direccion", inmueble.Direccion);
                comm.Parameters.AddWithValue("@ambientes", inmueble.Ambientes);
                comm.Parameters.AddWithValue("@superficie", inmueble.Superficie);
                comm.Parameters.AddWithValue("@cantidadPisos", inmueble.cantidadPisos);
                comm.Parameters.AddWithValue("@idPropietario", inmueble.IdPropietario);
                conn.Open();
                res = Convert.ToInt32(comm.ExecuteScalar());
                conn.Close();
                inmueble.id = res;
            }

        }
        return res;
    }

    public int Modificar(Inmueble inmueble)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {

            SqlCommand comando = new SqlCommand("UPDATE Inmuebles SET @direccion, @ambientes, @superficie, @cantidadPisos, @idPropietario WHERE id=@id", conn);
            comando.Parameters.Add("@id", SqlDbType.Int);
            comando.Parameters["@id"].Value = inmueble.id;
            comando.Parameters.Add("@direccion", SqlDbType.VarChar);
            comando.Parameters["@direccion"].Value = inmueble.Direccion;
            comando.Parameters.Add("@ambientes", SqlDbType.VarChar);
            comando.Parameters["@ambientes"].Value = inmueble.Ambientes;
            comando.Parameters.Add("@superficie", SqlDbType.VarChar);
            comando.Parameters["@superficie"].Value = inmueble.Superficie;
            comando.Parameters.Add("@cantidadPisos", SqlDbType.VarChar);
            comando.Parameters["@cantidadPisos"].Value = inmueble.cantidadPisos;
            comando.Parameters.Add("@idPropietario", SqlDbType.VarChar);
            comando.Parameters["@idPropietario"].Value = inmueble.IdPropietario;
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
                    SqlCommand comando = new SqlCommand("DELETE FROM Inmuebles WHERE id=@id", conn);
                    comando.Parameters.Add("@id", SqlDbType.Int);
                    comando.Parameters["@id"].Value = id;
                    conn.Open();
                    int i = comando.ExecuteNonQuery();
                    conn.Close();
                    return i;
                }
            }
        }
    
