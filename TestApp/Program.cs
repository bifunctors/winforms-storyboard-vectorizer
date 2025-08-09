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

        string svgText = serializer.Serialize(form).Document.ToString();
        Debug.WriteLine(svgText);

        string reference = @"C:\Users\mgrac\Documents\repos\winforms-storyboard-vectorizer\WinformsStoryboardVectorizer\reference.svg";
        File.WriteAllText(reference, svgText);
#endif

        Application.Run(new Form1());
    }
}