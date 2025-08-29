using System.Diagnostics;
using System.Xml.Linq;
using WinformsStoryboardVectorizer.ControlConversion;
using WinformsStoryboardVectorizer.BlockControlConversion;
using WinformsStoryboardVectorizer.BlockControlConversion.Converters;
using WinformsStoryboardVectorizer.Factories;

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
        DefautControlConverterFactory factory = new();
        BlockControlConverterFactory blockFactory = new();

        SvgControlConverter converter = new(blockFactory);

        Form form = new Form1();

        XElement svg = converter.Convert(form, form.ClientRectangle.Width, form.ClientRectangle.Height);


        string svgText = svg.ToString();

        Debug.WriteLine(svgText);

        string reference = @"C:\Users\mgrac\Documents\repos\winforms-storyboard-vectorizer\WinformsStoryboardVectorizer\reference.svg";
        File.WriteAllText(reference, svgText);
#endif

        // return;
        Application.Run(new Form1());
    }
}