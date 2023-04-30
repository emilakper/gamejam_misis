using System.Collections.Generic;
using TreeEditor;

[System.Serializable]
public class CoffeeType
{
    public CoffeeType(string name_)
    {
        name = name_;
    }

    private CoffeeType(string name_, string sprite_path_, int base_reward_, double base_wait_time_)
    {
        name = name_;
        sprite_path = sprite_path_;
        base_reward = base_reward_;
        base_wait_time = base_wait_time_;
    }
    private static CoffeeType create_new(string name_, string sprite_path_, int base_reward_, double base_wait_time_)
    {
        return new(name_, sprite_path_, base_reward_, base_wait_time_);
    }

    public string name { get; }
    private string sprite_path { get; }
    public int base_reward { get; }
    public double base_wait_time { get; }

    public static readonly CoffeeType Espresso = new CoffeeType("Espresso", "espresso.png", 100, 90);
    public static readonly CoffeeType Cappuccino = new CoffeeType("Cappuccino", "cappuccino.png", 150, 150);

    // Template for trash object
    public static readonly CoffeeType Trash = new CoffeeType("Trash", "trash.png", 0, 0);
}


[System.Serializable]
public enum Ingredients { Espresso = 0x1, Milk = 0x2  }
[System.Serializable]
public class CupOfCoffee
{
    public CoffeeType finish_coffee()
    {
        int sum = 0;
        foreach (Ingredients i in ingredients)
        {
            sum |= (int)i;
        }
        if (sum == 0x1)
        {
            return CoffeeType.Espresso;
        }
        else if (sum == 0x3)
        {
            return CoffeeType.Cappuccino;
        }

        return CoffeeType.Trash;
        
    }
    public List<Ingredients> ingredients = new List<Ingredients>();
}
