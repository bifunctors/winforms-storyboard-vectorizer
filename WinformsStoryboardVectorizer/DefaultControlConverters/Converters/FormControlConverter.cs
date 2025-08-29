using System.Xml.Linq;
using WinformsStoryboardVectorizer.ControlConversion.Converters;
using WinformsStoryboardVectorizer.ControlConversion.Interfaces;
using WinformsStoryboardVectorizer.Factories;

using static WinformsStoryboardVectorizer.ControlConversion.Helpers.SvgConstants;

namespace WinformsStoryboardVectorizer.DefaultControlConverters.Converters;
public class FormControlConverter : ControlConverter<Form> {
    protected override XElement Convert(Form form, ControlConverterFactory converterFactory, ControlIdGenerator controlIdGenerator) {
        XElement formSvg = new(SvgNamespace + "rect",
            new XAttribute("id", controlIdGenerator.GetNextId(form.Name)),
            new XAttribute("x", 0),
            new XAttribute("y", 0),
            new XAttribute("width", form.ClientRectangle.Width),
            new XAttribute("height", form.ClientRectangle.Height),
            new XAttribute("fill", $"rgb({form.BackColor.R},{form.BackColor.G},{form.BackColor.B})"));

        string clipPathId = controlIdGenerator.GetNextId(form.Name + "-clipPath");
        XElement clipGroup = new(SvgNamespace + "clipPath",
            new XAttribute("id", clipPathId),
            new XElement(SvgNamespace + "rect",
                new XAttribute("id", controlIdGenerator.GetNextId(form.Name + "-innerClipRectangle")),
                new XAttribute("x", 0),
                new XAttribute("y", 0),
                new XAttribute("width", form.ClientRectangle.Width),
                new XAttribute("height", form.ClientRectangle.Height)));

        XElement childSvgs = new(SvgNamespace + "g",
            new XAttribute("x", 0),
            new XAttribute("y", 0),
            new XAttribute("clip-path", $"url(#{clipPathId})"));

        foreach (XElement childControl in GetConvertedChildren(form, converterFactory, controlIdGenerator)) {
            childSvgs.Add(childControl);
        }

        return new XElement(SvgNamespace + "g", clipGroup, formSvg, childSvgs);
    }
}

