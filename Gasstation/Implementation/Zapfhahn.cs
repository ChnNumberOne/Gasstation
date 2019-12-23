namespace Gasstation.Implementation
{
    public class Zapfhahn
    {
        // Important Notes and TOOD:
        // each Zapfhahn has an entire list of Tanks ( Because its easier)

      

        private readonly IFuelType fueltype;

        /// <summary>
        /// Erstellt einen Zapfhahn für einen STRING fueltypeName
        /// Sucht den Tank dieses fuelTypes heraus und bindet diese auf den zapfhahn
        /// </summary>
        /// <param name="fuelType"></param>

        public Zapfhahn(IFuelType fuelType)
        {
            this.fueltype = fuelType;
        }

        public IFuelType GetFuelType()
        {
            return fueltype;
        }
        
        /// <summary>
        /// Boolean Check if this is locked
        /// </summary>
        /// <returns></returns>
  
       
    }   
}
