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
    public partial class Search : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        public Search()
        {
            InitializeComponent();
        }

        private void Search_Load(object sender, EventArgs e)
        {
            cbCategory.Height = 30;
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
            
                if (textSearch.Text.Length == 0)
                {
                    MessageBox.Show("Bạn chưa nhập thông tin!!");
                }
                else
                {
                    adapter = new SqlDataAdapter("select Sach.[Mã sách], [Tên sách], [Chủng loại], " +
                        "[Nhà xuất bản], [Tác giả], [Giá], [Số lượng] from Sach, Kho " +
                        "where Sach.[Mã sách] = Kho.[Mã sách]" +
                        $"and Sach.[Tên sách] like N'%{textSearch.Text}%'", conn);
                    dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows.Count <= 0)
                    {
                        MessageBox.Show("Không có sách nào được tìm thấy.");
                    }
                    else
                        dataGridView1.DataSource = dt;
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
