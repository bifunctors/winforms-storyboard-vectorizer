using System.Xml.Linq;
using WinformsStoryboardVectorizer.ControlConversion.Converters;
using WinformsStoryboardVectorizer.ControlConversion.Interfaces;
using WinformsStoryboardVectorizer.Factories;
using WinformsStoryboardVectorizer.BlockControlConversion.Helpers;

using static WinformsStoryboardVectorizer.ControlConversion.Helpers.SvgConstants;

namespace WinformsStoryboardVectorizer.BlockControlConversion.Converters;
public class BlockControlConverter : ControlConverter<Control> {
    protected override XElement Convert(Control control, ControlConverterFactory converterFactory, ControlIdGenerator controlIdGenerator) {
        XElement controlSvg = new(SvgNamespace + "rect",
            new XAttribute("id", controlIdGenerator.GetNextId(control.Name)),
            new XAttribute("x", 0),
            new XAttribute("y", 0),
            new XAttribute("width", control.ClientRectangle.Width),
            new XAttribute("height", control.ClientRectangle.Height),
            new XAttribute("stroke", BlockSvgConstants.BorderColor),
            new XAttribute("stroke-width", BlockSvgConstants.BorderThickness),
            new XAttribute("fill", BlockSvgConstants.BackgroundColor));

        string clipPathId = controlIdGenerator.GetNextId(control.Name + "-clipPath");
        XElement clipGroup = new(SvgNamespace + "clipPath",
            new XAttribute("id", clipPathId),
            new XElement(SvgNamespace + "rect",
                new XAttribute("id", controlIdGenerator.GetNextId(control.Name + "-innerClipRectangle")),
                new XAttribute("x", BlockSvgConstants.BorderThickness / 2f),
                new XAttribute("y", BlockSvgConstants.BorderThickness / 2f),
                new XAttribute("width", control.ClientRectangle.Width - BlockSvgConstants.BorderThickness),
                new XAttribute("height", control.ClientRectangle.Height - BlockSvgConstants.BorderThickness)));

        XElement childSvgs = new(SvgNamespace + "g",
            new XAttribute("x", 0),
            new XAttribute("y", 0),
            new XAttribute("clip-path", $"url(#{clipPathId})"));

        if (control.Controls.Count > 0) {
            foreach (Control childControl in control.Controls) {
                childSvgs.Add(converterFactory.GetControlConverter(childControl).Convert(childControl, converterFactory, controlIdGenerator));
            }
        }

        return new XElement(SvgNamespace + "g", new XAttribute("transform", $"translate({control.Location.X}, {control.Location.Y})"), clipGroup, controlSvg, childSvgs);
    }
}
