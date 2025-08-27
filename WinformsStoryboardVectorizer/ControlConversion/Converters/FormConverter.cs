using System.Xml.Linq;
using WinformsStoryboardVectorizer.ControlConversion.Interfaces;

namespace WinformsStoryboardVectorizer.ControlConversion.Converters;
public class FormConverter : ControlConverter<Form> {
    protected override XElement Convert(Form form, ControlConverterFactory converterFactory, ControlIdGenerator controlIdGenerator) {
        XElement formSvg = new(_svgNamespace + "rect",
            new XAttribute("id", controlIdGenerator.GetNextId(form.Name)),
            new XAttribute("x", 0),
            new XAttribute("y", 0),
            new XAttribute("width", form.Width),
            new XAttribute("height", form.Height),
            new XAttribute("fill", $"rgb({form.BackColor.R},{form.BackColor.G},{form.BackColor.B})"));

        string clipPathId = controlIdGenerator.GetNextId(form.Name + "clipPath");
        XElement clipGroup = new(_svgNamespace + "clipPath",
            new XAttribute("id", clipPathId),
            new XElement(_svgNamespace + "rect",
                new XAttribute("id", controlIdGenerator.GetNextId(form.Name + "innerClipRectangle")),
                new XAttribute("x", 0),
                new XAttribute("y", 0),
                new XAttribute("width", form.Width),
                new XAttribute("height", form.Height)));

        XElement childSvgs = new(_svgNamespace + "g",
            new XAttribute("x", 0),
            new XAttribute("y", 0),
            new XAttribute("clip-path", $"url(#{clipPathId})"));

        foreach (Control childControl in form.Controls) {
            childSvgs.Add(converterFactory.GetControlConverter(childControl).Convert(childControl, converterFactory, controlIdGenerator));
        }

        return new XElement(_svgNamespace + "g", clipGroup, formSvg, childSvgs);
    }
}
