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
    public partial class Add_Form : Form
    {
        Database database = new Database();
        public Add_Form()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            database.openConnection();
            var name = textBox1.Text;
            var number = textBox3.Text;
            var date = textBox4.Text;
            var count = textBox5.Text;
            var cost_price = textBox6.Text;
            var price = textBox7.Text;
            int code;
            if(int.TryParse(textBox2.Text, out code))
            {
                var addQuery = $"insert into Database_TRPO1 (name_of_product, code_of_product, number_of_factory, date_of_creation, count_of, cost_price, price) values ('{name}', '{code}', '{number}', '{date}', '{count}', '{cost_price}', '{price}')";
                var command = new SqlCommand(addQuery, database.getConnection());
                command.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("Ошибка!");
            }
            database.closeConnection();

        }
    }
}
