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
        private readonly string name;

        private bool lockStatus = false;

        private bool tankingState = false;

        private DispatcherTimer tankingTimer;

        private List<Zapfhahn> zapfhaehne;

        private Zapfhahn selectedZapfhahn;

        private int currentFuelTransactionAmountOfLiter;

        private FuelType currentFuelTransactionFuelType;

        public Zapfsaeule(string name, List<Zapfhahn> zapfhaehne)
        {
            this.zapfhaehne = zapfhaehne;
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }

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
            currentFuelTransactionAmountOfLiter = 0;
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
        /// <returns></returns>
        public Transaction StopTankingTimer()
        {
            this.tankingTimer.Stop();
            this.tankingState = false;
            return new Transaction(
                this.currentFuelTransactionFuelType.GetCostPerLiterInCent(), 
                this.currentFuelTransactionAmountOfLiter, 
                this.currentFuelTransactionFuelType, 
                this.name, 
                () => this.Unlock());
        }

        public bool isTanking()
        {
            return this.tankingState;
        }

        public int GetCurrentTransactionFuelAmount()
        {
            return this.currentFuelTransactionAmountOfLiter;
        }

      

    }
}
