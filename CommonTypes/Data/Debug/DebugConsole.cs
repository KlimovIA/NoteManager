using System.Runtime.InteropServices;

namespace NoteManager.CommonTypes.Data.Debug
{
    internal static class DebugConsole
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FreeConsole();

        public static void WriteLogMessage(string? message)
        { 
            Console.WriteLine($"[{DateTime.Now}] >> {message?? " "}");
        }
    }
}
