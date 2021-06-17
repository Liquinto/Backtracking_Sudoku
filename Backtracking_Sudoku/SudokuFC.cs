using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Backtracking_Sudoku
{
    class SudokuFC
    {
        public List<int>[,] sudokugrid = new List<int>[9, 9];       //dit is een temp sudoku, waarin niet de waarde van de cel wordt opgeslagen, maar de waarden die volgens de sudoku regels ingevuld mogen worden in de cell

        public SudokuFC()               //constructor functie van SudokuFC
        {
        }

        public void Fill(string raw)
        {
            /*
             *Deze functie vult de sudoku op dezelfde manier als de functie Fill in Sudoku
             *In plaats van een 0 of het cijfer in te vullen op de plek van de cell wordt er nu of het cijfer toegevoegd aan een lijst, wanneer het getal niet 0 is,
             *of wanneer het getal 0 is wordt de lijst gevuld met de cijfers 1 t/m 9.
             */
            raw = raw.Replace(" ", String.Empty);           
            char[] charArr = raw.ToCharArray();             
            int counter = 0;                                
            
            //de lijsten in de array worden geinitialiseerd.
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
                    //als het getal niet 0 is, wordt alleen dit getal aan de lijst toegevoegd van de desbetreffende cell
                    if (Convert.ToInt32(charArr[counter].ToString()) != 0)
                    {
                        sudokugrid[i, j].Add(Convert.ToInt32(charArr[counter].ToString()));
                        sudokugrid[i, j].Add(0);
                    }
                    //als het getal 0 is gaat de lijst hier gevuld worden met de cijfers 1 t/m 9
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
