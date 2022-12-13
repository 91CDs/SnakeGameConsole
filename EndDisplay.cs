
namespace Snake;
partial class SnakeGame
{
    static void displayLose(double time, int score)
    {
        Console.SetCursorPosition(0,0);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("|----------|");
        Console.WriteLine("| You Lost |");
        Console.WriteLine("|----------|");
        Console.WriteLine($"> Time: {time}s ");
        Console.WriteLine($"> Score: {score} ");

    }
    static void displayWin(double time, int score)
    {
        Console.SetCursorPosition(0,0);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(@"|---------|");
        Console.WriteLine(@"| You Won |");
        Console.WriteLine(@"|---------|");
        Console.WriteLine($"> Time: {time}s ");
        Console.WriteLine($"> Score: {score} ");
    }
}