using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Text.RegularExpressions;


namespace Курсовая_работа__ООАиП
{
    
    public class SQL_request
    {
        public string path = @"Server=localhost\SQLEXPRESS;Database=EBI-212;Trusted_Connection=True;";
        public string[] listOfRequest= new string[]
        {
            "SELECT password, role FROM Users WHERE login = @login",
            $"SELECT login FROM [EBI-212].[dbo].[users] WHERE @userName=login",
            $"SELECT TOP 1 id FROM [EBI-212].[dbo].[users] ORDER  BY id DESC",
            //$"INSERT INTO [EBI-212].[dbo].[users](id, login, password, role)VALUES({id}, '{userName}', '{password}', 'guest') "
        };
        public SqlParameter sqlParameter;
        public SqlCommand sqlCommand=new SqlCommand();
        


    };

    public class User
    {
        public int id;
        public string userName;
        public string password;
    }
}
