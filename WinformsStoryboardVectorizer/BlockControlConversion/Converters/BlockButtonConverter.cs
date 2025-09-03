using System.Xml.Linq;
using WinformsStoryboardVectorizer.ControlConversion.Converters;
using WinformsStoryboardVectorizer.ControlConversion.Interfaces;
using WinformsStoryboardVectorizer.Factories;
using WinformsStoryboardVectorizer.BlockControlConversion.Helpers;

using static WinformsStoryboardVectorizer.ControlConversion.Helpers.SvgConstants;

namespace WinformsStoryboardVectorizer.BlockControlConversion.Converters;
public class BlockButtonConverter : ControlConverter<Button> {
    protected override XElement Convert(Button button, ControlConverterFactory converterFactory, ControlIdGenerator controlIdGenerator) {
        XElement buttonSvg = new(SvgNamespace + "rect",
            new XAttribute("id", controlIdGenerator.GetNextId(button.Name)),
            new XAttribute("x", 0),
            new XAttribute("y", 0),
            new XAttribute("width", button.ClientRectangle.Width),
            new XAttribute("height", button.ClientRectangle.Height),
            new XAttribute("stroke", BlockSvgConstants.BorderColor),
            new XAttribute("stroke-width", BlockSvgConstants.BorderThickness),
            new XAttribute("fill", BlockSvgConstants.BackgroundColor));

        string clipPathId = controlIdGenerator.GetNextId(button.Name + "-clipPath");
        XElement clipGroup = new(SvgNamespace + "clipPath",
            new XAttribute("id", clipPathId),
            new XElement(SvgNamespace + "rect",
                new XAttribute("id", controlIdGenerator.GetNextId(button.Name + "-innerClipRectangle")),
                new XAttribute("x", BlockSvgConstants.BorderThickness / 2f),
                new XAttribute("y", BlockSvgConstants.BorderThickness / 2f),
                new XAttribute("width", button.ClientRectangle.Width - BlockSvgConstants.BorderThickness),
                new XAttribute("height", button.ClientRectangle.Height - BlockSvgConstants.BorderThickness)));

        XElement buttonTextSvg = new(SvgNamespace + "text",
                new XAttribute("id", controlIdGenerator.GetNextId(button.Name + "-text")),
                new XAttribute("x", button.Width / 2f),
                new XAttribute("y", button.Height / 2f + BlockSvgConstants.TextOffset),
                new XAttribute("text-anchor", "middle"),
                new XAttribute("dominant-baseline", "middle"),
                new XAttribute("font-family", BlockSvgConstants.TextFont),
                new XAttribute("font-size", TextRenderer.MeasureText(button.Text, new Font(BlockSvgConstants.TextFont, button.Font.Size)).Height + BlockSvgConstants.FontSizeCorrection),
                new XAttribute("fill", BlockSvgConstants.TextColor),
                button.Text);

        return new XElement(SvgNamespace + "g", new XAttribute("transform", $"translate({button.Location.X}, {button.Location.Y})"), clipGroup, buttonSvg, buttonTextSvg);
    }
}
