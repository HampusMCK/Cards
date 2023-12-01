using System.Numerics;
using Raylib_cs;

//------------------------------------------Visual Window Data-----------------------------
Raylib.InitWindow(600, 600, "Texas Holdem");
int currentMonitor = Raylib.GetCurrentMonitor();
int windowWidth = Raylib.GetMonitorWidth(currentMonitor) - (Raylib.GetMonitorWidth(currentMonitor) / 8);
int windowHeight = Raylib.GetMonitorHeight(currentMonitor) - (Raylib.GetMonitorHeight(currentMonitor) / 8);
Raylib.SetWindowSize(windowWidth, windowHeight);
Raylib.SetWindowPosition(windowWidth / 16, windowHeight / 16);
//-------------------------------------------------------------------------------------------

//-----------------------------------References---------------------------------------------
Deck d = new();
Human h = new();
Player p = new();
Opponent o = new();
Table table = new();
//------------------------------------------------------------------------------------------

//------------------------------------Point&Click Data------------------------------------
Vector2 MousePos = new Vector2();
//---------------------------------------------------------------------------------------

//-----------------------------------Score Data-------------------------------------------
bool moneyDevided = false;
//------------------------------------------------------------------------------------------

//--------------------------------Scene Managment-------------------------------------------
int step = 0;
//------------------------------------------------------------------------------------------

// ---------------------------------------------------Buttons------------------------------------------
int buttonWidth = windowWidth / 7;
int buttonHeight = windowHeight / 12;
int xPlace = windowWidth / 8;
int yPlace = windowHeight - (windowHeight / 8);
Rectangle Bet = new Rectangle(xPlace, yPlace, buttonWidth, buttonHeight);
Rectangle Call = new Rectangle(xPlace + (buttonWidth * 1.5f), yPlace, buttonWidth, buttonHeight);
Rectangle Fold = new Rectangle(xPlace + (buttonWidth * 3), yPlace, buttonWidth, buttonHeight);
Rectangle Check = new Rectangle(xPlace + (buttonWidth * 4.5f), yPlace, buttonWidth, buttonHeight);
Rectangle Restart = new Rectangle((windowWidth / 2) - (buttonWidth / 2), (windowHeight / 2) - (buttonHeight / 2), buttonWidth, buttonWidth);
List<Rectangle> buttons = new List<Rectangle>()
{
    Bet,
    Call,
    Fold,
    Check
};
List<string> names = new List<string>()
{
    "Bet",
    "Call",
    "Fold",
    "Check"
};
//-----------------------------------------------------------------------------------------------------

//---------------------------------------Cards Data----------------------------------------------------
List<Card> AllOppCards = new List<Card>(7);
List<Card> AllPlayerCards = new List<Card>(7);
int cardsPlaced = 0;
//-----------------------------------------------------------------------------------------------------

