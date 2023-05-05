using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace Big_project1
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;

        public Form1()
        {
            InitializeComponent();
        }
        private void Load_Data()
        {
            adapter = new SqlDataAdapter("select Sach.[Mã sách], [Tên sách], [Chủng loại], [Nhà xuất bản], [Tác giả], [Giá], [Số lượng] from Sach, Kho where Sach.[Mã sách] = Kho.[Mã sách]", conn);
            dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(@"Data Source=DESKTOP-13D72EF\SQLEXPRESS;Initial Catalog=QuanLySach;Integrated Security=True");
                conn.Open();
                Load_Data();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add add = new Add();
            add.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Purchase purchase = new Purchase();
            purchase.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Search search = new Search();
            search.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Check_soluong check_Soluong = new Check_soluong();
            check_Soluong.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Receipt receipt = new Receipt();
            receipt.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Sales sales = new Sales();
            sales.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Update update = new Update();
            update.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;
            if(index >= 0)
            {
                string Ma = dataGridView1.Rows[index].Cells[0].Value.ToString();
                var confirm = MessageBox.Show("Bạn có muốn xóa sách này không??", "Xác nhận", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    cmd = new SqlCommand(" ");
                }
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete delete = new Delete();
            delete.Show();
            this.Hide();
        }
    }
}
