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

namespace Курсовая_ТРПО_2022
{
    public partial class log_in : Form
    {
        Database database = new Database();
        public log_in()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void log_in_Load(object sender, EventArgs e)
        {
            textBox_password.PasswordChar = '•';
            textBox_login.MaxLength = 50;
            textBox_password.MaxLength = 50;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var loginUser = textBox_login.Text;
            var passUser = textBox_password.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string querystring = $"select id_user, login_user, password_user from register where login_user = '{loginUser}' and password_user = '{passUser}'";
            SqlCommand command = new SqlCommand(querystring, database.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if(table.Rows.Count == 1)
            {
                MessageBox.Show("Вы успешно вошли в систему", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Form1 frm = new Form1();
                this.Hide();
                //frm.ShowDialog();
                //this.Show();
                frm.Closed += (s, args) => this.Close();
                frm.Show();
            }
            else
            {
                MessageBox.Show("Такого аккаунта не существует", "Аккаунта не существует", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            sign_up sign = new sign_up();
            sign.Show();
            this.Hide();
        }
    }
}
