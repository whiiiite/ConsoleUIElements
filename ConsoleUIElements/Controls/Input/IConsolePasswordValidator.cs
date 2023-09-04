namespace ConsoleUIElements.Controls.Input
{
    /// <summary>
    /// Base inteface for validate user input password
    /// </summary>
    public interface IConsolePasswordValidator
    {
        public bool Validate(ConsoleKeyInfo passwordChar);
    }
}
