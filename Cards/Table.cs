public class Table
{
    public List<Card> table = new List<Card>();
    public int pot;

    public void dealCards(Table t, Deck d, int cardToDraw, int cardsToPlace)
    {
        for (int i = cardToDraw; i < cardToDraw + cardsToPlace; i++)
        {
            t.table.Add(d.deck[i]);
        }
    }
}