while (!Raylib.WindowShouldClose())
{
    MousePos = Raylib.GetMousePosition();
    Raylib.BeginDrawing();
    cardsPlaced = table.table.Count + h.hand.Count + o.hand.Count;
    if (!moneyDevided && !h.folded && !o.folded)
    {
        Raylib.ClearBackground(Color.WHITE);

        Raylib.DrawText($"${table.pot} in the pot", windowWidth - 300, 100, 25, Color.BLACK);
        Raylib.DrawText($"You have ${h.money}", windowWidth - 300, 50, 25, Color.BLACK);

        // ---------------------------Cards--------------------------------
        int xDisplay = windowWidth / 8;
        int index = 0;
        foreach (Card c in h.hand)
        {
            Raylib.DrawRectangle(xDisplay, 100, 200, 200, Color.GRAY);
            Raylib.DrawRectangleLines(xDisplay, 100, 200, 200, Color.BLACK);
            Raylib.DrawText($"{c.printName}", xDisplay + 5, 175, 20, Color.RED);
            index++;
            xDisplay += 200 * index;
        }
        int xTableDisplay = windowWidth / 8;
        index = 0;
        foreach (Card c in table.table)
        {
            Raylib.DrawRectangle(xTableDisplay, windowHeight / 2, 200, 200, Color.GRAY);
            Raylib.DrawRectangleLines(xTableDisplay, windowHeight / 2, 200, 200, Color.BLACK);
            Raylib.DrawText($"{c.printName}", xTableDisplay + 5, (windowHeight / 2) + 90, 20, Color.RED);
            index++;
            xTableDisplay = (windowWidth / 8) + (200 * index);
        }
        //------------------------------------------------------------------------
    }
    if (step == 0)
    {
        reset();
        step++;
    }
    if (step == 1)
    {
        p.dealToPlayers(h, o, d);
        h.myTurn = true;
        step++;
    }
    if (h.folded || o.folded && step != 0)
        step = 3;
    if (!h.folded && !o.folded)
    {
        if (h.myTurn)
        {
            DisplayButtons();
        }
        if (o.myTurn)
        {
            UpdateLists();
            o.calculateNextMove(o, h, o.hand, AllOppCards, table);
        }
        if (!o.myTurn && !h.myTurn && cardsPlaced != 9)
        {
            table.dealCards(table, d, cardsPlaced, (10 - cardsPlaced) / 2);
            h.myTurn = true;
        }
    }
    if (cardsPlaced == 9 && !o.myTurn && !h.myTurn)
    {
        UpdateLists();
        h.calculateResults(h, AllPlayerCards, table);
        o.calculateResults(o, AllOppCards, table);
        step = 3;
    }
    if (step == 3)
    {
        if (h.score > o.score || o.folded)
        {
            Raylib.ClearBackground(Color.GREEN);
        }
        else if (o.score > h.score || h.folded)
        {
            Raylib.ClearBackground(Color.RED);
        }
        else
        {
            Raylib.ClearBackground(Color.ORANGE);
        }
        Raylib.DrawText($"Player1, {h.result}", 800, 100, 30, Color.BLACK);
        Raylib.DrawText($"John, {o.result}", 800, 150, 30, Color.BLACK);
        if (!moneyDevided)
        {
            if ((h.score > o.score || o.folded) && !h.folded)
            {
                h.money += table.pot;
                table.pot = 0;
            }
            else if ((o.score > h.score || h.folded) && !o.folded)
            {
                o.money += table.pot;
                table.pot = 0;
            }
            else
            {
                o.money += table.pot / 2;
                h.money += table.pot / 2;
                table.pot = 0;
            }
            moneyDevided = true;
        }
        Raylib.DrawRectangleRec(Restart, Color.GRAY);
        Raylib.DrawRectangleLinesEx(Restart, 2, Color.BLACK);
        Raylib.DrawText("Restart!", (int)Restart.X + 5, (int)Restart.Y + 5, 25, Color.BLACK);
        if (Raylib.CheckCollisionPointRec(MousePos, Restart))
        {
            Raylib.DrawRectangleRec(Restart, Color.SKYBLUE);
            Raylib.DrawRectangleLinesEx(Restart, 3, Color.BLACK);
            Raylib.DrawText("Restart!", (int)Restart.X + 5, (int)Restart.Y + 5, 25, Color.RED);
            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
                step = 0;
        }
    }


    Raylib.EndDrawing();
}

void clearLists()
{
    table.table.Clear();
    h.hand.Clear();
    o.hand.Clear();
}

void DisplayButtons()
{
    for (int i = 0; i < buttons.Count; i++)
    {
        Raylib.DrawRectangleRec(buttons[i], Color.GRAY);
        Raylib.DrawRectangleLinesEx(buttons[i], 2, Color.BLACK);
        Raylib.DrawText($"{names[i]}", (int)buttons[i].X + 5, (int)buttons[i].Y + (buttonHeight / 2), 20, Color.BLACK);
        if (Raylib.CheckCollisionPointRec(MousePos, buttons[i]))
        {
            Raylib.DrawRectangleRec(buttons[i], Color.SKYBLUE);
            Raylib.DrawRectangleLinesEx(buttons[i], 3, Color.BLACK);
            Raylib.DrawText($"{names[i]}", (int)buttons[i].X + 5, (int)buttons[i].Y + (buttonHeight / 2), 20, Color.RED);
            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
            {
                Actions(i);
                h.myTurn = false;
            }
        }
    }
}

void Actions(int index)
{
    switch (index)
    {
        case 0:
            h.bet(h, table, 50);
            o.myTurn = true;
            break;
        case 1:
            h.call(h, table);
            break;
        case 2:
            h.fold(h);
            break;
        case 3:
            o.myTurn = true;
            break;
    }
}

void UpdateLists()
{
    AllOppCards.Clear();
    AllPlayerCards.Clear();
    foreach (Card c in table.table)
    {
        AllOppCards.Add(c);
        AllPlayerCards.Add(c);
    }
    foreach (Card c in h.hand)
    {
        AllPlayerCards.Add(c);
    }
    foreach (Card c in o.hand)
    {
        AllOppCards.Add(c);
    }
}

void reset()
{
    clearLists();
    d.fillDeck();
    d.shuffleDeck();
    h.folded = false;
    o.folded = false;
    moneyDevided = false;
    h.betted = 0;
    o.betted = 0;
    h.myTurn = false;
    o.myTurn = false;
}