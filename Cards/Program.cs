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
Opponent o = new();
Table table = new();
//------------------------------------------------------------------------------------------

//------------------------------------Point&Click Data------------------------------------
Vector2 MousePos = new Vector2();
//---------------------------------------------------------------------------------------

//-----------------------------------Score Data-------------------------------------------
int playerScore = 0;
int oppScore = 0;
string playerResult = "";
string oppResult = "";
//------------------------------------------------------------------------------------------

//--------------------------------Scene Managment-------------------------------------------
bool myTurn = false;
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
    List<Card> AllOppCards = new List<Card>(7);
    List<Card> AllPlayerCards = new List<Card>(7);
//----------------------------------All Cards Lists----------------------------------------------------


while (!Raylib.WindowShouldClose())
{
    MousePos = Raylib.GetMousePosition();
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.WHITE);

    Raylib.DrawText($"${table.pot} in the pot", windowWidth - 150, 100, 25, Color.BLACK);

    // ---------------------------Cards--------------------------------
    int xDisplay = windowWidth / 8;
    foreach (Card c in h.hand)
    {
        Raylib.DrawRectangle(xDisplay, 100, 150, 200, Color.GRAY);
        Raylib.DrawRectangleLines(xDisplay, 100, 150, 200, Color.BLACK);
        Raylib.DrawText($"{c.printName}", xDisplay + 5, 175, 20, Color.RED);
        xDisplay += xDisplay * 2;
    }
    //------------------------------------------------------------------------
    if (step == 0)
    {
        clearLists();
        d.fillDeck();
        d.shuffleDeck();
        step++;
    }
    if (step == 1)
    {
        for (int i = 0; i < 4; i++)
        {
            if (i % 2 != 0)
                h.hand.Add(d.deck[i]);
            else
                o.hand.Add(d.deck[i]);
        }
        myTurn = true;
        step++;
    }
    if (step == 2)
    {
        UpdateLists();
        o.calculateNextMove(o, h, o.hand, AllOppCards, table.table, table);
        step++;
    }
    if (step == 3)
    {
        
    }

    if (myTurn)
    {
        DisplayButtons();
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
                myTurn = false;
            }
        }
    }
}

void Actions(int index)
{
    switch(index)
    {
        case 0:
            h.bet(h, table, 50);
            break;
        case 1:
            h.call(h, table);
            break;
        case 2:
            break;
        case 3:
            h.fold(h);
            break;
    }
}

void UpdateLists()
{
    AllOppCards.Clear();
    AllPlayerCards.Clear();
foreach(Card c in table.table)
    {
        AllOppCards.Add(c);
        AllPlayerCards.Add(c);
    }
    foreach(Card c in h.hand)
    {
        AllPlayerCards.Add(c);
    }
    foreach(Card c in o.hand)
    {
        AllOppCards.Add(c);
    }
}