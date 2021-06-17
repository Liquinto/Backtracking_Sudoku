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
        public Backtracking_FC()        //dit is de constructor methode voor het backtracking met forward check algorithme
        {
        }

        public bool Algorithm(SudokuFC sud, Sudoku sudoku)
        {
            /*Dit algorithme werkt bijna hetzelfde als het algorithme van gewoon backtracking. Het verschil tussen de twee is dat het gewone backtracking algorithme op elke plek alle getallen van
             *1 t/m 9 gaat proberen in te vullen, het algorithme met forward checking heeft vooraf een check voor restricties, komt het getal 1 al voor in de box, dan gaat hier niet op gecheckt worden.
             *Komt het getal 3 al voor in de rij, dan gaat hier niet op gecheckt worden, en komt het getal 5 al voor in de kolom, ook dan wordt hier niet op gecheckt. Dit zorgt ervoor dat het algorithme
             *minder staten hoeft te onderzoeken, en dus dat het algorithme, in ieder geval op grotere en lastigere sudoku's, sneller werkt.
             */
            Tuple<int, int> possud = Find_Zeros(sudoku);
            if(possud == null)              //we kijken of er nog een 0 in de sudoku zit, zodra dat niet meer het geval is dan termineren we, omdat de sudoku is opgelost
            {
                return true;
            }
            for (int i = 0; i < sud.sudokugrid[possud.Item1, possud.Item2].Count; i++)      //we loopen hier over alle mogelijkheden in de lijst van een bepaalde cell heen, dit wordt zoals in de opdracht beschreven van links naar rechts, van onder naar boven gedaan.
            {
                int value = sud.sudokugrid[possud.Item1, possud.Item2][i];      //we maken even een variabele aan voor deze waarde.
                if(Check(sudoku, value, possud))                                //we checken of deze variabele nog ingevuld mag worden, ivm met andere waarden die zijn ingevuld
                {
                    sudoku.sudokugrid[possud.Item1, possud.Item2] = value;      //De cell in de sudoku wordt aangepast naar het nummer dat is uitgekozen uit de lijst.
                    AddConstraint(possud, sud, value);
                    if (Algorithm(sud, sudoku))                                  //recursieve aanroep
                    {
                        return true;
                    }
                    RemoveConstraint(possud, sud, sudoku.sudokugrid[possud.Item1, possud.Item2]);
                    sudoku.sudokugrid[possud.Item1, possud.Item2] = 0;          //als er een fout is gemaakt zetten we de waarde terug en proberen het op een andere manier
                   
                }
            }

            return false;
        }

        public void Constraint(SudokuFC sud)
        {
            /*Met het vullen van de SudokuFC wordt elke niet gegeven cell gevuld met de cijfers 1 t/m 9, in deze fucntie wordt er over heel de sudoku gelooped
             *wanneer wij een lijst tegenkomen die maar 1 element, dan weten we dat deze al is gegeven door de sudoku, heeft dan loopen wij door zijn rij, kolom en block heen
             *en verwijderen we deze uit de lijst met mogelijkheden voor die cell.  
             */
            //check hier alle rijen, kolommen en blokken zodat de cijfers die het sowieso niet kunnen zijn uit de juiste lijsten gehaald kunnen worden.
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (sud.sudokugrid[i, j][sud.sudokugrid[i, j].Count - 1] != 0)
                    {
                        //verwijderd cijfers die sowieso niet in een rij voor kunnen komen, omdat ze al eerder in de rij voorkomen
                        for (int row = 0; row < 9; row++)
                        {
                            if (sud.sudokugrid[i, row].Count > 0 && sud.sudokugrid[i, row][sud.sudokugrid[i, row].Count - 1] != 0)
                            {
                                sud.sudokugrid[i, row].Remove(sud.sudokugrid[i, j][0]);
                            }
                        }
                        //verwijderd cijfers die sowieso niet in een kolom kunnen voorkomen omdat ze al eerder in de kolom voorkomen
                        for (int colom = 0; colom < 9; colom++)
                        {
                            if (sud.sudokugrid[colom, j].Count > 0 && sud.sudokugrid[colom, j][sud.sudokugrid[colom, j].Count - 1] != 0)
                            {
                                sud.sudokugrid[colom, j].Remove(sud.sudokugrid[i, j][0]);
                            }
                        }
                        //verwijderd cijfers in de blokken, omdat deze al een keer eerder in de blokken zijn voorgekomen
                        for (int rowx = (j / 3) * 3; rowx < (j / 3) * 3 + 3; rowx++)                                  
                        {
                            for (int colomy = (i / 3) * 3; colomy < (i / 3) * 3 + 3; colomy++)                              
                            {
                                if (sud.sudokugrid[rowx, colomy].Count > 0 && sud.sudokugrid[rowx, colomy][sud.sudokugrid[rowx, colomy].Count - 1] != 0)
                                {
                                    sud.sudokugrid[rowx, colomy].Remove(sud.sudokugrid[i, j][0]);
                                }
                            }
                        }
                    }
                }
            }

        }

        public void AddConstraint(Tuple<int,int> position, SudokuFC sud, int value)
        {
            //het ingevulde cijfer moet van de lijsten van de cellen in de rij, kolom en blok gehaald worden
            int posx = position.Item1;
            int posy = position.Item2;

 
            if (sud.sudokugrid[posx, posy].Count > 0 && sud.sudokugrid[posx, posy][sud.sudokugrid[posx, posy].Count - 1] != 0)
            {
                //verwijderd cijfers in de rij
                for(int row = 0; row < 9; row++)
                {
                    if(sud.sudokugrid[posx, row].Count > 0 && sud.sudokugrid[posx,row][sud.sudokugrid[posx, row].Count - 1] != 0)
                    {
                        if (!sud.sudokugrid[posx, row].Contains(value))
                        {
                            sud.sudokugrid[posx, row].Remove(value);
                        }
                    }
                }
                //verwijderd cijfers in de kolom
                for(int colom = 0; colom < 9; colom++)
                {
                    if(sud.sudokugrid[colom, posy].Count > 0 && sud.sudokugrid[colom, posy][sud.sudokugrid[colom, posy].Count - 1] != 0)
                    {
                        if (!sud.sudokugrid[colom, posy].Contains(value))
                        {
                            sud.sudokugrid[colom, posy].Remove(value);
                        }
                    }
                }

                
                //verwijderd cijfers in het blok
                for (int rowx = (position.Item2 / 3) * 3; rowx < (position.Item2 / 3) * 3 + 3; rowx++)
                {
                    for (int colomy = (position.Item1 / 3) * 3; colomy < (position.Item1 / 3) * 3 + 3; colomy++)
                    {
                        if (sud.sudokugrid[rowx, colomy].Count > 0 && sud.sudokugrid[rowx, colomy][sud.sudokugrid[rowx, colomy].Count - 1] != 0)
                        {
                            if (!sud.sudokugrid[rowx, colomy].Contains(value))
                            {
                                sud.sudokugrid[rowx, colomy].Remove(value);
                            }
                        }
                    }
                }
            }
          

        }
        public void RemoveConstraint(Tuple<int,int> position, SudokuFC sud, int value)
        {
            //het ingevulde cijfer moet van de lijsten van de cellen in de rij, kolom en blok gehaald worden
            int posx = position.Item1;
            int posy = position.Item2;


            if (sud.sudokugrid[posx, posy].Count > 0 && sud.sudokugrid[posx, posy][sud.sudokugrid[posx, posy].Count - 1] != 0)              //hier nog comments
            {
                //verwijderd cijfers in de rij
                for (int row = 0; row < 9; row++)
                {
                    if (sud.sudokugrid[posx, row].Count > 0 && sud.sudokugrid[posx, row][sud.sudokugrid[posx, row].Count - 1] != 0)
                    {
                        if (!sud.sudokugrid[posx, row].Contains(value))
                        {
                            sud.sudokugrid[posx, row].Insert(0,value);
                        }
                    }
                }
                //verwijderd cijfers in de kolom
                for (int colom = 0; colom < 9; colom++)
                {
                    if (sud.sudokugrid[colom, posy].Count > 0 && sud.sudokugrid[colom, posy][sud.sudokugrid[colom, posy].Count - 1] != 0)
                    {
                        if (!sud.sudokugrid[colom, posy].Contains(value))
                        {
                            sud.sudokugrid[colom, posy].Insert(0,value);
                        }
                    }
                }


                //verwijderd cijfers in het blok
                for (int rowx = (position.Item2 / 3) * 3; rowx < (position.Item2 / 3) * 3 + 3; rowx++)
                {
                    for (int colomy = (position.Item1 / 3) * 3; colomy < (position.Item1 / 3) * 3 + 3; colomy++)
                    {
                        if (sud.sudokugrid[rowx, colomy].Count > 0 && sud.sudokugrid[rowx, colomy][sud.sudokugrid[rowx, colomy].Count - 1] != 0)
                        {
                            if (!sud.sudokugrid[rowx, colomy].Contains(value))
                            {
                                sud.sudokugrid[rowx, colomy].Insert(0,value);
                            }
                        }
                    }
                }
            }
        }
    }
}
