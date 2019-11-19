using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_OOP_Testing.Klasses;
using WPF_OOP_Testing.Viewmodels;

namespace WPF_OOP_Testing.Models
{
    public class TankstelleTankZapfseuleModel : INotifyPropertyChanged
    {
        private Tankstelle tankstelle;
        private TankViewmodel tankViewmodel;
        private ZapfseulenViewmodel zapfseulenViewmodel;
        private Zapfseule zapfseule;
        private Treibstoff treibstoff;

        public event PropertyChangedEventHandler PropertyChanged;
        
        public Tankstelle Tankstelle
        {
            get
            {
                return tankstelle;
            }
            set
            {
                tankstelle = value;
                OnChanged();
            }
        }
        public TankViewmodel TankViewmodel
        {
            get
            {
                return tankViewmodel;
            }
            set
            {
                tankViewmodel = value;
                OnChanged();
            }
        }
        public ZapfseulenViewmodel ZapfseulenViewmodel
        {
            get
            {
                return zapfseulenViewmodel;
            }
            set
            {
                zapfseulenViewmodel = value;
                OnChanged();
            }
        }
        public Zapfseule Zapfseule
        {
            get
            {
                return zapfseule;
            }
            set
            {
                zapfseule = value;
                OnChanged();
            }
        }
        public Treibstoff Treibstoff
        {
            get
            {
                return treibstoff;
            }
            set
            {
                treibstoff = value;
                OnChanged();
            }
        }

        void OnChanged(string pn = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(pn));
            }
        }
    }
}
