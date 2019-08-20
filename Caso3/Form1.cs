using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caso3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString);

        public void listaPedidos()
        {
            using (SqlCommand cmd = new SqlCommand("usp_listapedidos_registrados", cn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    //da.SelectCommand.Parameters.AddWithValue("@anio", cbAñoPedido.SelectedValue);

                    using (DataSet df = new DataSet())
                    {
                        da.Fill(df, "Pedidos");
                        dgPedidos.DataSource = df.Tables["Pedidos"];
                        //lblPedido.Text = df.Tables["Pedidos"].Rows.Count.ToString();
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listaPedidos();
        }

        private void dgPedidos_DoubleClick(object sender, EventArgs e)
        {
            int codigo;
            codigo = Convert.ToInt32(dgPedidos.CurrentRow.Cells[0].Value);
            using (SqlCommand cmd = new SqlCommand("usp_detalle_pedido", cn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter())
                {
                    da.SelectCommand = cmd;
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@idpedido", codigo);
                    using (DataSet df = new DataSet())
                    {
                        da.Fill(df, "detallesdepedidos");
                        dgProductos.DataSource = df.Tables["detallesdepedidos"];
                        //lblDetalle.Text = df.Tables["detallesdepedidos"].Compute("Sum(monto)", "").ToString();

                    }
                }
            }
        }
    }
}
