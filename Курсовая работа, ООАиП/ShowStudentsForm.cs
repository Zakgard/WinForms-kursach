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

namespace Курсовая_работа__ООАиП
{
    public partial class ShowStudentsForm : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private DataTable dataTable = null;
        public ShowStudentsForm()
        {
            InitializeComponent();
        }

        private void ShowStudentsForm_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(SQL_request.path);

            sqlConnection.Open();

            sqlDataAdapter = new SqlDataAdapter("SELECT * FROM student", sqlConnection);

            dataTable = new DataTable();

            sqlDataAdapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;

            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indexOfRow=Convert.ToInt32(e.RowIndex.ToString());
            FillTheBoxes(indexOfRow);
        }

        private void FillTheBoxes(int indexOfRow)
        {
            textBox1.Text = dataGridView1.Rows[indexOfRow].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[indexOfRow].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[indexOfRow].Cells[2].Value.ToString();
            dateTimePicker1.Value = Convert.ToDateTime($"{dataGridView1.Rows[indexOfRow].Cells[3].Value.ToString()}.{dataGridView1.Rows[indexOfRow].Cells[4].Value.ToString()}.{dataGridView1.Rows[indexOfRow].Cells[5].Value.ToString()}");
            comboBox1.Text = dataGridView1.Rows[indexOfRow].Cells[8].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[indexOfRow].Cells[6].Value.ToString();
            textBox4.Text = dataGridView1.Rows[indexOfRow].Cells[10].Value.ToString();
            textBox5.Text = dataGridView1.Rows[indexOfRow].Cells[7].Value.ToString();
            textBox6.Text = dataGridView1.Rows[indexOfRow].Cells[9].Value.ToString();
        }
    }
}
