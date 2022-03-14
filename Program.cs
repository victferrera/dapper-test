using DapperSimpleTest.Models;
using Dapper;
using DapperSimpleTest.Data;
using System;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

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
            System.Console.WriteLine("2 - Cadastro de tarefa");
            System.Console.WriteLine("3 - Lista de usuários cadastrado");
            System.Console.WriteLine("4 - Lista de Tarefas cadastradas");
            System.Console.WriteLine("41 - Lista de Tarefas com usuário vinculado");
            System.Console.WriteLine("5 - Finalizar tarefa");
            System.Console.WriteLine("6 - Excluir tarefa");
            System.Console.WriteLine("7 - Excluir usuário");
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

                    Thread.Sleep(60000);
                }

                if (option == 4)
                {
                    System.Console.Clear();

                    var TasksList = GetTaskList();

                    foreach (var tsk in TasksList)
                    {
                        System.Console.WriteLine($"Id: {tsk.Id}, Title: {tsk.Title}, Description: {tsk.Description}, Create At: {tsk.CreateAt}, End At: {tsk.EndAt}");
                    }


                    Thread.Sleep(60000);
                }

                if (option == 5)
                {
                    System.Console.Clear();
                    var TasksList = GetTaskList();

                    System.Console.WriteLine("Digite o ID da tarefa a finalizar");

                    foreach (var tsk in TasksList)
                    {
                        System.Console.WriteLine();
                        System.Console.WriteLine($"Id: {tsk.Id}, Title: {tsk.Title}, Description: {tsk.Description}, Create At: {tsk.CreateAt}, End At: {tsk.EndAt}");
                    }

                    var opt = System.Console.ReadLine();

                    var task = GetTaskById(int.Parse(opt));

                    CloseTask(task);
                }

                if (option == 6)
                {
                    System.Console.Clear();

                    var TasksList = GetTaskList();

                    System.Console.WriteLine("Informe o ID da tarefa que deseja remover");
                    System.Console.WriteLine();

                    foreach (var tsk in TasksList)
                    {
                        System.Console.WriteLine($"Id: {tsk.Id}, Title: {tsk.Title}, Description: {tsk.Description}, Create At: {tsk.CreateAt}, End At: {tsk.EndAt}");
                    }

                    var opt = int.Parse(System.Console.ReadLine());

                    RemoveTask(GetTaskById(opt));
                }

                if (option == 7)
                {
                    System.Console.Clear();

                    var UsersList = GetUserList();

                    foreach (var usr in UsersList)
                    {
                        System.Console.WriteLine($"Id : {usr.Id}, Nome: {usr.Nome}, Email: {usr.Email}");
                    }

                    var idToRemove = int.Parse(System.Console.ReadLine());

                    RemoveUser(idToRemove);
                }

                if (option == 41)
                {
                    System.Console.Clear();

                    var taskList = GetTaskWithUser();

                    foreach (var tsk in taskList)
                    {
                        System.Console.WriteLine($"Task Id: {tsk.Id}, Task Title: {tsk.Title}, User Name: {tsk.User.Nome},  User Id: {tsk.User.Id}");
                    }

                    System.Console.ReadLine();
                }

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

        static IEnumerable<Task> GetTaskList()
        {
            using var _connection = Connection.GetConnection();

            var query = "SELECT * FROM [Task]";

            var list = _connection.Query<Task>(query);

            return list;

        }

        static Task GetTaskById(int id)
        {
            using var _connection = Connection.GetConnection();

            var query = "SELECT * FROM [Task] WHERE ID = @p1";

            var task = _connection.Query<Task>(query, new { p1 = id }).FirstOrDefault();

            return task;
        }

        static void CloseTask(Task task)
        {
            using var _connection = Connection.GetConnection();

            var query = "UPDATE [Task] SET EndAt = @p1 WHERE [Id] = @p2";

            try
            {
                _connection.Execute(query, new { p1 = DateTime.Now, p2 = task.Id });
                System.Console.Clear();
                System.Console.WriteLine("Task alterada...");
                Thread.Sleep(3000);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        static void RemoveTask(Task task)
        {
            using var _connection = Connection.GetConnection();

            var query = "DELETE FROM [Task] WHERE Id = @p1";

            try
            {
                _connection.Execute(query, new { p1 = task.Id });
                System.Console.Clear();
                System.Console.WriteLine("Tarefa removida...");
                Thread.Sleep(3000);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        static void RemoveUser(int userId)
        {
            using var _connection = Connection.GetConnection();

            System.Console.Clear();
            System.Console.WriteLine("Excluíndo este usuário, as tarefas a seguir serão excluídas também: ");

            var userTaskList = GetTaskByUserId(userId);

            foreach (var tsk in userTaskList)
            {
                System.Console.WriteLine(
                $@"
                Task Id: {tsk.Id}
                Task Title: {tsk.Title}"
                );
            }

            System.Console.WriteLine();
            System.Console.WriteLine("Deseja continuar? s/n ");

            var option = System.Console.ReadLine();

            if (option == "n")
                return;

            var query = "DELETE FROM [User] WHERE Id = @p1";

            try
            {
                _connection.Delete(userTaskList);
            }
            catch (Exception e)
            {
                throw e;
            }

            try
            {
                _connection.Execute(query, new { p1 = userId });
                System.Console.Clear();
                System.Console.WriteLine("Usuário removido e tarefas removidos...");
                System.Console.ReadLine();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        static List<Task> GetTaskWithUser()
        {
            using var _connection = Connection.GetConnection();

            var query = @"SELECT * FROM 
            [Task] t 
            INNER JOIN [User] u
            on t.UserId = u.Id";

            var tasks = _connection.Query<Task, User, Task>(query, (task, user) =>
            {
                task.User = user;
                return task;
            }, splitOn: "UserId");

            return tasks.ToList();
        }

        static IEnumerable<Task> GetTaskByUserId(int id)
        {
            using var _connection = Connection.GetConnection();

            var query = @"SELECT * FROM [Task] t WHERE t.UserId = @p1";

            var taskList = _connection.Query<Task>(query, new { p1 = id });

            return taskList;
        }
    }
}
