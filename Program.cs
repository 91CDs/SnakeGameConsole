using System.Text;
using WenceyWang.FIGlet;

namespace Snake;
enum Sprite {
    TopEdge,
    MiddleEdge,
    BottomEdge,
    SnakeHead,
    SnakeBody,
    Food
}
class SnakeGame
{
    #region Setup
    static void defaultSetup()
    {
        Console.ForegroundColor = ConsoleColor.Gray;
    }
    static void setup()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
    }
    #endregion
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
    #region Mechanics
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
    #endregion
    // Snake Board (0, 0) starts at top left of the screen 
    static (int, int) getcursorPos(int x, int y) 
    {
        int cx = 4 * x + 2;
        int cy = 2 * y + 1;
        return (cx, cy);
    }
    static void displaySnake(Snake snake, string[] board)
    {
        var SnakePoints = snake.getAllPoints();
        var (initX, initY) = Console.GetCursorPosition();
        for (int i = 0; i < SnakePoints.Count; i++)
        {
            var point = SnakePoints[i];
            var (cursorX, cursorY) = getcursorPos(Convert.ToInt32(point.X), Convert.ToInt32(point.Y));
            Console.SetCursorPosition(cursorX, cursorY);
            if (i == 0) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(board[(int)Sprite.SnakeHead]);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(board[(int)Sprite.SnakeBody]);
            }
        }

        Console.SetCursorPosition(initX, initY);
    }
    static void displaySnakeBoard(Snake snake)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkGray;
        string path = @"./Sprite.txt";
        string[] board = File.ReadAllLines(path);

        int maxX = 20;
        int maxY = 20;

        for (int i = 0; i < maxY; i++)
        {   
            for (int j = 0; j < 3; j++)
            {
                if (i != 0 && j == 0) continue;
                var edges = new StringBuilder();
                edges.Insert(edges.Length, board[j]);
                edges.Insert(edges.Length, board[j].Remove(0,1), maxX - 1);
                Console.WriteLine(edges.ToString());
            }
        }

        displaySnake(snake, board);
    }
    static void playSnake()
    {
        var snake = new Snake();
        int loop = 0;
        while (true)
        {
            Thread.Sleep(500);
            displaySnakeBoard(snake);
            snake.Walk();
            
            loop++;
            if (loop >= 8)
            {
                break;
            }
        }

        Console.ReadKey(true);
    }
    public static void Main()
    {
        setup();
        
        string selectedMenu;
        do
        {            
            selectedMenu = displayMenu();
            if (selectedMenu == "Mechanics")
            {
                displayMechanics();
                setup();
                continue;
            } 
            else if (selectedMenu == "Play")
            {
                playSnake();
                setup();
                continue;
            }
        } while (selectedMenu != "Exit");
        
        defaultSetup();  
    }
}