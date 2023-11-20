Cards c = new Cards();
int n = 0;
string co = "";
string fo = "";

for (int s = 0; s < 4; s++)
{
    n = 0;
    if (s == 0)
    {
        co = "Red";
        fo = "Heart";
    }
    else if (s == 1)
        fo = "Diamond";
    else if (s == 2)
    {
        co = "Black";
        fo = "Club";
    }
    else
        fo = "Spade";
    for (int v = 2; v < 15; v++)
    {
        c.cards.Add(new Cards { value = v, color = co, form = fo, name = c.names[n] });
        n++;
    }
}

c.Shuffle(c.cards);

for (int i = 0; i < c.cards.Count; i++)
{
    Console.WriteLine($"{i + 1}. {c.cards[i].value}");
}

Console.ReadLine();