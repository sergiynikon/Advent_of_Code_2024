namespace ConsoleApp2.Day14;

public record Robot(Coordinate Position, Coordinate Velocity)
{
    public Coordinate Position { get; set; } = Position;
    
    public Coordinate Velocity { get; set; } = Velocity;

    public void Move(Square borders)
    {
        Position = TeleportIfNeeded(Position + Velocity, borders);
    }

    public bool IsInsideBorders(Square borders)
    {
        return Position.IsInsideBorders(borders);
    }

    private Coordinate TeleportIfNeeded(Coordinate c, Square square)
    {
        int newX = ((c.X - square.XMin) % (square.XMax - square.XMin + 1) + (square.XMax - square.XMin + 1)) % (square.XMax - square.XMin + 1) + square.XMin;
        int newY = ((c.Y - square.YMin) % (square.YMax - square.YMin + 1) + (square.YMax - square.YMin + 1)) % (square.YMax - square.YMin + 1) + square.YMin;

        return new Coordinate(newX, newY);
    }
}