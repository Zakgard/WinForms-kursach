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
using System.Text.RegularExpressions;


namespace Курсовая_работа__ООАиП
{
    
    public partial class Form1 : Form
    {
        private User _user=new User();
        private SQL_request _sqlRequest = new SQL_request();

        private bool _isNullValue=false;

        private Regex _numberCheck = new Regex(@"[\d]");
        private Regex _specialCheck = new Regex(@"[!@#$%^]");
        private Regex _letterCheck = new Regex(@"[qwertyuiopasdfghjklzxcvbnm]");
       
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if(!CheckForNullValues(textBox1.Text, textBox1.Text))
            {

                SQLREquesForLoggingIn(textBox1.Text, textBox2.Text, 0);
            }
            else
            {
                _isNullValue=false ;
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
                 _isNullValue=true;
            }
            return _isNullValue;               
        }

        void SQLREquesForLoggingIn(string login, string password, int indexOfRequest)
        {
            string dbPassword="";
            
            string dbRole="";

            SqlDataReader reader;

            using (SqlConnection conn = new SqlConnection(_sqlRequest.path))
            {
                conn.Open();
                SqlCommand command= new SqlCommand(_sqlRequest.listOfRequest[indexOfRequest], conn);
                SqlParameter loginParameter= new SqlParameter("@login", login);
                command.Parameters.Add(loginParameter);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    dbPassword = Convert.ToString(reader.GetValue(0));
                    dbRole = Convert.ToString(reader.GetValue(1));
                }

                if (reader.HasRows)
                {
                    if (password != dbPassword)
                        MessageBox.Show("Вы ввели неверный пароль!", "Ошибка");
                    else
                    {
                        Form2 form2 = new Form2(login, password, dbRole);
                        form2.ShowDialog();
                    }
                }
                else
                    MessageBox.Show("Пользователь не найден!", "Ошибка");
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!CheckForNullValues(textBox3.Text, textBox4.Text) && PasswordCheck(textBox4.Text))
            {
                
                SQLREquesForLoggingIn(textBox3.Text, textBox4.Text, 1);
            }
            else
            {
                _isNullValue = false;
            }
        }

        private bool PasswordCheck(string password)
        {
            Match m1=_numberCheck.Match(password);
            Match m2=_letterCheck.Match(password);
            Match M3=_specialCheck.Match(password);
            if (m1.Success && m2.Success && M3.Success)
                return true;
            else
            {
                MessageBox.Show("Пароль не удовлетворяет требованиям: минимум 1 спец. символ, минимум 1 прописная буква, мимимум одня цифра!" , "Ошибка");
                return false;
            }
           
        }

        void RegisterInUserDatabase(string login, string password, int indexOfRequest)
        {
            string tempLoginForCheck = "";
            int id = 0;

            SqlDataReader loginReader;
            SqlDataReader idReader;

            using(SqlConnection conn = new SqlConnection(_sqlRequest.path))
            {
                conn.Open();
            }
        }
    }
}
