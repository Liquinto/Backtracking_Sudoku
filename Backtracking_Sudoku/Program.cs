using System;

namespace Backtracking_Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             *De main functie van Program wordt altijd uitgevoerd, hier gaat gekozen worden of het Backtrack algorithme gebruikt gaat worden,
             *of dat het BacktrackFC (backtracking met Forward Checking) uitgevoerd gaat worde.
             */
            Console.Write("Type in 1 for backtrack, and 2 for backtracking with forward check. Then press enter, and enter your sudoku: ");
            string choice = Console.ReadLine();

            if(choice == "1")
            {
                Backtrack();
            }
            if(choice == "2")
            {
                BacktrackFC();
            }
        }

        public static void Backtrack()
        {
            /*
             * In deze functie wordt het backtrack algorithme uitgevoerd
             */
            Console.WriteLine("You've chosen to solve the sudoku with the Backtracking algorithm");         //de gebruiker wordt verteld waar hij voor heeft gekozen en gevraagd om de sudoku in te vullen
            Console.Write("Type here your sudoku, where empty spaces are represented by a 0: ");
            string raw = Console.ReadLine();                            
            Sudoku sudoku = new Sudoku();                           //sudoku wordt aangemaakt
            sudoku.Fill(raw);
            Console.WriteLine("The original sudoku: ");
            sudoku.Print_Sudoku();                                      
            Console.WriteLine("__________________________________");  
            Backtracking backtrack = new Backtracking();        
            backtrack.Algorithm(sudoku);                            //sudoku wordt door het backtrack algorithme opgelost
            Console.WriteLine("The solved sudoku: ");
            sudoku.Print_Sudoku();                                 
        }

        public static void BacktrackFC()
        {
            /*
             * In deze functie wordt het backtrack algorithme met forward checking 
             */
            Console.WriteLine("You've chosen to solve the sudoku with the Backtracking algorithm with forward check"); //de gebruiker wordt verteld waar hij voor heeft gekozen en gevraagd om de sudoku in te vullen
            Console.Write("Type here your sudoku, where empty spaces are represented by a 0: ");
            string raw = Console.ReadLine();
            Console.WriteLine("The original sudoku: ");
            SudokuFC sudokufc = new SudokuFC();                     //sudoku en sudokufc worden aangemaakt
            sudokufc.Fill(raw);
            Sudoku sudoku = new Sudoku();
            sudoku.Fill(raw);
            sudoku.Print_Sudoku();
            Console.WriteLine("_______________________________");
            Backtracking_FC backtrackfc = new Backtracking_FC();
            backtrackfc.Algorithm(sudokufc, sudoku);                //backtracking algorithme met forward checking lost de sudoku op
            Console.WriteLine("The solved sudoku: ");
            sudoku.Print_Sudoku();
        }
    }
}
