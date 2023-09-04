namespace ConsoleUIElements.Views;
public class ConsoleUnorderedList : IConsoleUIElement, IConsoleContainer
{
    /// <summary>
    /// Mark sign before item, example: * itemText
    /// </summary>
    public string MarkSign { get; set; } = "*";

    private IList<ConsoleListItem> _items;

    /// <summary>
    /// Initialize list by IList content given by argument
    /// </summary>
    /// <param name="items"></param>
    public ConsoleUnorderedList(IList<ConsoleListItem> items)
    {
        _items = items;
    }


    public ConsoleUnorderedList()
    {
        _items = new List<ConsoleListItem>();
    }


    /// <summary>
    /// Draws(prints) unordinal list on the console 
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Draw()
    {
        foreach (var item in _items)
        {
            Console.WriteLine(string.Format("{0} {1}", MarkSign, item.Text));
        }
    }


    /// <summary>
    /// Remove item from list
    /// </summary>
    /// <param name="item"></param>
    public void Remove(ConsoleListItem item)
    {
        _items.Remove(item);
    }


    /// <summary>
    /// Add item to end of list
    /// </summary>
    /// <param name="item"></param>
    public void Add(ConsoleListItem item)
    {
        _items.Add(item);
    }


    /// <summary>
    /// Inserts item to list by index
    /// </summary>
    /// <param name="item"></param>
    /// <param name="index"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void Insert(ConsoleListItem item, int index)
    {
        _items.Insert(index, item);
    }

    /// <summary>
    /// Removes items at index
    /// </summary>
    /// <param name="index"></param>
    public void RemoveAt(int index)
    {
        _items.RemoveAt(index);
    }

    /// <summary>
    /// Clears the items in list
    /// </summary>
    public void Clear()
    {
        _items.Clear();
    }
}
