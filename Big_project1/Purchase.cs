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
    public partial class Purchase : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        public Purchase()
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
        private void Purchase_Load(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtSoLuong.Text.Length <= 0)
                {
                     MessageBox.Show("Bạn chưa nhập số lượng!");
                }
                else
                {
                    int soluong = Convert.ToInt32(txtSoLuong.Text);
                    cmd = new SqlCommand($"update Kho set [Số lượng] = (select sum([Số lượng]) from Kho where [Mã sách] = (select [Mã sách] from Sach where [Tên sách] = N'{cbbTen.Text}')) + {soluong} where [Mã sách] = (select [Mã sách] from Sach where [Tên sách] = N'{cbbTen.Text}')", conn);
                    if(cmd.ExecuteNonQuery() <= 0)
                    {
                        MessageBox.Show("Nhập số lượng không thành công!!");
                    }
                    else
                    {
                        MessageBox.Show("Nhập số lượng thành công!!");
                    }
                    this.Close();
                    Form1 form1 = new Form1();
                    form1.Show();
                    this.Hide();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Lỗi không đúng định dạng số!");
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
