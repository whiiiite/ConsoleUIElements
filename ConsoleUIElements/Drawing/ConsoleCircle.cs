namespace ConsoleUIElements.Drawing;
/// <summary>
/// Represents circle that will be drawn to the console
/// </summary>
public class ConsoleCircle : IConsoleShape
{
    public ConsoleColor FillColor { get; set; } = Console.ForegroundColor;
    public string Fill { get; set; } = _Consts.RectChar.ToString();


    private int _radius = 3;
    /// <summary>
    /// Radius of the circle
    /// </summary>
    public int Radius
    {
        get
        {
            return _radius;
        }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Radius cannot be less than zero");
            }
            _radius = value;
        }
    }


    private int _offsetX;
    /// <summary>
    /// Offset cirlce by X axis on console
    /// </summary>
    public int OffsetX
    {
        get
        {
            return _offsetX;
        }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("OffsetX cannot be less than zero");
            }

            _offsetX = value;
        }
    }


    private int _offsetY;
    /// <summary>
    /// Offset cirlce by Y axis on console
    /// </summary>
    public int OffsetY
    {
        get
        {
            return _offsetY;
        }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("OffsetY cannot be less than zero");
            }

            _offsetY = value;
        }
    }


    private void DrawOffsetY()
    {
        for (int i = 0; i < _offsetY; i++) Console.WriteLine();
    }


    private void DrawOffsetX()
    {
        for (int i = 0; i < _offsetX; i++) Console.Write(' ');
    }


    /// <summary>
    /// Draws non filled circle
    /// </summary>
    public void Draw()
    {
        ConsoleColor tmpForeColor = Console.ForegroundColor;
        Console.ForegroundColor = FillColor;

        DrawOffsetY();

        for (int y = 0; y <= 2 * _radius; y++)
        {
            DrawOffsetX();
            for (int x = 0; x <= 2 * _radius; x++)
            {
                double distance = Math.Sqrt(Math.Pow(x - _radius, 2) + Math.Pow(y - _radius, 2));
                if (distance > _radius - 0.5 && distance < _radius + 0.5)
                {
                    Console.Write(Fill);
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }

        Console.ForegroundColor = tmpForeColor;
    }


    /// <summary>
    /// Draws filled circle
    /// </summary>
    public void DrawFilled()
    {
        ConsoleColor tmpForeColor = Console.ForegroundColor;
        Console.ForegroundColor = FillColor;

        DrawOffsetY();

        for (int y = -_radius; y <= _radius; y++)
        {
            DrawOffsetX();
            for (int x = -_radius; x <= _radius; x++)
            {
                if (x * x + y * y <= _radius * _radius)
                {
                    Console.Write(Fill);
                }
                else
                {
                    Console.Write(" ");
                }
            }
            Console.WriteLine();
        }

        Console.ForegroundColor = tmpForeColor;
    }
}
