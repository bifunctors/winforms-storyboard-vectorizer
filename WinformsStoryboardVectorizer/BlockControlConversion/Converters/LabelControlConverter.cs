using System.Xml.Linq;
using WinformsStoryboardVectorizer.ControlConversion.Converters;
using WinformsStoryboardVectorizer.ControlConversion.Interfaces;
using WinformsStoryboardVectorizer.Factories;
using WinformsStoryboardVectorizer.BlockControlConversion.Helpers;

using static WinformsStoryboardVectorizer.ControlConversion.Helpers.SvgConstants;

namespace WinformsStoryboardVectorizer.BlockControlConversion.Converters;
public class BlockLabelConverter : ControlConverter<Label> {
    protected override XElement Convert(Label label, ControlConverterFactory converterFactory, ControlIdGenerator controlIdGenerator) {
        XElement labelSvg = new(SvgNamespace + "text",
                new XAttribute("id", controlIdGenerator.GetNextId(label.Name)),
                new XAttribute("x", label.Width / 2f),
                new XAttribute("y", label.Height / 2f + BlockSvgConstants.TextOffset),
                new XAttribute("text-anchor", "middle"),
                new XAttribute("dominant-baseline", "middle"),
                new XAttribute("font-family", BlockSvgConstants.TextFont),
                new XAttribute("font-size", TextRenderer.MeasureText(label.Text, new Font(BlockSvgConstants.TextFont, label.Font.Size)).Height + BlockSvgConstants.FontSizeCorrection),
                new XAttribute("fill", BlockSvgConstants.TextColor),
                label.Text);

        string clipPathId = controlIdGenerator.GetNextId(label.Name);
        XElement clipGroup = new(SvgNamespace + "clipPath",
            new XAttribute("id", clipPathId),
            new XElement(SvgNamespace + "rect",
                new XAttribute("id", controlIdGenerator.GetNextId(label.Name + "-clipPath")),
                new XAttribute("x", BlockSvgConstants.BorderThickness),
                new XAttribute("y", BlockSvgConstants.BorderThickness),
                new XAttribute("width", label.ClientRectangle.Width),
                new XAttribute("height", label.ClientRectangle.Height)));

        return new XElement(SvgNamespace + "g", new XAttribute("transform", $"translate({label.Location.X}, {label.Location.Y})"), clipGroup, labelSvg);
    }
}
