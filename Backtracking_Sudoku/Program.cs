using System;

namespace Backtracking_Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
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
            Console.WriteLine("You've chosen to solve the sudoku with the Backtracking algorithm");
            Console.Write("Type here your sudoku, where empty spaces are represented by a 0: ");
            string raw = Console.ReadLine();                            
            Sudoku sudoku = new Sudoku();                           
            sudoku.Fill(raw);
            Console.WriteLine("The original sudoku: ");
            sudoku.Print_Sudoku();                                      
            Console.WriteLine("__________________________________");  
            Backtracking backtrack = new Backtracking();        
            backtrack.Algorithm(sudoku);
            Console.WriteLine("The solved sudoku: ");
            sudoku.Print_Sudoku();                                 
        }

        public static void BacktrackFC()
        {
            Console.WriteLine("You've chosen to solve the sudoku with the Backtracking algorithm with forward check");
            Console.Write("Type here your sudoku, where empty spaces are represented by a 0: ");
            string raw = Console.ReadLine();
            Console.WriteLine("The original sudoku: ");
            SudokuFC sudokufc = new SudokuFC();
            sudokufc.Fill(raw);
            Sudoku sudoku = new Sudoku();
            sudoku.Fill(raw);
            sudoku.Print_Sudoku();
            Console.WriteLine("_______________________________");
            Backtracking_FC backtrackfc = new Backtracking_FC();
            backtrackfc.Algorithm(sudokufc, sudoku);
            Console.WriteLine("The solved sudoku: ");
            sudoku.Print_Sudoku();
        }
    }
}
