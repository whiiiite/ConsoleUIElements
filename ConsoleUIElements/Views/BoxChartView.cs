using ConsoleUIElements.Controls.Output;
using ConsoleUIElements.Drawing;

namespace ConsoleUIElements.Views;

/// <summary>
/// Class for box charts for some comparations
/// </summary>
public class BoxChartView : IConsoleUIElement
{
    private List<ChartBox> chartBoxes;


    private int _offY;
    /// <summary>
    /// Position for Y in console window
    /// </summary>
    public int OffsetY
    {
        get { return _offY; }
        set 
        {
            if (value < 0)
            {
                throw new ArgumentException("Offset by Y can not be less than zero");
            }

            if (_offY == value) return;

            _offY = value; 
        }
    }


    private int _offX;
    /// <summary>
    /// Offset for X in console window
    /// </summary>
    public int OffsetX
    {
        get { return _offX; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Offset by X can not be less than zero");
            }

            if (_offX == value) return;

            _offX = value;
        }
    }


    private int _spaceBetween;
    /// <summary>
    /// Represents space between chart boxes
    /// </summary>
    public int SpaceBetween
    {
        get { return _spaceBetween; }
        set
        {
            if (value < 0)
                throw new ArgumentException("SpaceBetween can not be less than zero");

            _spaceBetween = value;
        }
    }


    /// <summary>
    /// Color that will be fill background of all chart area
    /// </summary>
    public ConsoleColor? BackColor
    {
        get; set;
    } = null;

    public BoxChartView() 
    {
        chartBoxes = new List<ChartBox>();
    }


    public BoxChartView(List<ChartBox> chartBoxes)
    {
        this.chartBoxes = chartBoxes;
    }


    /// <summary>
    /// Draws the chart. All chart boxes of it.
    /// <br/>
    /// This element calculat his own size when it drawing.
    /// So you can just add some extra offsets to X and Y if needed
    /// </summary>
    public void Draw()
    {
        if (chartBoxes.Count <= 0) return;

        ChartBox highestBox = chartBoxes.OrderByDescending(box => box.Height).First();

        // calculate Y for not to ruin other console elements
        int y = Console.GetCursorPosition().Top + highestBox.Height + OffsetY;

        _FillBackground(OffsetX, OffsetY + 1, highestBox.Height);

        Console.SetCursorPosition(OffsetX, y);

        // offset for position of box and draw underline after
        int boxOffsetX = 0;
        foreach (ChartBox box in chartBoxes)
        {
            box.__internal_posX = boxOffsetX + OffsetX;
            box.__internal_posY = y;
            box.Draw();
            boxOffsetX += SpaceBetween + box.Width;
        }

        ConsoleOutput.CarriageReturn();
        Console.Write(new string(' ', OffsetX));
        Console.Write(new string(_Consts.RectChar, boxOffsetX));
    }


    private void _FillBackground(int ox, int oy, int height)
    {
        if (BackColor == null) return;

        int width = 0;
        foreach (var box in chartBoxes)
        {
            width += SpaceBetween + box.Width;
        }

        ConsoleRectangle consoleRectangle = new ConsoleRectangle();
        consoleRectangle.FillColor = (ConsoleColor)BackColor!;
        consoleRectangle.Fill = _Consts.RectChar.ToString();
        consoleRectangle.Height = height;
        consoleRectangle.Width = width;
        consoleRectangle.OffsetX = ox;
        consoleRectangle.OffsetY = oy;

        consoleRectangle.Draw();
    }
}
