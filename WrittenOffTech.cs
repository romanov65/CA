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
using System.Drawing.Imaging;
using System.Drawing.Printing;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;

namespace CA
{
    public partial class WrittenOffTech : Form
    {
        public SqlConnection con = new SqlConnection(@"Data Source=ROMANOV\SQLEXPRESS;Initial Catalog=CompTech;Integrated Security=True");

        public WrittenOffTech()
        {
            InitializeComponent();
        }

        private void FillTable()
        {
            listView1.Items.Clear();
            SqlCommand cm = new SqlCommand("SELECT * FROM Компьютер WHERE Списание = 1 ORDER BY Инвентарный_номер", con);
            try
            {
                SqlDataReader dr = cm.ExecuteReader();

                while (dr.Read())
                {
                    ListViewItem it = new ListViewItem(dr["Инвентарный_номер"].ToString());
                    it.SubItems.Add(dr["Материнская_плата"].ToString());
                    it.SubItems.Add(dr["Процессор"].ToString());
                    it.SubItems.Add(dr["Оперативная_память"].ToString());
                    it.SubItems.Add(dr["Видеокарта"].ToString());
                    it.SubItems.Add(dr["Звуковая_карта"].ToString());
                    it.SubItems.Add(dr["Жёсткий_диск"].ToString());
                    it.SubItems.Add(dr["Статус"].ToString());
                    listView1.Items.Add(it);
                }
                dr.Close();
                dr.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void WrittenOffTech_Shown(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                FillTable();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, System.Windows.Forms.Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Windows.Forms.Application.ExitThread();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ExportBtn(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Excel.Application app = new Excel.Application();
                    Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                    Worksheet ws = (Worksheet)app.ActiveSheet;
                    app.Visible = false;
                    ws.Cells[1, 1] = "Инвентарный номер";
                    ws.Cells[1, 2] = "Материнская плата";
                    ws.Cells[1, 3] = "Процессор";
                    ws.Cells[1, 4] = "Оперативная память";
                    ws.Cells[1, 5] = "Видеокарта";
                    ws.Cells[1, 6] = "Звуковая карта";
                    ws.Cells[1, 7] = "Жёсткий диск";
                    ws.Cells[1, 8] = "Статус";
                    int i = 2;
                    foreach(ListViewItem item in listView1.Items)
                    {
                        ws.Cells[i, 1] = item.SubItems[0].Text;
                        ws.Cells[i, 2] = item.SubItems[1].Text;
                        ws.Cells[i, 3] = item.SubItems[2].Text;
                        ws.Cells[i, 4] = item.SubItems[3].Text;
                        ws.Cells[i, 5] = item.SubItems[4].Text;
                        ws.Cells[i, 6] = item.SubItems[5].Text;
                        ws.Cells[i, 7] = item.SubItems[6].Text;
                        ws.Cells[i, 8] = item.SubItems[7].Text;
                        i++;
                    }
                    wb.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Экспорт данных в Excel успешно выполнен!", "Уведомление",MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                }
            }
        }
    }
}
