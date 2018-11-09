using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_238_Intermediate_Fallout_Hacking_Game
{
    class Program
    {
        private readonly static List<string> CutDownList = System.IO.File.ReadAllLines("enable1.txt").Where(x => x.Length == 4 || x.Length == 7 || x.Length == 9 || x.Length == 12 || x.Length == 15).ToList();



        /// <summary>
        /// Call the get user input method
        /// </summary>
        /// <param name="args">Does Nothing</param>
        static void Main(string[] args)
        {
            GetUserInput();
        }

        /// <summary>
        /// Asks user what difficulty they want to play on. Sets the FromFilewordList based on the user input. 
        /// Calls BuildPuzzle method and passes the newly set FromFileWordList List.
        /// Calls PlayGame method. On Completion of game asks for difficulty again from user.
        /// </summary>
        private static void GetUserInput()
        {
            List<string> FromFileWordList = null;
            string UserInput = "1";
            while (!UserInput.Equals("qqq") && !UserInput.Equals("quit"))
            {
                List<string> ChosenWordList = new List<string>();
                Console.WriteLine("Enter Difficulty Level\n1: Easy\n2: Medium\n3: Hard\n4: Very Hard\n5: Borderline impossible\n\n'qqq' or 'quit' to quit\n");
                try
                {
                    UserInput = Console.ReadLine();

                    switch (UserInput.ToLower())
                    {
                        case "1":
                        case "easy":

                            FromFileWordList = CutDownList.Where(x => x.Length == 4).ToList();
                            ChosenWordList = BuildPuzzle(FromFileWordList, 1); break;
                        case "2":
                        case "medium":

                            FromFileWordList = CutDownList.Where(x => x.Length == 7).ToList();
                            ChosenWordList = BuildPuzzle(FromFileWordList, 2); break;
                        case "3":
                        case "hard":

                            FromFileWordList = CutDownList.Where(x => x.Length == 9).ToList();
                            ChosenWordList = BuildPuzzle(FromFileWordList, 3); break;
                        case "4":
                        case "very hard":

                            FromFileWordList = CutDownList.Where(x => x.Length == 12).ToList();
                            ChosenWordList = BuildPuzzle(FromFileWordList, 4); break;
                        case "5":
                        case "borderline impossible":

                            FromFileWordList = CutDownList.Where(x => x.Length == 15).ToList();
                            ChosenWordList = BuildPuzzle(FromFileWordList, 5); break;
                        default: Console.Clear(); Console.WriteLine("Enter either the number (1) or the name of the diffuculty (easy)\n"); break;
                    }

                    PlayGame(ChosenWordList);


                }
                catch (Exception) { Console.WriteLine("\nEnter either the number (1) or the name of the difficulty (easy)\n"); }
            }
        }


        /// <summary>
        /// Let user guess at what the selected word is, notify user how many letters are correct in the correct order.
        /// </summary>
        /// <param name="chosenWordList">List of words to choose from</param>
        private static void PlayGame(List<string> chosenWordList)
        {
            //Set up a Random to select one of the words in ChosenWordList
            Random rng = new Random();
            //Set Number of guesses alloted as well as the number of letters correctly guessed.
            int GuessesLeft = 8;
            int NumberOfCorrectLetters = 0;
            //Pick a word from the word list with random.
            string WordToGuess = chosenWordList[rng.Next(0, chosenWordList.Count())];
            string UserInput = "";

            Console.Clear();

            //Print the options that the user can choose from.
            foreach (var item in chosenWordList)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();


            //Run while the user still has guesses and has not guessed the word yet.
            while (!UserInput.Equals(WordToGuess) && GuessesLeft > 0)
            {
                //Get user input and set it to lower.
                UserInput = Console.ReadLine().ToLower();
                //Check if the guessed word is in the list of possible words.
                if (chosenWordList.Contains(UserInput))
                {
                    NumberOfCorrectLetters = 0;
                    //If the user hasnt guessed the correct word yet.
                    if (!UserInput.Equals(WordToGuess))
                    {
                        //Check every letter in the string input to see if its the same letter in the same index as the chosen word.
                        for (int i = 0; i < UserInput.Count(); i++)
                        {

                            if (UserInput[i].Equals(WordToGuess[i]))
                                NumberOfCorrectLetters++;
                        }
                        GuessesLeft--;
                        Console.WriteLine("{0} / 8 Correct! {1} Guesses Left", NumberOfCorrectLetters, GuessesLeft);
                    }
                    else
                    {
                        Console.WriteLine("Correct! The word was {0}. Congratulations\n", WordToGuess);
                    }
                }
                else
                {
                    GuessesLeft--;
                    Console.WriteLine("0 / 8 Correct! {0} Guesses Left ", GuessesLeft);
                }
            }

            if (GuessesLeft < 1)
                Console.WriteLine("You Lost! The Word was {0}.", WordToGuess);

            Console.WriteLine("Press Any Key To Continue...");
            Console.ReadKey();
            Console.Clear();



        }


        /// <summary>
        /// Selects 10 words randomly from the words in the file and returns them.
        /// </summary>
        /// <param name="WordList">List of words from the file that has been reduced to only the words that fit the length set for the difficulty(4 letters for easy, 7 for medium)</param>
        /// <param name="i">Corresponds to the difficulty chosen by the user. On the hardest setting it makes the puzzle more difficult</param>
        /// <returns>A list of 10 words of a certain length</returns>
        private static List<string> BuildPuzzle(List<string> WordList, int i)
        {
            List<string> ChosenWords = new List<string>();
            Random RNG = new Random();
            bool Different = true;
            //Add the first random word out here so it can be compared to the second
            ChosenWords.Add(WordList[RNG.Next(0, WordList.Count)]);

            //If the user chose the hardest difficulty
            if (i == 5)
            {
                //Check to see if any other selected words have letters in common at the same index.
                //Get a different word if thats the case.
                for (int o = 1; o < 10; o++)
                {
                    Different = true;
                    string word = WordList[RNG.Next(0, WordList.Count)];

                    for (int z = 0; z < word.Count(); z++)
                        if (word[z].Equals(ChosenWords[o - 1][z]))
                        {
                            o--;
                            Different = false;
                            break;
                        }

                    if (Different)
                        ChosenWords.Add(word);
                }
            }
            else
            {
                //Add 10 random words 
                for (int o = 1; o < 10; o++)
                {

                    string word = WordList[RNG.Next(0, WordList.Count)];
                    ChosenWords.Add(word);
                }
            }
            return ChosenWords;
        }

    }
}
