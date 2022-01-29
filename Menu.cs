using System;
using Grades.Models;
using Spectre.Console;

namespace Grades
{
    internal class Menu
    {
        public enum MenuOptions
        {
            Add,
            Read,
            Exit
        }

        private readonly DataModel _dataModel = new();

        public Menu()
        {
            DataModel = _dataModel;
        }

        public DataModel DataModel { get; set; }

        public MenuOptions ChooseAction()
        {
            var menuOptions = Enum.GetNames(typeof(MenuOptions));

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose your [green]menu action[/]?")
                    .AddChoices(menuOptions));

            return (MenuOptions) Enum.Parse(typeof(MenuOptions), choice);
        }

        public void GetUserInput()
        {
            _dataModel.Name = AnsiConsole.Ask<string>("What is your [green]name[/]?");
            _dataModel.Semester = AnsiConsole.Prompt(
                new TextPrompt<string>("For which [green]semester[/] are you registering?")
                    .InvalidChoiceMessage("[red]That's not a valid semester[/]")
                    .DefaultValue("Spring 2022")
                    .AddChoice("Fall 2022")
                    .AddChoice("Spring 2023"));
            _dataModel.Classes = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                    .Title("For which [green]classes[/] are you registering?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more classes)[/]")
                    .InstructionsText(
                        "[grey](Press [blue]<space>[/] to toggle a class, " +
                        "[green]<enter>[/] to accept)[/]")
                    .AddChoices("History", "English", "Spanish", "Math", "Computer", "Literature", "Science",
                        "Chemistry", "Economics"));
        }

        public void Exit()
        {
            AnsiConsole.Write(
                new FigletText("Thanks!")
                    .LeftAligned()
                    .Color(Color.Green));
        }
    }
}