using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheWordApplication.Models
{
    public static class UserInputs
    {
        public static string currentUser;
        public static List<string> userWords;

        public static int GetIntNumbers()
        {
            int num;
            while (!int.TryParse(Console.ReadLine(), out num))
            {
                Console.WriteLine("Invalid entry. Please enter again");
            }
            return num;
        }

        public static double GetDoubleNumber()
        {
            double number;
            while (!double.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Invalid entry. Please enter again");
            }
            return number;
        }


    }
}
