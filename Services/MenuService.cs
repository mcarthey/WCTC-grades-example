using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Spectre.Console;

namespace Grades.Services
{
    internal class MenuService : IMenuService
    {
        private readonly ILogger<MenuService> _logger;

        public MenuService(ILoggerFactory loggerFactory, IConfigurationRoot config)
        {
            RegisterExceptionHandler();
        }

        public void Invoke()
        {
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