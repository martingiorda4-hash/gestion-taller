using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gestion_taller
{
    internal class Controller
    {
        public List<Trabajo> Listar()
        {
            List<Trabajo> trabajos = new List<Trabajo>();
            SqlConnection _conn = Database.ConexionBD();

            try
            {
                _conn.Open();
                string sql = "SELECT * FROM Trabajo";
                SqlCommand cmd = new SqlCommand(sql, _conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Trabajo t = new Trabajo(
                        int.Parse(reader["TrabajoId"].ToString()),
                           DateTime.Parse(reader["Fecha"].ToString()),
                            reader["Cliente"].ToString(),
                            reader["Vehiculo"].ToString(),
                            reader["Descripcion"].ToString(),
                           int.Parse(reader["precio"].ToString())
                        );
                    trabajos.Add(t);
                }
                reader.Close();

            }
            catch (Exception ex)
            {

                 MessageBox.Show("Error al listar");
            }
            finally
            {
                _conn.Close();
            }
                return trabajos;
        }
        public void AgregarTrabajo(Trabajo t)
        {
            

            SqlConnection _conn = Database.ConexionBD();
            try
            {
                _conn.Open();
                string sql = $"INSERT INTO Trabajo (Fecha, Cliente, Vehiculo, Descripcion, precio) VALUES ('{t.Fecha}', '{t.Cliente}','{t.Vehiculo}', '{t.Descripcion}', '{t.Precio}')";
                SqlCommand cmd = new SqlCommand(sql, _conn);
                cmd.ExecuteNonQuery();
               
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _conn.Close();
            }
        }
        public void Editar(Trabajo t)
        {
            SqlConnection _conn = Database.ConexionBD();
            try
            {
                _conn.Open();
                string sql = $"UPDATE Trabajo SET Fecha = '{t.Fecha}', Cliente= '{t.Cliente}', Vehiculo = '{t.Vehiculo}', Descripcion = '{t.Descripcion}', precio = '{t.Precio}' WHERE TrabajoID = {t.Id}";
                SqlCommand cmd = new SqlCommand(sql, _conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al editar"+ ex.Message);
            }
            finally
            {
                _conn.Close();
            }
        }
        public void Eliminar(int id)
        {
            SqlConnection _conn = Database.ConexionBD();

            try
            {
                _conn.Open();
                string sql = $"DELETE FROM Trabajo WHERE TrabajoID = {id}";
                SqlCommand cmd = new SqlCommand(sql, _conn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al eliminar"+ ex.Message);
            }
            finally
            {
                _conn.Close();
            }
        }
        public List<Trabajo>ListarPorFecha(DateTime desde, DateTime hasta)
        {
            List<Trabajo> trabajos = new List<Trabajo>();
            SqlConnection _conn = Database.ConexionBD();
            try
            {
                _conn.Open();
                string query = "SELECT * FROM Trabajo WHERE Fecha >= @desde AND Fecha <= @hasta ORDER BY Fecha DESC";
                SqlCommand cmd = new SqlCommand(query, _conn);
                cmd.Parameters.AddWithValue("@desde", desde);
                cmd.Parameters.AddWithValue("@hasta", hasta);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Trabajo t = new Trabajo(
                        int.Parse(reader["TrabajoId"].ToString()),
                           DateTime.Parse(reader["Fecha"].ToString()),
                            reader["Cliente"].ToString(),
                            reader["Vehiculo"].ToString(),
                            reader["Descripcion"].ToString(),
                           int.Parse(reader["precio"].ToString())
                        );
                    trabajos.Add(t);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al listar por fechas: "+ ex.Message);
            }
            finally
            {
                _conn.Close();
            }
            return trabajos;
        }
        public int TotalDelDia(DateTime fecha)
        {
            int total = 0;  
            SqlConnection _conn = Database.ConexionBD();
            try
            {
                _conn.Open();

                //FILTRO POR FECHA
                DateTime desde = fecha.Date; // FECHA A LAS 00:00:00
                DateTime hasta = fecha.Date.AddDays(1).AddSeconds(-1); // AddDays(1) PASA A LA FECHA SIGUIENTE A LAS 00 
                                                                       // AddSeconds(-1) RESTA 1 SEGUNDO 23:59

                string query = "SELECT SUM(precio) FROM Trabajo WHERE Fecha BETWEEN @desde AND @hasta";
                SqlCommand cmd = new SqlCommand(query, _conn);
                cmd.Parameters.AddWithValue("@desde", desde);
                cmd.Parameters.AddWithValue("@hasta", hasta);

                object resultado = cmd.ExecuteScalar();

                if (resultado != DBNull.Value)
                {
                    total = (int)resultado;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error al sacar el total: "+ ex.Message);
            }
            finally
            {
                _conn.Close();
            }
            return total;
        }
        public DataTable Buscador(string _busqueda)
        {
            SqlConnection _conn = Database.ConexionBD();
            DataTable dt = new DataTable();
            try
            {
                string sqlBuscar = "SELECT * FROM Trabajo ";
                if (!string.IsNullOrWhiteSpace(_busqueda) && _busqueda.Length >= 2)
                {
                    sqlBuscar += "WHERE Vehiculo like '%" + _busqueda + "%'";
                }
                SqlDataAdapter adapater = new SqlDataAdapter(sqlBuscar,_conn);
                
                adapater.Fill(dt);
                

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error en busqueda: "+ ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            return dt;
        }   
    }
}
