using System;
using System.IO;

namespace Lab03WordGuessGame
{
    class Program
    {
       static string path = "../../../MyFile.txt";

        static void Main(string[] args)
        {
            string[] defualtWords = new string[] { "StarWars", "Empire", "Rebels", "Vader", "Luke", "Boba Fett" };//defualt array of words
           

            CreateFile(defualtWords);
            WelcomeScreen();


        }

        static void CreateFile(string[] defualtArray)//Creates a file with defualt values if file has not been created
        {
            if (!File.Exists(path))
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    try
                    {
                        foreach (var item in defualtArray)
                        {
                            sw.Write(item);
                        }
                        
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error in CreateFile() method");
                    }
                    finally
                    {
                        sw.Close();
                    }
                   
                }
            }
           
        }

        static void ReadFile()
        {
            
            using (StreamReader sr = File.OpenText(path))
            {
                /*string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }*/

                try
                {
                    string[] myText = File.ReadAllLines(path);
                    foreach (string value in myText)
                    {
                        Console.WriteLine(value);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        static void UpdateFile()
        {
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine("write something");
            }
        }

        static void DeleteFile()
        {
            File.Delete(path);
        }

        static void WelcomeScreen()
        {
            bool mainMenuOpen = true; // make a bool so that if main is open it will stay open

            while (mainMenuOpen)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the word Guess Game Main Menu.");
                Console.WriteLine("1) Play Game.");
                Console.WriteLine("2) Admin");
                Console.WriteLine("3) Exit");
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
                        mainMenuOpen = false;
                        Environment.Exit(0);
                        break;
                }
            }
           
        }

        static void AdminView()
        {
            Console.Clear();
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
                        //Vew words method
                        break;
                    case "2":
                        //update Words method
                        break;
                    case "3":
                        //Delete a word / update methods
                        break;
                    case "4":
                        WelcomeScreen();
                        break;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error thrown in AdminView() retrun to Main Menu");
                Console.ReadLine();
                WelcomeScreen();
                //throw;
            }
        }


    }
}
