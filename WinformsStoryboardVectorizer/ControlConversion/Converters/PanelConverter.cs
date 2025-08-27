using System.Xml.Linq;
using WinformsStoryboardVectorizer.ControlConversion.Interfaces;

namespace WinformsStoryboardVectorizer.ControlConversion.Converters;
public class PanelConverter : ControlConverter<Panel> {
    protected override XElement Convert(Panel panel, ControlConverterFactory converterFactory, ControlIdGenerator controlIdGenerator) {
        XElement panelSvg = new(_svgNamespace + "rect",
            new XAttribute("id", controlIdGenerator.GetNextId(panel.Name)),
            new XAttribute("x", 0),
            new XAttribute("y", 0),
            new XAttribute("width", panel.Width),
            new XAttribute("height", panel.Height),
            new XAttribute("fill", $"rgb({panel.BackColor.R},{panel.BackColor.G},{panel.BackColor.B})"));

        string clipPathId = controlIdGenerator.GetNextId(panel.Name + "clipPath");
        XElement clipGroup = new(_svgNamespace + "clipPath",
            new XAttribute("id", clipPathId),
            new XElement(_svgNamespace + "rect",
                new XAttribute("id", controlIdGenerator.GetNextId(panel.Name + "innerClipRectangle")),
                new XAttribute("x", 0),
                new XAttribute("y", 0),
                new XAttribute("width", panel.Width),
                new XAttribute("height", panel.Height)));

        XElement childSvgs = new(_svgNamespace + "g",
            new XAttribute("x", 0),
            new XAttribute("y", 0),
            new XAttribute("clip-path", $"url(#{clipPathId})"));

        foreach (Control childControl in panel.Controls) {
            childSvgs.Add(converterFactory.GetControlConverter(childControl).Convert(childControl, converterFactory, controlIdGenerator));
        }

        return new XElement(_svgNamespace + "g", clipGroup, panelSvg, childSvgs);
    }
}
