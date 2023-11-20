Cards c = new Cards();
Player p = new Player();
Opp o = new Opp();
Chip chip = new Chip();
Table t = new Table();

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

Console.ReadLine();
p.chips.Add(new chipValue { count = 100, value = 25 });
p.chips.Add(new chipValue { count = 100, value = 100 });
p.chips.Add(new chipValue { count = 100, value = 500 });
p.chips.Add(new chipValue { count = 100, value = 1000 });
p.chips.Add(new chipValue { count = 100, value = 5000 });
p.chips.Add(new chipValue { count = 100, value = 25000 });
o.chips.Add(new chipValue { count = 100, value = 25 });
o.chips.Add(new chipValue { count = 100, value = 100 });
o.chips.Add(new chipValue { count = 100, value = 500 });
o.chips.Add(new chipValue { count = 100, value = 1000 });
o.chips.Add(new chipValue { count = 100, value = 5000 });
o.chips.Add(new chipValue { count = 100, value = 25000 });

while (p.chips.Count > 0 && o.chips.Count > 0)
{
    Console.Clear();
    c.Shuffle(c.cards);
    for (int i = 0; i < 4; i++)
    {
        if (i % 2 != 0)
        {
            p.hand.Add(c.cards[0]);
            c.cards.RemoveAt(0);
        }
        else
        {
            o.hand.Add(c.cards[0]);
            c.cards.RemoveAt(0);
        }
    }

    for (int i = 0; i < 2; i++)
    {
        Console.WriteLine($"In Hand: {i + 1}. {p.hand[i].value}");
    }
    Console.ReadLine();

    for (int i = 0; i < 3; i++)
    {
        t.cards.Add(c.cards[0]);
        c.cards.RemoveAt(0);
        Console.WriteLine($"\nOn Table: {i + 1}. {t.cards[i].value}");
    }
    Console.ReadLine();

    t.cards.Add(c.cards[0]);
    c.cards.RemoveAt(0);

    for (int i = 0; i < t.cards.Count; i++)
    {
        Console.WriteLine($"\nOn Table: {i + 1}. {t.cards[i].value}");
    }
    Console.ReadLine();

    t.cards.Add(c.cards[0]);
    c.cards.RemoveAt(0);

    for (int i = 0; i < t.cards.Count; i++)
    {
        Console.WriteLine($"\nOn Table: {i + 1}. {t.cards[i].value}");
    }
    Console.ReadLine();
    for (int i = 0; i < o.hand.Count; i++)
    {
        Console.WriteLine($"\nopp hand: {i + 1}. {o.hand[i].value}");
    }
    for (int i = 0; i < p.hand.Count; i++)
    {
        Console.WriteLine($"\nYour hand: {i + 1}. {p.hand[i].value}");
    }
    for (int i = 0; i < t.cards.Count; i++)
    {
        Console.WriteLine($"\nOn Table: {i + 1}. {t.cards[i].value}");
    }
    Console.ReadLine();

    for (int i = 0; i < t.cards.Count; i++)
    {
        c.cards.Add(t.cards[i]);
    }
    for (int i = 0; i < p.hand.Count; i++)
    {
        c.cards.Add(p.hand[i]);
    }
    for (int i = 0; i < o.hand.Count; i++)
    {
        c.cards.Add(o.hand[i]);
    }
    o.hand.Clear();
    t.cards.Clear();
    p.hand.Clear();
}

for (int i = 0; i < c.cards.Count; i++)
{
    Console.WriteLine($"{i + 1}. {c.cards[i].value}");
}

Console.ReadLine();