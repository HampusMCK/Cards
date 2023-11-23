public class Deck
{
    public Card[] deck = new Card[52];

    public void fillDeck()
    {
        int index = 0;
        foreach (string suit in Card.suitName)
        {
            for (int value = 2; value <= 14; value++)
            {
                deck[index] = new Card(value, suit);
                index++;
            }
        }
    }

    public void shuffleDeck()
    {
        for (int i = deck.Length - 1; i >= 0; i--)
        {
            int rnd = Random.Shared.Next(deck.Length);
            Card c = deck[rnd];
            deck[rnd] = deck[i];
            deck[i] = c;
        }
    }
}
