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
    public partial class Auth : Form
    {
        public Auth()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=ROMANOV\SQLEXPRESS;Initial Catalog=CompTech;Integrated Security=True");
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select Роль From Пользователи where Логин ='" + textBox1.Text + "' and Пароль ='" + textBox2.Text + "'", con);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("Заполните пустые поля");
                }
                else
                {
                    string ID = dt.Rows[0][0].ToString();
                    this.Hide();
                    MainWin ss = new MainWin(dt.Rows[0][0].ToString());
                    ss.Show();
                }
            }
            else
            {
                MessageBox.Show("Неправильно введённые имя или пароль");
            }
        }
    }
}
