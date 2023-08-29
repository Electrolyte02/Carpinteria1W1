using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog_II__15_del_08
{
    internal class DetallePresupuesto
    {
        private Producto producto;
        private int cantidad;

        public DetallePresupuesto()
        {
            producto = null;
            cantidad = 0;
        }

        public DetallePresupuesto(Producto p, int c)
        {
            producto = p;
            cantidad = c;
        }
        public Producto Producto
        {
            get { return producto; }
            set { producto = value; }
        }

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        public override string ToString()
        {
            return "Cantidad:"+cantidad+producto.ToString();
        }

        public double CalcularSubtotal()
        {
            return (producto.Precio*cantidad);
        }
    }
}
