public class Chip
{
    public List<chipValue> chipValues = new List<chipValue>
    {
        new chipValue{count = 200, value = 25},
        new chipValue{count = 150, value = 100},
        new chipValue{count = 100, value = 500},
        new chipValue{count = 75, value = 1000},
        new chipValue{count = 50, value = 5000},
        new chipValue{count = 25, value = 25000}
    };
}

public class chipValue
{
    public int count;
    public int value;
}