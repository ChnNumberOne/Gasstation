using System;

public class Class1
{
    // saves the value of cost in CHF
    private float receiptCost;

    // date of purchase
    private DateTime date;

	public Class1(float cost, DateTime date)
	{
        receiptCost = cost;
        this.date = date;
	}

    public float GetCostValue()
    {
        return receiptCost;
    }
}
