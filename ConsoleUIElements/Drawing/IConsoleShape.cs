namespace ConsoleUIElements.Drawing;
/// <summary>
/// Base intefrace for console shapes
/// </summary>
public interface IConsoleShape : IConsoleUIElement
{
    /// <summary>
    /// Main drawing color of the shape
    /// </summary>
    ConsoleColor FillColor
    {
        get; set;
    }


    /// <summary>
    /// Fill characthers of shape. Usually is 1 char
    /// </summary>
    string Fill { get; set; }
}

