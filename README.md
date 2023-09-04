# Console UI Elements Documentation
Console UI Elements - is a quite simple library that was wrote in C# language for .NET Console Projects. Actually that was not wrote as real library. I just found it on my old drive from days when i was learning C#.

This library provides a simple tool kit for make development of your console applications more easier.     
Also it provides tools for more comfortable work with user input, console output, drawing, and even some sort of console graphics.

Library have tools for:
- Operating with variable sorts of user input for variable data types(password input, datetime, strings, int, bool).
    - remark: Also this inputs was improved to have more safety for program. This use safe ways to parse, convert and get data for not to break your program.
- Working with console output. Like text output. Some sort of text animations. Some controls like progress bar.
- Working with primitive drawing. Like shapes(circles, rectangles, squares), lines, pixels(actually symbols places on cmd window).
- Working with imaging. Like real images rendering, as in real high quality rendering(without quality losses), as in console version rendering (by character places in console window or "pixels").
    - remark: console version of image is rendering also in colors as real image. But obviously not in the same quality as real image of real pixels
- Working with collections or views. Like: ordered list view, unordered list view, tree view, charts etc.

This library was wrote just for some console projects, like remote installer, package managers, etc.. and it was not meant to be enhanched and published. But i did it.                                     
It was just made for make development of small(or big) console projects more comfortable and make those projects "colorful" and with some sort of graphics.

# Requirements
This library only for Windows OS. Because some sections of this lib is using ```Bitmap``` class, that available only for Windows OS. For that fact some of tools like for imaging may not to works on others OS. But others parts was wrote with using cross-platform tools.                           
Im highly recommends use this library for Windows 10 and highter.

Also your console project Should works in Unicode(also may in UTF-8 and standart UTF-16) output and input encodings. But not neccesery if you will change some standart symbols in some methods or classes for output as ASCII or others encodings specific for your project. But first of all this library was wrote for unicode encoding.

# External dependencies that this library using
- System.Drawing.Common (ver 7.0.0)
You need it for use drawing tools

# Introduction to library
Is not very hard library to understand. Is just small set of tools for console apps.

Let's start from basics.
# Console Input
 
Input tools for console is located in ConsoleUIElements.Controls.Input
    
Components for user input is quite simple and monotonous

Class that manage all input tools is named ``` ConsoleUserInput ```. Is a static class that contains all necessary methods for deal with console user input.

```c#
using ConsoleUIElements.Controls.Input;

bool? bVal = ConsoleUserInput.GetBoolean("My prompt here for boolean: ");
int? iVal = ConsoleUserInput.GetInt(); // we may not to write prompt if not needed
DateTime? time = ConsoleUserInput.GetDateTime("Datetime here");
string str = ConsoleUserInput.GetString("Get string: ");
string password = ConsoleUserInput.GetPassword("Password: ", '\0'); // get password with zero terminate hide char

Console.WriteLine();
Console.WriteLine("+----------------------------------+");

Console.WriteLine();
Console.WriteLine(bVal);
Console.WriteLine(iVal);
Console.WriteLine(time);
Console.WriteLine(str);
Console.WriteLine(bVal);
Console.WriteLine(password);
```

Output of this sample: 
``` 
My prompt here for boolean: 1
123
Datetime here28.08.2023 16:11
Get string: my string here
Password:

+----------------------------------+

True
123
28.08.2023 16:11:00
my string here
True
123321
```

For password we choosen '\0' char that means no chars will be output, that hides even length of password. But there can be every char you want, for example '*'.        
Also for password you can write your own validator that will validate every key. By default method using standart password validator that can keep ASCII letters, digits and some symbols like _ and -  
For write your own validator, just create class of that and inherit from ```IConsolePasswordValidator``` that contains only 1 method for implement. Then pass it as third parameter to ```GetPassword()``` Method
 
Methods like ```GetInt(), GetBoolean(), GetDatetime()``` can return null if parse was not success. But it will never throws any exceptions for not to break your app.   
Note: ```GetBoolean()``` can also get values like 1 and 0 that means true and false

