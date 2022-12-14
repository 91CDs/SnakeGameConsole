using System.Text;
using WenceyWang.FIGlet;

namespace Snake;

partial class SnakeGame
{
    static void defaultSetup()
    {
        Console.ForegroundColor = ConsoleColor.Gray;
    }
    static void setup()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
    }
    #region Menu
    static void removeMenu(string[] menus, string cursor)
    {
        int initialRow = Console.CursorTop - 1; 
        int backspaceNum = 100;
        string backspaces = new StringBuilder().Insert(0, "\b \b", backspaceNum).ToString();
        for (int i = 0; i < menus.Length; i++)
        {
            Console.SetCursorPosition(backspaceNum, initialRow - i);
            Console.Write(backspaces);
        }
    }
    static void addMenu(string[] menus, string cursor, int cursorIndex) 
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (i == cursorIndex) 
            {
                Console.WriteLine(menus[i] + cursor);
            } 
            else
            {
                Console.WriteLine(menus[i]);
            }
        }
    }
    static string displayMenu(string[] menus, string cursor)
    {
        ConsoleKey keypress;
        int cursorIndex = 0;
        int loop = 0;

        do
        {   
            if (loop != 0)
            {
                removeMenu(menus, cursor);
            }
            addMenu(menus, cursor, cursorIndex);
            loop++;

            keypress = Console.ReadKey(true).Key;
            if (keypress == ConsoleKey.UpArrow && cursorIndex != 0)
            {
                cursorIndex--;
            } 
            if (keypress == ConsoleKey.DownArrow && cursorIndex < menus.Length - 1)
            {
                cursorIndex++;
            }
        } while (keypress != ConsoleKey.Enter);
        
        removeMenu(menus, cursor);
        return menus[cursorIndex];
    }
    static string displayStartMenu()
    {
        var display = new AsciiArt("Snake Game");
        Console.WriteLine(display.ToString());   
        Console.WriteLine("By 91CDs");
        Console.WriteLine();

        string[] startMenus = { "Mechanics", "Play", "Options", "Exit" };
        string cursor = "  <";  

        return displayMenu(startMenus, cursor);
    }
    #endregion
    static void displayMechanics()
    {
        Console.WriteLine("Mechanics");
        Console.WriteLine(
    @"
    In the game of Snake, the player uses the arrow keys to move a 'snake' around the board. 

    As the snake finds food, it eats the food, and thereby grows larger. The game ends when the snake either 
    moves off the screen or moves into itself. The goal is to make the snake as large as possible before that 
    happens."
        );
        Console.WriteLine("Press any key to go back to menu screen");
        Console.ReadKey(true);
    }
    static Options displayOptions()
    {
        string cursor = "  <";
        Options options;
        do
        {    
            Console.Clear();
            Console.WriteLine("Options \n");

            Console.WriteLine("Size: ");
            string[] sizeMenu = { "Small", "Medium", "Large" };
            string selectSize = displayMenu(sizeMenu, cursor);
            Console.WriteLine(selectSize);
            Console.WriteLine();

            Console.WriteLine("Speed: ");
            string[] speedMenu = { "Slow", "Medium", "Fast" };
            string selectSpeed = displayMenu(speedMenu, cursor);
            Console.WriteLine(selectSpeed);

            SnakeSpeed speed = selectSpeed switch
            {
                "Slow" => SnakeSpeed.Slow,
                "Medium" => SnakeSpeed.Medium,
                "Fast" => SnakeSpeed.Fast,
                _ => throw new ArgumentOutOfRangeException($"Invalid speed: {selectSpeed}"),
            };
            var (boardX, boardY) = selectSize switch
            {
                "Small" => (15,15),
                "Medium" => (20,20),
                "Large" => (25,25),
                _ => throw new ArgumentOutOfRangeException($"Invalid size: {selectSize}"),
            };

            options = new Options(boardX, boardY, speed);

            Console.WriteLine("Press [Enter] to save changes or press any other key to start again");
        } while (Console.ReadKey(true).Key != ConsoleKey.Enter);
        return options;
    }
}

class Options
{
    public int boardX { get; set; }
    public int boardY { get; set; }
    public SnakeSpeed speed { get; set; }
    public Options()
    {
        boardX = 20;
        boardY = 20;
        speed = SnakeSpeed.Medium;
    }

    public Options(int boardXsize, int boardYsize, SnakeSpeed snakeSpeed)
    {
        boardX = boardXsize;
        boardY = boardYsize;
        speed = snakeSpeed;
    }
}