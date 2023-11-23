public class Card
{
    public string suit;
    public static string[] suitName = new string[4] { "Hearts", "Diamonds", "Clubs", "Spades" };
    public int value;
    public string printName;

    public Card(int _value, string _suit)
    {
        suit = _suit;
        value = _value;

        string tempName = "";
        string suitName = "";
        switch (value)
        {
            case 11:
                tempName = "Jack";
                break;
            case 12:
                tempName = "Queen";
                break;
            case 13:
                tempName = "King";
                break;
            case 14:
                tempName = "Ace";
                break;
            default:
                tempName = value.ToString();
                break;
        }
        switch (suit)
        {
            case "Hearts":
                suitName = " Of Hearts";
                break;
            case "Diamonds":
                suitName = " Of Diamonds";
                break;
            case "Clubs":
                suitName = " Of Clubs";
                break;
            case "Spades":
                suitName = " Of Spades";
                break;
        }
        printName = tempName + suitName;
    }
}