Method ```GetString()``` garantee you, that it will return value even if is null, it will return string.Empty.

There is no something very difficult to understand in this section.


# Console Output

Output tools for console is located at ConsoleUIElements.Controls.Output

This section a bit different by structure. Because it has some more classes and features.

Main class for handle console output is ```ConsoleOutput```. Also this section has class ```ConsoleProgressBar``` but about this we will speak a little bit later.

Here is the sample for all methods from ```ConsoleOutput```: 
```c#
using ConsoleUIElements.Controls.Output;

ConsoleOutput.PrintTextByPosition("some text", 15, 0);
ConsoleOutput.CarriageReturn();
ConsoleOutput.PrintText("some text with colors", ConsoleColor.DarkBlue, ConsoleColor.White);
ConsoleOutput.CarriageReturn();
ConsoleOutput.Tab();
await ConsoleOutput.PrintTextAnimatedAsync("animated text", 250);
ConsoleOutput.Audible();
```

Here is everything quite simple. ```PrintTextByPosition()``` just prints text by some specific position on the console by x and y paramenters.  
```CarriageReturn()``` just prints '\r\n' as escape sequence to the console  
```PrintText()``` prints the text by given colors. Very imported thing that color applying only for this text but not for next prints.  
```Tab()``` just prints '\t' as escape sequence to the console.  
```PrintTextAnimatedAsync()``` is awaitable async method that returns Task for await. And prints text by delay in given milliseconds. In our case is 250.  
```Audible()``` prints '\a' as escape sequence. But it does not have any visual interpritation, is just system sound.

Also output has support of ansi sequences.  
Standart method for underlined text and more flexible method for print any ansi sequence you want.
Sample: 
```C#
using ConsoleUIElements.Controls.Output;

ConsoleOutput.PrintUnderlineText("Test");
ConsoleOutput.PrintStringWithAnsi("Test", "\u001b[31m\x1B[4m");
```  
This code will output underlined text Test and in next line it will also output underlined Test but now is in red color. You can write every ansi sequence you want.  


### ```ConsoleProgressBar```  
is just like normal progress bar from every UI desktop framework or from web html, but in console.  
Here is the sample for progress bar:  
```c#
using ConsoleUIElements.Controls.Output;

ConsoleProgressBar progressBar = new ConsoleProgressBar();
progressBar.MinValue = 0;
progressBar.MaxValue = 100;
progressBar.Value = 32;
progressBar.BackColor = ConsoleColor.White;
progressBar.ForeColor = ConsoleColor.Red;

progressBar.Draw();
```

Output:
```
[██████              ]
```

