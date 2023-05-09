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
    public partial class Login : Form
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader reader;

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                MessageBox.Show("Bạn chưa nhập tài khoản hoặc mật khẩu", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {
                    string sql = "select * from NguoiDung where [Tài khoản]=@username and [Mật khẩu]=@password";

                    cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Close();
                        conn.Close();

                        Home home = new Home();
                        home.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Tài khoản hoặc mật khẩu không đúng", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void checkShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkShowPass.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.PasswordChar = '\0';
            }
            else txtPassword.UseSystemPasswordChar = true;
        }

        private void Login_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin.PerformClick();
                e.Handled = true;
            }
        }
    }
}
