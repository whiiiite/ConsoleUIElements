namespace ConsoleUIElements.Views;
/// <summary>
/// Base intefrace for console container
/// </summary>
public interface IConsoleContainer
{
    public void Add(ConsoleListItem item);
    public void Remove(ConsoleListItem item);
    public void Insert(ConsoleListItem item, int index);
    public void RemoveAt(int index);
    public void Clear();
}

