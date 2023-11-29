public class Player
{
    public string name;
    public List<Card> hand = new List<Card>(5);
    public int money;
    public int betted;

    public virtual void bet(Player player, Table table, int stake)
    {
        player.money -= stake;
        table.pot += stake;
        player.betted += stake;
    }

    public void call(Player player, Table table)
    {
        int stake = table.pot - (player.betted * 2);
        bet(player, table, stake);
    }

    public bool fold(Player player)
    {
        return true;
    }
}
