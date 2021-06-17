using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Backtracking_Sudoku
{
    class Backtracking
    {
        /*
         *  De Backtracking Klasse maakt aanpassingen in de sudoku grid, om precies te zijn doet de functie Algorithm dat. Deze functie heeft
         *  echter nog een aantal andere functies nodig om te zorgen dat de aanpassingen op de juiste manier gedaan worden.
        */
        public Backtracking()  //de constructor van een Backtracking object.
        {
        }

        public Tuple<int, int> Find_Zeros(Sudoku sud)   //de sudoku moet meegegeven worden aan deze functie, zodat de eerst volgende 0 gevonden kan worden.
        {
            /*
             * Deze functie kijkt wanneer de eerste 0 wordt gevonden, dit doet hij van links naar rechts en van boven naar onder zoals in de opdracht staat beschreven, 
             * Als de eerste 0 wordt gevonden wordt de locatie in een tuple teruggegeven door de functie
             */
            for(int i = 0; i < 9; i++)                  //de sudoku wordt van links naar rechts, en van boven naar onder doorgelopen zoals in de opdracht beschreven staat.
            {
                for (int j = 0; j < 9; j++)             
                {
                    if(sud.sudokugrid[i,j] == 0)        //slaagt zodra er een 0 wordt gevonden.
                    {
                        return new Tuple<int,int>(i, j);        //de positie van de eerst volgende 0 wordt gevonden in de sudoku, en de positie van die 0 wordt doorgegeven in een tuple.
                    }
                }
            }
            return null;                                //wanneer er geen 0 meer in de sudoku is, moet deze opgelost zijn en is het later belangrijk om null te returnen voor onze base case.
        }

        public bool Check(Sudoku sud, int value, Tuple<int,int> position)
        {
            /*Deze functie is bedoeld om te kijken of het cijfer dat ingevuld moet worden ingevuld mag worden op die plek.
             * 
             */
            for (int i = 0; i < 9; i++)
            {
                //check rij
                if (sud.sudokugrid[position.Item1, i] == value && position.Item1 != i)         //slaagt zodra i al in de rij staat, zonder zichzelf mee te rekenen (daar is het tweede stukje voor)
                {
                    return false;                                                              //returned false omdat dit getal niet geldig is om neer te zetten.
                }
            }
            //check kolom
            for (int i = 0; i < 9; i++)
            {
                if (sud.sudokugrid[i, position.Item2] == value && position.Item2 != i)          //slaagt zodra i al in de kolom staat, zonder zichzelf mee te rekenen (daar is het tweede stukje voor)
                {
                    return false;                                                               //returned false omdat dit getal niet geldig is om neer te zetten.
                }
            }

            int block_x = position.Item2 / 3;                                                   //variabelen worden aangemaakt om makkelijker in het block te kunnen checken
            int block_y = position.Item1 / 3;

            //check block
            for (int i = block_y * 3; i < block_y * 3 + 3; i++)                                  //wiskundige representatie om te kijken over welke rij blokken het gaat, en daarna de eerstvolgende 3 te pakken.
            {
                for (int j = block_x * 3; j < block_x * 3 + 3; j++)                              //wiskundige representatie om te kijken over welke kolom blokken het gaat, en daarvan de eerstvolgende 3 te pakken.
                {
                    if (sud.sudokugrid[i, j] == value && new Tuple<int, int>(i, j) != position)     //slaagt zodra de value al in het block te vinden is, zonder zijn eigen positie mee te rekenen.
                    {
                        return false;                                                           //als aan bovenstaande waarde wordt voldaan returned hij false omdat het niet geldig is het getal hier neer te zetten.
                    }
                }
            }
            return true;                                        //als geen van deze regels worden verbroken mag het getal dus hier neer gezet worden.
        }

        public bool Algorithm(Sudoku sud)
        {
            /*Hier is het backtracking algorithme, wat de sudoku gaat oplossen.
             *Het algorithme werkt recursief.
             */
            Tuple<int, int> pos = Find_Zeros(sud);              //de positie van de eerst volgende 0 wordt gevonden, als er geen nullen meer zijn wordt pos null
            if (pos == null)                                    //als pos null is betekend dit dat de sudoku is opgelost, dit is dus de base case
            {
                return true;                                    //we returnen true omdat de sudoku is opgelost
            }
            for(int i = 1; i <= 9; i++)                         //als we wel een positie hebben gaan we proberen de cijfers 1 t/m 9 in de rij in te voeren.
            {
                if(Check(sud, i, pos))                          //als het cijfer dat we willen invoeren op deze plek ingevoerd mag worden gaan we verder, anders proberen we een ander cijfer (i).
                {
                    sud.sudokugrid[pos.Item1, pos.Item2] = i;       //in de sudokugrid wordt de 0 aangepast naar het desbetreffende cijfer tussen 1 en 9 (i).
                    if (Algorithm(sud))                             //recursieve aanroep van het algorithme.
                    {
                        return true;                                //als algorithm(sud) klopt betekend dit dat we verder kunnen en gaan we met de eerstvolgende 0 verder.
                    }
                    sud.sudokugrid[pos.Item1, pos.Item2] = 0;           //als algorithm(sud) niet klopt betekend dit dat we ergens een foutje hebben gemaakt en gaan we backtracken
                }

            }
            return false;
        }
    }
}
