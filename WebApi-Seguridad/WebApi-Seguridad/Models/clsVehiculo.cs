using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApi_Seguridad.Models
{
    public class clsVehiculo
    {
        public Int64 PER_ID { get; set; }
        public String PER_NOMBRE { get; set; }
        public DateTime PER_FECHA_NACIMIENTO { get; set; }
        public Double PER_ALTURA { get; set; }
        public String PER_GENERO { get; set; }
        public Int32 PER_ESTADO { get; set; }
        private SqlConnection con;

        private void Conectar()
        {
            string constr = ConfigurationManager.ConnectionStrings["db"].ToString();
            con = new SqlConnection(constr);
        }
        public List<clsVehiculo> funSelectTodos()
        {
            Conectar();
            List<clsVehiculo> lstPersonas = new List<clsVehiculo>();

            SqlCommand com = new SqlCommand("SELECT PER_ID, PER_NOMBRE, "
                                                  + "CONVERT(VARCHAR,PER_FECHA_NACIMIENTO,103) AS PER_FECHA_NACIMIENTO, "
                                                  + "PER_ALTURA, "
                                                  + "PER_GENERO, "
                                                  + "PER_ESTADO "
                                                  + "FROM TBL_PERSONA", con);
            con.Open();
            SqlDataReader registros = com.ExecuteReader();
            while (registros.Read())
            {
                clsVehiculo objPersona = new clsVehiculo
                {
                    PER_ID = int.Parse(registros["PER_ID"].ToString()),
                    PER_NOMBRE = registros["PER_NOMBRE"].ToString(),
                    PER_FECHA_NACIMIENTO = Convert.ToDateTime(registros["PER_FECHA_NACIMIENTO"].ToString()),
                    PER_ALTURA = Double.Parse(registros["PER_ALTURA"].ToString()),
                    PER_GENERO = registros["PER_GENERO"].ToString(),
                    PER_ESTADO = Convert.ToInt32(registros["PER_ESTADO"].ToString())
                };
                lstPersonas.Add(objPersona);
            }
            con.Close();
            return lstPersonas;
        }
        public int Alta(clsVehiculo objPersona)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("INSERT INTO [dbo].[TBL_PERSONA] " +
                "([PER_NOMBRE],[PER_FECHA_NACIMIENTO],[PER_ALTURA],[PER_GENERO],[PER_ESTADO]) " +
                "VALUES " +
                "(@PER_NOMBRE,@PER_FECHA_NACIMIENTO,@PER_ALTURA,@PER_GENERO,@PER_ESTADO)", con);
            comando.Parameters.AddWithValue("@PER_ID", objPersona.PER_ID);
            comando.Parameters.AddWithValue("@PER_NOMBRE", objPersona.PER_NOMBRE);
            comando.Parameters.AddWithValue("@PER_FECHA_NACIMIENTO", objPersona.PER_FECHA_NACIMIENTO);
            comando.Parameters.AddWithValue("@PER_ALTURA", objPersona.PER_ALTURA);
            comando.Parameters.AddWithValue("@PER_GENERO", objPersona.PER_GENERO);
            comando.Parameters.AddWithValue("@PER_ESTADO", objPersona.PER_ESTADO);
            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public clsVehiculo Recuperar(int id)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("SELECT [PER_ID]," +
                                                "[PER_NOMBRE]," +
                                                "[PER_FECHA_NACIMIENTO]," +
                                                "[PER_ALTURA]," +
                                                "[PER_GENERO]," +
                                                "[PER_ESTADO] " +
                                                "FROM [dbo].[TBL_PERSONA] " +
                                                "WHERE PER_ID=@PER_ID", con);
            comando.Parameters.Add("@PER_ID", SqlDbType.Int);
            comando.Parameters["@PER_ID"].Value = id;
            con.Open();
            SqlDataReader registros = comando.ExecuteReader();
            clsVehiculo objPersona = new clsVehiculo();
            if (registros.Read())
            {
                objPersona.PER_ID = int.Parse(registros["PER_ID"].ToString());
                objPersona.PER_NOMBRE = registros["PER_NOMBRE"].ToString();
                objPersona.PER_FECHA_NACIMIENTO = DateTime.Parse(registros["PER_FECHA_NACIMIENTO"].ToString());
                objPersona.PER_GENERO = registros["PER_GENERO"].ToString();
                objPersona.PER_ALTURA = Double.Parse(registros["PER_ALTURA"].ToString());
                objPersona.PER_ESTADO = Convert.ToInt32(registros["PER_ESTADO"].ToString());
            }
            con.Close();
            return objPersona;
        }
        public int Modificar(clsVehiculo objPersona)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("UPDATE [dbo].[TBL_PERSONA] SET " +
                                                "PER_NOMBRE = @PER_NOMBRE, " +
                                                "PER_FECHA_NACIMIENTO = @PER_FECHA_NACIMIENTO, " +
                                                "PER_ALTURA = @PER_ALTURA, " +
                                                "PER_GENERO = @PER_GENERO, " +
                                                "PER_ESTADO = @PER_ESTADO " +
                                                "WHERE PER_ID = @PER_ID", con);
            comando.Parameters.AddWithValue("@PER_ID", objPersona.PER_ID);
            comando.Parameters.AddWithValue("@PER_NOMBRE", objPersona.PER_NOMBRE);
            comando.Parameters.AddWithValue("@PER_FECHA_NACIMIENTO", objPersona.PER_FECHA_NACIMIENTO);
            comando.Parameters.AddWithValue("@PER_ALTURA", objPersona.PER_ALTURA);
            comando.Parameters.AddWithValue("@PER_GENERO", objPersona.PER_GENERO);
            comando.Parameters.AddWithValue("@PER_ESTADO", objPersona.PER_ESTADO);
            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }
        public int Borrar(int id)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("DELETE FROM TBL_PERSONA WHERE PER_ID=@PER_ID", con);
            comando.Parameters.AddWithValue("@PER_ID", id);
            con.Open();
            int i = comando.ExecuteNonQuery();
            con.Close();
            return i;
        }

    }
}