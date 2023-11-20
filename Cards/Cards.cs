using System.ComponentModel.DataAnnotations;

public class Cards
{
    [Range(2, 14)]
    public int value;

    public string color;
    public string form;
    public string name;

    public List<Cards> cards = new List<Cards>();
    public List<string> names = new List<string>{
        "Two",
        "Three",
        "Four",
        "Five",
        "Six",
        "Seven",
        "Eight",
        "Nine",
        "Ten",
        "Jack",
        "Queen",
        "King",
        "Ace"
    };

    public void Shuffle(IList<Cards> list)
    {
        Random gen = new Random();
        for (int x = 0; x < 2; x++)
        {
            for (int i = list.Count - 1; i > 1; i--)
            {
                int rnd = gen.Next(i + 1);

                Cards value = list[rnd];
                list[rnd] = list[i];
                list[i] = value;
            }
        }
    }
}