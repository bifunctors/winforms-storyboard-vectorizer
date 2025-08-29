using System.Xml.Linq;
using WinformsStoryboardVectorizer.ControlConversion.Converters;
using WinformsStoryboardVectorizer.ControlConversion.Interfaces;
using WinformsStoryboardVectorizer.Factories;

using static WinformsStoryboardVectorizer.ControlConversion.Helpers.SvgConstants;

namespace WinformsStoryboardVectorizer.DefaultControlConverters.Converters;
public class PanelConverter : ControlConverter<Panel> {
    protected override XElement Convert(Panel panel, ControlConverterFactory converterFactory, ControlIdGenerator controlIdGenerator) {
        XElement panelSvg = new(SvgNamespace + "rect",
            new XAttribute("id", controlIdGenerator.GetNextId(panel.Name)),
            new XAttribute("x", 0),
            new XAttribute("y", 0),
            new XAttribute("width", panel.Width),
            new XAttribute("height", panel.Height),
            new XAttribute("fill", $"rgb({panel.BackColor.R},{panel.BackColor.G},{panel.BackColor.B})"));

        string clipPathId = controlIdGenerator.GetNextId(panel.Name + "-clipPath");
        XElement clipGroup = new(SvgNamespace + "clipPath",
            new XAttribute("id", clipPathId),
            new XElement(SvgNamespace + "rect",
                new XAttribute("id", controlIdGenerator.GetNextId(panel.Name + "-innerClipRectangle")),
                new XAttribute("x", 0),
                new XAttribute("y", 0),
                new XAttribute("width", panel.Width),
                new XAttribute("height", panel.Height)));

        XElement childSvgs = new(SvgNamespace + "g",
            new XAttribute("x", 0),
            new XAttribute("y", 0),
            new XAttribute("clip-path", $"url(#{clipPathId})"));

        foreach (Control childControl in panel.Controls) {
            childSvgs.Add(converterFactory.GetControlConverter(childControl).Convert(childControl, converterFactory, controlIdGenerator));
        }

        return new XElement(SvgNamespace + "g", new XAttribute("transform", $"translate({panel.Location.X}, {panel.Location.Y})"), clipGroup, panelSvg, childSvgs);
    }
}
