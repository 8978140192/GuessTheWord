using System;
using GuessTheWordApplication.Models;
namespace GuessTheWordApplication
{
    class Program
    {
        GuessTheWord guessTheWord;
        public Program()
        {
            guessTheWord = new();

        }
        
        public void Menu()
        {

            int userChoice;
            do
            {
                MenuList();
                userChoice = UserInputs.GetIntNumbers();
                switch (userChoice)
                {

                    case 1:
                        guessTheWord.UserLogin();
                       
                        break;
                    case 2:
                        guessTheWord.newUserRegistration();
                        break;

                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                }

            } while (!(userChoice == 1 || userChoice == 2));
        }
        public void MenuList()
        {

            Console.WriteLine("1 LOGIN");
            Console.WriteLine("2 REGISTER");
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Menu();
            Console.ReadKey();
            
        }
    }
}
