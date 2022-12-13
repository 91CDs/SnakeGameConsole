
namespace Snake;
partial class SnakeGame
{
    static void displayLose()
    {
        Console.SetCursorPosition(0,0);
        Console.WriteLine(@"|----------|");
        Console.WriteLine(@"| You Lost |");
        Console.WriteLine(@"|----------|");
    }
    static void displayWin()
    {
        Console.SetCursorPosition(0,0);
        Console.WriteLine(@"|---------|");
        Console.WriteLine(@"| You Won |");
        Console.WriteLine(@"|---------|");
    }
}