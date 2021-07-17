using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManager.BL.Services.Implementations;
using UserManager.DAL.Entities;

namespace UserManager.ConsoleApp
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var consoleService = new ConsoleService();

            await UserManagerConsole(consoleService);

        }

        private static async Task UserManagerConsole(ConsoleService consoleService)
        {
            DisplayHelp();

            string input = "";

            while (input.Equals("exit", StringComparison.OrdinalIgnoreCase) != true)
            {
                Console.WriteLine("Введите команду:");
                input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "get users":
                        Console.WriteLine("Список объектов:");
                        Console.WriteLine();

                        var users = await consoleService.GetUsersAsync();

                        foreach (UserInfo u in users)
                        {
                            Console.WriteLine($"{u.ID}.{u.Name} - {u.Status}");
                        }
                        Console.WriteLine();
                        break;

                    case "create user":
                        await CreateUser(consoleService);
                        break;

                    case "remove user":
                        await RemoveUser(consoleService);
                        break;

                    case "set status":
                        await SetStatus(consoleService);
                        break;

                    case "user info":
                        await UserInfo(consoleService);
                        break;

                    case "sign in":
                        consoleService.Login();
                        break;

                    case "sign out":
                        consoleService.Logout();
                        break;

                    case "help":
                        DisplayHelp();
                        break;

                    default:
                        break;
                }
            }
        }



        private static async Task CreateUser(ConsoleService consoleService)
        {
            consoleService.Login();

            Console.WriteLine("Введите Id пользователя:");
            string userIdStr = Console.ReadLine();
            int userId;

            while (Int32.TryParse(userIdStr, out userId) == false)
            {
                Console.WriteLine("Ошибка - введите число");
                userIdStr = Console.ReadLine();
            }

            Console.WriteLine("Enter user name:");
            string userName = Console.ReadLine();

            var content = await consoleService.CreateUserRequestAsync(new UserInfo
            {
                ID = userId,
                Name = userName,
                Status = Core.Enums.UserStatusEnum.New
            });

            Console.WriteLine($"Result: \n {content}");
            Console.WriteLine();
        }

        private static async Task RemoveUser(ConsoleService consoleService)
        {
            consoleService.Login();

            Console.WriteLine("Введите Id пользователя:");
            string userIdStr = Console.ReadLine();
            int userId;

            while (Int32.TryParse(userIdStr, out userId) == false)
            {
                Console.WriteLine("Ошибка - введите число");
                userIdStr = Console.ReadLine();
            }

            var content = await consoleService.RemoveUserRequestAsync(userId);

            Console.WriteLine($"Result: \n {content}");
            Console.WriteLine();
        }

        private static async Task SetStatus(ConsoleService consoleService)
        {
            consoleService.Login();

            Console.WriteLine("Введите Id пользователя:");
            string userIdStr = Console.ReadLine();

            Console.WriteLine("Введите новый статус пользователя (New, Active, Blocked, Deleted):");
            string newStatus = Console.ReadLine();

            while (new List<string> { "New", "Active", "Blocked", "Deleted" }.Contains(newStatus) == false)
            {
                Console.WriteLine("Ошибка - введите один из статусов: New, Active, Blocked, Deleted");
                newStatus = Console.ReadLine();
            }

            var content = await consoleService.SetStatusRequest(userIdStr, newStatus);

            Console.WriteLine($"Result: \n {content}");
            Console.WriteLine();
        }

        private static async Task UserInfo(ConsoleService consoleService)
        {
            Console.WriteLine("Введите Id пользователя:");
            string userIdStr = Console.ReadLine();
            int userId;

            while (Int32.TryParse(userIdStr, out userId) == false)
            {
                Console.WriteLine("Ошибка - введите число");
                userIdStr = Console.ReadLine();
            }

            var content = await consoleService.UserInfoRequest(userId);

            Console.WriteLine($"Result: \n {content}");
            Console.WriteLine();

        }

        private static void DisplayHelp()
        {
            Console.WriteLine(@"
*** Общее описание ***

Консольное приложение позволяет запрашивать и изменять данные о пользователях, используя 
запросы к веб апи. 

Для всех запросов, изменяющих базу данных программа запрашивает авторизацию.

При запросе информации о пользователе (user info) информация берётся из списка объектов в 
памяти. Этот список актуализируется запросом к базе данных раз в 10 минут.
       
*** Список доступных команд и их описание:  ***

create-user - добавить пользователя в базу данных
remove user - удалить пользователя из базы данных
set status - изменить статус пользователя и сохранить изменение в базе данных
user info - вывести информацию о пользователе
sign in - авторизоваться
sign out - разлогиниться
exit - завершить работу приложения
help - вызвать справку

");
        }


    }
}
