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
    public partial class Delete : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        public Delete()
        {
            InitializeComponent();
        }

        private void Delete_Load(object sender, EventArgs e)
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

        private void txtMa_TextChanged(object sender, EventArgs e)
        {
            adapter = new SqlDataAdapter($"select * from Sach where [Mã sách] = '{txtMa.Text}'", conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtMa.Text.Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập mã sách!!");
            }
            else
            {
                cmd = new SqlCommand($"select count(*)  from Sach where [Mã sách] = '{txtMa.Text}'", conn);
                int result = (int)cmd.ExecuteScalar();
                if (result > 0)
                {
                    try
                    {
                        cmd = new SqlCommand($"delete from Sach where [Mã sách] = '{txtMa.Text}'", conn);
                        if (cmd.ExecuteNonQuery() <= 0)
                        {

                            MessageBox.Show("Xóa thất bại!!");
                        }
                        else
                        {
                            MessageBox.Show("Xóa thành công!!");
                        }
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Xóa thất bại!!");
                    }
                    this.Close();
                    Home form1 = new Home();
                    form1.Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("Mã không tồn tại!!");
                }
            }
        }
    }
}
