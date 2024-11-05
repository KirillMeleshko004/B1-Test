namespace FileWorker.Services
{
    public class FileImportService
    {
        public FileImportService()
        {
            
        }
        public async Task ImportFiles()
        {
            // string connectionString = @"Server=localhost\SQLEXPRESS;Database=Test;Trusted_Connection=True;TrustServerCertificate=True;";
            // SqlConnection connection = new(connectionString);
            // try
            // {
            //     // Открываем подключение
            //     await connection.OpenAsync();
            //     Console.WriteLine("Подключение открыто");
            // }
            // catch (SqlException ex)
            // {
            //     Console.WriteLine(ex.Message);
            // }
            // finally
            // {
            //     // если подключение открыто
            //     if (connection.State == ConnectionState.Open)
            //     {
            //         // закрываем подключение
            //         await connection.CloseAsync();
            //         Console.WriteLine("Подключение закрыто...");
            //     }
            // }
        }
    }
}