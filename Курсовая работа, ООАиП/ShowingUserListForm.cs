using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Курсовая_работа__ООАиП
{
    public partial class ShowingUserListForm : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private DataTable dataTable = null;

        private bool _isNullValue;

        Form1 form1 = new Form1();
        public ShowingUserListForm()
        {
            InitializeComponent();
        }

        private void ShowingUserListForm_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(SQL_request.path);

            sqlConnection.Open();

            sqlDataAdapter = new SqlDataAdapter("SELECT * FROM Users", sqlConnection);

            dataTable = new DataTable();

            sqlDataAdapter.Fill(dataTable);

            dataGridView1.DataSource = dataTable;

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!CheckForNullValues(textBox1.Text, textBox2.Text) && PasswordCheck(this.textBox2.Text) && !form1.CheckIfUserExists(this.textBox1.Text, this.textBox2.Text, 1))
                form1.SQLRequestForRegistration(textBox1.Text, textBox2.Text, comboBox1.Text);
        }

        private bool PasswordCheck(string password)
        {

            if (Regex.IsMatch(password, @"[\p{N}]") && Regex.IsMatch(textBox2.Text, @"[\p{L}]") && textBox2.Text.Length >= 5 && Regex.IsMatch(textBox2.Text, @"[!@#$%^]"))
                return true;
            else
            {
                MessageBox.Show("Пароль не удовлетворяет требованиям: минимум 1 спец. символ, минимум 1 буква, мимимум одня цифра, минимум 5 знаков!", "Ошибка");
                return false;
            }


        }

        private bool CheckForNullValues(string login, string password)
        {
            if (login == "")
            {
                MessageBox.Show("Вы не ввели логин!", "Ошибка");
                _isNullValue = true;
            }

            if (password == "")
            {
                MessageBox.Show("Вы не ввели пароль!", "Ошибка");
                _isNullValue = true;
            }
            return _isNullValue;
        }
    }
}