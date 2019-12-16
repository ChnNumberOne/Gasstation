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


        public List<Zapfhahn> GetZapfhaene()
        {
            return zapfhaehne;
        }

        public void Selectzapfhahn(Zapfhahn requestedZapfhahn)
        {

            this.selectedZapfhahn = requestedZapfhahn;
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

        public void StartTankingTimer(FuelTank currentFuelTank, Action<FuelType,int> callback)
        {
            this.currentFuelTransactionAmountOfLiter = 0;
            this.currentFuelTransactionFuelType = currentFuelTank.GetFuelType();
            this.tankingState = true;
            this.tankingTimer = new DispatcherTimer();
            this.tankingTimer.Tick += (s, e) =>
            {
                this.currentFuelTransactionAmountOfLiter += currentFuelTank.DrainFuel(1);
                callback(currentFuelTank.GetFuelType(), this.currentFuelTransactionAmountOfLiter);
            };
            this.tankingTimer.Interval = TimeSpan.FromSeconds(1);
            this.tankingTimer.Start();
        }
        public Transaction StopTankingTimer()
        {
            this.tankingTimer.Stop();
            // Transaktion wird erstellt
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
