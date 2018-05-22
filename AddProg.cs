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

namespace CA
{
    public partial class AddProg : Form
    {
        public SqlConnection con = new SqlConnection(@"Data Source=ROMANOV\SQLEXPRESS;Initial Catalog=CompTech;Integrated Security=True");

        public AddProg()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Пожалуйста, заполните пустые поля!");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO ПрограммноеОбеспечение (Код_компьютера, Название, Версия" +
                    ", Лицензионный_ключ, Описание, Разработчик) VALUES(@Код_компьютера, @Название, @Версия" +
                    ", @Лицензионный_ключ, @Описание, @Разработчик)", con);
                cmd.Parameters.AddWithValue("@Код_компьютера", textBox1.Text);
                cmd.Parameters.AddWithValue("@Название", textBox2.Text);
                cmd.Parameters.AddWithValue("@Версия", textBox3.Text);
                cmd.Parameters.AddWithValue("@Лицензионный_ключ", textBox4.Text);
                cmd.Parameters.AddWithValue("@Описание", textBox5.Text);
                cmd.Parameters.AddWithValue("@Разработчик", textBox6.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("ПО успешно добавлено!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void AddProg_Shown(object sender, EventArgs e)
        {
            try
            {
                con.Open();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
            }
        }
    }
}
