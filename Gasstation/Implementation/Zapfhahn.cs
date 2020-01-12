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
//      class:              Zapfhahn
//      classDescription:   Helper Class which holds the Fueltype




namespace Gasstation.Implementation
{
    public class Zapfhahn
    {

        /// <summary>
        /// The Fueltype of this Zapfhahn
        /// </summary>
        private readonly IFuelType fueltype;

        /// <summary>
        ///  constructor for the Zapfhahn
        /// </summary>
        /// <param name="fuelType">Fueltype which the gas nozzle will dispense</param>

        public Zapfhahn(IFuelType fuelType)
        {
            this.fueltype = fuelType;
        }

        /// <summary>
        /// Gets the Fueltype of this Zapfhahn
        /// </summary>
        /// <returns>the Fueltype</returns>
        public IFuelType GetFuelType()
        {
            return fueltype;
        }
       
    }   
}
