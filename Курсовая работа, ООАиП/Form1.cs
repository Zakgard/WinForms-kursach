using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;


namespace Курсовая_работа__ООАиП
{
    
    public partial class Form1 : Form
    {
        

        private bool _isNullValue=false;

        
       
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

        public void SQLREquesForLoggingIn(string login, string password, int indexOfRequest)
        {
            string dbPassword="";
            
            string dbRole="";

            SqlDataReader reader;

            using (SqlConnection conn = new SqlConnection(SQL_request.path))
            {
                conn.Open();
                SqlCommand command= new SqlCommand(SQL_request.listOfRequest[indexOfRequest], conn);
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
            if (!CheckForNullValues(textBox3.Text, textBox4.Text) && PasswordCheck(textBox4.Text) && !CheckIfUserExists(textBox3.Text, textBox4.Text, 1))
            {
                SQLRequestForRegistration(textBox3.Text, textBox4.Text, "guest");
            }
            else
            {
                _isNullValue = false;
            }
        }

        private bool PasswordCheck(string password)
        {
            
            if (Regex.IsMatch(password, @"[\p{N}]") && Regex.IsMatch(password, @"[\p{L}]") && password.Length>=5 && Regex.IsMatch(password, @"[!@#$%^]"))
                return true;
            else
            {
                MessageBox.Show("Пароль не удовлетворяет требованиям: минимум 1 спец. символ, минимум 1 буква, мимимум одня цифра, минимум 5 знаков!" , "Ошибка");
                return false;
            }
           
        }

        public bool CheckIfUserExists(string login, string password, int indexOfRequest)
        {
            string tempLoginForCheck = "";            
            SqlDataReader loginReader;           

            using(SqlConnection conn = new SqlConnection(SQL_request.path))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(SQL_request.listOfRequest[indexOfRequest], conn);
                SqlParameter loginParameter = new SqlParameter("@userName", login);
                command.Parameters.Add(loginParameter);
                loginReader=command.ExecuteReader();

                while(loginReader.Read())
                    tempLoginForCheck=loginReader.GetString(0);
                if (tempLoginForCheck != "")
                {
                    MessageBox.Show("Данный логин занят!");
                    conn.Close();
                    return true;
                }
                else
                {
                    return false; 
                    conn.Close();
                }
                    
                
            }
            
        }

        public void SQLRequestForRegistration(string login, string password, string role)
        {
            int id = GetLastIDOFUser();
            
            
            using (SqlConnection connection = new SqlConnection(SQL_request.path))
            {
                connection.Open();
                SqlCommand command= new SqlCommand($"INSERT INTO [EBI-212].[dbo].[users](id, login, password, role)VALUES({id}, '{login}', '{password}', '{role}') ", connection);
               

                
                if (command.ExecuteNonQuery().ToString() == Convert.ToString(1))
                    MessageBox.Show("Пользователь успешно зарегестрирован!");
            

            }
        }

        public int GetLastIDOFUser()
        {
            SqlDataReader idReader;
            int id=0;
            using(SqlConnection connection = new SqlConnection(SQL_request.path))
            {
                connection.Open();
                SqlCommand command1 = new SqlCommand(SQL_request.listOfRequest[2], connection);
                SqlParameter idParameter = new SqlParameter("@id", id);
                command1.Parameters.Add(idParameter);
                idReader = command1.ExecuteReader();
                while(idReader.Read())
                    id=idReader.GetInt32(0);
                id++;
            }
            return id;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
