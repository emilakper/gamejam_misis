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
}


