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
    
    public partial class Update : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        public Update()
        {
            InitializeComponent();
        }

        private void Update_Load(object sender, EventArgs e)
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
            if (txtMa.Text.Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập mã sách!!");
            }
            else
            {
                cmd = new SqlCommand($"select * from Sach where [Mã sách] = '{txtMa.Text}'", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    try
                    {
                        string ten = txtTen.Text.Length > 0 ? txtTen.Text : reader["Tên sách"].ToString();
                        string loai = txtLoai.Text.Length > 0 ? txtLoai.Text : reader["Chủng loại"].ToString();
                        string nxb = txtNxb.Text.Length > 0 ? txtNxb.Text : reader["Nhà xuất bản"].ToString();
                        string tg = txtTg.Text.Length > 0 ? txtTg.Text : reader["Tác giả"].ToString();
                        int gia = txtGia.Text.Length > 0 ? Convert.ToInt32(txtGia.Text) : Convert.ToInt32(reader["Giá"]);

                        reader.Close();

                        cmd = new SqlCommand($"update Sach set [Tên sách] = N'{ten}', [Chủng loại] = N'{loai}', [Nhà xuất bản] = N'{nxb}', [Tác giả] = N'{tg}', [Giá] = {gia} where [Mã sách] = N'{txtMa.Text}' ", conn);
                        if (cmd.ExecuteNonQuery() <= 0)
                        {
                            throw new Exception();
                        }
                        else
                        {
                            MessageBox.Show("Sửa thành công!!");
                        }

                        this.Close();
                        Home form1 = new Home();
                        form1.Show();
                        this.Hide();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Giá trị sửa không hợp lệ!");
                    }
                }
                else
                {
                    reader.Close();
                    MessageBox.Show("Mã không tồn tại!!");
                }
            }


        }

        private void txtMa_TextChanged(object sender, EventArgs e)
        {
            try
            {
                adapter = new SqlDataAdapter($"select * from Sach where [Mã sách] = '{txtMa.Text}'", conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mã sách phải là kiểu số nguyên", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMa.ResetText();
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                txtMa.Text = row.Cells[0].Value.ToString();
                txtTen.Text = row.Cells[1].Value.ToString();
                txtLoai.Text = row.Cells[2].Value.ToString();
                txtNxb.Text = row.Cells[3].Value.ToString();
                txtTg.Text = row.Cells[4].Value.ToString();
                txtGia.Text = row.Cells[5].Value.ToString();
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
