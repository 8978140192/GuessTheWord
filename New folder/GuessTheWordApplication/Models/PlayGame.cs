using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheWordApplication.Models
{
    public class PlayGame:GuessTheWord
    {
        void UpdateScore()
        {
            ScoreBoard updateScore = context.ScoreBoards.First(i => i.UserName == UserInputs.currentUser);
            
            updateScore.Score = updateScore.Score + 10;
            context.SaveChanges();
            
        }
        public void UserGuessing(List<string> userAvailableWords,string user)
        {
            
            int count = 0, userGuessChoice;
            bool match;
            Console.WriteLine("Available words");
            foreach (var word in userAvailableWords)
            {
                count++;
                //Console.WriteLine($"{count } {string.Concat(Enumerable.Repeat("X", word.Length))}");
                Console.WriteLine($"{count } {word}");
            }
            Console.WriteLine("0 GO TO PREVIOS MENU");

            if (count ==0)
            {
                Console.WriteLine("All words completed");
                return;
            }

            do
            {
                Console.WriteLine("Enter your choice");
                userGuessChoice = UserInputs.GetIntNumbers();

            } while (userGuessChoice < 0 || userGuessChoice > count);
            
            if (userGuessChoice == 0)
            {
                return;
            }
            else
            {
                do
                {
                    Console.WriteLine("Enter Your Guess");
                    string userGuess = Console.ReadLine().ToUpper();
                    match = MatchWords(userGuess, userAvailableWords[userGuessChoice - 1]);//function should return false if user entered correct word

                } while (match);
                //Update score 
                UpdateScore();
                Console.WriteLine("That is the word. Congrats!!!!!");
                userAvailableWords.Remove(userAvailableWords[userGuessChoice - 1]);
                if (userAvailableWords.Count==0)
                {
                    Console.WriteLine("All words completed");

                }
                else
                {
                    UserGuessing(userAvailableWords,user);
                }
            }
        }
       

        public List<string> FetechAvailableUsers()
        {
            List<string> availableUser = new();
            foreach (var item in context.UserDetails)
            {
                availableUser.Add(item.UserName);
            }
            return availableUser;
        }
        void InserIntoUserAssignedWord(string assignWord,string toUser)
        {
            UserAssignedWord challengeWord = new();
            challengeWord.FromUser = UserInputs.currentUser;
            challengeWord.ToUser = toUser;
            challengeWord.Word = assignWord;
            try
            {
                context.UserAssignedWords.Add(challengeWord);
                context.SaveChanges();
            }
            catch (Exception)
            {

                Console.WriteLine("Word alredy present...");
            }

        }
        public void AssignWordToplayer()
        {
            int toUser;
            int userCount = 0;
        
            Dictionary<int, string> toUserDis = new();
            Console.WriteLine("Available users");
            foreach (var item in context.UserDetails)
            {
                
                if (UserInputs.currentUser != item.UserName)
                {
                    userCount++;
                    Console.WriteLine($"{userCount} "+item.UserName);
                    
                    toUserDis.Add(userCount, item.UserName);
                }
            }

            if (toUserDis.Count == 0)
                return;
            do
            {
                Console.WriteLine("Select the username");
                toUser = UserInputs.GetIntNumbers();

            } while (toUser<1 || toUser > toUserDis.Count);
          
            Console.WriteLine("Enter the word");
            InserIntoUserAssignedWord(Console.ReadLine().ToUpper(), toUserDis[toUser]);
        }

        public void ShowScoreBoard()
        {
            
            foreach (var item in context.ScoreBoards)
            {
                Console.WriteLine("UserName: "+item.UserName);
                Console.WriteLine("Score: "+ item.Score);
                Console.WriteLine("--------------------");
            }

        }

        bool MatchWords(string wordToCompare, string wordToGuess)
        {
            int bullCount = 0;
            int cowCount = 0;
            if (wordToCompare.Length == wordToGuess.Length)
            {

                for (int i = 0; i < wordToCompare.Length; i++)
                {
                    char leter = wordToCompare[i];
                    int indexOfleter = wordToGuess.IndexOf(leter);
                    if (indexOfleter > -1 && leter == wordToGuess.ElementAt(i))
                    {
                        cowCount++;
                    }
                    else if (indexOfleter > -1 && i != indexOfleter)
                    {
                        bullCount++;
                    }

                }
                Console.WriteLine("Bull : " + bullCount);
                Console.WriteLine("Cow : " + cowCount);

            }
            else
            {
                Console.WriteLine("Enter same length word");
                return true;
            }
            if (cowCount == wordToCompare.Length)
            {
                return false;
            }
            else
                return true;

        }
        

        
    }
}
