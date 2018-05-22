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
    public partial class AddComp : Form
    {
        public SqlConnection con = new SqlConnection(@"Data Source=ROMANOV\SQLEXPRESS;Initial Catalog=CompTech;Integrated Security=True");
        
        public AddComp()
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
                SqlCommand cmd = new SqlCommand("INSERT INTO Компьютер(Инвентарный_номер, Материнская_плата, Процессор" +
                    ", Оперативная_память, Видеокарта, Звуковая_карта, Жёсткий_диск, Статус) VALUES(@Инвентарный_номер, @Материнская_плата, @Процессор" +
                    ", @Оперативная_память, @Видеокарта, @Звуковая_карта, @Жёсткий_диск, @Статус)", con);
                cmd.Parameters.AddWithValue("@Инвентарный_номер", textBox1.Text);
                cmd.Parameters.AddWithValue("@Материнская_плата", textBox2.Text);
                cmd.Parameters.AddWithValue("@Процессор", textBox3.Text);
                cmd.Parameters.AddWithValue("@Оперативная_память", textBox4.Text);
                cmd.Parameters.AddWithValue("@Видеокарта", textBox5.Text);
                cmd.Parameters.AddWithValue("@Звуковая_карта", textBox6.Text);
                cmd.Parameters.AddWithValue("@Жёсткий_диск", textBox7.Text);
                cmd.Parameters.AddWithValue("@Статус", textBox8.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Компьютер успешно добавлен!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Text = textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = textBox8.Text = "";
            }
        }

        private void AddComp_Shown(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
