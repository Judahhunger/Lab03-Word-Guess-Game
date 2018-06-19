using System;
using System.IO;

namespace Lab03WordGuessGame
{
     public class Program
    {
       static string defualtPath = "../../../MyFile.txt";

        static void Main(string[] args)
        {
            string[] defualtWords = new string[] { "StarWars", "Empire", "Rebels", "Vader", "Luke", "Boba Fett" };//defualt array of words
            CreateFile(defualtPath, defualtWords);
       
            WelcomeScreen();
       
        }

        /// <summary>
        /// Makes a file if the file does not exsist with string path variable defined at top of page.
        /// Also puts in defualt values from array defined in main() above.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="defualtArray"></param>
        public static string CreateFile(string path, string[] defualtArray)//Creates a file with defualt values if file has not been created
        {
            if (!File.Exists(path))
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    try
                    {
                        foreach (string item in defualtArray)
                        {
                            if (item != "removeThisText")
                            {
                                sw.WriteLine(item);
                            }
                       
                        }
                       
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Error in CreateFile() method");
                        Console.ReadLine();
                    }
                    finally
                    {
                        sw.Close();
                    }

                }

            }
            return "File made";
        }

        /// <summary>
        /// On page start will display messages to console and give use options.
        /// Lets user play game, go to admin view or exit.
        /// </summary>
        public static void WelcomeScreen()//main menu
        {
            bool mainMenuOpen = true; // make a bool so that if main is open it will stay open

            while (mainMenuOpen)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the word Guess Game Main Menu.");
                Console.WriteLine("1) Play Game.");
                Console.WriteLine("2) Admin");
                Console.WriteLine("3) Exit");

                try
                {
                    int userMenuInput = Int32.Parse(Console.ReadLine());

                    switch (userMenuInput)
                    {
                        case 1:
                            PlayGame();
                            break;
                        case 2:
                            AdminView();
                            break;
                        case 3:
                            Console.WriteLine("Press enter to Exit app");
                            mainMenuOpen = false;
                            Environment.Exit(0);
                            continue;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Please enter a number 1 , 2 or 3.");
                    Console.ReadLine();
                }
            }

        }

        /// <summary>
        /// Lets user pick an option to see all words in file,
        /// add words, delete words or return to main menu.
        /// </summary>
        public static void AdminView()//admin menu
        {
            bool adminBool = true;

            while (adminBool)
            {
                Console.Clear();
                Console.WriteLine("Pick what you would like to do.");
                Console.WriteLine("1) View all Words");
                Console.WriteLine("2) Add a Word");
                Console.WriteLine("3) Delete a Word");
                Console.WriteLine("4) Back to Main Menu");
                string userAdminInput = Console.ReadLine();

                try
                {
                    switch (userAdminInput)
                    {
                        case "1":
                            foreach (string item in GetArrayOfWords())
                            {
                                Console.WriteLine(item);
                            }
                            Console.ReadLine();
                            break;
                        case "2":
                            Console.WriteLine("What word would you like to add?");
                            string userAddedWord = Console.ReadLine();
                            AddWord(defualtPath, userAddedWord);
                            Console.WriteLine($"{userAddedWord} is in the list of words");
                            Console.ReadLine();
                            break;
                        case "3":
                            Console.WriteLine("What word would you like to delete?");
                            string wordToDeleted = Console.ReadLine();

                            DeleteWord(wordToDeleted);
                            
                            break;
                        case "4":
                            adminBool = false;
                            WelcomeScreen();
                            break;
                    }

                }
                catch (Exception)
                {
                    Console.WriteLine("Error thrown return to Main Menu");
                    Console.ReadLine();
                    WelcomeScreen();
                   
                }
            }

        }

        /// <summary>
        /// Uses StreamReader to open filepath the MyFile.txt
        /// Stores every line in file as value in array then 
        /// returns that array.
        /// </summary>
        /// <returns></returns>
        public static string[] GetArrayOfWords()//returns lines of text to an array
        { 
            using (StreamReader sr = File.OpenText(defualtPath))
            {
                
                 string[] wordArray = File.ReadAllLines(defualtPath);
                 return wordArray;
            }
        }

        /// <summary>
        /// checks to see if word user is trying to add is in array from GetArrayOfWords() method.
        /// if it is not it will add that word to end line of MyFile.txt
        /// </summary>
        /// <param name="path"></param>
        /// <param name="userWordToAdd"></param>
        public static string AddWord(string path, string userWordToAdd)//checks to see if word is already in array of words if not then it adds it.
        {
            foreach (string word in GetArrayOfWords())
            {
                if (string.Equals(word, userWordToAdd, StringComparison.CurrentCultureIgnoreCase))
                {
                    return "Word added";
                }
            }

            using (StreamWriter sw = File.AppendText(path))
            {   
                sw.WriteLine(userWordToAdd);
            }
            return "Word added";
        }

        /// <summary>
        /// checks to see if word that user wants to add is in array from GetArrayOfWords(), 
        /// if it is then it makes new file without that word,
        /// deletes old file and replaces it with new file.
        /// </summary>
        /// <param name="userWordToDelete"></param>
        public static string DeleteWord(string userWordToDelete)//checks to see if word is in array if so then deletes old text file and creates new one without word.
        {
            string[] tempArray = GetArrayOfWords();

            for (int i = 0; i < tempArray.Length; i++)
            {
                if (string.Equals(tempArray[i], userWordToDelete, StringComparison.CurrentCultureIgnoreCase))
                {
                    tempArray[i] = "removeThisText";
                }
            }
            string tempPath = "../../../MyTempFile.txt";
            CreateFile(tempPath, tempArray);
            File.Delete(defualtPath);
            File.Move(tempPath, defualtPath);
            return "File Replaced";
        }

        /// <summary>
        /// makes a random word from lines of MyFile.txt
        /// makes new array with blank spaces to fill in per letter of random word
        /// Then stores the word in a char array
        /// checks if user input a letter in char array
        /// if found the blank space from new array with blanks will now have a letter in place.
        /// Stores user letters to help user guess what letters have not been tried.
        /// </summary>
        public static void PlayGame()
        {  
            Random randomIndex = new Random();
            int number = randomIndex.Next(0, GetArrayOfWords().Length); //make random number to pick index of line for word.
            string playerGuessed = "";
            string randomWord = GetArrayOfWords()[number];//stores the random word from file
            char[] lettersInWordArray = randomWord.ToLower().Trim().ToCharArray();//makes array of letters for easy comparison.
            string[] displayWord = new string[randomWord.ToLower().Trim().Length];
            bool correctAnswer = false;

            for (int i = 0; i < randomWord.Length; i++)
            {
                displayWord[i] = " _ ";
                if (lettersInWordArray[i] == ' ')
                {
                    displayWord[i] = " ";
                }
                
            }

            foreach (string item in displayWord)
            {
                Console.Write(item);
            }
          
            while (correctAnswer == false)
            {
                Console.WriteLine("Guess a Letter");
                string letter = Console.ReadLine().ToLower();
                for (int i = 0; i < randomWord.Length; i++)
                {
                    if (letter != null && lettersInWordArray[i].ToString() == letter)
                    {
                            displayWord[i] = letter;
                    }                    
                }
                 playerGuessed += letter;

                for (int i = 0; i < displayWord.Length; i++)
                {
                    Console.Write(displayWord[i]);  
                }

                Console.WriteLine();
                Console.WriteLine($"{playerGuessed} are the letters you have guessed");
                if (String.Equals(String.Join("", displayWord), randomWord.ToLower()))
                {
                    Console.WriteLine("you got the word");
                    Console.WriteLine("enter 1 to play again or 2 to exit");
                    string willPlayOrExit = Console.ReadLine();

                    switch (willPlayOrExit)
                    {
                        case "1":
                            PlayGame();
                            break;
                        default:
                            Environment.Exit(0);
                            break;
                    }
                    correctAnswer = true;
                }
                
            }

        }
    }
}
