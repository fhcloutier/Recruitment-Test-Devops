using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {

        //items contains all the items to buy
        List<Item> items = new List<Item>();

        //add the table, paddle and balls with the required quantities
        items.Add(new Table());
        items.AddRange(Enumerable.Repeat(new Paddle(), 2));
        items.AddRange(Enumerable.Repeat(new Balls(), 3));

        //Display the price of each item in items
        foreach (Item item in items)
        {
            Console.WriteLine(item.ToString());
        }

        Console.ReadLine();
    }
}

public abstract class Item
{
    protected int price;
    protected float weight;
    abstract public float getFullPrice();
    public float Weight { get { return weight; } }

    public override string ToString()
    {
        return $"{this.GetType().Name}: {getFullPrice().ToString("c2")}$";
    }
}

public class TaxableItem : Item
{
    public override float getFullPrice()
    {
        return 1.2f * price;
    }
}

public class Table : TaxableItem
{
    public Table(int price = 600, float weight = 90)
    {
        this.price = price;
        this.weight = weight;
    }
}

public class Paddle : TaxableItem
{
    public Paddle(int price = 13, float weight = .2f)
    {
        this.price = price;
        this.weight = weight;
    }
}

public class Balls : TaxableItem
{
    public Balls(int price = 8, float weight = .06f)
    {
        this.price = price;
        this.weight = weight;
    }
}