Markdown can not show the colors but trust me, filled area is red, and back area is white colors. (Maybe i'll pin some pictures later).

But also we have some magic trick here!   
Just see this sample: 
```c#
using ConsoleUIElements.Controls.Output;
using System.Text;

ConsoleProgressBar progressBar = new ConsoleProgressBar();
progressBar.MinValue = 0;
progressBar.MaxValue = 100;
progressBar.Value = 32;
progressBar.BackColor = ConsoleColor.White;
progressBar.ForeColor = ConsoleColor.Red;

Console.OutputEncoding = Encoding.UTF8;
progressBar.Draw();
```

Notice that we change ```OutputEncoding``` to UTF8(also it can be Unicode), but why?

here is output after that trick: 
```
 ____________________
[██████              ]
 ‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾
```
Is became more detailized.  
Trick here that it has 2 modes for output. For some reason standart Output Encoding of console in C# don't want to output char '‾' and it requires encodings like UTF-8 or Unicode. So i did output for 2 modes. Also it useful when you deal with ASCII output or switch it between encodings.

# Imaging

Imaging in this library is for rendering some media elements. For now is just images.  
It has only 1 class that named ```ConsoleImage```

Here is the sample:
```c#
ConsoleImage img = new ConsoleImage("C:\\Progs\\de\\AllProjects\\ConsoleUIElements\\docs\\input_img.PNG");
img.Width = 120;
img.Height = 120;
img.DrawConsoleImage();
img.DrawConsoleImageGrayScale();
img.DrawRealImage();
```

Here is everything very very simple. I just used some of my local images. It can take every image you want, png, jpg, bmp, etc.   
```Width``` and ```Height``` property using only for drawing real image from pixels. But not for console versions of it.

```DrawConsoleImage()``` method that drawing console version of your image by symbols. All colors is stay like on real image. But you should know that console version of real pictures can not be in high quality as real images from a lot of pixels. Just because console has less colors and one symbol take a bunch of pixels to draw 1 real pixel. So its will look like image from 4k was shrinked to 32x32 pixels. Is just specifics of console. But it draws good for small images.

```DrawConsoleImageGrayScale()``` method that doing just the same as ```DrawConsoleImage()```, but without colors, only in grayscale. It convert RGB to gray tones. It can be useful if your picture is also in grayscale. Actually simple grayscale images is get perfect results being drawing in console.

```DrawRealImage()``` method that draws real image pixel-by-pixel to console using pixels of console window in original quality. But is not just perfect as it can seems. If you scroll somewhere from view area of this image it will be dissapeared. Because console don't really care about why we drawn on pixels of it. It looks only for places for symbols. Secondly is quite dangerous operation, because under the hood is using pointers and functons from user32.dll for get Handle(HWND) of console window for draw pixels on it. This method bring the problems, but it was interesting to implement it in console.


# Views
Everyting you saw before this moment. It was like some helper tools that can be useful, but is very small part. Now we will dive into some real useful things.  
All views is located at ConsoleUIElements.Views

Views has a lot of useful components like, visual lists, ordered or not. Tree view. And even charts for statistic.

### Unordered list  
Let's start from simplest one. Is unordered list.  
From name of it you can understand how it works. Is just list that do not have any order. Just bunch of items.

Here is the sample for ```ConsoleUnorderedList```:  
``` C#
using ConsoleUIElements.Views;

ConsoleListItem item1 = "Item1";
ConsoleListItem item2 = "Item2";
ConsoleListItem item3 = new ConsoleListItem("Item3");

List<ConsoleListItem> items = new List<ConsoleListItem>()
{
    item1, item2, item3
};

ConsoleUnorderedList unordinalList = new ConsoleUnorderedList(items);
unordinalList.MarkSign = "-";

ConsoleListItem item4 = new ConsoleListItem("Item4");
unordinalList.Add(item4);
unordinalList.Add("Item5");
unordinalList.Insert("Item6", 0);

unordinalList.Draw();
```

Output:  
```
- Item6
- Item1
- Item2
- Item3
- Item4
- Item5
```

As you can see we have been created ```ConsoleUnorderedList``` and filled it with ```ConsoleListItem``` items. We did it in different ways. You can create separate instances of list item and also you can just pass just string to ```Add()``` method. You can do it just because ```ConsoleListItem``` have implicit operator from string to instance.  
Also you can use different method for work with that container. Meethods like ```Add(), Insert(), Clear()``` and etc.

```C#
public static implicit operator ConsoleListItem(string text)
{
    return new ConsoleListItem(text);
}
```  
Just like that.  
Also ```ConsoleUnordinalList``` class have ```MarkSign``` property that respond to mark the item. Is take string as value. Usually you will put just 1 char for that purpose. But also you may put there something else, maybe something more suitable.

### Ordinal list    

Ordinal list as you can understand from name is something that have order. Is just like Unordinal list, but is numerate items of itself.

Here is the sample for ```ConsoleOrdinalList```  
```c#
using ConsoleUIElements.Views;

ConsoleListItem item1 = "Item1";
ConsoleListItem item2 = "Item2";
ConsoleListItem item3 = new ConsoleListItem("Item3");

List<ConsoleListItem> items = new List<ConsoleListItem>()
{
    item1, item2, item3
};

ConsoleOrdinalList ordinalList = new ConsoleOrdinalList(items);

ConsoleListItem item4 = new ConsoleListItem("Item4");
ordinalList.Add(item4);
ordinalList.Add("Item5");

ordinalList.Draw();
```

Output:  
``` 
1. Item1
2. Item2
3. Item3
4. Item4
5. Item5
```
Is just similar to ```ConsoleUnorderedList``` there is no much difference between them.

### Tree View  
So now we start with something interesting thing. Is just tree view of items. Is can be useful to output some tree structure, like folders on drive. Or some nodes that have other nodes.

Here is the sample for ```ConsoleTreeView```:
```C#
using ConsoleUIElements.Views;

ConsoleTreeNode rootNode_1 = new ConsoleTreeNode("Root_1");
ConsoleTreeNode node_1_1 = new ConsoleTreeNode("Node_1_1");
ConsoleTreeNode node_1_2 = new ConsoleTreeNode("Node_1_2");
ConsoleTreeNode node_1_3 = new ConsoleTreeNode("Node_1_3");
rootNode_1.ChildNodes.Add(node_1_1);
rootNode_1.ChildNodes.Add(node_1_2);
rootNode_1.ChildNodes.Add(node_1_3);

ConsoleTreeNode rootNode_2 = new ConsoleTreeNode("Root_2");
ConsoleTreeNode node_2_1 = new ConsoleTreeNode("Node_2_1");
ConsoleTreeNode node_2_2 = new ConsoleTreeNode("Node_2_2");
ConsoleTreeNode node_2_3 = new ConsoleTreeNode("Node_2_3");
ConsoleTreeNode node_2_4 = new ConsoleTreeNode("Node_2_4");
rootNode_2.ChildNodes.Add(node_2_1);
rootNode_2.ChildNodes.Add(node_2_2);
rootNode_2.ChildNodes.Add(node_2_3);
rootNode_2.ChildNodes.Add(node_2_4);

ConsoleTreeNode node_2_3_1 = new ConsoleTreeNode("Node_2_3_1");
node_2_3.ChildNodes.Add(node_2_3_1);

ConsoleTreeNode rootNode_3 = new ConsoleTreeNode("Root_3");

List<ConsoleTreeNode> nodes = new List<ConsoleTreeNode>();  
nodes.Add(rootNode_1);
nodes.Add(rootNode_2);
nodes.Add(rootNode_3);

ConsoleTreeView treeView = new ConsoleTreeView(nodes);
treeView.Draw();
```

Output:
```
Root_1─┐
    Node_1_1
    Node_1_2
    Node_1_3
Root_2─┐
    Node_2_1
    Node_2_2
    Node_2_3─┐
        Node_2_3_1
    Node_2_4
Root_3
```
We just create nodes. And we also can add some other nodes as child nodes. And for child nodes we can add some other child nodes, and for this other child nodes we can add some others child nodes, and for this some others child nodes we can add some additional child node, and for this some additional child nodes we can... I hope you understood it.  
Note: To TreeView we put just root nodes or nodes that will drawing as first nodes.

There is nothing something i can say more about that. 

### BoxChartView  
Now we continue with something interesting and last in views section.  
Is a box chart for some statistics. It can be useful when you need to visualize some data that higher or less than others data in sequence or something.

Here is the sample for ```BoxChartView```:
``` c#
using ConsoleUIElements.Views;

ChartBox box1 = new ChartBox();
box1.Width = 3;
box1.Height = 10;
box1.BoxColor = ConsoleColor.Green;

ChartBox box2 = new ChartBox();
box2.Width = 3;
box2.Height = 5;
box2.BoxColor = ConsoleColor.Red;

ChartBox box3 = new ChartBox();
box3.Width = 3;
box3.Height = 21;
box3.BoxColor = ConsoleColor.Magenta;

List<ChartBox> boxes = new List<ChartBox>();
boxes.Add(box1);
boxes.Add(box2);
boxes.Add(box3);

BoxChartView chartView = new BoxChartView(boxes);
chartView.OffsetX = 0; // offset for drawing whole chart desk 
chartView.OffsetY = 0;
chartView.SpaceBetween = 3; // space between boxes
chartView.BackColor = ConsoleColor.Cyan;

chartView.Draw();
```  
Here we create ```ChartBox``` instances. Is our data for visualize.  
Then we create out View. And put ```IList``` of boxes to view for draw. We can specifie how we want it to be drawn. We may set offsets for X and Y axis for whole chart. ```SpaceBetween``` stay for space between boxes in chart. You can just try it for youself and see it.  
Also we can specifie background color for chart.  
Note: When view is drawing is calculates whole height to need to be drawn by highest box in sequence.


# Drawing  
So we start new and last section of this simple library.

Tools for drawing in console is located at ConsoleUIElements.Drawing  

This section contains some primitives, like lines, circle, rectangle, pixel, square. Also it provides base interface ```IConsoleShape``` to implement own shape

### Circle
Is just simple circle as in any others UI libraries but it in the console.  

Here is the sample for ```ConsoleCircle```:  
```C#
ConsoleCircle consoleCircle = new ConsoleCircle();
consoleCircle.Fill = "*";
consoleCircle.FillColor = ConsoleColor.Yellow;
consoleCircle.Radius = 10;
consoleCircle.Draw();
consoleCircle.DrawFilled();
```  
We just create instance for circle and set string for fill every 'pixel' of circle. Also it has color for draw. And it can be drawn as in just border of circle, and as full filled circle. And it will be drawn with radius 10. Also it can have offsets by x and y axis by properies.

### Rectangle  
Just rectangle

Sample for ```ConsoleRectangle```
```c#
using ConsoleUIElements.Drawing;

ConsoleRectangle rectangle = new ConsoleRectangle();
rectangle.FillColor = ConsoleColor.Yellow;
rectangle.OffsetX = 5;
rectangle.OffsetY = 5;
rectangle.Width = 10;
rectangle.Height = 15;
rectangle.Draw();
```

Here is everything simple. Just width, height, offsets for axis, and color for drawing.

### Square
Just square

Sample for ```ConsoleSquare```:  
``` c#
using ConsoleUIElements.Drawing;

ConsoleSquare square = new ConsoleSquare();
square.FillColor = ConsoleColor.Yellow;
square.OffsetX = 5;
square.OffsetY = 5;
square.SideSize = 10;
square.Draw();
```

Here just similar to rectangle. But it can have only 1 side size just like..... Square!


### ConsoleGraphics  
This is graphic class that contains everything above from drawing, but it also has some additional method.

Sample for ```ConsoleGraphics```:  
```c#
using ConsoleUIElements.Controls.Output;
using ConsoleUIElements.Drawing;

ConsoleGraphics graphics = new ConsoleGraphics();
graphics.DrawLine(0, 0, 0, 10, ConsoleColor.White, "*");
graphics.DrawLine(5, 5, 20, 20, ConsoleColor.White);

ConsoleOutput.CarriageReturn();
ConsoleOutput.CarriageReturn();
ConsoleOutput.CarriageReturn();
ConsoleOutput.CarriageReturn();
ConsoleOutput.CarriageReturn();
ConsoleOutput.CarriageReturn();

graphics.DrawSquare(10, 0, 0, ConsoleColor.Red);
ConsoleOutput.CarriageReturn();
graphics.DrawRectangle(5, 10, 0, 0, ConsoleColor.Red);

graphics.PutPixel(2, 2, ConsoleColor.Red);
graphics.PutPixel(2, 4, ConsoleColor.Red);
graphics.PutPixel(2, 6, ConsoleColor.Red);
```

It can be looks too long. But here just too much separators by carriage return. As you can see we call methods for drawing lines, squares, rectangle, pixels. Also it can draw circles and everything that exists in drawing.  
Note: we can draw line just by symbol like 0x2588(square char). But also we can draw it by string primirive that you can set as you want for draw it.


# The end.
