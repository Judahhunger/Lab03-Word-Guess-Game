using System;
using System.IO;

namespace Lab03WordGuessGame
{
    class Program
    {
       static string defualtPath = "../../../MyFile.txt";

        static void Main(string[] args)
        {
            string[] defualtWords = new string[] { "StarWars", "Empire", "Rebels", "Vader", "Luke", "Boba Fett" };//defualt array of words
            CreateFile(defualtPath, defualtWords);
       
            WelcomeScreen();
       
        }

        static void CreateFile(string path, string[] defualtArray)//Creates a file with defualt values if file has not been created
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
           
        }

        static void WelcomeScreen()//main menu
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
                            // PlayGame();
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

        static void AdminView()//admin menu
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

        static string[] GetArrayOfWords()//returns lines of text to an array
        { 
            using (StreamReader sr = File.OpenText(defualtPath))
            {
                
                 string[] wordArray = File.ReadAllLines(defualtPath);
                 return wordArray;
            }
        }

        static void AddWord(string path, string userWordToAdd)//checks to see if word is already in array of words if not then it adds it.
        {
            foreach (string word in GetArrayOfWords())
            {
                if (string.Equals(word, userWordToAdd, StringComparison.CurrentCultureIgnoreCase))
                {
                    return;
                }
            }

            using (StreamWriter sw = File.AppendText(path))
            {   
                sw.WriteLine(userWordToAdd);
            }
        }

        static void DeleteWord(string userWordToDelete)//checks to see if word is in array if so then deletes old text file and creates new one without word.
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
        }

        static void PlayGame()
        {
            Random randomIndex = new Random();
           // int number = randomIndex.Next(0, //method that return array.Length)
        }
    }
}
