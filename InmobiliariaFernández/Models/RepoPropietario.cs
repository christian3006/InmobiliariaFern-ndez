using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace InmobiliariaFernández.Models
{
    public class RepoPropietario : RepoBase
    {

        public RepoPropietario(IConfiguration configuration) : base (configuration)
        {

        }

        public int AltaP(Propietario P)
        {
            int res = -1;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"INSERT INTO Propietario (Nombre, Apellido, Dni, Telefono, Email, Clave) VALUES (@nombre, @apellido, @dni, @telefono, @email, @clave)
SELECT SCOPE_IDENTITY();";
                using (SqlCommand comm = new SqlCommand(sql, conn))
                {

                    comm.Parameters.AddWithValue("@nombre", P.Nombre);
                    comm.Parameters.AddWithValue("@apellido", P.Apellido);
                    comm.Parameters.AddWithValue("@dni", P.Dni);
                    comm.Parameters.AddWithValue("@telefono", P.Telefono);
                    comm.Parameters.AddWithValue("@email", P.Email);
                    comm.Parameters.AddWithValue("@clave", P.Clave);
                    conn.Open();
                    res = Convert.ToInt32(comm.ExecuteScalar());
                    conn.Close();
                    P.IdPropietario = res;
                }
            }
            return res;
        }

        public IList<Propietario> ObtenerPropietarios()
        {
            IList<Propietario> res = new List<Propietario>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"SELECT IdPropietario, Nombre, Apellido, Dni, Telefono, Email, Clave FROM Propietario";
                using (SqlCommand comm = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    var reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        var p = new Propietario
                        {
                            IdPropietario = reader.GetInt32(0),
                            Nombre = reader[nameof(Propietario.Nombre)].ToString(),
                            Apellido = reader[nameof(Propietario.Apellido)].ToString(),
                            Dni = reader[nameof(Propietario.Dni)].ToString(),
                            Telefono = reader[nameof(Propietario.Telefono)].ToString(),
                            Email = reader[nameof(Propietario.Email)].ToString(),
                            Clave = reader[nameof(Propietario.Clave)].ToString()
                        };

                        res.Add(p);
                    }
                    conn.Close();
                }
            }
            return res;
        }

        public int ModificarP(Propietario p)
        {
            int idP = p.IdPropietario;
            int e=-1;
            string nombrep = p.Nombre;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                SqlCommand comando = new SqlCommand("UPDATE Propietario SET Nombre=@nombre,Apellido=@apellido,Dni=@dni,Telefono=@telefono,Email=@email,Clave=@clave WHERE IdPropietario=@idpropietario", conn);
                comando.Parameters.Add("@idpropietario", SqlDbType.Int);
                comando.Parameters["@idpropietario"].Value = p.IdPropietario;
                comando.Parameters.Add("@nombre", SqlDbType.VarChar);
                comando.Parameters["@nombre"].Value = p.Nombre;
                comando.Parameters.Add("@apellido", SqlDbType.VarChar);
                comando.Parameters["@apellido"].Value = p.Apellido;
                comando.Parameters.Add("@dni", SqlDbType.VarChar);
                comando.Parameters["@dni"].Value = p.Dni;
                comando.Parameters.Add("@telefono", SqlDbType.VarChar);
                comando.Parameters["@telefono"].Value = p.Telefono;
                comando.Parameters.Add("@email", SqlDbType.VarChar);
                comando.Parameters["@email"].Value = p.Email;
                comando.Parameters.Add("@clave", SqlDbType.VarChar);
                comando.Parameters["@clave"].Value = p.Clave;
                conn.Open();
                e = comando.ExecuteNonQuery();
                conn.Close();
                
            }
            return e;
        }

        public Propietario ObtenerPPorId(int id)
        {
            Propietario p = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT idpropietario, Nombre, Apellido, Dni, Telefono, Email, Clave FROM Propietario" +
                    $" WHERE idpropietario=@idpropietario";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@idpropietario", SqlDbType.Int).Value = id;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        p = new Propietario
                        {
                            IdPropietario = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Apellido = reader.GetString(2),
                            Dni = reader.GetString(3),
                            Telefono = reader.GetString(4),
                            Email = reader.GetString(5),
                            Clave = reader.GetString(6)

                        };
                        return p;
                    }
                    connection.Close();
                }
            }
            return p;
        }

        public int BajaP(int idpropietario)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand comando = new SqlCommand("DELETE FROM Propietario WHERE IdPropietario=@idpropietario", conn);
                comando.Parameters.Add("@idpropietario", SqlDbType.Int);
                comando.Parameters["@idpropietario"].Value = idpropietario;
                conn.Open();
                int i = comando.ExecuteNonQuery();
                conn.Close();
                return i;
            }
        }
    }
}