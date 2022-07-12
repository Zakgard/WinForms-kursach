namespace Курсовая_работа__ООАиП
{
    
    public class SQL_request
    {
        public static string path = @"Server=localhost\SQLEXPRESS;Database=EBI-212;Trusted_Connection=True;";
        public static string[] listOfRequest= new string[]
        {
            $"SELECT password, role FROM Users WHERE login = @login",
            $"SELECT login FROM [EBI-212].[dbo].[users] WHERE @userName=login",
            $"SELECT TOP 1 id FROM [EBI-212].[dbo].[users] ORDER  BY id DESC",
            $"SELECT studNum FROM student WHERE studNum=@studNum"
        };


        

    };

   
}
