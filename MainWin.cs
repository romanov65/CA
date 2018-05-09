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
    public partial class MainWin : Form
    {
        public MainWin(string who)
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = "Вы вошли как " + who;
            if (who == "Администратор                 ")
            {
                панельАдминистратораToolStripMenuItem.Visible = true;
            }
            //Запрос и подключение к БД
            var select = "SELECT Логин AS ыва FROM Пользователи";

            SqlConnection con = new SqlConnection(@"Data Source=ROMANOV;Initial Catalog=CompTech;Integrated Security=True");
            SqlDataAdapter dataAdapter = new SqlDataAdapter(select, con);
            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
            //
        }

        private void панельАдминистратораToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdminPanel adminPanel = new AdminPanel();
            adminPanel.Show();
        }
    }
}
