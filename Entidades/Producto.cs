using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog_II__15_del_08
{
    internal class Producto
    {
        private int productoNro;
        private string nombre;
        private double precio;
        private bool activo;

        public Producto()
        {
            productoNro = 0;
            nombre = string.Empty;
            precio = 0;
            activo = true;
        }

        public Producto(int p, string n, double pr, bool a)
        {
            productoNro = p;
            nombre = n;
            precio = pr;
            activo = a;
        }

        public int ProductoNro
        {
            get { return productoNro; }
            set { productoNro = value; }
        }
        
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public double Precio
        {
            get { return precio;}
            set {precio = value;}   
        }

        public bool Activo
        {
            get { return activo;}
            set { activo = value; }
        }

        public override string ToString()
        {
            return productoNro + " " + nombre;
        }

        public string MostrarDatos()
        {
            return nombre+""+productoNro+" (" + precio + ")";
        }
    }
}
