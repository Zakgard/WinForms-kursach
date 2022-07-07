using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                button2.Enabled = false;
                button4.Enabled = false;
            }
            if (role == "normal")
                button1.Enabled = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            dateTimePicker1.AllowDrop = false;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            CheckForNulls();
        }

        private bool CheckForNulls()
        {
            StringBuilder sb= new StringBuilder("", 50);
            bool isMistake=false;
            if (textBox1.Text == "")
            {
                isMistake = true;
                sb.AppendLine("Вы не ввели фамилию студента!");
            }else if (Convert.ToString(textBox1.Text.GetType()) == "System.String")
            {
                sb.AppendLine("В поле фамилия допускаются только буквы!");
            }
                
            if (textBox2.Text == "")
            {
                isMistake = true;
                sb.AppendLine("Вы не ввели имя студента!");
            }
            else if (Convert.ToString(textBox2.Text.GetType()) == "System.String")
            {
                sb.AppendLine("В поле имя допускаются только буквы!");
            }

            if (textBox3.Text == "")
            {
                isMistake = true;
                sb.AppendLine("Вы не ввели отчество студента!");
            }
            else if (Convert.ToString(textBox1.Text.GetType()) == "System.String")
            {
                sb.AppendLine("В поле отчество допускаются только буквы!");
            }

            if (dateTimePicker1.Text=="")
            {
                isMistake = true;
                sb.AppendLine("Вы не выбрали дату рождения студента!");
            }
            

            if (comboBox1.Text == "")
            {
                isMistake = true;
                sb.AppendLine("Вы не выбрали пол студента!");
            }
            

            if (comboBox2.Text == "")
            {
                isMistake = true;
                sb.AppendLine("Вы не выбрали основу обучения!");
            }
           

            if (textBox6.Text == "")
            {
                isMistake = true;
                sb.AppendLine("Вы не ввели номер студенческого!");
            }
            else if (Convert.ToString(textBox1.Text.GetType()) == "System.Int")
            {
                sb.AppendLine("В поле номер студенческого допускаются только цифры!");
            }

            if (isMistake == true)
                MessageBox.Show(sb.ToString(), "Были обнаружены следующие ошибки:");
            return isMistake;

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
