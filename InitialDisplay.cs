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
    static string displayMenu()
    {
        var display = new AsciiArt("Snake Game");
        Console.WriteLine(display.ToString());   
        Console.WriteLine("By 91CDs");
        Console.WriteLine();

        string[] menus = { "Mechanics", "Play", "Exit" };
        string cursor = "  <";  

        ConsoleKey keypress;
        int cursorIndex = 0;
        int loop = 0;

        do
        {   
            if (loop != 0)
            {
                removeMenu(menus, cursor);
            }
            addMenu(menus, cursor,cursorIndex);
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
}