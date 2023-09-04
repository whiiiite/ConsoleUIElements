namespace ConsoleUIElements.Views;


public class ConsoleTreeView : IConsoleUIElement
{
    private IList<ConsoleTreeNode> _nodes;

    /// <summary>
    /// Initialize tree view by IList from parameter
    /// </summary>
    /// <param name="nodes"></param>
    public ConsoleTreeView(IList<ConsoleTreeNode> nodes)
    {
        _nodes = nodes;
    }


    public ConsoleTreeView()
    {
        _nodes = new List<ConsoleTreeNode>();
    }


    /// <summary>
    /// Draw the tree view on the console 
    /// </summary>
    public void Draw()
    {
        foreach (var rootNode in _nodes)
        {
            DrawNode(rootNode, 0);
        }
    }


    private void DrawNode(ConsoleTreeNode node, int indentLevel)
    {
        string indentation = new string(' ', indentLevel * 4);

        if(node.HasChilds) Console.WriteLine($"{indentation}{node.Text}─┐");
        else Console.WriteLine($"{indentation}{node.Text}");

        foreach (var childNode in node.ChildNodes)
        {
            DrawNode(childNode, indentLevel + 1); 
        }
    }


    /// <summary>
    /// Adds node to tree view
    /// </summary>
    /// <param name="item"></param>
    public void Add(ConsoleTreeNode item)
    {
        _nodes.Add(item);
    }


    /// <summary>
    /// Removes node from tree view
    /// </summary>
    /// <param name="item"></param>
    public void Remove(ConsoleTreeNode item)
    {
        _nodes.Remove(item);
    }


    /// <summary>
    /// Insert node to tree view by index
    /// </summary>
    /// <param name="item"></param>
    /// <param name="index"></param>
    public void Insert(ConsoleTreeNode item, int index)
    {
        _nodes.Insert(index, item);
    }


    /// <summary>
    /// Removes tree node at index
    /// </summary>
    /// <param name="index"></param>
    public void RemoveAt(int index)
    {
        _nodes.RemoveAt(index);
    }


    /// <summary>
    /// Clear all tree view
    /// </summary>
    public void Clear()
    {
        _nodes.Clear();
    }
}
