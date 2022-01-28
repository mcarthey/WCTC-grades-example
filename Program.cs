using System;
using Spectre.Console;

namespace Grades
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            RegisterExceptionHandler();

            var menu = new Menu();
            var fileManager = new FileManager();

            Menu.MenuOptions menuChoice;
            do
            {
                menuChoice = menu.ChooseAction();

                switch (menuChoice)
                {
                    case Menu.MenuOptions.Add:
                        menu.GetUserInput();
                        fileManager.Write(menu.DataModel);
                        break;
                    case Menu.MenuOptions.Read:
                        fileManager.Read();
                        fileManager.Display();
                        break;
                }
            } while (menuChoice != Menu.MenuOptions.Exit);

            menu.Exit();
        }

        public static void RegisterExceptionHandler()
        {
            AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
            {
                AnsiConsole.WriteException(eventArgs.Exception,
                    ExceptionFormats.ShortenPaths | ExceptionFormats.ShortenTypes |
                    ExceptionFormats.ShortenMethods | ExceptionFormats.ShowLinks);
            };
        }
    }
}