using System.Runtime.InteropServices;

namespace ConsoleUIElements;

/// <summary>
/// Imports from kernel, user32 and other windows OS dlls
/// </summary>
internal class _KernelImports
{

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("kernel32.dll")]
    internal static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

    [DllImport("kernel32.dll")]
    internal static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

    [DllImport("kernel32.dll", EntryPoint = "GetConsoleWindow", SetLastError = true)]
    public static extern IntPtr GetConsoleHandle();
}
