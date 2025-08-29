using System.Xml.Linq;
using WinformsStoryboardVectorizer.ControlConversion.Converters;
using WinformsStoryboardVectorizer.ControlConversion.Interfaces;
using WinformsStoryboardVectorizer.Factories;

namespace WinformsStoryboardVectorizer.ControlConversion; 
public class SvgControlConverter {
    private ControlConverterFactory _controlConverterFactory;

    public SvgControlConverter(ControlConverterFactory controlConverterFactory) {
        _controlConverterFactory = controlConverterFactory;
    }

    public XElement Convert(Control control, int outputWidth, int outputHeight) {
        return Convert(control, new ControlIdGenerator(), outputWidth, outputHeight);
    }

    public XElement Convert(Control control, ControlIdGenerator controlIdGenerator, int outputWidth, int outputHeight) {

        XElement controlSvg = _controlConverterFactory.GetControlConverter(control).Convert(control, _controlConverterFactory, controlIdGenerator);

        XNamespace ns = "http://www.w3.org/2000/svg";
        XElement svg = new(ns + "svg",
            new XAttribute("width", outputWidth),
            new XAttribute("height",outputHeight),
            new XAttribute("version", "1.1"),
            controlSvg);

        return svg;
    }
}
