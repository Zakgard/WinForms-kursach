using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Курсовая_работа__ООАиП
{
    public partial class ShowStudentsForm : Form
    {
        private int _indexOfEnteredCellsRow=0;
        private SqlConnection _sqlConnection = null;
        private SqlDataAdapter _sqlDataAdapter = null;
        private DataTable _dataTable = null;
        
        public ShowStudentsForm()
        {
            InitializeComponent();
        }

        private void ShowStudentsForm_Load(object sender, EventArgs e)
        {
            GetDataFromSQLTable();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != _dataTable.Rows.Count)
            {
                 int indexOfRow=Convert.ToInt32(e.RowIndex.ToString()); 
                _indexOfEnteredCellsRow=indexOfRow;
                 FillTheBoxes(indexOfRow);
            }
            
            
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

        private void button3_Click(object sender, EventArgs e)
        {
            int studNum = Convert.ToInt32(textBox5.Text);
            DeleteStudentFormSQLTable(studNum);
        }

        private void DeleteStudentFormSQLTable(int studNum)
        {
            using(SqlConnection connection = new SqlConnection(SQL_request.path))
            {
                try
                {
                    string query = "DELETE From [EBI-212].dbo.student WHERE studNum=@StudNum";
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlParameter studNumParameter = new SqlParameter("@StudNum", studNum);
                    cmd.Parameters.Add(studNumParameter);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Успешное удаление записи о студенте!", "Результат операции");                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Уведомление об ошибке");
                }
                connection.Close();                
            }
            GetDataFromSQLTable();

        }

        private void GetDataFromSQLTable()
        {
            _sqlConnection = new SqlConnection(SQL_request.path);
            _sqlConnection.Open();
            _sqlDataAdapter = new SqlDataAdapter("SELECT * FROM student", _sqlConnection);
            _sqlConnection.Close();
            _dataTable = new DataTable();
            _sqlDataAdapter.Fill(_dataTable);
            dataGridView1.DataSource = _dataTable;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (IsStudentDataChanged(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox6.Text, comboBox1.Text, comboBox2.Text, dateTimePicker1.Value))
                UpdateStudentDataInSQLTable(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox6.Text, comboBox1.Text, comboBox2.Text, dateTimePicker1.Value, Convert.ToInt32(textBox5.Text));
            else
                MessageBox.Show("Обновлять нечего, изменений не обнаружено!");
        }

        private bool IsStudentDataChanged(string secondName, string firstName, string thirdName, string other, string debts, string payMethod, string gender, DateTime dateTime)
        {
            StringBuilder stringBuilder = new StringBuilder("");
            bool isStudentDataChanged = false;
            
            if (secondName != dataGridView1.Rows[_indexOfEnteredCellsRow].Cells[0].Value.ToString())
            {
                isStudentDataChanged=true;
                stringBuilder.AppendLine($"Изменена фамилия! {dataGridView1.Rows[_indexOfEnteredCellsRow].Cells[0].Value.ToString()} => {secondName}");               
            }
                
            if(firstName != dataGridView1.Rows[_indexOfEnteredCellsRow].Cells[1].Value.ToString())
            {
                isStudentDataChanged = true;
                stringBuilder.AppendLine($"Изменено имя! {dataGridView1.Rows[_indexOfEnteredCellsRow].Cells[1].Value.ToString()} => {firstName}");
            }
                
            if(thirdName != dataGridView1.Rows[_indexOfEnteredCellsRow].Cells[2].Value.ToString())
            {
                isStudentDataChanged = true;
                stringBuilder.AppendLine($"Изменено отчество! {dataGridView1.Rows[_indexOfEnteredCellsRow].Cells[2].Value.ToString()} => {thirdName}");
            }
                
            if(other != dataGridView1.Rows[_indexOfEnteredCellsRow].Cells[10].Value.ToString())
            {
                isStudentDataChanged = true;
                stringBuilder.AppendLine($"Изменено примечание! {dataGridView1.Rows[_indexOfEnteredCellsRow].Cells[10].Value.ToString()} => {other}");
            }
                
            if(debts != dataGridView1.Rows[_indexOfEnteredCellsRow].Cells[9].Value.ToString())
            {
                isStudentDataChanged = true;
                stringBuilder.AppendLine($"Изменено кол-во задолженностей! {dataGridView1.Rows[_indexOfEnteredCellsRow].Cells[9].Value.ToString()} => {debts}");
            }
               
            if(payMethod != dataGridView1.Rows[_indexOfEnteredCellsRow].Cells[8].Value.ToString())
            {
                isStudentDataChanged = true;
                stringBuilder.AppendLine($"Изменена основа обучения! {dataGridView1.Rows[_indexOfEnteredCellsRow].Cells[8].Value.ToString()} => {payMethod}");
            }
                
            if(gender!= dataGridView1.Rows[_indexOfEnteredCellsRow].Cells[6].Value.ToString())
            {
                isStudentDataChanged = true;
                stringBuilder.AppendLine($"Изменен пол! {dataGridView1.Rows[_indexOfEnteredCellsRow].Cells[6].Value.ToString()} => {gender}");
            }
                
            if(dateTime.Day!= Convert.ToInt32(dataGridView1.Rows[_indexOfEnteredCellsRow].Cells[3].Value.ToString()))
            {
                isStudentDataChanged = true;
                stringBuilder.AppendLine($"Изменен день рождения! {dataGridView1.Rows[_indexOfEnteredCellsRow].Cells[3].Value.ToString()} => {dateTime.Day}");
            }
                
            if (dateTime.Month != Convert.ToInt32(dataGridView1.Rows[_indexOfEnteredCellsRow].Cells[4].Value.ToString()))
            {
                isStudentDataChanged = true;
                stringBuilder.AppendLine($"Изменен месяц рождения! {dataGridView1.Rows[_indexOfEnteredCellsRow].Cells[4].Value.ToString()} => {dateTime.Month}");
            }
                
            if (dateTime.Year != Convert.ToInt32(dataGridView1.Rows[_indexOfEnteredCellsRow].Cells[5].Value.ToString()))
            {
                isStudentDataChanged = true;
                stringBuilder.AppendLine($"Изменен год рождения! {dataGridView1.Rows[_indexOfEnteredCellsRow].Cells[5].Value.ToString()} => {dateTime.Year}");
            }

            if (isStudentDataChanged == true)
                MessageBox.Show(stringBuilder.ToString(), "Были обнаружены следующие изменения:");
            else
                MessageBox.Show("Изменений не обнаружено!");
            return isStudentDataChanged;
        }
       
        private void UpdateStudentDataInSQLTable(string secondName, string firstName, string thirdName, string other, string debts, string payMethod, string gender, DateTime dateTime, int studNum)
        {
            try
            {
               using (SqlConnection connection = new SqlConnection(SQL_request.path))
               {                     
                    string request =$"UPDATE [EBI-212].[dbo].[student] SET surName=N'{secondName}', name=N'{firstName}', secondName=N'{thirdName}', birthD={dateTime.Day}, birthM={dateTime.Month}, birthY={dateTime.Year}, gender=N'{gender}', payMet=N'{payMethod}', debts={Convert.ToInt32(debts)}, other=N'{other}' WHERE studNum={studNum}";                
                    connection.Open();               
                    SqlCommand cmd = new SqlCommand(request, connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();               
                    MessageBox.Show($"Данные о студенте успешно обновлены!");               
               }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            GetDataFromSQLTable();



        }
    }
}
