using System.Diagnostics;
using System.Text;

namespace ConsoleUIElements.Controls.Output;
/// <summary>
/// Class that contains useful methods and props for output on the console
/// </summary>
public static class ConsoleOutput
{
    internal static bool IsOutUTF8()
    {
        return Console.OutputEncoding.EncodingName == Encoding.UTF8.EncodingName;
    }


    internal static bool IsOutUnicode()
    {
        return Console.OutputEncoding.EncodingName == Encoding.Unicode.EncodingName; 
    }


    internal static bool IsOutAscii()
    {
        return Console.OutputEncoding.EncodingName == Encoding.ASCII.EncodingName; 
    }


    /// <summary>
    /// Print text to the console by give colors
    /// </summary>
    /// <remarks>
    /// Text print does not make any indents or line breaks
    /// </remarks>
    /// <param name="foreColor"></param>
    /// <param name="backColor"></param>
    public static void PrintText(string text, ConsoleColor foreColor, ConsoleColor backColor)
    {
        // save previous state
        ConsoleColor tmpForecolor = Console.ForegroundColor;
        ConsoleColor tmpBackcolor = Console.BackgroundColor;

        // set new state
        Console.ForegroundColor = foreColor;
        Console.BackgroundColor = backColor;

        Console.Write(text);

        // repair previous state
        Console.ForegroundColor = tmpForecolor;
        Console.BackgroundColor = tmpBackcolor;
    }


    /// <summary>
    /// Prints underlined text by ANSI sequences
    /// </summary>
    /// <param name="text"></param>
    public static void PrintUnderlineText(string text)
    {
        var handle = _KernelImports.GetStdHandle(_Consts.STD_OUTPUT_HANDLE);
        uint mode;
        _KernelImports.GetConsoleMode(handle, out mode);
        mode |= _Consts.ENABLE_VIRTUAL_TERMINAL_PROCESSING;
        _KernelImports.SetConsoleMode(handle, mode);

        Console.WriteLine(_Consts.UNDERLINE + text + _Consts.RESET);
    }


    /// <summary>
    /// Prints string and adding your ansi sequence to it
    /// </summary>
    /// <param name="text"></param>
    public static void PrintStringWithAnsi(string text, string sequence)
    {
        var handle = _KernelImports.GetStdHandle(_Consts.STD_OUTPUT_HANDLE);
        uint mode;
        _KernelImports.GetConsoleMode(handle, out mode);
        mode |= _Consts.ENABLE_VIRTUAL_TERMINAL_PROCESSING;
        _KernelImports.SetConsoleMode(handle, mode);

        Console.WriteLine(sequence + text + _Consts.RESET);
    }


    /// <summary>
    /// Prints text by given position on the console
    /// </summary>
    /// <param name="text"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public static void PrintTextByPosition(string text, int x, int y)
    {
        (int tx, int ty) = (Console.CursorLeft, Console.CursorTop);

        Console.CursorLeft = x;
        Console.CursorTop = y;

        Console.WriteLine(text);

        Console.CursorLeft = tx;
        Console.CursorTop = ty;
    }


    /// <summary>
    /// Prints text by given position and colors on the console
    /// </summary>
    /// <param name="text"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public static void PrintTextByPosition(string text, int x, int y,
        ConsoleColor foreColor, ConsoleColor backColor)
    {
        (int tx, int ty) = (Console.CursorLeft, Console.CursorTop);

        ConsoleColor tmpForecolor = Console.ForegroundColor;
        ConsoleColor tmpBackcolor = Console.BackgroundColor;

        Console.ForegroundColor = foreColor;
        Console.BackgroundColor = backColor;

        Console.CursorLeft = x;
        Console.CursorTop = y;

        Console.WriteLine(text);

        Console.CursorLeft = tx;
        Console.CursorTop = ty;

        Console.ForegroundColor = tmpForecolor;
        Console.BackgroundColor = tmpBackcolor;
    }


    /// <summary>
    /// Print text on console char by char with animate effect
    /// </summary>
    /// <remarks>
    /// <b>Note: </b>
    /// This method use Task.Delay to sleep for animSpeedMs milliseconds
    /// </remarks>
    /// <param name="text"></param>
    /// <param name="animSpeed"></param>
    public static async Task PrintTextAnimatedAsync(string text, int animSpeedMs)
    {
        foreach (char character in text)
        {
            Console.Write(character);
            await Task.Delay(animSpeedMs);
        }
    }


    /// <summary>
    /// Prints \r\n as escape sequence
    /// </summary>
    public static void CarriageReturn()
    {
        Console.Write("\r\n");
    }


    /// <summary>
    /// Prints \t as escape sequence
    /// </summary>
    public static void Tab()
    {
        Console.Write('\t');
    }


    /// <summary>
    /// Prints \a as escape sequence
    /// </summary>
    public static void Audible()
    {
        Console.Write('\a');
    }
}
