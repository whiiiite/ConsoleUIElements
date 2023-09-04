namespace ConsoleUIElements.Drawing;

public class ConsoleSquare : IConsoleShape
{
    private int _sideSize = 1;
    /// <summary>
    /// Side size of square
    /// </summary>
    public int SideSize
    {
        get { return _sideSize; }
        set
        {
            if (_sideSize == value) return;

            if (value < 0)
                throw new ArgumentOutOfRangeException("SideSize cannot be less than zero");

            _sideSize = value;
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
    /// Foreground color of square
    /// </summary>
    public ConsoleColor FillColor
    {
        get; set;
    } = Console.ForegroundColor;


    /// <summary>
    /// Fill string for 1 element. Usually is 1 char
    /// </summary>
    public string Fill { get; set; } = _Consts.SquareCharStr;


    public ConsoleSquare() { }


    public ConsoleSquare(int sideSize)
    {
        SideSize = sideSize;
    }


    /// <summary>
    /// Draws square on the console
    /// </summary>
    public void Draw()
    {
        ConsoleColor tmpForeColor = Console.ForegroundColor;
        Console.ForegroundColor = FillColor;

        for (int i = 0; i < OffsetY; i++)
        {
            Console.WriteLine();
        }

        for (int i = 0; i < SideSize; i++)
        {
            Console.Write(new string(' ', OffsetX));
            string line = string.Concat(Enumerable.Repeat(Fill, SideSize));
            Console.WriteLine(line);
        }

        Console.ForegroundColor = tmpForeColor;
    }
}
