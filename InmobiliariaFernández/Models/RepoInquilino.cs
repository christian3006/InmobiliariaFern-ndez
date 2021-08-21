using InmobiliariaFernández.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

public class RepoInquilino
{
    String connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=InmobiliariaFernandez;Trusted_Connection=True;MultipleActiveResultSets=true";

    public RepoInquilino()
    {

    }

    public int Alta(Inquilino I)
    {
        int res = -1;
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string sql = @"INSERT INTO Inquilino (Nombre, Apellido, NroDpto, DNI, Telefono) VALUES (@nombre, @apellido, @nrodpto, @dni, @telefono)
SELECT SCOPE_IDENTITY();";
            using (SqlCommand comm = new SqlCommand(sql, conn))
            {

                comm.Parameters.AddWithValue("@nombre", I.Nombre);
                comm.Parameters.AddWithValue("@apellido", I.Apellido);
                comm.Parameters.AddWithValue("@nrodpto", I.NroDpto);
                comm.Parameters.AddWithValue("@dni", I.DNI);
                comm.Parameters.AddWithValue("@telefono", I.Telefono);
                conn.Open();
                res = Convert.ToInt32(comm.ExecuteScalar());
                conn.Close();
                I.id = res;
            }
        }
        return res;
    }

    public IList<Inquilino> ObtenerTodos()
    {
        IList<Inquilino> res = new List<Inquilino>();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string sql = @"SELECT Id, Nombre, Apellido, NroDpto, DNI, Telefono FROM Inquilino";
            using (SqlCommand comm = new SqlCommand(sql, conn))
            {
                conn.Open();
                var reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    var i = new Inquilino
                    {
                        id = reader.GetInt32(0),
                        Nombre = reader[nameof(Inquilino.Nombre)].ToString(),
                        Apellido = reader[nameof(Inquilino.Apellido)].ToString(),
                        NroDpto = reader[nameof(Inquilino.NroDpto)].ToString(),
                        DNI = reader[nameof(Inquilino.DNI)].ToString(),
                        Telefono = reader[nameof(Inquilino.Telefono)].ToString()
                    };

                    res.Add(i);
                }
                conn.Close();
            }
        }
        return res;
    }

    public int Modificar(Inquilino i)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {

            SqlCommand comando = new SqlCommand("UPDATE Inquilino SET Nombre=@nombre,Apellido=@apellido,NroDpto=@nrodpto,DNI=@dni,Telefono=@telefono WHERE id=@id", conn);
            comando.Parameters.Add("@id", SqlDbType.Int);
            comando.Parameters["@id"].Value = i.id;
            comando.Parameters.Add("@nombre", SqlDbType.VarChar);
            comando.Parameters["@nombre"].Value = i.Nombre;
            comando.Parameters.Add("@apellido", SqlDbType.VarChar);
            comando.Parameters["@apellido"].Value = i.Apellido;
            comando.Parameters.Add("@nrodpto", SqlDbType.VarChar);
            comando.Parameters["@nrodpto"].Value = i.NroDpto;
            comando.Parameters.Add("@dni", SqlDbType.VarChar);
            comando.Parameters["@dni"].Value = i.DNI;
            comando.Parameters.Add("@telefono", SqlDbType.VarChar);
            comando.Parameters["@telefono"].Value = i.Telefono;
            conn.Open();
            int e = comando.ExecuteNonQuery();
            conn.Close();
            return e;
        }
    }

        public int Baja(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString)) {
                SqlCommand comando = new SqlCommand("DELETE FROM Inquilino WHERE id=@id", conn);
                comando.Parameters.Add("@id", SqlDbType.Int);
                comando.Parameters["@id"].Value = id;
                conn.Open();
                int i = comando.ExecuteNonQuery();
                conn.Close();
                return i;
            }
        }
    }



