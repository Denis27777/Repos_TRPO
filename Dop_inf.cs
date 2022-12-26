using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Курсовая_ТРПО_2022
{
    public partial class Dop_inf : Form
    {
        Form1 form1;
        public Dop_inf(Form1 fm)
        {
            InitializeComponent();
            this.form1 = fm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int sum = 0;
            for(int i = 0;i<form1.dataGridView1.Rows.Count;i++)
            {
                sum += (int)form1.dataGridView1.Rows[i].Cells[5].Value;
            }
            
            int sum1 = 0;
            for (int i = 0; i < form1.dataGridView1.Rows.Count; i++)
            {
                sum1 += (int)form1.dataGridView1.Rows[i].Cells[6].Value;
            }
            MessageBox.Show(Convert.ToString(sum1 - sum));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int sm2 = 0;
            DateTime t1;
            DateTime t2;
            
            for (int i = 0;i<form1.dataGridView1.Rows.Count; i++)
            {               
                if(DateTime.TryParse(textBox1.Text, out t1)&&DateTime.TryParse(form1.dataGridView1.Rows[i].Cells[3].Value.ToString(), out t2))
                {
                    if(t1 > t2)
                    {
                        sm2++;
                    }
                }
            }
            MessageBox.Show(sm2.ToString());
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var txt = textBox2.Text;
            for(int i = 0;i<form1.dataGridView1.Rows.Count;i++)
            {
                if(txt == form1.dataGridView1.Rows[i].Cells[0].Value.ToString())
                {
                    MessageBox.Show(form1.dataGridView1.Rows[i].Cells[6].Value.ToString());
                }

            }
        }
    }
}
