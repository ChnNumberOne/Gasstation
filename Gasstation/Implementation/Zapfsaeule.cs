using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Threading;

namespace Gasstation.Implementation
{
    public class Zapfsaeule
    {

        public Zapfsaeule(List<Zapfhahn> zapfhaehne)
        {
            this.zapfhaehne = zapfhaehne;

        }

        private bool lockStatus = false;

        private bool tankingState = false;

        private DispatcherTimer tankingTimer;

        private List<Zapfhahn> zapfhaehne;

        private Zapfhahn selectedZapfhahn;

        private int currentFuelTransactionAmountOfLiter;

        private FuelType currentFuelTransactionFuelType;

        private Transaction openTransaction;


        public List<Zapfhahn> GetZapfhaene()
        {
            return zapfhaehne;
        }

        public void Selectzapfhahn(Zapfhahn requestedZapfhahn)
        {

            this.selectedZapfhahn = requestedZapfhahn;
        }

        public Zapfhahn GetSelectedZapfhahn()
        {
            return this.selectedZapfhahn;
        }

        public void Lock()
        {
            this.lockStatus = true;
        }

        public void Unlock()
        {
            this.lockStatus = false;
        }

        public void GetFuelOfType(FuelType fueltype)
        {
            Zapfhahn zapfhahn = zapfhaehne.Find(x => fueltype == x.GetFuelType());
        
        }

        public bool isLocked()
        {
            return lockStatus;
        }


        /// <summary>
        /// Starts the Tanking Process and calls upon a Callback Method to Update the GUI
        /// </summary>
        /// <param name="currentFuelTank"></param>
        /// <param name="callback"></param>
        public void StartTankingTimer(FuelTank currentFuelTank, Action<FuelType,int,Zapfsaeule> callback, bool isContinuation = false)
        {
            // checks if this is just a continuation of an already started timer
            if (!isContinuation)
            {
                this.currentFuelTransactionAmountOfLiter = 0;
                this.currentFuelTransactionFuelType = currentFuelTank.GetFuelType();
                this.tankingState = true;
                this.tankingTimer = new DispatcherTimer();
                this.tankingTimer.Tick += (s, e) =>
                {
                    this.currentFuelTransactionAmountOfLiter += currentFuelTank.DrainFuel(1);
                    // WARNING -> DASS HIER IST EINE CALLBACK METHODE AUFS GUI DAS THIS BEDEUTET, DASS ES SICH UM DIESES OBJEKT HANDELT ALS EVENTABSENDER. DO NOT TOUCH WITHOUT ASKING ME ABOUT THIS
                    callback(currentFuelTank.GetFuelType(), this.currentFuelTransactionAmountOfLiter, this);
                };
                this.tankingTimer.Interval = TimeSpan.FromSeconds(1);
                this.tankingTimer.Start();
            }
            else if (this.tankingTimer != null)
            {
                this.tankingTimer.Tick += (s, e) =>
                {
                    this.currentFuelTransactionAmountOfLiter += currentFuelTank.DrainFuel(1);
                    callback(currentFuelTank.GetFuelType(), this.currentFuelTransactionAmountOfLiter, this);
                };
                this.tankingTimer.Interval = TimeSpan.FromSeconds(1);
            }
        }

        /// <summary>
        /// Stops the timer and the Tanking Process and returns a Transaction
        /// </summary>
        /// <returns></returns>
        public Transaction StopTankingTimer()
        {
            this.tankingTimer.Stop();
            this.tankingState = false;
            return new Transaction(this.currentFuelTransactionFuelType.GetCostPerLiterInCent(), this.currentFuelTransactionAmountOfLiter);
        }

        public bool isTanking()
        {
            return this.tankingState;
        }

 
















        public int GetCurrentFuelTransaction()
        {
            return this.currentFuelTransactionAmountOfLiter;
        }











    }
}
