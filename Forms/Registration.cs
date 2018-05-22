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
    public partial class Registration : Form
    {
        public SqlConnection con = new SqlConnection(@"Data Source=ROMANOV\SQLEXPRESS;Initial Catalog=CompTech;Integrated Security=True");

        public Registration()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Пожалуйста, заполните пустые поля!");
            }
            else
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Пользователи(Логин, Пароль) VALUES(@Логин, @Пароль)", con);
                cmd.Parameters.AddWithValue("@Логин", textBox1.Text);
                cmd.Parameters.AddWithValue("@Пароль", textBox2.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Пользователь успешно добавлен!");
                Clear();
            }
        }

        void Clear()
        {
            textBox1.Text = textBox2.Text = "";
        }

        private void Registration_Shown(object sender, EventArgs e)
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
