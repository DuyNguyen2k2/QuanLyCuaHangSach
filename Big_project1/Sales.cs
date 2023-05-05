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
    public partial class Sales : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        public Sales()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtpNgay.Enabled = false;
            cbbMonth.Enabled = false;
            txtNam.Enabled = false;
            if (cbbCachtinh.SelectedIndex == 1)
            {
                dtpNgay.Enabled = true;
            }
            else if (cbbCachtinh.SelectedIndex == 2)
            {
                cbbMonth.Enabled = true;
                txtNam.Enabled = true;
            }
            else if(cbbCachtinh.SelectedIndex == 3)
            {
                txtNam.Enabled = true;
            }
        }

        private void Sales_Load(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(@"Data Source=DESKTOP-13D72EF\SQLEXPRESS;Initial Catalog=QuanLySach;Integrated Security=True");
                conn.Open();
                cbbCachtinh.SelectedIndex = 0;
                dtpNgay.Enabled = false;
                cbbMonth.Enabled = false;
                txtNam.Enabled = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbbCachtinh.Text == "Chọn cách tính") MessageBox.Show("Vui lòng chọn cách tính!");
            else
            {
                if (dtpNgay.Enabled == true)
                {
                    adapter = new SqlDataAdapter($"select ChiTietHoaDon.[Mã hóa đơn], HoaDon.[Ngày tạo], HoaDon.[Tên khách hàng], [Tên sách], ChiTietHoaDon.[Số lượng], [Giá], [Thành tiền] from Sach, HoaDon, ChiTietHoaDon where Sach.[Mã sách] = ChiTietHoaDon.[Mã sách] and HoaDon.[Mã hóa đơn] = ChiTietHoaDon.[Mã hóa đơn] and [Ngày tạo] = '{dtpNgay.Value.ToShortDateString()}'", conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    cmd = new SqlCommand($"select SUM([Thành tiền]) from ChiTietHoaDon, HoaDon where ChiTietHoaDon.[Mã hóa đơn] = HoaDon.[Mã hóa đơn] and [Ngày tạo] = '{dtpNgay.Value.ToShortDateString()}'", conn);
                    Results.Text = " " + cmd.ExecuteScalar() + " VNĐ";
                }
                else if (cbbMonth.Enabled == false && txtNam.Enabled == true)
                {
                    adapter = new SqlDataAdapter($"select ChiTietHoaDon.[Mã hóa đơn], HoaDon.[Ngày tạo], HoaDon.[Tên khách hàng], [Tên sách], ChiTietHoaDon.[Số lượng], [Giá], [Thành tiền] from Sach, HoaDon, ChiTietHoaDon where Sach.[Mã sách] = ChiTietHoaDon.[Mã sách] and HoaDon.[Mã hóa đơn] = ChiTietHoaDon.[Mã hóa đơn] and [Ngày tạo] like '{txtNam.Text}%'", conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    cmd = new SqlCommand($"select SUM([Thành tiền]) from ChiTietHoaDon, HoaDon where ChiTietHoaDon.[Mã hóa đơn] = HoaDon.[Mã hóa đơn] and [Ngày tạo] like '{txtNam.Text}%'", conn);
                    Results.Text = " " + cmd.ExecuteScalar() + " VNĐ";
                }
                else if (cbbMonth.Enabled == true && txtNam.Enabled == true)
                {
                    adapter = new SqlDataAdapter($"select ChiTietHoaDon.[Mã hóa đơn], HoaDon.[Ngày tạo], HoaDon.[Tên khách hàng], [Tên sách], ChiTietHoaDon.[Số lượng], [Giá], [Thành tiền] from Sach, HoaDon, ChiTietHoaDon where Sach.[Mã sách] = ChiTietHoaDon.[Mã sách] and HoaDon.[Mã hóa đơn] = ChiTietHoaDon.[Mã hóa đơn] and [Ngày tạo] like '{txtNam.Text}-{cbbMonth.Text}%'", conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    cmd = new SqlCommand($"select SUM([Thành tiền]) from ChiTietHoaDon, HoaDon where ChiTietHoaDon.[Mã hóa đơn] = HoaDon.[Mã hóa đơn] and [Ngày tạo] like '{txtNam.Text}-{cbbMonth.Text}%'", conn);
                    Results.Text = " " + cmd.ExecuteScalar() + " VNĐ";
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
