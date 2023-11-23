Deck d = new();
Human h = new();
Opponent o = new();
d.fillDeck();
// d.shuffleDeck();
List<Card> table = new List<Card>();
int cardsPlace = 0;
Console.ReadLine();
for (int i = cardsPlace; i < 4; i++)
{
    if (i % 2 != 0)
        h.hand.Add(d.deck[i]);
    else
        o.hand.Add(d.deck[i]);
    cardsPlace++;
}
foreach (Card c in h.hand)
{
    Console.WriteLine(c.printName);
}
Console.ReadLine();
int temp = cardsPlace;
for (int i = cardsPlace; i < temp + 3; i++)
{
    table.Add(d.deck[i]);
    cardsPlace++;
}
Console.Clear();
Console.WriteLine("On The Tabel:");
foreach (Card c in table)
{
    Console.WriteLine(c.printName);
}
Console.WriteLine("\nIn Your Hand:");
foreach (Card c in h.hand)
{
    Console.WriteLine(c.printName);
}
Console.ReadLine();
table.Add(d.deck[cardsPlace]);
cardsPlace++;
Console.Clear();
Console.WriteLine("On The Tabel:");
foreach (Card c in table)
{
    Console.WriteLine(c.printName);
}
Console.WriteLine("\nIn Your Hand:");
foreach (Card c in h.hand)
{
    Console.WriteLine(c.printName);
}
Console.ReadLine();
table.Add(d.deck[cardsPlace]);
cardsPlace++;
Console.Clear();
Console.WriteLine("On The Tabel:");
foreach (Card c in table)
{
    Console.WriteLine(c.printName);
}
Console.WriteLine("\nIn Your Hand:");
foreach (Card c in h.hand)
{
    Console.WriteLine(c.printName);
}
Console.ReadLine();

string result = "";
displayResult();
Console.WriteLine(result);

Console.ReadLine();



void displayResult()
{
    int point()
    {
        List<Card> allCards = new List<Card>();
        foreach (Card c in table)
        {
            allCards.Add(c);
        }
        foreach (Card c in h.hand)
        {
            allCards.Add(c);
        }
        int score = 0;

        //CHECK PAIR AMOUNT
        int sameCount = 0;
        foreach (Card c in table)
        {
            if (h.hand[0].value == c.value)
                sameCount++;
            if (h.hand[1].value == c.value)
                sameCount++;
        }
        if (h.hand[0].value == h.hand[1].value && sameCount < 4)
            sameCount++;
        score = sameCount;

        // CHECK FOR FLUSH
        foreach (Card c in allCards)
        {
            int sameSuitCount = 0;
            for (int i = 0; i < allCards.Count; i++)
            {
                if (allCards[i] != c)
                {
                    if (allCards[i].suit == c.suit)
                        sameSuitCount++;
                }
            }
            if (sameSuitCount >= 4)
                score = 5;
        }

        //CHECK FOR STRAIGHT
        int stepCount = 0;
        foreach (Card c in allCards)
        {
            for (int i = 0; i < allCards.Count; i++)
            {
                int steps = 1;
                int start = c.value;
                int next = start + 1;
                if (allCards[i].value == next)
                {
                    next++;
                    steps++;
                    for (int x = 0; x < allCards.Count; x++)
                    {
                        if (allCards[x].value == next)
                        {
                            next++;
                            steps++;
                            for (int y = 0; y < allCards.Count; y++)
                            {
                                if (allCards[y].value == next)
                                {
                                    next++;
                                    steps++;
                                    for (int z = 0; z < allCards.Count; z++)
                                    {
                                        if (allCards[z].value == next)
                                        {
                                            next++;
                                            steps++;
                                            stepCount = steps;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        if (stepCount >= 5 && score == 5)
            score = 7;
        else if (stepCount >= 5)
            score = 6;

        //CHECK FOR FULL HOUSE
        bool three = false;
        bool two = false;
        foreach (Card c in allCards)
        {
            int counter = 0;
            foreach (Card l in allCards)
            {
                if (c != l)
                {
                    if (c.value == l.value)
                        counter++;
                }
            }
            if (counter == 3)
                three = true;
            if (counter == 2)
                two = true;
        }
        if (two && three)
            score = 8;

        return score;
    }
    switch (point())
    {
        case 1:
            result = "You Have A Pair!";
            break;
        case 2:
            result = "You Have Two Pairs!";
            break;
        case 3:
            result = "You Have Three Of A Kind!";
            break;
        case 4:
            result = "You Have Four Of A Kind!";
            break;
        case 5:
            result = "You Have A Flush";
            break;
        case 6:
            result = "You Have A Straight";
            break;
        case 7:
            result = "You Have A Straight Flush";
            break;
        case 8:
            result = "You Have Full House";
            break;
        default:
            result = "You Have Nothing";
            break;
    }
}