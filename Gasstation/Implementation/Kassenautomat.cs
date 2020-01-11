//
//                          LEGACY CODE
//
//      author:             Thomas Fischer  t.fischer@siemens.com
//      date:               27/10/2019      
//      projectname:        kassenautomat
//      version:            1.0
//      description:        ein Programm das als Schnittstelle für einen Kassenautomat fungieren sollte
//                          ein und Ausgabe von Münzbeträgen und korrektes zuweisen von Münzen
//                          reguliert Münzbeträge für Selbsterhalt
//
//      klasse:             Kassenautomat
//      klasenbeschreibung: Hauptfunktionalität des Programmes mit seinen Algorithmen
//                          wird pro Maschine einmal erstellt.




using Gasstation.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gasstation
{
    public class Kassenautomat
    {

        public Kassenautomat(List<Container> cointypes, int maximumTotalValue)
        {

            cointypes.Sort((a, b) => -1 * a.GetValue().CompareTo(b.GetValue())); // descending sort so we always try to get the bigges coin first
            this.cointypes = cointypes;
            this.maximumTotalValue = maximumTotalValue;

        }

        private List<Container> cointypes;


        protected int valueInput;


        private int maximumTotalValue;
        


        /// <summary>
        /// Gibt das vom Kunden gewählte Produkt aus (WIP)
        /// </summary>
        /// <returns>gewähltes Produkt</returns>

        /// <summary>
        /// Gibt die Münzbehälter aus
        /// </summary>
        /// <returns>Liste aller Münzbehälter</returns>
        public List<Container> GetCointypes()
        {
            return this.cointypes;
        }


        /// <summary>
        /// Gibt den dem Kunden zugeordneten Betrag aus
        /// </summary>
        /// <returns>Betrag des Kudens</returns>
        public int GetValueInput()
        {
            return this.valueInput;

        }

        public void ResetAutomat()
        {
            this.valueInput = 0;
        }

        /// <summary>
        /// Gibt den Gesamten Wert des Automateninhalts in Rappen aus
        /// </summary>
        /// <returns>Automateninhalt in Rappen</returns>
        public int GetValueTotal()
        {
            int valueTotal = 0;
            foreach (Container ct in this.cointypes)
            {
                valueTotal += ct.GetCoinAmount() * ct.GetValue();
            }
            return valueTotal;
        }

        /// <summary>
        /// Gibt Prozentualen füllstand des gewählten Behälters an
        /// </summary>
        /// <param name="coinTypeValue">gewählter Behälter</param>
        /// <returns>Prozentualer füllstand des Behälters</returns>

        public int GetPercentCoin(int coinTypeValue)
        {
            int fillPercentage = -1;

            // check if requested cointype actually exists
            if (this.cointypes.Any(n => n.GetValue() == coinTypeValue))
            {

                // get the cointype of that value
                Container cointype = this.cointypes.Find(x => x.GetValue() == coinTypeValue);
                fillPercentage = cointype.GetPercentCoin();
            }

            return fillPercentage;
        }

        /// <summary>
        /// Gibt die Gesamtmenge aller vorhandener Münzen im Automaten zurück
        /// </summary>
        /// <returns>Menge an Münzen im Automate</returns>
        public int GetQuantityCoins()
        {
            int coinCounter = 0;
            foreach (Container cointype in this.cointypes)
            {
                coinCounter += cointype.GetCurrentFill();
            }
            return coinCounter;
        }

        /// <summary>
        /// Dies sollter später ein Eventhandler werden um eine Warnung auszugeben,
        /// falls ein Container auf zu tiefem füllstand ist.
        /// </summary>
        /// <param name="cointype">Münze fpr die Eventhandler registreirt wird</param>
        private void AlertCoinMinimum(int cointype)
        {
            Console.WriteLine(cointype + " reached bottom threshold");
        }

        /// <summary>
        /// Dies sollte später ein Eventhandler werden um eine Warnung auszugeben,
        /// falls ein Container auf hohem füllstand ist.
        /// </summary>
        /// <param name="cointype">Münze für die Eventhandler registriert wird</param>
        private void AlertCoinMaximum(int cointype)
        {
            Console.WriteLine(cointype + " reached top threshold");
        }

        /// <summary>
        /// Dies sollte später ein Eventhandler werden um eine Warnung auszugeben,
        /// falls der Automat den maximalen füllstand bald erreicht ( gewisser Frankenbetrag)
        /// </summary>
        /// <param name="totalCoinCount"></param>
        private void AlertValueTotalMaximum(int totalCoinCount)
        {
            Console.WriteLine("Kassenautomat voll");
        }

    
        /// <summary>
        /// nimmt einen Münzwert entgegen und überprüft ob dieser ein legaler Münztyp ist
        /// falls ja ordnet er diesen einem Behälter zu und gibt dem Kunden dies zu seinem
        /// wählbetrag => InputValue, mit dem er Produkte kaufen kann.
        /// </summary>
        /// <param name="coinValue">Betrag der vom Kunden eingeworfen wird</param>
        public void InsertCoin(int coinValue)
        {
            // check if inserted value is a legal coin
            if (this.cointypes.Any(n => n.GetValue() == coinValue)) {
                
                // get the cointype of that value
                Container cointype = this.cointypes.Find(x => x.GetValue() == coinValue);

                // check if the cointype is still able to take more
                if (cointype.GetMaxmimumFill() > cointype.GetCurrentFill())
                {
                    // add coin to container ( we dont care what is thrown in even if the customer wants his money back we give it back in a way that works for us)
                    // Example it doesnt matter if we get 2 * .50 we can give back 1 * 1.- or 2*.50 or 5*.20 or 10 *.10 or whatever works
                 
                    cointype.AddCoin();
                    this.valueInput += cointype.GetValue();

                }
                else
                {
                    Console.WriteLine("cant hold more coins of type ["+cointype.GetValue()+"]");
                }


            } else
            {
                Console.WriteLine("illegal cointype");
            }

        }

      


        /// <summary>
        /// Get Change überprüft den eingegeenen Betrag auf seine korrektheit und versucht Wechselgeld 
        /// in folgender Rangordnung auszugeben:
        /// 1. Selbsterhaltung durch gezieltes ausgeben überhöhter Münzbehälter
        /// 2. Bei komplexen Beträgen asugeben ohne Rücksicht auf Münzbehälter
        /// </summary>
        /// <param name="change">Wechselbetrag in Rappen</param>
        /// <returns>falls möglich eine Liste von Münzbeträgen</returns>
        public List<int> GetChange(int change)
        {
            // Validierung
            List<int> changeOutput = null;
            if ( change % 10 != 0 || change <= 0 || change == null)
            {
                
                Console.WriteLine("Illegal return");
                return changeOutput;
            }

            // priorisiere Ausgabe mit Selbsterhaltung
            changeOutput = SolvePriority(change);
            if(changeOutput != null)
            {
                // entferne die gefundenen Münzen
                Console.WriteLine("Priorisierte Ausgabe");
                foreach (int validCoin in changeOutput)
                {
                    Container cointype = this.cointypes.Find(x => x.GetValue() == validCoin);
                    cointype.RemoveCoin();
                }
                return changeOutput;
            }
            
            // falls Betrag nicht ausgegeben werden kann löse komplexe Beträge
            changeOutput = SolveBasic(change);
            if (changeOutput != null)
            {
                // entferne die gefundenen Münzen
                Console.WriteLine("Spezial Ausgabe");
                foreach (int validCoin in changeOutput)
                {
                    Container cointype = this.cointypes.Find(x => x.GetValue() == validCoin);
                    cointype.RemoveCoin();
                }
                return changeOutput;
            }
            // falls der Betrag nicht gelöst werden kann (Programmfehler)
            Console.WriteLine("Defekt bitte kontaktieren Sie uns über unsere Hotline: XXX-XXXXX-XXXX");
            return null;
        }

        /// <summary>
        /// SolveBasic gibt komplexe Münzbeträge aus die vom vereinfachten algorithmus nicht
        /// gelöst werden können ( zB die Problematik wenn keine 10 räppler mer vorhanden sind
        /// und ungerade Beträge aufgelöst werden müssen. Bezieht den Füllstand nur bedingt mit ein
        /// ( ist etwas vorhanden oder nicht).
        /// </summary>
        /// <param name="change">benötigter Wechselbetrag in Rappen</param>
        /// <returns>eine Liste von Münzbeträgen</returns>
        public List<int> SolveBasic(int change)
        {
            // Vorbehalt => kann wahrscheinlich verbssert werden bzw bräuchte deutlich mehr testgrundlage
            // sehr komplexer algorithmus

            // bleibe immer auf einem münzbehälter solange geht
            List<int> result = new List<int>();
            foreach (Container cointype in cointypes)
            {
                // solange etwas gemacht wurde
                bool actionTaken = true;
                while (actionTaken)
                {
                    // überprüfe ob der restbetrag abziehbar ist
                    if (change / cointype.GetValue() != 0 && cointype.GetCurrentFill() > 0)
                    {
                        // falls ungerade und 10 räppler leer
                        if((change / 10) % 2 != 0 && this.cointypes.Find(x => x.GetValue() == 10).GetCurrentFill() == 0)
                        {
                            Console.WriteLine("odd number that needs pre compensation");
                            Container cointype50 = this.cointypes.Find(x => x.GetValue() == 50);
                            if( change - cointype50.GetValue() >= 0)
                            {
                                Console.WriteLine("precompensation possible");
                                change -= cointype50.GetValue();
                                result.Add(cointype50.GetValue());

                            }
                        }

                        // Im falle eines 50ers
                        if (cointype.GetValue() == 50)
                        {
                            Console.WriteLine("asd");
                            // sind noch 10er vorhanden?
                            if(this.cointypes.Find(x => x.GetValue() == 10).GetCurrentFill() > 0){
                                result.Add(cointype.GetValue());
                                change -= cointype.GetValue();

                              
                            } else
                            {
                                
                                if ((change / 10) % 2 == 0)
                                {
                                    
                                    // 50 2mal abziehabr?
                                    if(change / (cointype.GetValue() * 2) != 0)
                                    {
                                        
                                        result.Add(cointype.GetValue());
                                        result.Add(cointype.GetValue());
                                        change -= cointype.GetValue() * 2;
                                        
                                    }
                                    else
                                    {
                                        actionTaken = false;
                                    }
                                }
                                else
                                {
                                    result.Add(cointype.GetValue());
                                    change -= cointype.GetValue();
                                }
                            }
                        }
                        else
                        {
                            result.Add(cointype.GetValue());
                            change -= cointype.GetValue();
                        }
                       
                        
                    } else
                    {
                        actionTaken = false;
                    }
                }
               
            }
            if (change == 0)
            {
                return result;
            }
            return null;
        }

        

        /// <summary>
        /// Solve Priority gibt mit einem etwas vereinfachten algorithmus möglichst viele Münzen aus
        /// Behältern, die einen Hohen Füllstand haben und möglichst wenige die einen Tiefstand haben
        /// </summary>
        /// <param name="change">benötigter wechselbetrag in rappen</param>
        /// <returns>eine Liste von Münzwerten</returns>
        public List<int> SolvePriority(int change)
        {
            int newChange = change;
            List<int> result = new List<int>();
            bool actionTaken;
               
            // löse soviele Münzen wie möglich mit Priorität
            foreach (Container ct in cointypes)
            {
                actionTaken = true;
                while (actionTaken)
                {
                    if (change >= ct.GetValue() && ct.GetPercentCoin() > ct.GetTopWarnLevel())
                    {
                        result.Add(ct.GetValue());
                        change -= ct.GetValue();
                    } else
                    {
                        actionTaken = false;
                    }
                }
                
            }

            // löse soviele Münzen wie möglich ohne tiefe Container zu benutzen
            foreach (Container ct in cointypes)
            {
                actionTaken = true;
                while (actionTaken) { 
                    if (change >= ct.GetValue() && (ct.GetPercentCoin() > ct.GetBottomWarnLevel()))
                    {
                       
                        result.Add(ct.GetValue());
                        change -= ct.GetValue();
                    } else
                    {
                        actionTaken = false;
                    }
                }
            }

            // löse soviele Münzen wie möglich ohne einschränkungen
            foreach (Container ct in cointypes)
            {
                actionTaken = true;
                while (actionTaken)
                {
                    if (change >= ct.GetValue() && ct.GetCoinAmount() > 0)
                    {
                        result.Add(ct.GetValue());
                        change -= ct.GetValue();
                    }
                    else
                    {
                        actionTaken = false;
                    }
                }
              
            }
         
            // wenn gelöst return liste
            if (change == 0)
            {
                Console.WriteLine("worked with simple algortihm");
                return result;
            }
            return null;

        }

        /// <summary>
        /// Umsetzung von AcceptValue wird nicht wirklich benötigt, 
        /// da die Zuweisung zum Münzbehälter direkt erfolgen kann wenn
        /// die Münze eingeworfen wird
        /// </summary>  
        public void AcceptValueInput()
        {
            Console.WriteLine("Input Accepted");
            this.valueInput = 0;
          
        }

        /// <summary>
        /// Umsetzung von NotAcceptValue wird nicht wirklich benötigt, 
        /// da die Zuweisung zum Münzbehälter direkt erfolgen kann wenn
        /// die Münze eingeworfen wird
        /// </summary>
        public void NotAcceptValueInput()
        {

            Console.WriteLine("Input Not Accepted");
            this.valueInput = 0;
        }
        
   
   
    }
}
