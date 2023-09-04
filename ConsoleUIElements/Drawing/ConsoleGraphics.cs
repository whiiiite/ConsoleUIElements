using ConsoleUIElements.Imaging;

namespace ConsoleUIElements.Drawing;
/// <summary>
/// Graphics class that accumulates
/// drawing and imaging controls.
/// <br/>
/// Also contains own methods
/// </summary>
public class ConsoleGraphics
{

    /// <summary>
    /// Puts pixel (actually char) by coords on console area
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="color"></param>
    /// <param name="pixelChar"></param>
    public void PutPixel(int x, int y, ConsoleColor color, char pixelChar = _Consts.RectChar)
    {
        int tmpX = Console.GetCursorPosition().Left;
        int tmpY = Console.GetCursorPosition().Top;

        ConsoleColor tmpForeground = Console.ForegroundColor;
        Console.CursorLeft = x;
        Console.CursorTop = y;
        Console.ForegroundColor = color;

        Console.Write(pixelChar);

        Console.CursorLeft = tmpX;
        Console.CursorTop = tmpY;
        Console.ForegroundColor = tmpForeground;
    }


    /// <summary>
    /// Drawing rectangle to the console by parameters
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="offsetX"></param>
    /// <param name="offsetY"></param>
    /// <param name="color"></param>
    /// <param name="fill"></param>
    public void DrawRectangle(int width, int height, int offsetX, int offsetY, ConsoleColor color,
        string fill = _Consts.SquareCharStr)
    {
        ConsoleRectangle consoleRectangle = new ConsoleRectangle();
        consoleRectangle.Fill = fill;
        consoleRectangle.Width = width;
        consoleRectangle.Height = height;
        consoleRectangle.FillColor = color;
        consoleRectangle.OffsetX = offsetX;
        consoleRectangle.OffsetY = offsetY;

        consoleRectangle.Draw();
    }


    /// <summary>
    /// Drawing square to the console by parameters
    /// </summary>
    /// <param name="sideSize"></param>
    /// <param name="offsetX"></param>
    /// <param name="offsetY"></param>
    /// <param name="color"></param>
    /// <param name="fill"></param>
    public void DrawSquare(int sideSize, int offsetX, int offsetY, ConsoleColor color,
        string fill = _Consts.SquareCharStr)
    {
        ConsoleSquare sq = new ConsoleSquare();
        sq.Fill = fill;
        sq.SideSize = sideSize;
        sq.FillColor = color;
        sq.OffsetX = offsetX;
        sq.OffsetY = offsetY;

        sq.Draw();
    }


    /// <summary>
    /// Drawing image in native pixels to the console window by HWND of it
    /// </summary>
    /// <param name="sideSize"></param>
    /// <param name="offsetX"></param>
    /// <param name="offsetY"></param>
    /// <param name="color"></param>
    /// <param name="fill"></param>
    public void DrawImageNative(string imgPath, int offsetX = 0, int offsetY = 0,
        int width = 0, int height = 0)
    {
        ConsoleImage img = new ConsoleImage(imgPath);
        img.Width = width;
        img.Height = height;
        img.DrawRealImage(offsetX, offsetY);
    }


    /// <summary>
    /// Draws native image to console version in symbols instead of pixels
    /// </summary>
    /// <param name="imgPath"></param>
    /// <param name="offsetX"></param>
    /// <param name="offsetY"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public void DrawImageConsole(string imgPath)
    {
        ConsoleImage img = new ConsoleImage(imgPath);
        img.DrawConsoleImage();
    }


    /// <summary>
    /// Draws circle on the screen
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="offsetX"></param>
    /// <param name="offsetY"></param>
    /// <param name="color"></param>
    /// <param name="fill"></param>
    /// <param name="isFilled"></param>
    public void DrawCircle(int radius, int offsetX, int offsetY, ConsoleColor color,
        string fill = _Consts.SquareCharStr, bool isFilled = false)
    {
        ConsoleCircle consoleCircle = new ConsoleCircle();
        consoleCircle.Fill = fill;
        consoleCircle.FillColor = color;
        consoleCircle.Radius = radius;
        consoleCircle.OffsetX = offsetX;
        consoleCircle.OffsetY = offsetY;

        if (isFilled)
            consoleCircle.DrawFilled();
        else
            consoleCircle.Draw();
    }


    /// <summary>
    /// Drawing line by specific color and coordinates
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="color"></param>
    /// <remarks>
    /// <b> Note: </b> use 0x2588 char as standart primitive 
    /// for place('pixel') on the console
    /// </remarks>
    public void DrawLine(int x1, int y1, int x2, int y2, ConsoleColor color)
    {
        DrawLineGeneral(x1, y1, x2, y2, color, _Consts.RectChar.ToString());
    }


    /// <summary>
    /// Drawing line by specific color, coordinates 
    /// and drawing primitive per console place('pixel')
    /// </summary>
    /// <param name="x1"></param>
    /// <param name="y1"></param>
    /// <param name="x2"></param>
    /// <param name="y2"></param>
    /// <param name="color"></param>
    /// <remarks>
    /// </remarks>
    public void DrawLine(int x1, int y1, int x2, int y2, ConsoleColor color, string primitive)
    {
        DrawLineGeneral(x1, y1, x2, y2, color, primitive);
    }


    private void DrawLineGeneral(int x1, int y1, int x2, int y2, ConsoleColor color,
        string primitive)
    {
        // is a standart Bresenham's line algorithm

        ConsoleColor tmpFore = Console.ForegroundColor;
        Console.ForegroundColor = color;

        int deltaX = Math.Abs(x2 - x1);
        int deltaY = Math.Abs(y2 - y1);
        int signX = x1 < x2 ? 1 : -1;
        int signY = y1 < y2 ? 1 : -1;
        int error = deltaX - deltaY;

        Console.SetCursorPosition(x2, y2);
        Console.Write(primitive);

        while (x1 != x2 || y1 != y2)
        {
            Console.SetCursorPosition(x1, y1);
            Console.Write(primitive);

            int error2 = error * 2;

            if (error2 > -deltaY)
            {
                error -= deltaY;
                x1 += signX;
            }

            if (error2 < deltaX)
            {
                error += deltaX;
                y1 += signY;
            }
        }

        Console.ForegroundColor = tmpFore;
    }
}

