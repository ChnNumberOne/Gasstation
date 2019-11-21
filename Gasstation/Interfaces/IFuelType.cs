namespace Gasstation.Implementation
{
    public interface IFuelType
    {
        

        string GetFuelTypeName();

        int GetCostPerLiterInCent();

        void SetCostPerLiterInCent(int newCostPerLiterInCent);


    }
}