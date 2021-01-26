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
using Tesseract;
using System.Runtime.InteropServices;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Fill_dataGridView1_Sanjesh()
        {
            SQLiteConnection DBpath = new SQLiteConnection(@"Data Source=" + Application.StartupPath + "\\Database.db");
            string CommnadstrLoadData = "SELECT * FROM Student_Information_Sanjesh; ";

            SQLiteCommand LoadData = new SQLiteCommand(CommnadstrLoadData, DBpath);

            DataTable EmployeeManagementData = new DataTable();
            SQLiteDataAdapter ReadDataFromDB = new SQLiteDataAdapter(LoadData);
            ReadDataFromDB.Fill(EmployeeManagementData);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = EmployeeManagementData;
        }

        private void Fill_dataGridView1_Kanoon()
        {
            SQLiteConnection DBpath = new SQLiteConnection(@"Data Source=" + Application.StartupPath + "\\Database.db");
            string CommnadstrLoadData = "SELECT * FROM Student_Information_Kanoon; ";

            SQLiteCommand LoadData = new SQLiteCommand(CommnadstrLoadData, DBpath);

            DataTable EmployeeManagementData = new DataTable();
            SQLiteDataAdapter ReadDataFromDB = new SQLiteDataAdapter(LoadData);
            ReadDataFromDB.Fill(EmployeeManagementData);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = EmployeeManagementData;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Form2 form2 = new Form2();
                form2.ShowDialog();
                Fill_dataGridView1_Sanjesh();
            }
            else
            {
                Form3 form3 = new Form3();
                form3.ShowDialog();
                Fill_dataGridView1_Kanoon();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Fill_dataGridView1_Kanoon();
            dataGridView1.Columns["birth_year"].Visible = false;

            comboBox1.Hide();


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                SQLiteConnection DBpath = new SQLiteConnection(@"Data Source=" + Application.StartupPath + "\\Database.db");
                string CommnadstrLoadData = "SELECT * FROM Student_Information_Sanjesh WHERE last_name like @LastName or first_name like @FirstName; ";

                SQLiteCommand LoadData = new SQLiteCommand(CommnadstrLoadData, DBpath);
                LoadData.Parameters.AddWithValue("@FirstName", "%" + textBox2.Text + "%");
                LoadData.Parameters.AddWithValue("@LastName", "%" + textBox2.Text + "%");
                DataTable EmployeeManagementData = new DataTable();
                SQLiteDataAdapter ReadDataFromDB = new SQLiteDataAdapter(LoadData);
                ReadDataFromDB.Fill(EmployeeManagementData);
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = EmployeeManagementData;
            }
            else
            {
                SQLiteConnection DBpath = new SQLiteConnection(@"Data Source=" + Application.StartupPath + "\\Database.db");
                string CommnadstrLoadData = "SELECT * FROM Student_Information_Kanoon WHERE last_name like @LastName or first_name like @FirstName; ";

                SQLiteCommand LoadData = new SQLiteCommand(CommnadstrLoadData, DBpath);
                LoadData.Parameters.AddWithValue("@FirstName", "%" + textBox2.Text + "%");
                LoadData.Parameters.AddWithValue("@LastName", "%" + textBox2.Text + "%");
                DataTable EmployeeManagementData = new DataTable();
                SQLiteDataAdapter ReadDataFromDB = new SQLiteDataAdapter(LoadData);
                ReadDataFromDB.Fill(EmployeeManagementData);
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = EmployeeManagementData;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SQLiteConnection DBpath = new SQLiteConnection(@"Data Source=" + Application.StartupPath + "\\Database.db");

            if (radioButton1.Checked)
            {
                var result = MessageBox.Show("آیا میخواهید اطلاعات دانش آموز مورد نظر را حذف کنید؟؟", "هشدار", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {

                    string CommnadstrDeleteStudent = "DELETE FROM Student_Information_Sanjesh WHERE person_id =@PersonID";
                    SQLiteCommand DeleteStudent = new SQLiteCommand(CommnadstrDeleteStudent, DBpath);

                    DeleteStudent.Parameters.AddWithValue("@PersonID", dataGridView1.CurrentRow.Cells["person_id"].Value);

                    DBpath.Open();
                    DeleteStudent.ExecuteNonQuery();
                    DBpath.Close();


                }
                Fill_dataGridView1_Sanjesh();
            }
            else
            {
                var result = MessageBox.Show("آیا میخواهید اطلاعات دانش آموز مورد نظر را حذف کنید؟؟", "هشدار", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {

                    string CommnadstrDeleteStudent = "DELETE FROM Student_Information_Kanoon WHERE person_id =@PersonID";
                    SQLiteCommand DeleteStudent = new SQLiteCommand(CommnadstrDeleteStudent, DBpath);

                    DeleteStudent.Parameters.AddWithValue("@PersonID", dataGridView1.CurrentRow.Cells["person_id"].Value);

                    DBpath.Open();
                    DeleteStudent.ExecuteNonQuery();
                    DBpath.Close();


                }
                Fill_dataGridView1_Kanoon();
            }
        }

        private string Solve_Captcha()
        {
            string captcha_text;

            for (; ; )
            {
                string captcha_link = webBrowser1.Document.GetElementsByTagName("img")[0].GetAttribute("src");
                System.Net.WebRequest request =
                System.Net.WebRequest.Create(captcha_link);
                System.Net.WebRequest.Create(captcha_link);
                System.Net.WebResponse response = request.GetResponse();
                System.IO.Stream responseStream =
                response.GetResponseStream();
                Bitmap img = new Bitmap(responseStream);

                for (int i = 0; i < img.Width; i++)
                {
                    for (int j = 0; j < img.Height; j++)
                    {
                        if (Convert.ToInt32(img.GetPixel(i, j).ToArgb()) >= Convert.ToInt32(Color.FromArgb(152, 167, 201).ToArgb())) img.SetPixel(i, j, Color.White);
                    }
                }

                TesseractEngine engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
                Page page = engine.Process(img, PageSegMode.Auto);
                captcha_text = page.GetText();
                if (Captcha.Captcha_Validation(captcha_text))
                {
                    break;
                }
            }


            return captcha_text;
        }

        // for deleting cockie
        private const int INTERNET_OPTION_END_BROWSER_SESSION = 42;

        [DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int lpdwBufferLength);
        static List<string> sanjesh_link = new List<string>();
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (comboBox1.Text== "")
                {
                    MessageBox.Show(".لطفا از بالای صفحه یک آزمون را انتخاب فرمایید");
                }
                else
                {
                    webBrowser1.Navigate(sanjesh_link[comboBox1.SelectedIndex]);
                    webBrowser1.Show();
                    while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                    {
                        Application.DoEvents();
                    }

                    string captcha_text = Solve_Captcha();

                    if (webBrowser1.Document != null)
                    {
                        webBrowser1.Document.GetElementById("name").SetAttribute("value", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                        webBrowser1.Document.GetElementsByTagName("input").GetElementsByName("id_number")[1].SetAttribute("value", dataGridView1.CurrentRow.Cells[3].Value.ToString());
                        webBrowser1.Document.GetElementsByTagName("input").GetElementsByName("sal_tav")[1].SetAttribute("value", dataGridView1.CurrentRow.Cells[4].Value.ToString());
                        webBrowser1.Document.GetElementsByTagName("input").GetElementsByName("captcha")[1].SetAttribute("value", captcha_text);
                    }

                    webBrowser1.Document.GetElementById("submit23").InvokeMember("Click");

                }
            }
            else
            {

                InternetSetOption(IntPtr.Zero, INTERNET_OPTION_END_BROWSER_SESSION, IntPtr.Zero, 0);
                webBrowser1.Navigate("http://www.kanoon.ir/Account/Student");

                while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                }


                webBrowser1.Document.GetElementById("Counter").SetAttribute("value", dataGridView1.CurrentRow.Cells[5].Value.ToString());
                webBrowser1.Document.GetElementById("Password").SetAttribute("value", dataGridView1.CurrentRow.Cells[3].Value.ToString());


                webBrowser1.Document.GetElementById("submit").InvokeMember("Click");

                System.Threading.Thread.Sleep(1000);

                webBrowser1.Navigate("http://www.kanoon.ir/Student/WorkBook/WorkBookProject");

            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Columns["counter"].Visible = true;
            dataGridView1.Columns["birth_year"].Visible = false;
            Fill_dataGridView1_Kanoon();
            comboBox1.Hide();
        }
        static bool do_this_once = true;

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Columns["counter"].Visible = false;
            dataGridView1.Columns["birth_year"].Visible = true;
            Fill_dataGridView1_Sanjesh();
            comboBox1.Show();

            if (do_this_once)
            {
                webBrowser1.Navigate("sanjeshserv.ir/karnameh.aspx");
                
                while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                {
                    Application.DoEvents();
                    if (webBrowser1.DocumentText.Contains("کارنامه نهایی"))
                    {
                        break;
                    }
                }
                MessageBox.Show("لیست آزمون ها با موفقیت بارگذاری شد");
                if (webBrowser1.Document != null)
                {
                    
                    HtmlElementCollection elems = webBrowser1.Document.GetElementsByTagName("a");
                    foreach (HtmlElement elem in elems)
                    {
                        String nameStr = elem.GetAttribute("href");
                        if (nameStr != null && nameStr.Length != 0)
                        {
                            string b = elem.InnerText;
                            if (b == null)
                            {
                                continue;
                            }
                            else
                            {
                                if (b.Contains("کارنامه نهایی آزمون"))
                                {

                                    comboBox1.Items.Add(b);
                                    sanjesh_link.Add(nameStr);

                                }
                            }

                        }
                    }
                }

                do_this_once = false;

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
            
