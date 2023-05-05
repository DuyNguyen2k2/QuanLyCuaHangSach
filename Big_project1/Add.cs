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
    public partial class Add : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        public Add()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> emptyFields = new List<string>(); // danh sách các ô trống

            if (txtMa.Text.Length == 0)
            {
                emptyFields.Add("Mã sách");
            }

            if (txtTen.Text.Length == 0)
            {
                emptyFields.Add("Tên sách");
            }

            if (txtLoai.Text.Length == 0)
            {
                emptyFields.Add("Chủng loại");
            }

            if (txtNxb.Text.Length == 0)
            {
                emptyFields.Add("Nhà xuất bản");
            }

            if (txtTg.Text.Length == 0)
            {
                emptyFields.Add("Tác giả");
            }

            if (txtGia.Text.Length == 0)
            {
                emptyFields.Add("Giá");
            }
            else
            {
                if (!int.TryParse(txtGia.Text, out int gia))
                {
                    MessageBox.Show("Giá phải là số nguyên!");
                    return;
                }
            }
            if (emptyFields.Count > 0)
            {
                string fields = string.Join(", ", emptyFields.ToArray());
                MessageBox.Show($"Không được để trống {fields}!");
            }
            else
            {
                cmd = new SqlCommand($"select COUNT(*)  from Sach where [Mã sách] = '{txtMa.Text}'", conn);
                int result = (int) cmd.ExecuteScalar();
                if (result == 0)
                {
                    try
                    {
                        if (txtTen.Text.Length == 0 || txtNxb.Text.Length == 0 || txtLoai.Text.Length == 0
                            || txtTg.Text.Length == 0 || txtGia.Text.Length == 0) throw new Exception();

                        int gia = Convert.ToInt32(txtGia.Text);

                        cmd = new SqlCommand($"insert into Sach values ('{txtMa.Text}',N'{txtTen.Text}',N'{txtLoai.Text}',N'{txtNxb.Text}',N'{txtTg.Text}', {gia})", conn);
                        if (cmd.ExecuteNonQuery() <= 0)
                        {

                            throw new Exception();
                        }
                        else
                        {
                            MessageBox.Show("Thêm mới thành công!!");
                        }

                        this.Close();
                        Form1 form1 = new Form1();
                        form1.Show();
                        this.Hide();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Giá trị nhập không hợp lệ!");
                    }
                }
                else
                {
                    MessageBox.Show("Mã đã tồn tại!!");
                }
            }
        }

        private void Add_Load(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
