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
        SqlDataReader reader;
        SqlDataAdapter adapter;
        DataTable dt;

        List<string> categories = new List<string>();
        public Search()
        {
            InitializeComponent();
        }

        private void Load_Data()
        {
            adapter = new SqlDataAdapter("select Sach.[Mã sách], [Tên sách], [Chủng loại] as 'Thể loại', [Nhà xuất bản], [Tác giả], [Giá], [Số lượng] from Sach, Kho where Sach.[Mã sách] = Kho.[Mã sách]", conn);
            dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Load_Combobox()
        {
            categories.Add("All");
            string sql = "select [Chủng loại] from Sach group by [Chủng loại]";
            cmd = new SqlCommand(sql, conn);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                categories.Add(reader.GetString(0));
            };
            cbCategory.Items.AddRange(categories.ToArray());
            cbCategory.SelectedIndex = 0;

            reader.Close();
        }

        private void Search_Load(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection(DatabaseConnection.ConnectionString);
                conn.Open();
                Load_Data();
                Load_Combobox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string name = txtName.Text;
            string category = (cbCategory.SelectedItem.ToString() == "All") ? "" : cbCategory.SelectedItem.ToString(); 
            string author = txtAuthor.Text;
            
            try
            {
                adapter = new SqlDataAdapter("select Sach.[Mã sách], [Tên sách], [Chủng loại] as 'Thể loại', " +
                        "[Nhà xuất bản], [Tác giả], [Giá], [Số lượng] from Sach, Kho " +
                        "where Sach.[Mã sách] = Kho.[Mã sách]" +
                        $"and [Tên sách] like N'%{name}%' and [Chủng loại] like N'%{category}%'" +
                        $" and [Tác giả] like N'%{author}%'", conn);
                dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("Không tìm thấy sách có thông tin như trên!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Load_Data();
                }
                else
                    dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Home home = new Home();
            home.Show();
            this.Hide();
        }
    }
}
