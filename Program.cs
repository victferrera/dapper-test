using System.Data.SqlClient;
using DapperSimpleTest.Models;
using Dapper;
using DapperSimpleTest.Data;
using System;
using System.Threading;
using System.Linq;
using System.Collections.Generic;

namespace DapperSimpleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            AppInit();
        }

        public static void AppInit()
        {
            System.Console.Clear();
            System.Console.WriteLine("Menu de opções");
            System.Console.WriteLine("======================================");
            System.Console.WriteLine("1 - Cadastro de usuário");
            System.Console.WriteLine("2 - Cadastro de Tarefa");
            System.Console.WriteLine("3 - Lista de usuários cadastrado");
            System.Console.WriteLine("4 - Lista de Tarefas cadastradas");
            System.Console.WriteLine("0 - Sair");


            var option = int.Parse(System.Console.ReadLine());

            while (option != 0)
            {

                if (option == 1)
                {
                    System.Console.Clear();
                    System.Console.Write("Informe o nome: ");
                    var name = System.Console.ReadLine();
                    System.Console.Write("Informe o email: ");
                    var email = System.Console.ReadLine();

                    var newUser = new User
                    {
                        Nome = name,
                        Email = email
                    };

                    CreateUser(newUser);
                }
                if (option == 2)
                {
                    System.Console.Clear();
                    System.Console.Write("Informe o título: ");
                    var title = System.Console.ReadLine();
                    System.Console.Write("Informe a descrição: ");
                    var description = System.Console.ReadLine();
                    System.Console.Write("Informe o Email do usuário: ");
                    var user = GetUser(System.Console.ReadLine());
                    var createAt = DateTime.UtcNow;

                    var newTask = new Task
                    {
                        Title = title,
                        Description = description,
                        CreateAt = createAt,
                        EndAt = null,
                        User = new User
                        {
                            Id = user.Id,
                            Nome = user.Nome,
                            Email = user.Email
                        }
                    };

                    CreateTask(newTask);
                }
                if (option == 3)
                {
                    System.Console.Clear();

                    var UsersList = GetUserList();

                    foreach (var usr in UsersList)
                    {
                        System.Console.WriteLine($"Id : {usr.Id}, Nome: {usr.Nome}, Email: {usr.Email}");
                    }
                }

                Thread.Sleep(60000);

                AppInit();

                return;
            }
        }

        static void CreateUser(User user)
        {
            using var _connection = Connection.GetConnection();

            var query = @"INSERT INTO [User] VALUES(@p1,@p2)";

            try
            {
                _connection.Query<User>(query, new { p1 = user.Nome, p2 = user.Email });
                System.Console.Clear();
                System.Console.WriteLine("Usuário criado...");
                Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        static User GetUser(string email)
        {
            using var _connection = Connection.GetConnection();

            var query = "SELECT * FROM [User] WHERE Email = @p1";

            var user = _connection.Query<User>(query, new { p1 = email }).FirstOrDefault();

            return user;
        }
        static void CreateTask(Task task)
        {
            using var _connection = Connection.GetConnection();

            var query = "INSERT INTO [Task] VALUES(@p1, @p2, @p3, @p4, @p5)";

            try
            {
                _connection.Query<Task>(query, new
                {
                    p1 = task.Title,
                    p2 = task.Description,
                    p3 = task.CreateAt,
                    p4 = task.EndAt,
                    p5 = task.User.Id
                });

                System.Console.Clear();
                System.Console.WriteLine("Tarefa criada...");
                Thread.Sleep(2000);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        static IEnumerable<User> GetUserList()
        {
            using var _connection = Connection.GetConnection();

            var query = "SELECT * FROM [User]";

            var list = _connection.Query<User>(query);

            return list;
        }
    }
}
