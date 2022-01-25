using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoreKeeper
{
    class InputValidation
    {
        // Validates user's input for first number
        public int GetValidFirstThrow(string userInput)
        {
            bool validInput = false;
            int rollResult = 0;
            while (!validInput)
            {
                if (int.TryParse(userInput, out int _))
                {
                    switch (userInput)
                    {
                        case "0":
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                        case "10":
                            validInput = true;
                            rollResult = int.Parse(userInput);
                            break;
                        default:
                            Console.WriteLine("Please enter a number between 0 and 10.");
                            userInput = Console.ReadLine();
                            break;
                    }

                }
                else
                {
                    Console.WriteLine("Please enter a number between 0 and 10.");
                    userInput = Console.ReadLine();
                }
            }
            return rollResult;
        }

        // Validates user's input for second number
        public int GetValidSecondThrow(string userInput, int firstThrow)
        {
            bool validInput = false;
            int rollResult = 0;
            while (!validInput)
            {
                if (int.TryParse(userInput, out int _))
                {
                    switch (userInput)
                    {
                        case "0":
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                        case "10":
                            rollResult = int.Parse(userInput);
                            if (rollResult + firstThrow > 10)
                            {
                                Console.WriteLine("There are only 10 pins in a frame. Please enter a valid number for the second roll.");
                                userInput = Console.ReadLine();
                            }
                            else if (rollResult + firstThrow <= 10)
                            {
                                validInput = true;
                            }
                            break;
                        default:
                            Console.WriteLine("Please enter a number between 0 and 10.");
                            userInput = Console.ReadLine();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a number between 0 and 10.");
                    userInput = Console.ReadLine();
                }
            }
            return rollResult;
        }

        // Validates user's input for the second number on the tenth frame
        public int GetValidFinalSecondThrow(string userInput, int firstFinalThrow)
        {
            bool validInput = false;
            int rollResult = 0;
            while (!validInput)
            {
                if (int.TryParse(userInput, out int _))
                {
                    switch (userInput)
                    {
                        case "0":
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                        case "10":
                            rollResult = int.Parse(userInput);
                            if (rollResult + firstFinalThrow > 10 && firstFinalThrow != 10)
                            {
                                Console.WriteLine("There are only 10 pins in a frame. Please enter a valid number for the second roll.");
                                userInput = Console.ReadLine();
                            }
                            else if (rollResult + firstFinalThrow <= 10 | firstFinalThrow == 10)
                            {
                                validInput = true;
                            }
                            break;
                        default:
                            Console.WriteLine("Please enter a number between 0 and 10.");
                            userInput = Console.ReadLine();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a number between 0 and 10.");
                    userInput = Console.ReadLine();
                }
            }
            return rollResult;
        }
    }
}
