using WebApi.Models;
using WebApi.Models.DTOs;

namespace WebApiClient
{
    internal class Program
    {
        private const int TableIndent = -15;

        static void Main(string[] args)
        {
            while (true)
            {
                DrawMenu();
                GetInput();
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void DrawMenu()
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Get");
            Console.WriteLine("2. Get by id");
            Console.WriteLine("3. Post");
            Console.WriteLine("4. Put");
            Console.WriteLine("5. Delete");
            Console.WriteLine("Esc чтобы выйти.");
        }

        private static async void GetInput()
        {
            ConsoleKey pressedKey = Console.ReadKey().Key;
            Console.Clear();
            string id;
            switch (pressedKey)
            {
                case ConsoleKey.D1:
                    List<UserDTO>? users = ServerInteractions.Get().Result;
                    if (users != null && users.Any())
                    {
                        DrawTable(users);
                    }
                    else
                    {
                        Console.WriteLine("Нет пользователей");
                    }
                    break;
                case ConsoleKey.D2:
                    Console.WriteLine("Введите номер для поиска");
                    id = ReadLine();
                    UserDTO? user = ServerInteractions.Get(id).Result;
                    if (user != null)
                    {
                        DrawTable(new List<UserDTO> { user });
                    }
                    break;
                case ConsoleKey.D3:
                    UserDTO userToPost = InputUser();
                    await ServerInteractions.Post(userToPost);
                    break;
                case ConsoleKey.D4:
                    Console.WriteLine("Введите номер пользователя для изменения");
                    id = ReadLine();
                    UserDTO userToPut = InputUser(false);
                    await ServerInteractions.Put(id, userToPut);
                    break;
                case ConsoleKey.D5:
                    Console.WriteLine("Введите номер пользователя для удаления");
                    id = ReadLine();
                    await ServerInteractions.Delete(id);
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
            }
            
            Console.WriteLine("Нажмите любую кнопку чтобы вернуться в меню.");
        }

        private static void DrawTable(List<UserDTO> users)
        {
            if (!users.Any())
            {
                return;
            }
            Console.WriteLine($"┌{MultiplyString(MultiplyString("─", -TableIndent) + "┬", 5).TrimEnd('┬')}┐");
            Console.WriteLine($"│{"Номер", TableIndent}│{"Имя",TableIndent}│{"Email",TableIndent}│{"Должность",TableIndent}│{"Доп. номера",TableIndent}│");
            foreach (UserDTO user in users)
            {
                Console.WriteLine($"├{MultiplyString(MultiplyString("─", -TableIndent) + "┼", 5).TrimEnd('┼')}┤");
                DrawRow(user);
            }
            Console.WriteLine($"└{MultiplyString(MultiplyString("─", -TableIndent) + "┴", 5).TrimEnd('┴')}┘");
        }

        private static void DrawRow(UserDTO user)
        {
            Console.WriteLine($"│{user.IdNumber, TableIndent}│{user.Name,TableIndent}│{user.Email,TableIndent}│{user.Position,TableIndent}│{user.UserNumbers?.FirstOrDefault()?.IdNumber,TableIndent}│");
            if (user.UserNumbers != null && user.UserNumbers.Count > 1)
            {
                foreach (UserNumberDTO userNumberDTO in user.UserNumbers.Skip(1))
                {
                    Console.WriteLine($"│{MultiplyString($"{"", TableIndent}│", 4)}{userNumberDTO.IdNumber,TableIndent}│");
                }
            }
        }

        private static UserDTO InputUser(bool hasId = true)
        {
            UserDTO user = new UserDTO();
            if (hasId)
            {
                Console.Write("Введите номер пользователя: ");
                user.IdNumber = ReadLine();
            }
            else
            {
                user.IdNumber = string.Empty;
            }
            Console.Write("Введите имя пользователя: ");
            user.Name = ReadLine();
            Console.Write("Введите e-mail пользователя: ");
            user.Email = ReadLine();
            Console.Write("Введите должность пользователя: ");
            user.Position = ReadLine();
            Console.Write("Нажмите Y если у пользователя есть дополнительные номера и любую кнопку чтобы пропустить ");
            ConsoleKey pressedKey = Console.ReadKey().Key;
            user.UserNumbers = new List<UserNumberDTO>();
            Console.WriteLine();
            if (pressedKey == ConsoleKey.Y)
            {
                while (true)
                {
                    Console.Write("Введите дополнительный номер пользователя (введите '-' для выхода): ");
                    string input = ReadLine();
                    if (input == "-")
                    {
                        break;
                    }
                    user.UserNumbers.Add(new UserNumberDTO { IdNumber = input });
                }
            }
            return user;
        }

        private static string MultiplyString(string input, int count)
        {
            return String.Concat(Enumerable.Repeat(input, count));
        }

        private static string ReadLine()
        {
            return Console.ReadLine() ?? string.Empty;
        }
    }
}