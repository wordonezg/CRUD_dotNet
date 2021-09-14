using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;

namespace web_umg_bd
{
    public class Empleado
    {
        ConexionBD conectar;
        private DataTable DropPuesto(){
            DataTable tabla = new DataTable();
            conectar = new ConexionBD();
            conectar.AbrirConexion();
            string strConsulta = string.Format("SELECT ID_PUESTO AS ID,PUESTO FROM PUESTOS;");
            MySqlDataAdapter consulta = new MySqlDataAdapter(strConsulta, conectar.conectar);
            consulta.Fill(tabla);
            conectar.CerarConexion();
            return tabla;
        }

        public void DropPuesto(DropDownList drop){
            drop.ClearSelection();
            drop.Items.Clear();
            drop.AppendDataBoundItems = true;
            drop.Items.Add("-- Seleccione Puesto --");
            drop.Items[0].Value = "0";
            drop.DataSource = DropPuesto();
            drop.DataTextField = "PUESTO";
            drop.DataValueField = "ID";
            drop.DataBind();
        }

        private DataTable GridEmpleados() {
            DataTable tabla = new DataTable();
            conectar = new ConexionBD();
            conectar.AbrirConexion();
            String consulta = string.Format("SELECT E.ID_EMPLEADO AS ID,E.CODIGO,E.NOMBRES,E.APELLIDOS,E.DIRECCION,E.TELEFONO,E.FECHA_NACIMIENTO,P.PUESTO,P.ID_PUESTO FROM EMPLEADOS AS E INNER JOIN PUESTOS AS P ON E.ID_PUESTO = P.ID_PUESTO;");
            MySqlDataAdapter query = new MySqlDataAdapter(consulta, conectar.conectar);
            query.Fill(tabla);
            conectar.CerarConexion();
            return tabla;
        }

        public void GridEmpleados(GridView grid){
            grid.DataSource = GridEmpleados();
            grid.DataBind();

        }

        public int Agregar(string codigo,string nombres,string apellidos,string direccion,string telefono,string fecha,int id_puesto){
            int no_ingreso = 0;
            conectar = new ConexionBD();
            conectar.AbrirConexion();
            string strConsulta = string.Format("INSERT INTO EMPLEADOS(CODIGO,NOMBRES,APELLIDOS,DIRECCION,TELEFONO,FECHA_NACIMIENTO,ID_PUESTO) VALUES('{0}','{1}','{2}','{3}','{4}','{5}',{6});",codigo,nombres,apellidos,direccion,telefono,fecha,id_puesto);
            MySqlCommand insertar = new MySqlCommand(strConsulta, conectar.conectar);
            
            insertar.Connection = conectar.conectar;
            no_ingreso = insertar.ExecuteNonQuery();
            conectar.CerarConexion();
            return no_ingreso;

        }

        public int Modificar(int id,string codigo, string nombres, string apellidos, string direccion, string telefono, string fecha, int id_puesto){
            int no_ingreso = 0;
            conectar = new ConexionBD();
            conectar.AbrirConexion();
            string strConsulta = string.Format("UPDATE EMPLEADOS SET CODIGO = '{0}',NOMBRES = '{1}',APELLIDOS = '{2}',DIRECCION='{3}',TELEFONO='{4}',FECHA_NACIMIENTO='{5}',ID_PUESTO = {6} WHERE ID_EMPLEADO = {7};", codigo, nombres, apellidos, direccion, telefono, fecha, id_puesto,id);
            MySqlCommand modificar = new MySqlCommand(strConsulta, conectar.conectar);

            modificar.Connection = conectar.conectar;
            no_ingreso = modificar.ExecuteNonQuery();
            conectar.CerarConexion();
            return no_ingreso;
        }

        public int Eliminar(int id){
            int no_ingreso = 0;
        conectar = new ConexionBD();
        conectar.AbrirConexion();
            string strConsulta = string.Format("DELETE FROM EMPLEADOS  WHERE ID_EMPLEADO = {0};", id);
        MySqlCommand eliminar = new MySqlCommand(strConsulta, conectar.conectar);

            eliminar.Connection = conectar.conectar;
            no_ingreso = eliminar.ExecuteNonQuery();
            conectar.CerarConexion();
            return no_ingreso;
        }

}
}