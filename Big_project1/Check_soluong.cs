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
    public partial class Check_soluong : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        public Check_soluong()
        {
            InitializeComponent();
        }
        private void Load_comboBox()
        {
            cmd = new SqlCommand("select [Tên sách] from Sach, Kho where Sach.[Mã sách] = Kho.[Mã sách]", conn);
            adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            cbbTen.DataSource = ds.Tables[0];
            cbbTen.ValueMember = "Tên sách";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand($"select sum ([Số lượng]) from Sach, Kho where Sach.[Mã sách] = Kho.[Mã sách] and [Tên sách] = N'{cbbTen.Text}'",conn);

            int amount = (int) cmd.ExecuteScalar();
            if (amount > 0)
                lbresult.Text = " " + amount;
            else
            {
                MessageBox.Show("Sách đã hết vui lòng nhập thêm.");
                lbresult.Text = " 0";
            }
        }

        private void Check_soluong_Load(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(@"Data Source=DESKTOP-13D72EF\SQLEXPRESS;Initial Catalog=QuanLySach;Integrated Security=True");
                conn.Open();
                Load_comboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
