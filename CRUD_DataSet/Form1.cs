using CRUD_DataSet.MyDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD_DataSet
{
    public partial class Form1 : Form
    {

        private ProductoTableAdapter _productoTableAdapter;

        public Form1()
        {
            InitializeComponent();
            _productoTableAdapter = new ProductoTableAdapter();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void RefreshData()
        {
            MyDataSet.ProductoDataTable dt = _productoTableAdapter.GetData();
            dgvProductos.DataSource = dt;
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            if (ValidarEntrada())
            {
                try
                {
                    _productoTableAdapter.Add(txtNombre.Text.Trim(), txtMarca.Text.Trim(), (int)txtPrecio.Value);
                    MessageBox.Show("Producto agregado correctamente");
                    RefreshData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al agregar producto: {ex.Message}");
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            int? id = GetSelectedProductId();
            if (id != null && ValidarEntrada())
            {
                try
                {
                    _productoTableAdapter.Edit(txtNombre.Text.Trim(), txtMarca.Text.Trim(), (int)txtPrecio.Value, (int)id);
                    MessageBox.Show("Producto modificado con éxito");
                    RefreshData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al modificar producto: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un producto válido para modificar.");
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int? id = GetSelectedProductId();
            if (id != null)
            {
                try
                {
                    _productoTableAdapter.Remove((int)id);
                    MessageBox.Show("Producto eliminado con éxito");
                    RefreshData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar producto: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un producto válido para eliminar.");
            }
        }
        private int? GetSelectedProductId()
        {
            try
            {
                return int.Parse(dgvProductos.CurrentRow.Cells[0].Value.ToString());
            }
            catch
            {
                return null;
            }
        }

        private bool ValidarEntrada()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtMarca.Text))
            {
                MessageBox.Show("Por favor, completa todos los campos.");
                return false;
            }
            if (txtPrecio.Value <= 0)
            {
                MessageBox.Show("El precio debe ser mayor que cero.");
                return false;
            }
            return true;
        }
    }
}
