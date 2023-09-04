namespace ConsoleUIElements.Controls.Input;
/// <summary>
/// Standart console password validator for user input
/// <br/>
/// Match keys: A-Za-z0-9 and _ and -
/// </summary>
public class StandartConsolePasswordValidator : IConsolePasswordValidator
{

    public bool Validate(ConsoleKeyInfo passwordChar)
    {
        char keyChar = passwordChar.KeyChar;
        if (keyChar <= 47 && keyChar != 45)
        {
            return false;
        }
        else if (keyChar > 57 && keyChar < 65)
        {
            return false;
        }
        else if (keyChar > 90 && keyChar < 97 && keyChar != '_')
        {
            return false;
        }
        else if (keyChar > 122)
        {
            return false;
        }

        return true;
    }
}
