using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prog_II__15_del_08.Entidades
{
    internal class Presupuesto
    {
        private int presupuestoNro;
        private DateTime fecha;
        private string cliente;
        private float costoMO;
        private List<DetallePresupuesto> detalles;
        private float descuento;
        private DateTime fechaBaja;

        public Presupuesto()
        {
            presupuestoNro = 0;
            fecha = DateTime.Now;
            cliente = string.Empty;
            costoMO = 0;
            detalles = new List<DetallePresupuesto>();
            descuento = 0;
            fechaBaja = DateTime.Now;
        }

        public int PresupuestoNro
        {
            get { return presupuestoNro; }
            set { presupuestoNro = value; }
        }

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public float CostoMO
        {
            get { return costoMO; }
            set { costoMO = value; }
        }

        public float Descuento
        {
            get { return descuento; }
            set { descuento = value;}
        }

        public DateTime FechaBaja
        {
            get { return fechaBaja; }
            set {fechaBaja = value;}
        }

        public void AgregarDetalle(DetallePresupuesto detalleP)
        {
            detalles.Add(detalleP);
        }

        public void QuitarDetalle(int posicion)
        {
            detalles.RemoveAt(posicion);
        }

        public double CalcularTotal()
        {
            double total = 0;
            foreach(DetallePresupuesto d in detalles)
            {
                total += d.CalcularSubtotal();
            }
            return total;
        }

    }
}
