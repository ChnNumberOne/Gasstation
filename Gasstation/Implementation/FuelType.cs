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
//      class:              FuelType
//      classDescription:   This class handles the Data Persistency via Properties because this is required for application settings as delivered by .NET




using System.Xml.Serialization;

namespace Gasstation.Implementation
{
    public class FuelType : IFuelType
    {
        /// <summary>
        /// Cost of 1 Liter of Fuel
        /// Property to save with application settings
        /// </summary>
        [XmlAttribute]
        public int CostPerLiterInCent { get; set; }

        /// <summary>
        /// Name of the Fuel
        /// Property to save with application settings
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }


        /// <summary>
        /// Standard Constructor for the application settings
        /// </summary>
        public FuelType()
        {            
        }

        /// <summary>
        /// constructor to generate a fueltype during runtime or when settings missing
        /// </summary>
        /// <param name="name">the name of the fueltype</param>
        /// <param name="costPerLiterInCent">the cost of 1 liter of this fuel</param>
        public FuelType(string name, int costPerLiterInCent)
        {
            this.Name = name;
            this.CostPerLiterInCent = costPerLiterInCent;
        }

        /// <summary>
        /// returns the cost of 1 liter of this fuel
        /// </summary>
        /// <returns>the cost of 1 liter</returns>
        public int GetCostPerLiterInCent()
        {
            return this.CostPerLiterInCent;
        }

        /// <summary>
        /// returns the fuel type name
        /// </summary>
        /// <returns>the name of the fueltype</returns>
        public string GetFuelTypeName()
        {
            return this.Name;
        }

        /// <summary>
        /// sets the new cost for one liter of fuel
        /// </summary>
        /// <param name="newCostPerLiterInCent">new cost of 1 liter</param>
        public void SetCostPerLiterInCent(int newCostPerLiterInCent)
        {
            this.CostPerLiterInCent = newCostPerLiterInCent;
        }

        
    }
}
