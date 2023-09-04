namespace ConsoleUIElements.Controls;

/// <summary>
/// Base console control interface
/// </summary>
public interface IControl : IConsoleUIElement
{
    ConsoleColor ForeColor
    {
        get; set;
    }


    ConsoleColor BackColor
    {
        get; set;
    }
}
