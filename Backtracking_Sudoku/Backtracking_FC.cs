using System;
using System.Collections.Generic;
using System.Text;

namespace Backtracking_Sudoku
{
    class Backtracking_FC : Backtracking
    {
        /*De backtracking_FC klasse lijkt erg op de normale backtracking klasse, hij inherit ook de functies van de gewone Backtracking klasse. 
         * Waar deze klasse in verschilt is dat hij een apart object van het type SudokuFC bijhoudt. In deze klasse worden ipv nummers een lijst met mogelijke nummers op een bepaalde positie opgeslagen.
         * Dit betekend dus dat ons algorithme niet bij elke cel van 1-9 hoeft te loopen, maar wanneer er bijvoorbeeld al een 1 in het block zit wordt deze waarde niet geprobeerd.
         * Dit zorgt er dus voor dat er minder vertakkingen zijn, dit betekend dat moeilijkere en grotere sudokus sneller opgelost worden. Het kan echter wel zo zijn dat deze methode iets langzamer is bij
         * makkelijkere sudoku's, dit komt omdat er extra complexiteit komt kijken bij het analyseren van het probleem.
         */
        public Backtracking_FC()
        {
        }

        public bool Algorithm(SudokuFC sud, Sudoku sudoku)
        {
            /*
             * 
             */
            Tuple<int, int> possud = Find_Zeros(sudoku);
            if(possud == null)
            {
                return true;
            }
            for (int i = 0; i < sud.sudokugrid[possud.Item1, possud.Item2].Count; i++)
            {
                int value = sud.sudokugrid[possud.Item1, possud.Item2][i];
                if(Check(sudoku, value, possud))
                {
                    sudoku.sudokugrid[possud.Item1, possud.Item2] = value;
                    if(Algorithm(sud, sudoku))
                    {
                        return true;
                    }
                    sudoku.sudokugrid[possud.Item1, possud.Item2] = 0;
                }
            }

            return false;
        }

        public void Constraint(SudokuFC sud)
        {
            //check hier alle rijen, kolommen en blokken zodat de cijfers die het sowieso niet kunnen zijn uit de juiste lijsten gehaald kunnen worden.
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (sud.sudokugrid[i, j].Count == 1)
                    {
                        //verwijderd cijfers die sowieso niet in een rij voor kunnen komen, omdat ze al eerder in de rij voorkomen
                        for (int row = 0; row < 9; row++)
                        {
                            if (sud.sudokugrid[i, row].Count > 1)
                            {
                                sud.sudokugrid[i, row].Remove(sud.sudokugrid[i, j][0]);
                            }
                        }
                        //verwijderd cijfers die sowieso niet in een kolom kunnen voorkomen omdat ze al eerder in de kolom voorkomen
                        for (int colom = 0; colom < 9; colom++)
                        {
                            if (sud.sudokugrid[colom, j].Count > 1)
                            {
                                sud.sudokugrid[colom, j].Remove(sud.sudokugrid[i, j][0]);
                            }
                        }
                        //verwijderd cijfers in de blokken, omdat deze al een keer eerder in de blokken zijn voorgekomen
                        for (int rowx = (j / 3) * 3; rowx < (j / 3) * 3 + 3; rowx++)                                  //wiskundige representatie om te kijken over welke rij blokken het gaat, en daarna de eerstvolgende 3 te pakken.
                        {
                            for (int colomy = (i / 3) * 3; colomy < (i / 3) * 3 + 3; colomy++)                              //wiskundige representatie om te kijken over welke kolom blokken het gaat, en daarvan de eerstvolgende 3 te pakken.
                            {
                                if (sud.sudokugrid[rowx, colomy].Count > 1)
                                {
                                    sud.sudokugrid[rowx, colomy].Remove(sud.sudokugrid[i, j][0]);
                                }
                            }
                        }
                    }
                }
            }

        }



    }
}
