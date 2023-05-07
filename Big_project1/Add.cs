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
                if (!Double.TryParse(txtGia.Text, out double gia))
                {
                    MessageBox.Show("Giá tiền phải là kiểu số", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (emptyFields.Count > 0)
            {
                string fields = string.Join(", ", emptyFields.ToArray());
                MessageBox.Show($"Không được để trống {fields}!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                try
                {
                    double gia = Convert.ToDouble(txtGia.Text);

                    cmd = new SqlCommand($"insert into Sach values (N'{txtTen.Text}',N'{txtLoai.Text}',N'{txtNxb.Text}',N'{txtTg.Text}', {gia})", conn);
                    if (cmd.ExecuteNonQuery() <= 0)
                    {

                        throw new Exception();
                    }
                    else
                    {
                        MessageBox.Show("Thêm mới thành công!!");
                    }

                    this.Close();
                    Home form1 = new Home();
                    form1.Show();
                    this.Hide();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }

        private void Add_Load(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Home form1 = new Home();
            form1.Show();
            this.Hide();
        }
    }
}
