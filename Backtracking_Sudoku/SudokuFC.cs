using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Backtracking_Sudoku
{
    class SudokuFC
    {
        public List<int>[,] sudokugrid = new List<int>[9, 9];

        public SudokuFC()
        {
        }

        public void Fill(string raw)
        {
            
            raw = raw.Replace(" ", String.Empty);           
            char[] charArr = raw.ToCharArray();             
            int counter = 0;                                
            
            for(int i = 0; i < 9; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    sudokugrid[i, j] = new List<int>();
                }
            }

            for (int i = 0; i < 9; i++)                      
            {
                for (int j = 0; j < 9; j++)
                {
                    if (Convert.ToInt32(charArr[counter].ToString()) != 0)
                    {
                        sudokugrid[i, j].Add(Convert.ToInt32(charArr[counter].ToString()));            
                    }
                    else
                    {
                        for(int q = 1; q <=9; q++)
                        {
                            sudokugrid[i, j].Add(q);
                        }
                    }
                    counter++;                                                                  
                }
            }
        }

    }
}
