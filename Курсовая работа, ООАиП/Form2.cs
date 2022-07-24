using System;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace Курсовая_работа__ООАиП
{
    public partial class Form2 : Form
    {
        public Form2(string login, string password, string role)
        {
            InitializeComponent();
            this.Name = $"Пользователь: {login}, его роль: {role}";
            if (role == "guest")
            {
                button1.Enabled = false;                               
                button4.Enabled = false;
            }
            if (role == "normal")
                button1.Enabled = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (IsInputwithoutNulls() && IsStudentNotExists(Convert.ToInt32(textBox6.Text)))
                AddNewStudent(textBox1.Text, textBox2.Text, textBox3.Text, dateTimePicker1.Value.Day, dateTimePicker1.Value.Month,dateTimePicker1.Value.Year , comboBox1.Text, comboBox2.Text, Convert.ToInt32(textBox6.Text), Convert.ToInt32(textBox7.Text), textBox8.Text);         
        }

        public static void AddNewStudent(string firstName, string secondName, string thirdName, int dayOfBirth, int monthOfBirth, int yearOfBirth, string sex, string payMethod, int studNUm, int debtsAmount, string other)
        {
            string expression = $"INSERT INTO [EBI-212].[dbo].[student](surName, name, secondName, birthD, birthM, birthY, gender, studNum, payMet, debts, other) " +
                $"VALUES (N'{firstName}', N'{secondName}', N'{thirdName}', N'{dayOfBirth}', N'{monthOfBirth}', N'{yearOfBirth}', N'{sex}', N'{studNUm}',N'{payMethod}', N'{debtsAmount}', N'{other}');";
            SqlConnection connection = new SqlConnection(SQL_request.path);
            connection.Open();

            SqlCommand command = new SqlCommand(expression, connection);

            if (command.ExecuteNonQuery().ToString() == Convert.ToString(1))
                MessageBox.Show("Данные о студенте внесены!");



        }

        private bool IsInputwithoutNulls()
        {            
            StringBuilder sb= new StringBuilder("", 50);
            bool isInputwithoutNulls=true;
            if (textBox1.Text == "")
            {
                isInputwithoutNulls = false;
                sb.AppendLine("Вы не ввели фамилию студента!");
            }else if (!Regex.IsMatch(textBox1.Text, @"^[\p{L}]+$"))
            {
                sb.AppendLine("В поле фамилия допускаются только буквы!");
            }
                
            if (textBox2.Text == "")
            {
                isInputwithoutNulls = false;
                sb.AppendLine("Вы не ввели имя студента!");
            }
            else if (!Regex.IsMatch(textBox2.Text, @"^[\p{L}]+$"))
            {
                sb.AppendLine("В поле имя допускаются только буквы!");
            }

            if (textBox3.Text == "")
            {
                isInputwithoutNulls = false;
                sb.AppendLine("Вы не ввели отчество студента!");
            }
            else if (!Regex.IsMatch(textBox3.Text, @"^[\p{L}]+$"))
            {
                sb.AppendLine("В поле отчество допускаются только буквы!");
            }

            if (dateTimePicker1.Text=="")
            {
                isInputwithoutNulls = false;
                sb.AppendLine("Вы не выбрали дату рождения студента!");
            }
            

            if (comboBox1.Text == "")
            {
                isInputwithoutNulls = false;
                sb.AppendLine("Вы не выбрали пол студента!");
            }
            

            if (comboBox2.Text == "")
            {
                isInputwithoutNulls = false;
                sb.AppendLine("Вы не выбрали основу обучения!");
            }

            
            if (textBox6.Text == "")
            {
                isInputwithoutNulls = false;
                sb.AppendLine("Вы не ввели номер студенческого!");
            }
            else if (!Regex.IsMatch(textBox6.Text, @"^[\p{N}]+$"))
            {
                sb.AppendLine("В поле номер студенческого допускаются только числа!");
                isInputwithoutNulls = false;
            }

            if(textBox7.Text == "")
            {
                textBox7.Text = "0";
            }else if(!Regex.IsMatch(textBox7.Text, @"^[\p{N}]+$"))
            {
                sb.AppendLine("В поле количество задолженностей допускаются только числа!");
                isInputwithoutNulls=false;
            }

            if(textBox8.Text=="")
                textBox8.Text = "Примечания отсутствуют";

            if (isInputwithoutNulls == false)
                MessageBox.Show(sb.ToString(), "Были обнаружены следующие ошибки:");
            return isInputwithoutNulls;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowStudentsForm showStudentsForm = new ShowStudentsForm();
            showStudentsForm.ShowDialog();
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            ShowingUserListForm showingUserListForm = new ShowingUserListForm();
            showingUserListForm.ShowDialog();
        }

      

        private bool IsStudentNotExists(int id)
        {
            int tempNumberFromSQLTable=0;
            SqlDataReader sqlDataReader;
            using(SqlConnection connection=new SqlConnection(SQL_request.path))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand(SQL_request.listOfRequest[3], connection);
                SqlParameter numParam = new SqlParameter("@studNum", id);
                sqlCommand.Parameters.Add(numParam);
                sqlDataReader = sqlCommand.ExecuteReader();
                while(sqlDataReader.Read())
                    tempNumberFromSQLTable=sqlDataReader.GetInt32(0);
                if (tempNumberFromSQLTable != 0)
                {
                    MessageBox.Show("Студент с данным номер студенчесского билет уже существует!", "Ошибка!");
                    return false;
                }else
                    return true;
                    

            }
        }
    }
}
