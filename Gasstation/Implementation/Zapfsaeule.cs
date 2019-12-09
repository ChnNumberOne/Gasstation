using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

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

        private Timer tankingTimer;

        private List<Zapfhahn> zapfhaehne;

        private Zapfhahn selectedZapfhahn;

        private int currentFuelTransaction = 0;


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
            
        public void StartTankingTimer(FuelTank currentFuelTank)
        {
            this.tankingState = true;
            this.tankingTimer = new Timer();
            this.tankingTimer.Elapsed += (s, e) => currentFuelTank.DrainFuel(1);
            this.tankingTimer.Interval = 100;
            this.tankingTimer.Enabled = true;
        }
        public void StopTankingTimer()
        {
            this.tankingTimer.Stop();
        }

        public bool isTanking()
        {
            return this.tankingState;
        }
        
      

















        public int GetCurrentFuelTransaction()
        {
            return this.currentFuelTransaction;
        }











    }
}
