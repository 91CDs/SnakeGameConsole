using System.Numerics;
using System.Text;

namespace Snake;
enum Sprite {
    TopEdge,
    MiddleEdge,
    BottomEdge,
    SnakeHead,
    SnakeBody,
    Food
}
partial class SnakeGame
{
    // Snake Board (0, 0) starts at top left of the screen 
    static (int, int) getcursorPos(int x, int y) 
    {
        int cx = 4 * x + 2;
        int cy = 2 * y + 1;
        return (cx, cy);
    }
    static void displaySnake(Snake snake, Food food, string[] board)
    {
        List<Vector2> SnakePoints = snake.getAllPoints();
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
        var fpoint = food.P;
        var (fcursorX, fcursorY) = getcursorPos(Convert.ToInt32(fpoint.X), Convert.ToInt32(fpoint.Y));
        Console.SetCursorPosition(fcursorX, fcursorY);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(board[(int)Sprite.Food]);

        Console.SetCursorPosition(initX, initY);
    }
    static void displaySnakeBoard(Snake snake, Food food, int maxX, int maxY)
    {
        Console.SetCursorPosition(0,0);
        Console.ForegroundColor = ConsoleColor.DarkGray;
        string path = @"./Sprite.txt";
        string[] board = File.ReadAllLines(path);

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

        displaySnake(snake, food, board);
    }

    static List<Vector2> getBoundaryPositions(int maxX, int maxY)
    {
        List<Vector2> boundaryPos = new List<Vector2>();
        for (int i = 0; i < maxX; i++)
        {
            boundaryPos.Add(new Vector2(i,-1));
            boundaryPos.Add(new Vector2(i,maxY));
        }
        for (int i = 0; i < maxY; i++)
        {
            boundaryPos.Add(new Vector2(-1,i));
            boundaryPos.Add(new Vector2(maxX,i));
        }
        return boundaryPos;

    }
    static void checkSnakeState(Snake snake, Food food, int maxX, int maxY)
    {
        var headPos = snake.Head.P;
        var foodPos = food.P;
        var bodyPos = snake.Body.Select(part => part.P);
        var boundaryPos = getBoundaryPositions(maxX, maxY);
        var isEating = headPos.Equals(foodPos);
        var isDead = bodyPos.Any(bodyPos => headPos.Equals(bodyPos)) || boundaryPos.Any(pos => headPos.Equals(pos));

        SnakeState state = SnakeState.Alive;
        if (isEating) state = SnakeState.Eating;
        if (isDead) state = SnakeState.Dead;
        snake.state = state;
    }
    
    static void playSnake(Options options)
    {
        int boardX = options.boardX;
        int boardY = options.boardY;
        SnakeSpeed speed = options.speed;
        var snake = new Snake(speed);
        var food = new Food();

        int loop = 0;
        int score = 0;
        while (true)
        {
            Thread.Sleep((int)snake.speed);
            displaySnakeBoard(snake, food, boardX, boardY);

            var prevSnake = new Snake(snake);
            var prevDirection = snake.Head.Direction;
            if (Console.KeyAvailable)
            {
                var inputKey = Console.ReadKey(true).Key;
                if (inputKey == ConsoleKey.Escape) { break; }
                var outputDirection = inputKey switch
                {
                    ConsoleKey.UpArrow => Direction.North,
                    ConsoleKey.DownArrow => Direction.South,
                    ConsoleKey.RightArrow => Direction.East,
                    ConsoleKey.LeftArrow => Direction.West,
                    _ => prevDirection,
                };
                snake.Head.Direction = outputDirection;
            }

            snake.Walk();

            checkSnakeState(snake, food, boardX, boardY);
            SnakeState snakeState = snake.state;
            if (snakeState == SnakeState.Dead)
            {
                displayLose(loop * 0.5, score);
                break;
            }
            if (snakeState == SnakeState.Win)
            {
                displayWin(loop * 0.5, score);
                break;
            }
            if (snakeState == SnakeState.Eating)
            {
                if (snake.Body.Count() == 0) snake.AddBody(prevSnake.Head);
                else snake.AddBody(prevSnake.Body.Last());
                food.generateNewFood(boardX, boardY, loop * 5);
                score++;
            }

            loop++;
        }

        Console.ReadKey(true);
    }
    public static void Main()
    {
        setup();
        
        Options options = new Options();
        string selectedMenu;
        do
        {            
            selectedMenu = displayStartMenu();
            if (selectedMenu == "Mechanics")
            {
                displayMechanics();
                setup();
                continue;
            } 
            else if (selectedMenu == "Options")
            {
                options = displayOptions();
                setup();
                continue;
            }
            else if (selectedMenu == "Play")
            {
                Console.Clear(); //to remove leftover title text if board size is small
                playSnake(options);
                setup();
                continue;
            }
        } while (selectedMenu != "Exit");
        
        defaultSetup();  
    }
}