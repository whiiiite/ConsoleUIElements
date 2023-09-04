using System.Text;

namespace ConsoleUIElements.Controls.Input;
/// <summary>
/// Contains methods for console user interaction with input
/// </summary>
public static class ConsoleUserInput
{
    /// <summary>
    /// Request input string by user.
    /// <br/>
    /// Print the prompt message before input if was passed.
    /// </summary>
    /// <param name="promptMessage"></param>
    /// <returns>
    /// <see cref="string.Empty"/> if is null or empty.
    /// <br></br>
    /// <see cref="string"/> if something was in input.
    /// </returns>
    public static string GetString(string? promptMessage = null)
    {
        if (promptMessage != null)
            Console.Write(promptMessage);

        string? response = Console.ReadLine();
        return response == null ? string.Empty : response;
    }


    /// <summary>
    /// Request input int by user.
    /// <br/>
    /// Try to parse an integer passed by user.
    /// <br/>
    /// Print the prompt message before input if was passed.
    /// </summary>
    /// <param name="promptMessage"></param>
    /// <returns>
    /// <see cref="null"/> if is null or empty or not valid format.
    /// <br></br>
    /// <see cref="int"/> value if successfully parsed
    /// </returns>
    public static int? GetInt(string? promptMessage = null)
    {
        if (promptMessage != null)
            Console.Write(promptMessage);

        string? response = Console.ReadLine();

        if (response == null) return null;

        bool isOk = int.TryParse(response, out int value);
        return !isOk ? null : value;
    }


    /// <summary>
    /// Request input <see cref="DateTime"/> by user.
    /// <br/>
    /// Try to parse <see cref="DateTime"/> passed by user.
    /// <br/>
    /// Print the prompt message before input if was passed.
    /// </summary>
    /// <param name="promptMessage"></param>
    /// <returns>
    /// <see cref="null"/> if is null or empty or not valid format.
    /// <br></br>
    /// <see cref="DateTime?"/> value if successfully parsed
    /// </returns>
    public static DateTime? GetDateTime(string? promptMessage = null)
    {
        if (promptMessage != null)
            Console.Write(promptMessage);

        string? response = Console.ReadLine();

        if (response == null) return null;

        bool isOk = DateTime.TryParse(response, out DateTime value);
        return !isOk ? null : value;
    }


    /// <summary>
    /// Request input <see cref="bool"/> by user.
    /// <br/>
    /// Try to parse <see cref="bool"/> passed by user.
    /// <br/>
    /// Print the prompt message before input if was passed.
    /// <br></br>
    /// Also can accept value as 1(True) and 0(False)
    /// </summary>
    /// <param name="promptMessage"></param>
    /// <returns>
    /// <see cref="null"/> if is null or empty or empty format.
    /// <br></br>
    /// <see cref="bool"/> value if successfully parsed
    /// </returns>
    public static bool? GetBoolean(string? promptMessage = null)
    {
        if (promptMessage != null)
            Console.Write(promptMessage);

        string? response = Console.ReadLine();

        if (response == null) return null;

        string normilizeResponse = response.Trim().ToLower();
        bool isOk = bool.TryParse(normilizeResponse, out bool value);
        if (isOk) return value;

        // if not parsed then look for alternative input (0 and 1)

        if (normilizeResponse == "0") return false;
        if (normilizeResponse == "1") return true;

        // if all ways is failed - return null
        return null;
    }


    /// <summary>
    /// Gets the string as password from the user input
    /// <br/>
    /// Also may validate inputed key by your or standart validator (if is null - is standart)
    /// </summary>
    /// <param name="passwordChar"></param>
    /// <returns>Password given by user</returns>
    public static string GetPassword(string? promptMessage = null, char passwordChar = '*',
        IConsolePasswordValidator? validator = null)
    {
        if (promptMessage != null)
            Console.Write(promptMessage);

        validator ??= new StandartConsolePasswordValidator();

        StringBuilder input = new StringBuilder();
        while (true)
        {
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                break;
            }
            if (key.Key == ConsoleKey.Backspace && input.Length > 0)
            {
                input.Remove(input.Length - 1, 1);

                if (passwordChar != '\0')
                {
                    Console.SetCursorPosition(x - 1, y);
                    Console.Write(" ");
                    Console.SetCursorPosition(x - 1, y);
                }
            }
            else if (!validator.Validate(key))
            {
                // not valid - do nothing
            }
            else if (key.Key != ConsoleKey.Backspace)
            {
                input.Append(key.KeyChar);
                Console.Write(passwordChar);
            }
        }
        return input.ToString();
    }
}

