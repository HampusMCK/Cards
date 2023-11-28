public class Player
{
    public string name;
    public List<Card> hand = new List<Card>(5);
    public int money;

    public void bet(Player player, Table table, int stake)
    {
        player.money -= stake;
        table.pot += stake;
    }
}
