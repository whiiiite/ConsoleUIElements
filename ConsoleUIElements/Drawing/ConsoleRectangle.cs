namespace ConsoleUIElements.Drawing;

public class ConsoleRectangle : IConsoleShape
{
    /// <summary>
    /// Fill string for 1 element. Usually is 1 char or 2
    /// </summary>
    public string Fill
    {
        get;
        set;
    } = _Consts.SquareCharStr;


    private int _height = 1;
    /// <summary>
    /// Height of rectangle
    /// </summary>
    public int Height
    {
        get { return _height; }
        set
        {
            if (_height == value) return;

            if (value < 0)
                throw new ArgumentOutOfRangeException("Height cannot be less than zero");

            _height = value;
        }
    }


    private int _width = 1;
    /// <summary>
    /// Width of rectangle
    /// </summary>
    public int Width
    {
        get { return _width; }
        set
        {
            if (_width == value) return;

            if (value < 0)
                throw new ArgumentOutOfRangeException("Width cannot be less than zero");

            _width = value;
        }
    }


    /// <summary>
    /// Offset by X axis for Square
    /// </summary>
    public int OffsetX
    {
        get; set;
    }


    /// <summary>
    /// Offset by Y axis for Square
    /// </summary>
    public int OffsetY
    {
        get; set;
    }



    /// <summary>
    /// Foreground color of rectangle
    /// </summary>
    public ConsoleColor FillColor
    {
        get; set;
    } = Console.ForegroundColor;


    public ConsoleRectangle()
    {

    }


    public ConsoleRectangle(int width, int height)
    {
        Width = width;
        Height = height;
    }


    /// <summary>
    /// Draw a rectangle on the console
    /// </summary>
    public void Draw()
    {
        ConsoleColor tmpForeColor = Console.ForegroundColor;
        Console.ForegroundColor = FillColor;

        for (int i = 0; i < OffsetY; i++)
        {
            Console.WriteLine();
        }

        for (int i = 0; i < Height; i++)
        {
            Console.Write(new string(' ', OffsetX));
            string line = string.Concat(Enumerable.Repeat(Fill, Width));
            Console.WriteLine(line);
        }

        Console.ForegroundColor = tmpForeColor;
    }
}
