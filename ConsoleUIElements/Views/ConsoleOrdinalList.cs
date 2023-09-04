namespace ConsoleUIElements.Views;

/// <summary>
/// Class that represents ordinal list for console output
/// </summary>
public class ConsoleOrdinalList : IConsoleUIElement, IConsoleContainer
{
    private IList<ConsoleListItem> _items;


    /// <summary>
    /// Initialize list by IList content given by argument
    /// </summary>
    /// <param name="items"></param>
    public ConsoleOrdinalList(IList<ConsoleListItem> items)
    {
        _items = items;
    }


    public ConsoleOrdinalList()
    {
        _items = new List<ConsoleListItem>();
    }


    public void Draw()
    {
        for (int i = 0; i < _items.Count; i++)
        {
            Console.WriteLine(string.Format("{0} {1}", (i + 1).ToString() + '.', _items[i]));
        }
    }

    public void Insert(ConsoleListItem item, int index)
    {
        _items.Insert(index, item);
    }

    public void Add(ConsoleListItem item)
    {
        _items.Add(item);
    }


    public void Remove(ConsoleListItem item)
    {
        _items.Remove(item);
    }


    public void RemoveAt(int index)
    {
        _items.RemoveAt(index);
    }


    public void Clear()
    {
        _items.Clear();
    }
}

