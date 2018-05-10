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
    public partial class AdminPanel : Form
    {
        public SqlConnection con = new SqlConnection(@"Data Source=ROMANOV\SQLEXPRESS;Initial Catalog=CompTech;Integrated Security=True");

        public AdminPanel()
        {
            InitializeComponent();
        }

        private void FillTable()
        {
            listView1.Items.Clear();
            SqlCommand cm = new SqlCommand("SELECT * FROM Пользователи ORDER BY Логин", con);
            try
            {
                SqlDataReader dr = cm.ExecuteReader();

                while(dr.Read())
                {
                    ListViewItem it = new ListViewItem(dr["Логин"].ToString());
                    it.SubItems.Add(dr["Пароль"].ToString());
                    it.SubItems.Add(dr["Роль"].ToString());
                    listView1.Items.Add(it);
                }
                dr.Close();
                dr.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AdminPanel_Shown(object sender, EventArgs e)
        {
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
                    
                    SqlCommand cmd = new SqlCommand("INSERT INTO Пользователи(Логин, Пароль, Роль) VALUES(@Логин, @Пароль, @Роль)", con);
                    cmd.Parameters.AddWithValue("@Логин", textBox1.Text);
                    cmd.Parameters.AddWithValue("@Пароль", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Роль", textBox3.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Пользователь успешно добавлен!");
                    Clear();
            }
        }

        void Clear()
        {
            textBox1.Text = textBox2.Text = textBox3.Text = "";
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count > 0)
            {
                button4.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SqlCommand cm = new SqlCommand("DELETE FROM Пользователи WHERE Логин = @Логин", con);
                cm.Parameters.AddWithValue("@Логин", listView1.SelectedItems[0].Text);

                try
                {
                    cm.ExecuteNonQuery();
                    FillTable();
                    button4.Enabled = false;
                    MessageBox.Show("Пользователь успешно удалён!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            FillTable();
        }
    }
}
