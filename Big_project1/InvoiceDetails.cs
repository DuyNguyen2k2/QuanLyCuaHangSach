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
    public partial class InvoiceDetails : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable dt;
        int mhd;
        public InvoiceDetails()
        {
            InitializeComponent();
        }
        public InvoiceDetails(int _mhd)
        {
            mhd = _mhd;
            InitializeComponent();
        }
        private void Load_comboBox()
        {
            cmd = new SqlCommand("select [Tên sách] from Sach, Kho where Sach.[Mã sách] = Kho.[Mã sách]", conn);
            adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            cbbTenSach.DataSource = ds.Tables[0];
            cbbTenSach.ValueMember = "Tên sách";
        }
        private void InvoiceDetails_Load(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(DatabaseConnection.ConnectionString);
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
                cmd = new SqlCommand($"select [Số lượng] from Kho, Sach where Kho.[Mã sách] = Sach.[Mã sách] and [Tên sách] = N'{cbbTenSach.Text}'", conn);
                int result = (int)cmd.ExecuteScalar();
                int sl = Convert.ToInt32(txtSl.Text);
                if(result < sl)
                {
                    MessageBox.Show($"Không đủ số lượng sách tồn!!\n Số lượng còn {result}");
                }
                else
                {
                    cmd = new SqlCommand($"insert into ChiTietHoaDon values ('{this.mhd}', (select [Mã sách] from Sach where [Tên sách] = N'{cbbTenSach.Text}'), {sl}, {sl} * (select [Giá] from Sach where [Tên sách] = N'{cbbTenSach.Text}'))", conn);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Thêm thành công!!");
                        Load_Data();
                    }
                    else
                    {
                        MessageBox.Show("Thêm không thành công!!");
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Lỗi số lượng không đúng định dạng số!!");
            }
            
        }
        private void Load_Data()
        {
            adapter = new SqlDataAdapter($"select [Tên sách], [Giá], ChiTietHoaDon.[Số lượng], [Thành tiền] from Sach, ChiTietHoaDon where Sach.[Mã sách] = ChiTietHoaDon.[Mã sách] and [Mã hóa đơn] = '{this.mhd}'", conn);
            dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
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
