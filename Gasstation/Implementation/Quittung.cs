using System;

public class Quittung
{
    // saves the value of cost in CHF
    private float receiptCost;

    // date of purchase
    private DateTime date;

	public Quittung(float cost, DateTime date)
	{
        receiptCost = cost;
        this.date = date;
	}

    public float GetCostValue()
    {
        return receiptCost;
    }
}
