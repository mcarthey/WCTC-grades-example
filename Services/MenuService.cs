using System;
using Grades.Dao;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Spectre.Console;

namespace Grades.Services
{
    internal class MenuService : IMenuService
    {
        private readonly ILogger<MenuService> _logger;

        public void Invoke()
        {
            var menu = new Menu();
            var fileManager = new GradesDao();

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
    }
}