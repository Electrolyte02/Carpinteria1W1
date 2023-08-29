using Prog_II__15_del_08.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Prog_II__15_del_08.Presentacion
{
    public partial class FrmNuevoPresupuesto : Form
    {
        Presupuesto presupuestoNuevo;
        public FrmNuevoPresupuesto()
        {
            InitializeComponent();
            presupuestoNuevo = new Presupuesto();
        }

        private void FrmNuevoPresupuesto_Load(object sender, EventArgs e)
        {
            ProximoPresupuesto();
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtFecha.Enabled = false;
            txtCliente.Text = "CONSUMIDOR FINAL";
            txtDescuento.Text = "0";
            txtCantidad.Text = "0";
            this.ActiveControl = cboProductos;
            txtTotal.Enabled = false;
            txtSubTotal.Enabled = false;
            CargarProductos();
        }

        private void CargarProductos()
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = "Data Source=172.16.10.196;Initial Catalog=Carpinteria_2023;User ID=alumno1w1;Password=alumno1w1";
            conexion.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "SP_CONSULTAR_PRODUCTOS";
            DataTable tabla = new DataTable();
            tabla.Load(comando.ExecuteReader());
            conexion.Close();
            //Por ahora estamos viendo esto,pero dsp necesitamos el precio, etc...
            cboProductos.DataSource=tabla;
            cboProductos.ValueMember = "id_producto";
            cboProductos.DisplayMember = "n_producto";
            cboProductos.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProductos.SelectedIndex = -1;
        }

        private void ProximoPresupuesto()
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = "Data Source=172.16.10.196;Initial Catalog=Carpinteria_2023;User ID=alumno1w1;Password=alumno1w1";
            conexion.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "SP_PROXIMO_ID";
            SqlParameter param = new SqlParameter();
            param.Direction = ParameterDirection.Output;
            param.ParameterName = "@next";
            param.DbType = DbType.Int32;
            comando.Parameters.Add(param);
            comando.ExecuteNonQuery();
            conexion.Close();

            lblPresupuestoNro.Text += (" " + param.Value.ToString()); 
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if(cboProductos.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un producto!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(txtCantidad.Text)|| !int.TryParse(txtCantidad.Text,out  _))
            {
                MessageBox.Show("Debe ingresar una cantidad valida!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach(DataGridViewRow row in dgvDetalles.Rows)
            {
                if (row.Cells["ColProducto"].Value.ToString()==(cboProductos.Text))
                {
                    MessageBox.Show("Este producto ya fue presupuestado...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            DataRowView item = (DataRowView)cboProductos.SelectedItem;
            int prod = (int)item.Row.ItemArray[0];
            string nombre = (string)item.Row.ItemArray[1];
            double precio = Convert.ToDouble(item.Row.ItemArray[2]);
            Producto p = new Producto(prod, nombre, precio,true);
            int cant = Convert.ToInt32(txtCantidad.Text);
            DetallePresupuesto detalle = new DetallePresupuesto(p, cant);

            presupuestoNuevo.AgregarDetalle(detalle);
            //a la grilla la row que agregamos, le tenemos que decir por c/u, similar al destructuring...ed
            dgvDetalles.Rows.Add(new object[] {detalle.Producto.ProductoNro,detalle.Producto.Nombre,detalle.Producto.Precio,detalle.Cantidad, "Quitar"});

            CalcularTotales();
        }

        private void CalcularTotales()
        {
            txtSubTotal.Text = presupuestoNuevo.CalcularTotal().ToString();
            if(!string.IsNullOrEmpty(txtDescuento.Text) && int.TryParse(txtDescuento.Text,out _))
            {
                double desc = presupuestoNuevo.CalcularTotal() * Convert.ToDouble(txtDescuento.Text) / 100;
                txtTotal.Text = (presupuestoNuevo.CalcularTotal() - desc).ToString();
            }
        }

        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalles.CurrentCell.ColumnIndex==4)
            {
                presupuestoNuevo.QuitarDetalle(dgvDetalles.CurrentRow.Index);
                dgvDetalles.Rows.RemoveAt(dgvDetalles.CurrentRow.Index);
                CalcularTotales();
            }
        }
    }
}
