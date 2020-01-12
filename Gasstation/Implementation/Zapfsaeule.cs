//
//      author:             Thomas Fischer  t.fischer@siemens.com
//      date:               11/1/2020   
//      projectname:        tankstelle / Gasstation
//      version:            1.0
//      description:        a framework for a Gasstation application. 
//                          Based on a GUI on WPF Pages
//                          Warning this is an explorative code and may have instabilities dead code or wrong design decisions
//                          
//
//      class:              
//      classDescription:   



using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace Gasstation.Implementation
{
    public class Zapfsaeule
    {

        /// <summary>
        /// The Name of this Zapfsaeule which is displayed on the GUI
        /// </summary>
        private readonly string name;

        /// <summary>
        /// the Lockstate of the Zapfsaeule to check if the Zapfsaeule is in use already
        /// </summary>
        private bool lockStatus = false;

        /// <summary>
        /// the tanking state of the Zapfsaeule to check if its currently tanking
        /// </summary>
        private bool tankingState = false;

        /// <summary>
        /// a Dispatcher Timer which creates a gradual tanking process
        /// </summary>
        private DispatcherTimer tankingTimer;

        /// <summary>
        /// a List of Zapfhaehne available on this Zapfsaeule
        /// </summary>
        private List<Zapfhahn> zapfhaehne;

        /// <summary>
        /// the current selected Zapfhahn for fuel draining
        /// </summary>
        private Zapfhahn selectedZapfhahn;

        /// <summary>
        /// the amount of liters taken during this current unwritten Transaction
        /// </summary>
        private int currentFuelTransactionAmountOfLiter;

        /// <summary>
        /// the current selected Fueltype for draining the tank
        /// </summary>
        private FuelType currentFuelTransactionFuelType;

        /// <summary>
        /// Constructor to create Zapfsaeule at Programm start
        /// </summary>
        /// <param name="name">the name of the zapfsaeule</param>
        /// <param name="zapfhaehne">a list of zapfhaehne</param>
        public Zapfsaeule(string name, List<Zapfhahn> zapfhaehne)
        {
            this.zapfhaehne = zapfhaehne;
            this.name = name;
        }

        public Zapfhahn Zapfhahn
        {
            get => default;
            set
            {
            }
        }

        /// <summary>
        /// Gets the Name of the Zapfsaeule
        /// </summary>
        /// <returns>the Name of the Zapfsaeule</returns>
        public string GetName()
        {
            return name;
        }

        /// <summary>
        /// gets the list of Zapfhaehne
        /// </summary>
        /// <returns>the list of Zapfhaehne</returns>
        public List<Zapfhahn> GetZapfhaene()
        {
            return zapfhaehne;
        }

        /// <summary>
        /// sets a Zapfhahn as the currently selected one
        /// </summary>
        /// <param name="requestedZapfhahn">the Zapfhhan to set as selected</param>
        public void Selectzapfhahn(Zapfhahn requestedZapfhahn)
        {

            this.selectedZapfhahn = requestedZapfhahn;
        }

        /// <summary>
        /// get the selected zapfhahn
        /// </summary>
        /// <returns>the currently selected Zapfhahn</returns>
        public Zapfhahn GetSelectedZapfhahn()
        {
            return this.selectedZapfhahn;
        }


        /// <summary>
        /// locks the Zapfsaeule
        /// </summary>
        public void Lock()
        {
            this.lockStatus = true;
        }

        /// <summary>
        ///  unlocks the zapfsaeule
        /// </summary>
        public void Unlock()
        {
            this.lockStatus = false;
            currentFuelTransactionAmountOfLiter = 0;
        }

        /// <summary>
        ///  gets the corresponding FuelTank with the Fueltype provided
        /// </summary>
        /// <param name="fueltype">the fueltype which is requested</param>
        public void GetFuelOfType(FuelType fueltype)
        {
            Zapfhahn zapfhahn = zapfhaehne.Find(x => fueltype == x.GetFuelType());
        
        }

        /// <summary>
        /// checks if the Zapfsaeule is locked
        /// </summary>
        /// <returns>boolean of if it is locked</returns>
        public bool isLocked()
        {
            return lockStatus;
        }

        /// <summary>
        /// Starts the Tanking Process and calls upon a Callback Method to Update the GUI
        /// </summary>
        /// <param name="currentFuelTank">the current selected fueltank</param>
        /// <param name="callback">the callback to update the gui</param>
        public void StartTankingTimer(FuelTank currentFuelTank, Action SaveFuelTanks)
        {
            this.currentFuelTransactionAmountOfLiter = 0;
            this.currentFuelTransactionFuelType = currentFuelTank.GetFuelType();
            this.tankingState = true;
            this.tankingTimer = new DispatcherTimer();
            this.tankingTimer.Tick += (s, e) =>
            {
                this.currentFuelTransactionAmountOfLiter += currentFuelTank.DrainFuel(1);
                SaveFuelTanks();
            };
            this.tankingTimer.Interval = TimeSpan.FromSeconds(1);
            this.tankingTimer.Start();
        }

        /// <summary>
        /// Stops the timer and the Tanking Process and returns a Transaction
        /// </summary>
        /// <returns>newly created unpaid transaction</returns>
        public Transaction StopTankingTimer()
        {
            this.tankingTimer.Stop();
            this.tankingState = false;
            return new Transaction(
                this.currentFuelTransactionFuelType.GetCostPerLiterInCent(), 
                this.currentFuelTransactionAmountOfLiter, 
                this.currentFuelTransactionFuelType, 
                this.name);
        }

        /// <summary>
        /// checks if the Zapfsaeule is tanking
        /// </summary>
        /// <returns>the tanking state as bool</returns>
        public bool isTanking()
        {
            return this.tankingState;
        }

        /// <summary>
        /// gets the currently drained amount of liters
        /// </summary>
        /// <returns>the fuelamount currently drained</returns>
        public int GetCurrentTransactionFuelAmount()
        {
            return this.currentFuelTransactionAmountOfLiter;
        }

      

    }
}
