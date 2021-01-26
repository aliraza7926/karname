using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace WindowsFormsApp3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result=MessageBox.Show("از صحت اطلاعات وارد شده اطمینان دارید؟؟","هشدار",MessageBoxButtons.YesNo);
            if (result==DialogResult.Yes)
            {
                Database.Add_New_Student_Sanjesh(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);
                MessageBox.Show(".اطلاعات با موفقیت ثبت شد");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";

            

            }
        }

       
    }
}
