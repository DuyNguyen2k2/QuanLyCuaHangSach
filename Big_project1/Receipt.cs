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

namespace Big_project1
{
    public partial class Receipt : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        public Receipt()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Receipt_Load(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(DatabaseConnection.ConnectionString);
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new SqlCommand($"insert into HoaDon values " +
                    $"('{NgayLap.Value.ToShortDateString()}', N'{txtTenkh.Text}'); " +
                    $"select SCOPE_IDENTITY()", conn);
                
                int maHD = Convert.ToInt32(cmd.ExecuteScalar());

                MessageBox.Show("Thêm mới hóa đơn thành công!!");
                InvoiceDetails invoice = new InvoiceDetails(maHD);
                invoice.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Home form1 = new Home();
            form1.Show();
            this.Hide();
        }
    }
}
