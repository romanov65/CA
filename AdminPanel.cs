using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace CA
{
    public partial class AdminPanel : Form
    {
        public AdminPanel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Пожалуйста, заполните пустые поля!");
            }
            else
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=ROMANOV;Initial Catalog=CompTech;Integrated Security=True"))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO Пользователи(Логин, Пароль, Роль) VALUES(@Логин, @Пароль, @Роль)", con);
                    cmd.Parameters.AddWithValue("@Логин", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Пароль", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Роль", textBox3.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Пользователь успешно добавлен!");
                    Clear();
                }
            }
        }
        void Clear()
        {
            textBox1.Text = textBox2.Text = textBox3.Text = "";
        }
    }
}
