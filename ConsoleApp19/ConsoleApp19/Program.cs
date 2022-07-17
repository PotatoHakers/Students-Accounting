using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ConsoleApp19
{
    class Program
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        private static SqlConnection sqlConnection = null;

        static void Main(string[] args)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            Console.WriteLine("******************************\nУчет студентов\n******************************");

            SqlDataReader sqlDataReader = null;

            string command = string.Empty;

            try
            {
                while (true)
                {
                    Console.Write("Введите команду:");
                    command = Console.ReadLine();

                    if (command.ToLower().Equals("exit"))
                    {
                        if (sqlConnection.State == ConnectionState.Open)
                        {
                            sqlConnection.Close();
                        }

                        if (sqlDataReader != null)
                        {
                            sqlDataReader.Close();
                        }

                        break;
                    }

                    SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);

                    switch (command.Split(' ')[0].ToLower())
                    {
                        case "select":

                            sqlDataReader = sqlCommand.ExecuteReader();

                            while (sqlDataReader.Read())
                            {
                                //Select*from [Students_Reg]
                                Console.WriteLine("\n");
                                Console.WriteLine($"{sqlDataReader["FIO"]} {sqlDataReader["Birthday"]} {sqlDataReader["Faculty"]} {sqlDataReader["Course"]} {sqlDataReader["Group_Number"]}");
                                Console.WriteLine("\n");
                            }

                            if (sqlDataReader != null)
                            {
                                sqlDataReader.Close();
                            }
                            break;


                        case "insert":
                            //Insert into Students_Reg (Нужное поле, нужное поле...) values (N'значения', 'дата вида - 01/01/1000'...)
                            Console.WriteLine($"Добавлен студент: {sqlCommand.ExecuteNonQuery()}");
                            break;


                        case "update":
                            //Update Students_Reg set (Нужное поле) = (Новое значение)
                            Console.WriteLine($"Обновлено: {sqlCommand.ExecuteNonQuery()}");

                            break;

                        case "delete":
                            //Delete from Students_Reg 
                            Console.WriteLine($"Удален студент: {sqlCommand.ExecuteNonQuery()}");
                            break;

                        default:
                            Console.WriteLine("Ошибка");
                            break;
                    }

                }
            }
            catch (Exception)
            {

                Console.WriteLine("Ошибка, что-то неверно");
            }
            
            
           
        }
    }
}