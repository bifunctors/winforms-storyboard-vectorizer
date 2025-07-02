using System.Diagnostics;
using WinformsStoryboardVectorizer;

namespace TestApp; 

internal static class Program {
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main() {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

#if DEBUG
        StoryboardSerializer serializer = new();
        DefaultControlsConverters converters = new(serializer);

        Form form = new Form1();

        Debug.WriteLine(serializer.Serialize(form));
#endif

        Application.Run(new Form1());
    }
}