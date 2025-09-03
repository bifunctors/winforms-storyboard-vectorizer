using System.Xml.Linq;
using WinformsStoryboardVectorizer.BlockControlConversion.Helpers;
using WinformsStoryboardVectorizer.ControlConversion.Converters;
using WinformsStoryboardVectorizer.ControlConversion.Interfaces;
using WinformsStoryboardVectorizer.Factories;

using static WinformsStoryboardVectorizer.ControlConversion.Helpers.SvgConstants;

namespace WinformsStoryboardVectorizer.BlockControlConversion.Converters;
public class BlockFormConverter : ControlConverter<Form> {
    protected override XElement Convert(Form form, ControlConverterFactory converterFactory, ControlIdGenerator controlIdGenerator) {
        XElement formSvg = new(SvgNamespace + "rect",
            new XAttribute("id", controlIdGenerator.GetNextId(form.Name)),
            new XAttribute("x", 0),
            new XAttribute("y", 0),
            new XAttribute("width", form.ClientRectangle.Width),
            new XAttribute("height", form.ClientRectangle.Height),
            new XAttribute("stroke", BlockSvgConstants.BorderColor),
            new XAttribute("stroke-width", BlockSvgConstants.BorderThickness),
            new XAttribute("fill", BlockSvgConstants.BackgroundColor));

        string clipPathId = controlIdGenerator.GetNextId(form.Name + "-clipPath");
        XElement clipGroup = new(SvgNamespace + "clipPath",
            new XAttribute("id", clipPathId),
            new XElement(SvgNamespace + "rect",
                new XAttribute("id", controlIdGenerator.GetNextId(form.Name + "-innerClipRectangle")),
                new XAttribute("x", BlockSvgConstants.BorderThickness / 2f),
                new XAttribute("y", BlockSvgConstants.BorderThickness / 2f),
                new XAttribute("width", form.ClientRectangle.Width - BlockSvgConstants.BorderThickness),
                new XAttribute("height", form.ClientRectangle.Height - BlockSvgConstants.BorderThickness)));

        XElement childSvgs = new(SvgNamespace + "g",
            new XAttribute("x", 0),
            new XAttribute("y", 0),
            new XAttribute("clip-path", $"url(#{clipPathId})"));

        if (form.Controls.Count > 0) {
            foreach (XElement childControl in GetConvertedChildren(form, converterFactory, controlIdGenerator)) {
                childSvgs.Add(childControl);
            }
        }

        return new XElement(SvgNamespace + "g", clipGroup, formSvg, childSvgs);
    }
}
