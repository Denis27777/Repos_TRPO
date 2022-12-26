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
    public partial class sign_up : Form
    {
        Database database = new Database();
        public sign_up()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            var login = textBox1.Text;
            var password = textBox2.Text;
            string querystring = $"insert into register(login_user, password_user) values('{login}', '{password}') ";
            SqlCommand command = new SqlCommand(querystring, database.getConnection());
            database.openConnection();
            if (chekuser(login,password))
            {
                return;
            }
            if(command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Аккаунт успешно создан");
                log_in _login = new log_in();
                this.Hide();
                _login.Closed += (s, args) => this.Close();
                _login.Show();
            }
            else
            {
                MessageBox.Show("Аккаунт не создан");
            }
            database.closeConnection();
            
        }
        public Boolean chekuser(string lg, string ps)
        {
            lg = textBox1.Text;
            ps = textBox2.Text;
            //var loginUser = textBox1.Text;
            //var passUser = textBox2.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();
            string querystring = $"select id_user login_user, password_user from register where login_user = '{lg}' and password_user = '{ps}'";
            SqlCommand command = new SqlCommand(querystring, database.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Аккаунт уже существует");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void sign_up_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '•';
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            log_in log_ = new log_in();
            log_.Show();
            this.Hide();
        }
    }
}
