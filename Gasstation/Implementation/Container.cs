//
//      author:             Thomas Fischer  t.fischer@siemens.com
//      date:               27/10/2019      
//      projectname:        kassenautomat
//      version:            1.0
//      description:        ein Programm das als Schnittstelle für einen Kassenautomat fungieren sollte
//                          ein und Ausgabe von Münzbeträgen und korrektes zuweisen von Münzen
//                          reguliert Münzbeträge für Selbsterhalt
//
//      klasse:             Container
//      klasenbeschreibung: Stellt einen Münzbehälter der Maschine dar
//                          können beliebig viele der Maschine zugewiesen werden


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Gasstation.Implementation
{
    [Serializable]
    public class Container
    {
        private enum Currency
        {
            CHF, EUR, USD
        };      

        private int value;

        private int maximumFill;     

        private int minimumFill;

        [XmlAttribute]
        private int currentFill;

        private int topWarnLevel;

        private int bottomWarnLevel;

        public Container()
        {

        }

        public Container(int value, int minimumFill, int maximumFill, int bottomWarnLevel, int topWarnLevel, int currentFill)
        {
            this.value = value;
            this.minimumFill = minimumFill;
            this.maximumFill = maximumFill;
            this.bottomWarnLevel = bottomWarnLevel;
            this.topWarnLevel = topWarnLevel;
            this.currentFill = currentFill;
        }


        /// <summary>
        /// entfernt eine Münze aus dem Behälter
        /// </summary>
        public void RemoveCoin()
        {
            this.currentFill -= 1;
        }

        /// <summary>
        /// addiert eine Münze zum Behälter
        /// </summary>
        public void AddCoin()
        {
            this.currentFill += 1;
        }

        /// <summary>
        /// Gibt den Füllstand in Prozent für diesen Behälter aus
        /// </summary>
        /// <returns></returns>
        public int GetPercentCoin()
        {
            float percentCoin = (float) currentFill / (float) maximumFill * (float)100;
            return (int) percentCoin;
        }

        /// <summary>
        /// Gibt die Menge der vorhandenen Mpnzen aus
        /// </summary>
        /// <returns></returns>
        public int GetCoinAmount()
        {

            return currentFill;
        }

        /// <summary>
        /// Gibt den Maximalfüllstand der eingestellt wurde aus (ABSOLUTE KAPAZITÄT)
        /// </summary>
        /// <returns></returns>
        public int GetMaxmimumFill()
        {
            return this.maximumFill;
        }

        /// <summary>
        /// gibt die Wertung dieses Behälters aus ( 1Fr. 50 rappen etc)
        /// </summary>
        /// <returns></returns>
        public int GetValue()
        {
            return this.value;
        }

        /// <summary>
        /// setzen des momentanigen Füllstandes ( TestZwekce)
        /// </summary>
        /// <param name="currentFill"></param>
        public void SetCurrentFill(int currentFill)
        {
            this.currentFill = currentFill;
        }

        /// <summary>
        /// Gibt den momentanigen Füllstand aus (ABSOLUT)
        /// </summary>
        /// <returns></returns>
        public int GetCurrentFill()
        {
            return this.currentFill;
        }

        /// <summary>
        /// Gibt den oberen Warn Leven aus ( prozentuler Wert)
        /// </summary>
        /// <returns></returns>
        public int GetTopWarnLevel()
        {
            return this.topWarnLevel;
        }

        /// <summary>
        /// gibt den unteren warn level aus ( prozentualer Wert)
        /// </summary>
        /// <returns></returns>
        public int GetBottomWarnLevel()
        {
            return this.bottomWarnLevel;
        }
    }
}
