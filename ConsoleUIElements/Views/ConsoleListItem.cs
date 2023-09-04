namespace ConsoleUIElements.Views;

/// <summary>
/// Represents console list item for some containers lists
/// </summary>
public class ConsoleListItem
{
    public string Text { get; set; } = string.Empty;


    public ConsoleListItem() 
    {

    }


    public ConsoleListItem(string text)
    {
        Text = text;
    }


    public static implicit operator ConsoleListItem(string text)
    {
        return new ConsoleListItem(text);
    }


    public override string ToString()
    {
        return Text;
    }
}
