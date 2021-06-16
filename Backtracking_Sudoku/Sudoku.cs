using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Backtracking_Sudoku
{
    class Sudoku
    {
        public int[,] sudokugrid = new int[9, 9];       //elke sudoku heeft een 2 dimensionale array waarin alle cellen gerepresenteerd worden door een waarde

        public Sudoku()
        {
        }

        public void Fill(string raw)
        {
            //de sudoku grid gaat hier gevuld worden met de meegegeven sudoku
            raw = raw.Replace(" ", String.Empty);           //alle spaties worden uit de string gehaald
            char[] charArr = raw.ToCharArray();             //van de string wordt een array gemaakt
            int counter = 0;                                //counter wordt op 0 geinitialiseerd

            for(int i = 0; i < 9; i++)                      //sudokugrid wordt van links naar recht van boven naar onder ingevuld.
            {
                for(int j = 0; j < 9; j++)
                {
                    sudokugrid[i, j] = Convert.ToInt32(charArr[counter].ToString());            //op positie i,j wordt de bijbehorende value gezet
                    counter++;                                                                  //de counter wordt opgehoogd zodat we de juiste waarden uit de array pakken.
                }
            }
        }

        public void Print_Sudoku()
        {
            //manier om de sudoku op een goed overzichtbare en leesbare manier weer te geven
            for (int i = 0; i < 9; i++)
            {
                if (i % 3 == 0 && i != 0)
                {
                    Console.WriteLine("------------------------");
                }
                for(int j = 0; j < 9; j++)
                {
                    if (j % 3 == 0 && j != 0)
                    {
                        Console.Write(" | ");
                    }
                    if(j == 8)
                    {
                        Console.WriteLine(sudokugrid[i,j].ToString());
                    }
                    else
                    {
                        Console.Write(sudokugrid[i,j].ToString() + " ");
                    }
                }
           
            }
        }

    }




}
