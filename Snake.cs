using System.Numerics;

namespace Snake;
enum Direction 
{
    North,
    South,
    East,
    West,
}
class Food
{
    public Vector2 P { get; set; }
    public Food(int x, int y)
    {
        P = new Vector2(x, y);
    }
    static Food generateFood(int maxX, int maxY, int time)
    {
        Random randomX = new Random(time);
        Random randomY = new Random(time);
        int x = randomX.Next(0, maxX);
        int y = randomY.Next(0, maxY);
        return new Food(x, y);
    }
}
class Snake
{
    public SnakePart Head { get; set; } = new SnakePart();
    public List<SnakePart> Body { get; set;} = new List<SnakePart>();
    public List<Vector2> getAllPoints()
    {
        List<Vector2> allPoints = new List<Vector2>();
        allPoints.Add(Head.P);
        foreach (var part in Body)
        {
            allPoints.Add(part.P);
        }
        return allPoints;
    }
    public void AddBody(SnakePart prevPart)
    {
        var dir = prevPart.Direction;
        var x = Convert.ToInt32(prevPart.P.X);
        var y = Convert.ToInt32(prevPart.P.Y);

        switch (dir)
        {
            case Direction.North:
                y--;
                break;
            case Direction.South:
                y++;
                break;
            case Direction.East:
                x--;
                break;
            case Direction.West:
                x++;
                break;
        }
        
        SnakePart newPart = new SnakePart(x, y, dir);
        Body.Add(newPart);
    }
    public void Walk() 
    {
        var head = Head;
        var hdir = Head.Direction;
        var hx = Convert.ToInt32(Head.P.X);
        var hy = Convert.ToInt32(Head.P.Y);

        switch (hdir)
        {
            case Direction.North:
                hy++;
                break;
            case Direction.South:
                hy--;
                break;
            case Direction.East:
                hx++;
                break;
            case Direction.West:
                hx--;
                break;
        }
        Head.Move(hx, hy, hdir);

        if (Body.Count != 0)
        {
            for (int i = 0; i < Body.Count; i++)
            {
                var prevPart = Body[i];
                if (i == 0)
                {
                    Body[i].Move(head);
                }
                else
                {
                    Body[i].Move(prevPart);
                }
            }
        }
    }
}
class SnakePart
{
    public Vector2 P { get; set; }
    public Direction Direction { get; set; }
    public SnakePart() 
    {
        P = new Vector2(3,10);
        Direction = Direction.East;
    }
    public SnakePart(int x, int y, Direction _Direction)
    {
        Vector2 point = new Vector2(x, y);
        P = point;
        Direction = _Direction;
    }
    public void Move(SnakePart part)
    {
        P = part.P;
        Direction = part.Direction;
    }
    public void Move(int x, int y, Direction _Direction)
    {
        Vector2 point = new Vector2(x, y);
        P = point;
        Direction = _Direction;
    }
}