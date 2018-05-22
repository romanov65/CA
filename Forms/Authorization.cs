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
using CA.Forms;
using BEL;
using BAL;

namespace CA
{
    public partial class Authorization : Form
    {
        public Information info = new Information();
        public Operations opr = new Operations();
        public string Роль;

        DataTable dt = new DataTable();

        public Authorization()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            info.Логин = textBox1.Text;
            info.Пароль = textBox2.Text;
            dt = opr.Логин(info);

            if (dt.Rows.Count > 0)
            {
                Роль = dt.Rows[0][4].ToString().Trim();
                if (Роль == "Администратор")
                {
                    this.Hide();
                    AdminPanel winAdmin = new AdminPanel();
                    winAdmin.Show();
                }
                else if (Роль == "Пользователь")
                {
                    this.Hide();
                    MainWinUser winUser = new MainWinUser();
                    winUser.Show();
                }
                else if (Роль == "ТехОтдел")
                {
                    this.Hide();
                    MainWinTech winTech = new MainWinTech();
                    winTech.Show();
                }
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = textBox2.Text = "";
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Registration opreg = new Registration();
            opreg.Show();
        }
    }
}
