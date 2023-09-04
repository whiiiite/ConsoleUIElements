using ConsoleUIElements.Controls.Output;
using System.Drawing;

namespace ConsoleUIElements.Imaging;

public class ConsoleImage
{

    public string Source { get; set; }


    private int _width = 0; // 0 means full size
    public int Width
    {
        get
        {
            return _width;
        }

        set
        {
            if (value < 0) 
                throw new ArgumentOutOfRangeException("Width cannot be less than zero");

            _width = value;
        }
    }


    private int _height = 0; // 0 means full size
    public int Height
    {
        get
        {
            return _height;
        }

        set
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException("Height cannot be less than zero");

            _height = value;
        }
    }


    public ConsoleImage(string source)
    {
        Source = source;
    }


    public ConsoleImage(string source, int width, int height)
    {
        Source = source;
        Width = width;
        Height = height;
    }


    /// <summary>
    /// Drawing image by window pointer Handler(HWND). Drawing image in all quality of it
    /// </summary>
    /// <param name="offsetX"></param>
    /// <param name="offsetY"></param>
    /// <remarks>
    /// <b>Note:</b> This method not stable and if you will scroll somewhere image
    /// will be dissapeared part by part
    /// <br/>
    /// Is specific of the console.
    /// </remarks>
    public void DrawRealImage(int offsetX = 0, int offsetY = 0)
    {
        IntPtr handler = _KernelImports.GetConsoleHandle();

        using (var graphics = Graphics.FromHwnd(handler))
        using (var image = Image.FromFile(Source))
        {
            int drawWidth = image.Width;
            int drawHeight = image.Height;

            if (Width > 0)
                drawWidth = Width;

            if (Height > 0)
                drawHeight = Height;

            int x = 0 + offsetX;
            int y = 0;

            // hack method to make y axis offset
            for (int i = 0; i < offsetY; i++)
            {
                Console.WriteLine();
            }

            graphics.DrawImage(image, x, y, drawWidth, drawHeight);
        }
    }


    /// <summary>
    /// Draws image to console version (in symbols instead of pixels)
    /// </summary>
    /// <param name="source"></param>
    public void DrawConsoleImage()
    {
        _DrawImgConsoleGeneral(grayScale: false);   
    }


    /// <summary>
    /// Draws image in gray tones of colors from RGB.
    /// </summary>
    public void DrawConsoleImageGrayScale()
    {
        _DrawImgConsoleGeneral(grayScale: true);
    }


    private void _DrawImgConsoleGeneral(bool grayScale = false)
    {
        ConsoleColor tmpFore = Console.ForegroundColor;
        ConsoleColor tmpBack = Console.BackgroundColor;

        // USING IS NEEDED HERE.
        // For free image handle descriptor in PC memory
        using Image source = new Bitmap(Source);

        int sMax = 39;
        decimal percent = Math.Min(decimal.Divide(sMax, source.Width), decimal.Divide(sMax, source.Height));
        Size dSize = new Size((int)(source.Width * percent), (int)(source.Height * percent));
        Bitmap bmpMax = new Bitmap(source, dSize.Width * 2, dSize.Height);
        for (int i = 0; i < dSize.Height; i++)
        {
            for (int j = 0; j < dSize.Width; j++)
            {
                if (!grayScale)
                {
                    @__ConsoleWritePixelRGB(bmpMax.GetPixel(j * 2, i));
                    @__ConsoleWritePixelRGB(bmpMax.GetPixel(j * 2 + 1, i));
                    continue; // <-- !!
                }

                // if is not gray scale this code will not be work
                @__ConsoleWritePixelGray(bmpMax.GetPixel(j * 2, i));
                @__ConsoleWritePixelGray(bmpMax.GetPixel(j * 2 + 1, i));
            }
            Console.WriteLine();
        }

        // we need to restore it after printing image
        Console.ForegroundColor = tmpFore;
        Console.BackgroundColor = tmpBack;
    }


    /// <summary>
    /// Is not safe to read this code
    /// </summary>
    /// <param name="cValue"></param>
    private void @__ConsoleWritePixelRGB(Color cValue)
    {
        int[] cColors = { 
            0x000000, 
            0x000080, 
            0x008000, 
            0x008080, 
            0x800000, 
            0x800080, 
            0x808000, 
            0xC0C0C0, 
            0x808080, 
            0x0000FF, 
            0x00FF00, 
            0x00FFFF, 
            0xFF0000, 
            0xFF00FF, 
            0xFFFF00, 
            0xFFFFFF 
        };
        
        Color[] cTable = cColors.Select(x => Color.FromArgb(x)).ToArray();
        char[] rList = new char[] { (char)9617, (char)9618, (char)9619, (char)9608 }; // 1/4, 2/4, 3/4, 4/4
        int[] bestHit = new int[] { 0, 0, 4, int.MaxValue }; //ForeColor, BackColor, Symbol, Score

        for (int rChar = rList.Length; rChar > 0; rChar--)
        {
            for (int cFore = 0; cFore < cTable.Length; cFore++)
            {
                for (int cBack = 0; cBack < cTable.Length; cBack++)
                {
                    int R = (cTable[cFore].R * rChar + cTable[cBack].R * (rList.Length - rChar)) / rList.Length;
                    int G = (cTable[cFore].G * rChar + cTable[cBack].G * (rList.Length - rChar)) / rList.Length;
                    int B = (cTable[cFore].B * rChar + cTable[cBack].B * (rList.Length - rChar)) / rList.Length;
                    int iScore = (cValue.R - R) * (cValue.R - R) + (cValue.G - G) * (cValue.G - G) + (cValue.B - B) * (cValue.B - B);
                    if (!(rChar > 1 && rChar < 4 && iScore > 50000)) // rule out too weird combinations
                    {
                        if (iScore < bestHit[3])
                        {
                            bestHit[3] = iScore; //Score
                            bestHit[0] = cFore;  //ForeColor
                            bestHit[1] = cBack;  //BackColor
                            bestHit[2] = rChar;  //Symbol
                        }
                    }
                }
            }
        }
        Console.ForegroundColor = (ConsoleColor)bestHit[0];
        Console.BackgroundColor = (ConsoleColor)bestHit[1];
        Console.Write(rList[bestHit[2] - 1]);
    }


    private void @__ConsoleWritePixelGray(Color cValue)
    {
        // this code might be quite simple

        // original formula: 
        // 0.299 * R + 0.587 * G + 0.114 * B
        // but float calculations might be too much for that simple operation
        int grayScale = (cValue.R + cValue.G + cValue.B) / 3;
       
        // just check the grayscale and pick pixel for it
        // gray is between 0 and 255. 0 - black, 255 - white
        if (grayScale <= 15)
        {
            ConsoleOutput.PrintText(" ", ConsoleColor.Black, ConsoleColor.Black);
        }
        else if (grayScale > 15 && grayScale <= 60)
        {
            Console.Write(_Consts.LightBlock);
        }
        else if (grayScale > 60 && grayScale <= 115)
        {
            Console.Write(_Consts.MidBlock);
        }
        else if (grayScale > 115 && grayScale <= 215)
        {
            Console.Write(_Consts.DarkBlock);
        }
        else if (grayScale > 215 && grayScale <= 255)
        {
            Console.Write(_Consts.RectChar);
        }
    }
}
