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
    enum RowState
    {
        Existed,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }
    
    public partial class Form1 : Form
    {
        Database database = new Database();
        int selectedRow;
        public Form1()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            

        }
        private void CreateColumns()
        {
            dataGridView1.Columns.Add("name_of_product", "Наименование изделия");
            dataGridView1.Columns.Add("code_of_product", "Код изделия");
            dataGridView1.Columns.Add("number_of_factory", "Номер цеха-изготовителя");
            dataGridView1.Columns.Add("date_of_creation", "Дата изготовления");
            dataGridView1.Columns.Add("count_of", "Колличество");
            dataGridView1.Columns.Add("cost_price", "Себестоимость");
            dataGridView1.Columns.Add("price", "Цена");
            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns.Add("IsNew", String.Empty);
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
        }
        private void ReadSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Rows.Add(record.GetString(0), record.GetInt32(1), record.GetInt32(2), record.GetDateTime(3), record.GetInt32(4), record.GetInt32(5), record.GetInt32(6), record.GetInt32(7), RowState.ModifiedNew);
        }
        private void RefreshDataGrid(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string queryString = $"select * from Database_TRPO1";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                ReadSingleRow(dgw, reader);
            }
            reader.Close();
                 
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            CreateColumns();
            RefreshDataGrid(dataGridView1);
       
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedRow = e.RowIndex;
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                name_of_product.Text = row.Cells[0].Value.ToString();
                code_of_product.Text = row.Cells[1].Value.ToString();
                number_of_factory.Text = row.Cells[2].Value.ToString();
                date_of_creation.Text = row.Cells[3].Value.ToString();
                count_of.Text = row.Cells[4].Value.ToString();
                cost_price.Text = row.Cells[5].Value.ToString();
                price.Text = row.Cells[6].Value.ToString();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add_Form add_Form = new Add_Form();
            add_Form.Show();
        }
        private void _Search(DataGridView dgw)
        {
            dgw.Rows.Clear();
            string searchstring = $"select * from Database_TRPO1 where concat (name_of_product, code_of_product, number_of_factory, date_of_creation, count_of, cost_price, price, id) like '%" + Search.Text + "%'";
            SqlCommand cmd = new SqlCommand(searchstring, database.getConnection());
            database.openConnection();
            SqlDataReader read = cmd.ExecuteReader();
            while(read.Read())
            {
                ReadSingleRow(dgw, read);
            }
            read.Close();
        }
        private void Search_TextChanged(object sender, EventArgs e)
        {
            _Search(dataGridView1);
        }
        private void deleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows[index].Visible = false;
            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[8].Value = RowState.Deleted;
                return;
            }
            dataGridView1.Rows[index].Cells[8].Value = RowState.Deleted;
        }
        private void update()
        {
            database.openConnection();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                var rowstate = (RowState)dataGridView1.Rows[i].Cells[8].Value;
                if (rowstate == RowState.Existed)
                    continue;
                if(rowstate == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[i].Cells[7].Value);
                    var deleteQuery = $"delete from Database_TRPO1 where id = {id}";
                    var comm = new SqlCommand(deleteQuery, database.getConnection());
                    comm.ExecuteNonQuery();

                }
                if(rowstate == RowState.Modified)
                {
                    var name = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    var code = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    var number = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    var date = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    var count = dataGridView1.Rows[i].Cells[4].Value.ToString();
                    var cost = dataGridView1.Rows[i].Cells[5].Value.ToString();
                    var price = dataGridView1.Rows[i].Cells[6].Value.ToString();
                    var id = dataGridView1.Rows[i].Cells[7].Value.ToString();
                    var changeQuery = $"update Database_TRPO1 set name_of_product = '{name}', code_of_product = '{code}', number_of_factory = '{number}', date_of_creation = '{date}', count_of = '{count}', cost_price = '{cost}', price = '{price}' where id = '{id}'";
                    var command = new SqlCommand(changeQuery, database.getConnection());
                    command.ExecuteNonQuery();
                }
            }
            database.closeConnection();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            deleteRow();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            update();
        }
        private void change()
        {
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
            var name = name_of_product.Text;
            var number = number_of_factory.Text;
            var date = date_of_creation.Text;
            var count = count_of.Text;
            var costprice = cost_price.Text;
            var price_ = price.Text;
            int code;
            if(dataGridView1.Rows[selectedRowIndex].Cells[7].Value.ToString()!=string.Empty)
            {
                if(int.TryParse(code_of_product.Text, out code))
                {
                    dataGridView1.Rows[selectedRowIndex].SetValues(name, code, number, date, count, costprice, price_);
                    dataGridView1.Rows[selectedRowIndex].Cells[8].Value = RowState.Modified;

                }
                else
                {
                    MessageBox.Show("Ошибка");
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            change();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Dop_inf dop = new Dop_inf(this);
            dop.Show();
        }
    }
}
