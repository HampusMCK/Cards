public class Player
{
    public string name;
    public List<Card> hand = new List<Card>(5);
    public int money;
    public int betted;
    public int score;
    public string result;
    public bool folded;

    public virtual void bet(Player player, Table table, int stake)
    {
        player.money -= stake;
        table.pot += stake;
        player.betted += stake;
    }

    public void call(Player player, Table table)
    {
        int stake = table.pot - (player.betted * 2);
        player.bet(player, table, stake);
    }

    public void fold(Player player)
    {
        player.folded = true;
        player.result = player.name + " Has Folded!";
    }

    public void calculateResults(Player p, List<Card> All, Table table)
    {
        int point()
        {
            int score = 0;
            //CHECK PAIR AMOUNT
            int sameCount = 0;
            foreach (Card c in table.table)
            {
                if (p.hand[0].value == c.value)
                    sameCount++;
                if (p.hand[1].value == c.value)
                    sameCount++;
            }
            if (p.hand[0].value == p.hand[1].value && sameCount < 4)
                sameCount++;
            score = sameCount;

            // CHECK FOR FLUSH
            foreach (Card c in All)
            {
                int sameSuitCount = 0;
                for (int i = 0; i < All.Count; i++)
                {
                    if (All[i] != c)
                    {
                        if (All[i].suit == c.suit)
                            sameSuitCount++;
                    }
                }
                if (sameSuitCount >= 4)
                    score = 5;
            }

            //CHECK FOR STRAIGHT
            int stepCount = 0;
            foreach (Card c in All)
            {
                for (int i = 0; i < All.Count; i++)
                {
                    int steps = 1;
                    int start = c.value;
                    int next = start + 1;
                    if (All[i].value == next)
                    {
                        next++;
                        steps++;
                        for (int x = 0; x < All.Count; x++)
                        {
                            if (All[x].value == next)
                            {
                                next++;
                                steps++;
                                for (int y = 0; y < All.Count; y++)
                                {
                                    if (All[y].value == next)
                                    {
                                        next++;
                                        steps++;
                                        for (int z = 0; z < All.Count; z++)
                                        {
                                            if (All[z].value == next)
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
            foreach (Card c in All)
            {
                int counter = 0;
                foreach (Card l in All)
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
            p.score = score;
            return score;
        }
        switch (point())
        {
            case 1:
                p.result = "You Have A Pair!";
                break;
            case 2:
                p.result = "You Have Two Pairs!";
                break;
            case 3:
                p.result = "You Have Three Of A Kind!";
                break;
            case 4:
                p.result = "You Have Four Of A Kind!";
                break;
            case 5:
                p.result = "You Have A Flush";
                break;
            case 6:
                p.result = "You Have A Straight";
                break;
            case 7:
                p.result = "You Have A Straight Flush";
                break;
            case 8:
                p.result = "You Have Full House";
                break;
            default:
                p.result = "You Have Nothing";
                break;
        }
    }
}
