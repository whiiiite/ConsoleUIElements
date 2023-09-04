namespace ConsoleUIElements.Views;

public class ConsoleTreeNode : ConsoleListItem
{
    public List<ConsoleTreeNode> ChildNodes { get; }
    public bool HasChilds
    {
        get { return ChildNodes.Count > 0; }
    }

    public ConsoleTreeNode(string text)
    {
        Text = text;
        ChildNodes = new List<ConsoleTreeNode>();
    }


    public static implicit operator ConsoleTreeNode(string text)
    {
        return new ConsoleTreeNode(text);
    }


    public override string ToString()
    {
        return base.ToString();
    }
}
