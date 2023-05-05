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
        SqlDataAdapter adapter;
        DataTable dt;
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
                conn = new SqlConnection(@"Data Source=DESKTOP-13D72EF\SQLEXPRESS;Initial Catalog=QuanLySach;Integrated Security=True");
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtMHD.Text ==  "" || txtTenkh.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mã hóa đơn hoặc tên khách hàng");
            }
            else
            {
                try
                {
                    cmd = new SqlCommand($"insert into HoaDon values('{txtMHD.Text}', '{NgayLap.Value.ToShortDateString()}', N'{txtTenkh.Text}')",conn);
                    if(cmd.ExecuteNonQuery() <= 0)
                    {
                        MessageBox.Show("Thêm không thành công");
                    }
                    else
                    {
                        MessageBox.Show("Thêm mới hóa đơn thành công!!");
                        InvoiceDetails invoice = new InvoiceDetails(txtMHD.Text);
                        invoice.Show();
                        this.Hide();
                    }
                    
                }
                catch(Exception )
                {
                    MessageBox.Show("Mã đã tồn tại!!");
                }
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
