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
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }
    }
}
