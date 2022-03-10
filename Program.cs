using System;
using DapperSimpleTest.Models;

namespace DapperSimpleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.Clear();
            System.Console.WriteLine("Menu de opções");
            System.Console.WriteLine("======================================");
            System.Console.WriteLine("1 - Cadastro de usuário");
            System.Console.WriteLine("2 - Cadastro de Tarefa");

            var option = int.Parse(System.Console.ReadLine());

            if (option == 1)
            {
                System.Console.Clear();
                System.Console.Write("Informe o nome: ");
                var name = System.Console.ReadLine();
                System.Console.WriteLine("Informe o email: ");
                var email = System.Console.ReadLine();

                var newUser = new User
                {
                    Name = name,
                    Email = email
                };

            }


        }

        static void CreateUser()
        {

        }
    }
}
