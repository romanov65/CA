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
    public partial class MainWinTech : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=ROMANOV\SQLEXPRESS;Initial Catalog=CompTech;Integrated Security=True");

        public MainWinTech()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = "Добро пожаловать ";
        }

        private void ПанельАдминистратораToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdminPanel adminPanel = new AdminPanel();
            adminPanel.Show();
        }

        private void ВыходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void СписаныеКомпьютерыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WrittenOffTech spis = new WrittenOffTech();
            spis.Show();
        }

        private void ОПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Программа: Учёт компьютерной техники\n" + "Разработчик: Москаленко Александр\n" + "2018 ©", "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void сменитьПользователяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Authorization backtoauth = new Authorization();
            backtoauth.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            string id_pc = Convert.ToString(listView1.FocusedItem.SubItems[0].Text);

            SqlDataAdapter ada = new SqlDataAdapter("SELECT * FROM ПрограммноеОбеспечение WHERE Код_компьютера = " + id_pc, con);
            DataTable dt = new DataTable();
            ada.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ListViewItem listitem = new ListViewItem(dr["Название"].ToString());
                listitem.SubItems.Add(dr["Версия"].ToString());
                listitem.SubItems.Add(dr["Лицензионный_ключ"].ToString());
                listitem.SubItems.Add(dr["Описание"].ToString());
                listitem.SubItems.Add(dr["Разработчик"].ToString());
                listView2.Items.Add(listitem);
            }
        }

        private void MainWinTech_Shown(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            SqlDataAdapter ada = new SqlDataAdapter("select * from компьютер", con);
            DataTable dt = new DataTable();
            ada.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ListViewItem listitem = new ListViewItem(dr["Инвентарный_номер"].ToString());
                listitem.SubItems.Add(dr["Материнская_плата"].ToString());
                listitem.SubItems.Add(dr["Процессор"].ToString());
                listitem.SubItems.Add(dr["Оперативная_память"].ToString());
                listitem.SubItems.Add(dr["Видеокарта"].ToString());
                listitem.SubItems.Add(dr["Звуковая_карта"].ToString());
                listitem.SubItems.Add(dr["Жёсткий_диск"].ToString());
                listitem.SubItems.Add(dr["Статус"].ToString());
                listView1.Items.Add(listitem);
            }

            try
            {
                con.Open();
                FillTable();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.ExitThread();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddComp addComp = new AddComp();
            addComp.Show();
        }

        private void MainWinTech_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "compTechDataSet1.Заявление". При необходимости она может быть перемещена или удалена.
            this.заявлениеTableAdapter1.Fill(this.compTechDataSet1.Заявление);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            SqlDataAdapter ada = new SqlDataAdapter("select * from компьютер", con);
            DataTable dt = new DataTable();
            ada.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ListViewItem listitem = new ListViewItem(dr["Инвентарный_номер"].ToString());
                listitem.SubItems.Add(dr["Материнская_плата"].ToString());
                listitem.SubItems.Add(dr["Процессор"].ToString());
                listitem.SubItems.Add(dr["Оперативная_память"].ToString());
                listitem.SubItems.Add(dr["Видеокарта"].ToString());
                listitem.SubItems.Add(dr["Звуковая_карта"].ToString());
                listitem.SubItems.Add(dr["Жёсткий_диск"].ToString());
                listitem.SubItems.Add(dr["Статус"].ToString());
                listView1.Items.Add(listitem);
            }
        }

        private void FillTable()
        {
            listView1.Items.Clear();
            SqlDataAdapter ada = new SqlDataAdapter("select * from компьютер", con);
            DataTable dt = new DataTable();
            ada.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ListViewItem listitem = new ListViewItem(dr["Инвентарный_номер"].ToString());
                listitem.SubItems.Add(dr["Материнская_плата"].ToString());
                listitem.SubItems.Add(dr["Процессор"].ToString());
                listitem.SubItems.Add(dr["Оперативная_память"].ToString());
                listitem.SubItems.Add(dr["Видеокарта"].ToString());
                listitem.SubItems.Add(dr["Звуковая_карта"].ToString());
                listitem.SubItems.Add(dr["Жёсткий_диск"].ToString());
                listitem.SubItems.Add(dr["Статус"].ToString());
                listView1.Items.Add(listitem);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SqlCommand cm = new SqlCommand("DELETE FROM Компьютер WHERE Инвентарный_номер = @Инвентарный_номер", con);
                cm.Parameters.AddWithValue("@Инвентарный_номер  ", listView1.SelectedItems[0].Text);

                try
                {
                    cm.ExecuteNonQuery();
                    FillTable();
                    MessageBox.Show("Компьютер успешно удалён!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddProg addProg = new AddProg();
            addProg.Show();
        }
    }
}
