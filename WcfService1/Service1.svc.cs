using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using System.Data.SqlClient;
using System.Data;

namespace WcfService1
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    // NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
        string IService1.ActualizarCliente(Cliente reg)
        {
            string msg = "";
            using (SqlConnection conexion = new SqlConnection(@"server=DESKTOP-AC42F41\SQLEXPRESS;database=Negocios2017;uid=sa;pwd=sql"))
            {
                conexion.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_ACTUALIZAR_CLIENTES", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;                    
                    cmd.Parameters.AddWithValue("@NOM", reg.nombre);
                    cmd.Parameters.AddWithValue("@DIR", reg.direccion);
                    cmd.Parameters.AddWithValue("@IDP", reg.idpais);
                    cmd.Parameters.AddWithValue("@TEL", reg.telefono);
                    cmd.Parameters.AddWithValue("@ID", reg.idcliente);
                    cmd.ExecuteNonQuery();
                    msg = "Registro Actualizado";
                }
                catch (SqlException ex) { msg = ex.Message; }
                finally { conexion.Close(); }
            }
            return msg;
        }

        string IService1.AgregarCliente(Cliente reg)
        {
            string msg = "";
            using (SqlConnection conexion = new SqlConnection(@"server=DESKTOP-AC42F41\SQLEXPRESS;database=Negocios2017;uid=sa;pwd=sql"))
            {
                conexion.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_CREAR_CLIENTES", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", reg.idcliente);
                    cmd.Parameters.AddWithValue("@NOM", reg.nombre);
                    cmd.Parameters.AddWithValue("@DIR", reg.direccion);
                    cmd.Parameters.AddWithValue("@IDP", reg.idpais);
                    cmd.Parameters.AddWithValue("@TEL", reg.telefono);
                    cmd.ExecuteNonQuery();
                    msg = "Registro Ingresado";
                }
                catch (SqlException ex) { msg = ex.Message; }
                finally { conexion.Close(); }
            }
            return msg;
        }

        string IService1.EliminarCliente(string id)
        {
            string msg = "";
            using (SqlConnection conexion = new SqlConnection(@"server=DESKTOP-AC42F41\SQLEXPRESS;database=Negocios2017;uid=sa;pwd=sql"))
            {
                conexion.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand("USP_ELIMINAR_CLIENTES", conexion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    
                    cmd.ExecuteNonQuery();
                    msg = "Registro Eliminado";
                }
                catch (SqlException ex) { msg = ex.Message; }
                finally { conexion.Close(); }
            }
            return msg;
        }

        List<Cliente> IService1.ListadoCliente()
        {
            List<Cliente> lista = new List<Cliente>();
            using (SqlConnection conexion = new SqlConnection(@"server=DESKTOP-AC42F41\SQLEXPRESS;database=Negocios2017;uid=sa;pwd=sql"))
            {
                SqlCommand cmd = new SqlCommand("USP_LISTAR_CLIENTES", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Cliente reg = new Cliente();
                    reg.idcliente = dr.GetString(0);
                    reg.nombre    = dr.GetString(1);
                    reg.direccion = dr.GetString(2);
                    reg.idpais    = dr.GetString(3);
                    reg.telefono  = dr.GetString(4);
                    lista.Add(reg);
                }
                dr.Close();conexion.Close();
            }
            return lista;
        }

        List<Pais> IService1.ListadoPais()
        {
            List<Pais> lista = new List<Pais>();
            using (SqlConnection conexion = new SqlConnection(@"server=DESKTOP-AC42F41\SQLEXPRESS;database=Negocios2017;uid=sa;pwd=sql"))
            {
                SqlCommand cmd = new SqlCommand("USP_LISTAR_PAIS", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                conexion.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Pais reg = new Pais();
                    reg.idpais = dr.GetString(0);
                    reg.nombrePais = dr.GetString(1);
                    lista.Add(reg);
                }
                dr.Close(); conexion.Close();
            }
            return lista;
        }
    }
}
