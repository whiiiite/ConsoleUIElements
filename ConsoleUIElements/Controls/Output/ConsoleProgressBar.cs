using System.Text;

namespace ConsoleUIElements.Controls.Output;

public class ConsoleProgressBar : IControl
{
    private int _value;
    public int Value
    {
        get
        {
            return _value;
        }
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(
                    "Value can not be negative");

            if (value < MinValue)
                throw new ArgumentOutOfRangeException(
                    "Value can not be less than MinValue");

            _value = value;
        }
    }


    private int _maxValue;
    public int MaxValue
    {
        get
        {
            return _maxValue;
        }
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(
                    "MaxValue can not be negative");

            if (value <= MinValue)
                throw new ArgumentOutOfRangeException(
                    "MaxValue can not be less than MinValue");

            _maxValue = value;
        }
    }


    private int _minValue;
    public int MinValue
    {
        get
        {
            return _minValue;
        }
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(
                    "MinValue can not be negative");

            if (value >= MaxValue)
                throw new ArgumentOutOfRangeException(
                    "MinValue can not be more than MaxValue");

            _minValue = value;
        }
    }


    private int _size;
    public int Size
    {
        get { return _size; }
        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(
                    "Size can not be negative");

            _size = value;
        }
    }


    public ConsoleColor ForeColor
    {
        get; set;
    } = Console.ForegroundColor;


    public ConsoleColor BackColor
    {
        get; set;
    } = Console.BackgroundColor;


    public ConsoleProgressBar(int maxValue = 100, int minValue = 0, int size = 20)
    {
        MaxValue = maxValue;
        MinValue = minValue;
        Size = size;
    }


    /// <summary>
    /// Draw progress bar by values on the console.
    /// <br/>
    /// May cause issues in ASCII console output encoding
    /// </summary>
    public void Draw()
    {
        if (ConsoleOutput.IsOutUTF8() || ConsoleOutput.IsOutUnicode())
        {
            Console.Write(' ');
            for (int i = 0; i < Size; i++)
            {
                Console.Write('_');
            }
            Console.WriteLine();
        }

        Console.Write('[');

        ConsoleColor tmpForeColor = Console.ForegroundColor;
        ConsoleColor tmpBackColor = Console.BackgroundColor;
        Console.ForegroundColor = ForeColor;
        Console.BackgroundColor = BackColor;

        int sizeFilled = (Value - MinValue) * Size / (MaxValue - MinValue);
        int sizeEmpty = Size - sizeFilled;

        for (int i = 0; i < sizeFilled; i++) Console.Write(_Consts.RectChar);

        for (int i = 0; i < sizeEmpty; i++) Console.Write(' ');

        Console.ForegroundColor = tmpForeColor;
        Console.BackgroundColor = tmpBackColor;

        Console.Write(']');

        if (ConsoleOutput.IsOutUTF8() || ConsoleOutput.IsOutUnicode())
        {
            Console.WriteLine();
            Console.Write(' ');
            for (int i = 0; i < Size; i++)
            {
                Console.Write('‾');
            }
        }

        Console.WriteLine();
    }
}
