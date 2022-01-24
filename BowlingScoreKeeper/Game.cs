using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoreKeeper
{
    class Game
    {
        private FrameRepository _frameRepo = new FrameRepository();
        
        public void Run()
        {
            SeedFrames();
            MainMenu();
        }

        // Displays main menu for user
        private void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome! Please select from the options below:\n" +
                "1. Start a new game\n" +
                "2. View final score\n" +
                "3. Exit\n");
            string userInput = Console.ReadLine();
            bool validInput = false;
            while (validInput == false)
            {
                switch (userInput)
                {
                    case "1":
                        validInput = true;
                        Start();
                        break;
                    case "2": 
                        validInput = true;
                        Console.Clear();
                        Console.WriteLine($"The final score from the previous game is: {_frameRepo.GetTotalScore()}");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        MainMenu();
                        break;
                    case "3":
                        validInput = true;
                        Console.Clear();
                        Console.WriteLine("Please press any key to close the program.");
                        Console.ReadKey();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Please select a valid option\n" +
                                "1. Start a new game\n" +
                                "2. View final score\n" +
                                "3. Exit\n");
                        userInput = Console.ReadLine();
                        break;
                }
            }
        }

        // Starts the game
        private void Start()
        {
            List<Frame> frames = _frameRepo.GetFrames();
            _frameRepo.ResetScore();
            foreach(Frame frame in frames)
            {
                Console.Clear();
                Console.WriteLine($"Enter your first roll for the {frame.Name} frame: ");
                var firstThrow = GetValidFirstThrow(Console.ReadLine());
                if(firstThrow == 10)
                {
                    _frameRepo.Roll(frame.FrameID, firstThrow, 0);
                    Console.WriteLine("That's a strike!");
                }
                else 
                {
                    Console.WriteLine($"Enter your second roll for the {frame.Name} frame: ");
                    var secondThrow = GetValidSecondThrow(Console.ReadLine(), firstThrow);
                    _frameRepo.Roll(frame.FrameID, firstThrow, secondThrow);
                }
                Console.WriteLine($"Total score after {frame.Name} frame: {_frameRepo.GetTotalScore()}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            Console.Clear();
            Console.WriteLine("Enter your first roll for the tenth frame: ");
            var finalFirstThrow = GetValidFirstThrow(Console.ReadLine());
            Console.WriteLine("Enter your second roll for the tenth frame: ");
            var finalSecondThrow = GetValidFinalSecondThrow(Console.ReadLine(), finalFirstThrow);
            _frameRepo.FinalRoll(finalFirstThrow, finalSecondThrow, 0);
            if((finalFirstThrow + finalSecondThrow) >= 10)
            {
                Console.WriteLine("Enter your third roll for the tenth frame: ");
                var finalThirdThrow = GetValidFirstThrow(Console.ReadLine());
                _frameRepo.FinalRoll(finalFirstThrow, finalSecondThrow, finalThirdThrow);
            }
            Console.WriteLine($"Your final score for this game is: {_frameRepo.GetTotalScore()}");
            Console.ReadKey();
            MainMenu();
        }

        // Validates user's input for first number
        private static int GetValidFirstThrow(string userInput)
        {
            bool validInput = false;
            int rollResult = 0;
            while(!validInput)
            {
                if (int.TryParse(userInput, out int _))
                {
                    switch(userInput)
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
        private static int GetValidSecondThrow(string userInput, int firstThrow)
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
        private static int GetValidFinalSecondThrow(string userInput, int firstFinalThrow)
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

        // Seeds empty frames for the game
        private void SeedFrames()
        {
            Frame firstFrame = new Frame(0, "first", 0 ,0 );
            Frame secondFrame = new Frame(1, "second", 0, 0);
            Frame thirdFrame = new Frame(2, "third", 0, 0);
            Frame fourthFrame = new Frame(3, "fourth", 0, 0);
            Frame fifthFrame = new Frame(4, "fifth", 0, 0);
            Frame sixthFrame = new Frame(5, "sixth", 0, 0);
            Frame seventhFrame = new Frame(6, "seventh", 0, 0);
            Frame eighthFrame = new Frame(7, "eighth", 0, 0);
            Frame ninthFrame = new Frame(8, "ninth", 0, 0);
            _frameRepo.CreateFrame(firstFrame);
            _frameRepo.CreateFrame(secondFrame);
            _frameRepo.CreateFrame(thirdFrame);
            _frameRepo.CreateFrame(fourthFrame);
            _frameRepo.CreateFrame(fifthFrame);
            _frameRepo.CreateFrame(sixthFrame);
            _frameRepo.CreateFrame(seventhFrame);
            _frameRepo.CreateFrame(eighthFrame);
            _frameRepo.CreateFrame(ninthFrame);
        }
    }
}
