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

        [XmlAttribute]
        public int Value { get; set; }

        [XmlAttribute]
        public int MaximumFill { get; set; }

        [XmlAttribute]
        public int MinimumFill { get; set; }

        [XmlAttribute]
        public int CurrentFill { get; set; }

        [XmlAttribute]
        public int TopWarnLevel { get; set; }

        [XmlAttribute]
        public int BottomWarnLevel { get; set; }

        public Container()
        {

        }

        public Container(int value, int minimumFill, int maximumFill, int bottomWarnLevel, int topWarnLevel, int currentFill)
        {
            this.Value = value;
            this.MinimumFill = minimumFill;
            this.MaximumFill = maximumFill;
            this.BottomWarnLevel = bottomWarnLevel;
            this.TopWarnLevel = topWarnLevel;
            this.CurrentFill = currentFill;
        }


        /// <summary>
        /// entfernt eine Münze aus dem Behälter
        /// </summary>
        public void RemoveCoin()
        {
            this.CurrentFill -= 1;
        }

        /// <summary>
        /// addiert eine Münze zum Behälter
        /// </summary>
        public void AddCoin()
        {
            this.CurrentFill += 1;
        }

        /// <summary>
        /// Gibt den Füllstand in Prozent für diesen Behälter aus
        /// </summary>
        /// <returns></returns>
        public int GetPercentCoin()
        {
            float percentCoin = (float) CurrentFill / (float) MaximumFill * (float)100;
            return (int) percentCoin;
        }

        /// <summary>
        /// Gibt die Menge der vorhandenen Mpnzen aus
        /// </summary>
        /// <returns></returns>
        public int GetCoinAmount()
        {

            return CurrentFill;
        }

        /// <summary>
        /// Gibt den Maximalfüllstand der eingestellt wurde aus (ABSOLUTE KAPAZITÄT)
        /// </summary>
        /// <returns></returns>
        public int GetMaxmimumFill()
        {
            return this.MaximumFill;
        }

        /// <summary>
        /// gibt die Wertung dieses Behälters aus ( 1Fr. 50 rappen etc)
        /// </summary>
        /// <returns></returns>
        public int GetValue()
        {
            return this.Value;
        }

        /// <summary>
        /// setzen des momentanigen Füllstandes ( TestZwekce)
        /// </summary>
        /// <param name="currentFill"></param>
        public void SetCurrentFill(int currentFill)
        {
            this.CurrentFill = currentFill;
        }

        /// <summary>
        /// Gibt den momentanigen Füllstand aus (ABSOLUT)
        /// </summary>
        /// <returns></returns>
        public int GetCurrentFill()
        {
            return this.CurrentFill;
        }

        /// <summary>
        /// Gibt den oberen Warn Leven aus ( prozentuler Wert)
        /// </summary>
        /// <returns></returns>
        public int GetTopWarnLevel()
        {
            return this.TopWarnLevel;
        }

        /// <summary>
        /// gibt den unteren warn level aus ( prozentualer Wert)
        /// </summary>
        /// <returns></returns>
        public int GetBottomWarnLevel()
        {
            return this.BottomWarnLevel;
        }
    }
}
