namespace ConsoleUIElements.Views;
/// <summary>
/// Class that represents chart box in <see cref="BoxChartView"/>
/// </summary>
public class ChartBox : IConsoleUIElement
{
    private int _height;
    public int Height
    {
        get { return _height; }
        set 
        {
            if (value <= 0)
                throw new ArgumentException("Height can not be less or equals zero");

            _height = value; 
        }
    }


    private int _width;
    public int Width
    {
        get { return _width; }
        set 
        {
            if (value <= 0)
                throw new ArgumentException("Width can not be less or equals zero");

            _width = value;
        }
    }


    /// <summary>
    /// Represents char that will be using for draw the box
    /// </summary>
    public char DrawChar
    {
        get; set;
    } = _Consts.RectChar;


    /// <summary>
    /// Color that will be used for draw the box
    /// </summary>
    public ConsoleColor BoxColor
    {
        get; set;
    } = Console.ForegroundColor;


    // only for this assembly using
    internal int __internal_posY { get; set; }
    internal int __internal_posX { get; set; }


    public void Draw()
    {
        (int tx, int ty) = Console.GetCursorPosition();

        Console.SetCursorPosition(__internal_posX, __internal_posY);

        ConsoleColor tmpFore = Console.ForegroundColor;
        Console.ForegroundColor = BoxColor;

        // dont use standart draw line method
        // because is too complicated for that simple operation
        for (int i = 0; i < Height; i++)
        {
            Console.Write(new string(DrawChar, Width));
            int curY = Console.GetCursorPosition().Top;
            Console.SetCursorPosition(__internal_posX, curY - 1);
        }

        Console.ForegroundColor = tmpFore;

        Console.SetCursorPosition(tx, ty);
    }
}
