using Grades.Dao;
using Microsoft.Extensions.Logging;

namespace Grades.Services
{
    internal class MenuService : IMenuService
    {
        private readonly ILogger<MenuService> _logger;
        private readonly IGradesDao _dao;

        public MenuService(ILogger<MenuService> logger, IGradesDao dao)
        {
            _logger = logger;
            _dao = dao;
        }

        public void Invoke()
        {
            var menu = new Menu();

            Menu.MenuOptions menuChoice;
            do
            {
                menuChoice = menu.ChooseAction();

                switch (menuChoice)
                {
                    case Menu.MenuOptions.Add:
                        _logger.LogInformation("Adding new grades");
                        menu.GetUserInput();
                        _dao.Write(menu.DataModel);
                        break;
                    case Menu.MenuOptions.Read:
                        _logger.LogInformation("Displaying grades");
                        _dao.Read();
                        _dao.Display();
                        break;
                }
            } while (menuChoice != Menu.MenuOptions.Exit);

            menu.Exit();
        }
    }
}