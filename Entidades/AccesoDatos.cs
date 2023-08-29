using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Prog_II__15_del_08.Entidades
{
    internal class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        private string stringConexion = "ingrese datos de conexion sql";

        public AccesoDatos()
        {
            conexion = new SqlConnection(stringConexion);
        }

    }
}
