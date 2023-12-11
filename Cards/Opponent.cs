public class Opponent : Player
{
    private bool _call = false;
    public Opponent()
    {
        name = "John";
        money = 500;
    }

    public override void bet(Player player, Table table, int stake)
    {
        if (!_call)
            stake = Random.Shared.Next(0, 50);
        player.money -= stake;
        table.pot += stake;
        player.betted += stake;
    }

    public void calculateNextMove(Player player, Human opp, List<Card> hand, List<Card> allCards, Table table)
    {
        int score()
        {
            foreach (Card c in table.table)
            {
                allCards.Add(c);
            }
            foreach (Card c in hand)
            {
                allCards.Add(c);
            }
            int score = 0;

            //CHECK PAIR AMOUNT
            int sameCount = 0;
            foreach (Card c in table.table)
            {
                if (hand[0].value == c.value)
                    sameCount++;
                if (hand[1].value == c.value)
                    sameCount++;
            }
            if (hand[0].value == hand[1].value && sameCount < 4)
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

        if (opp.betted > player.betted)
        {
            int action = Random.Shared.Next(0, score() + 2);
            if (action >= 1)
            {
                _call = true;
                call(this, table);
            }
            else
            {
                _call = false;
                fold(this);
            }
        }
        else
        {
            _call = false;
            int action = Random.Shared.Next(0, score() + 2);
            if (action > 1)
            {
                bet(this, table, 0);
                opp.myTurn = true;
            }
        }
        player.myTurn = false;
    }
}
