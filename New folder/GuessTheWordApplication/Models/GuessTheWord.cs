using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheWordApplication.Models
{
    public class GuessTheWord
    {
        protected GuessTheWordContext context;
        
        public GuessTheWord()
        {
            context = new GuessTheWordContext();
            
        }
        

        List<UserDetail> GetUserDetailsById(string userId)
        {
            List<UserDetail> userDetails;
            try
            {
                userDetails = context.UserDetails.Where(a => a.UserName == userId).ToList();
            }
            catch (Exception)
            {

                throw;
            }

            return userDetails;


        }
        UserDetail UserLoginChecking()
        {
            List<UserDetail> userDetail;
            string userId, userPassword;
            do
            {
                Console.WriteLine("Enter Valid user Id");
                userId = Console.ReadLine().ToUpper();
                userDetail = GetUserDetailsById(userId);

            } while (userDetail.Count == 0);

            do
            {
                Console.WriteLine("Enter Valid Password");
                userPassword = Console.ReadLine();

            } while ((userDetail[0].UserPassword != userPassword));
            return userDetail[0];
        }

        public void MenuList()
        {

            Console.WriteLine("1 WORDS REAADY FOR YOU TO PLAY");
            Console.WriteLine("2 ASSIGN WORD FOR ANOTHER PLAYER");
            Console.WriteLine("3 SEE SCOREBOARD");
            Console.WriteLine("0 EXIT");
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

                    case 0:
                        
                        Console.WriteLine("bye bye....");
                        return;
                        
                    case 1:                        
                        new PlayGame().UserGuessing(UserInputs.userWords, UserInputs.currentUser);
                        break;
                    case 2:                   
                        new PlayGame().AssignWordToplayer();
                        break;
                    case 3:                        
                        new PlayGame().ShowScoreBoard();
                        break;

                    default:
                        Console.WriteLine("Invalid Input");
                        break;
                }

            } while (userChoice != 0);
        }
        List<string> AvailableWords()
        {
            List<string> availablewords = new List<string>();

            foreach (var item in context.Words)
            {
                availablewords.Add(item.Word1);
                //Console.WriteLine(item.Word1);
            }
            List<UserAssignedWord> userAssignedWords = context.UserAssignedWords.Where(e => e.ToUser == UserInputs.currentUser).ToList();
            foreach (var item in userAssignedWords)
            {
                availablewords.Add(item.Word);
            }
            return availablewords;
        }


        public void UserLogin()
        {

            UserDetail user = new ();
            user = UserLoginChecking();
            UserInputs.currentUser = user.UserName;
            UserInputs.userWords = AvailableWords();
            //Console.WriteLine(UserInputs.currentUser);

            Menu();

        }

        void InsertNewUserIntoScoreBoard(string UserName)
        {
            ScoreBoard newuser = new();
            newuser.UserName = UserName;
            newuser.Score = 0;
            context.ScoreBoards.Add(newuser);
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
        public void newUserRegistration()
        {
            bool flag;
            UserDetail userDetail = new UserDetail();

            Console.WriteLine("Enter Your user Id");
            userDetail.UserName = Console.ReadLine().ToUpper();

            Console.WriteLine("Enter Your Password");
            userDetail.UserPassword = Console.ReadLine();

            Console.WriteLine("Enter Your Name");
            userDetail.UserFullName = Console.ReadLine().ToUpper();

            Console.WriteLine("Enter Your Contact Number");
            userDetail.UserContact = Convert.ToString(UserInputs.GetDoubleNumber());
            context.Add(userDetail);
            try
            {

                context.SaveChanges();
                flag = true;

            }
            catch (Exception)
            {

                Console.WriteLine("Your Id alredy registered...");
                flag = false;
            }

            if (flag)
            {
                Console.WriteLine($"Hello {userDetail.UserName} yor are succefully registered....");
                InsertNewUserIntoScoreBoard(userDetail.UserName);
            }
                

        }
    }
}
