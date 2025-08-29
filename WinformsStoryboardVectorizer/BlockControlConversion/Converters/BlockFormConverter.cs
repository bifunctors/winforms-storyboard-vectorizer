using System.Xml.Linq;
using WinformsStoryboardVectorizer.BlockControlConversion.Helpers;
using WinformsStoryboardVectorizer.ControlConversion.Converters;
using WinformsStoryboardVectorizer.ControlConversion.Interfaces;
using WinformsStoryboardVectorizer.Factories;

using static WinformsStoryboardVectorizer.ControlConversion.Helpers.SvgConstants;

namespace WinformsStoryboardVectorizer.BlockControlConversion.Converters;
public class BlockFormConverter : ControlConverter<Form> {
    protected override XElement Convert(Form form, ControlConverterFactory converterFactory, ControlIdGenerator formIdGenerator) {
        XElement formSvg = new(SvgNamespace + "rect",
            new XAttribute("id", formIdGenerator.GetNextId(form.Name)),
            new XAttribute("x", 0),
            new XAttribute("y", 0),
            new XAttribute("width", form.ClientRectangle.Width),
            new XAttribute("height", form.ClientRectangle.Height),
            new XAttribute("stroke", BlockSvgConstants.BorderColor),
            new XAttribute("stroke-width", BlockSvgConstants.BorderThickness),
            new XAttribute("fill", BlockSvgConstants.BackgroundColor));

        string clipPathId = formIdGenerator.GetNextId(form.Name + "-clipPath");
        XElement clipGroup = new(SvgNamespace + "clipPath",
            new XAttribute("id", clipPathId),
            new XElement(SvgNamespace + "rect",
                new XAttribute("id", formIdGenerator.GetNextId(form.Name + "-innerClipRectangle")),
                new XAttribute("x", BlockSvgConstants.BorderThickness / 2f),
                new XAttribute("y", BlockSvgConstants.BorderThickness / 2f),
                new XAttribute("width", form.ClientRectangle.Width - BlockSvgConstants.BorderThickness),
                new XAttribute("height", form.ClientRectangle.Height - BlockSvgConstants.BorderThickness)));

        XElement childSvgs = new(SvgNamespace + "g",
            new XAttribute("x", 0),
            new XAttribute("y", 0),
            new XAttribute("clip-path", $"url(#{clipPathId})"));

        if (form.Controls.Count > 0) {
            foreach (Control childControl in form.Controls) {
                childSvgs.Add(converterFactory.GetControlConverter(childControl).Convert(childControl, converterFactory, formIdGenerator));
            }
        }

        return new XElement(SvgNamespace + "g", clipGroup, formSvg, childSvgs);
    }
}
