using System.Xml.Linq;
using WinformsStoryboardVectorizer.ControlConversion.Converters;
using WinformsStoryboardVectorizer.ControlConversion.Interfaces;
using WinformsStoryboardVectorizer.Factories;
using WinformsStoryboardVectorizer.BlockControlConversion.Helpers;

using static WinformsStoryboardVectorizer.ControlConversion.Helpers.SvgConstants;

namespace WinformsStoryboardVectorizer.BlockControlConversion.Converters;
public class BlockRadioButtonConverter : ControlConverter<RadioButton> {
    protected override XElement Convert(RadioButton radioButton, ControlConverterFactory converterFactory, ControlIdGenerator controlIdGenerator) {
        XElement radioButtonSvg = new(SvgNamespace + "rect",
            new XAttribute("id", controlIdGenerator.GetNextId(radioButton.Name)),
            new XAttribute("x", 0),
            new XAttribute("y", 0),
            new XAttribute("width", radioButton.ClientRectangle.Width),
            new XAttribute("height", radioButton.ClientRectangle.Height),
            new XAttribute("stroke", BlockSvgConstants.BorderColor),
            new XAttribute("stroke-width", BlockSvgConstants.BorderThickness),
            new XAttribute("fill", BlockSvgConstants.BackgroundColor));

        string clipPathId = controlIdGenerator.GetNextId(radioButton.Name + "-clipPath");
        XElement clipGroup = new(SvgNamespace + "clipPath",
            new XAttribute("id", clipPathId),
            new XElement(SvgNamespace + "rect",
                new XAttribute("id", controlIdGenerator.GetNextId(radioButton.Name + "-innerClipRectangle")),
                new XAttribute("x", BlockSvgConstants.BorderThickness / 2f),
                new XAttribute("y", BlockSvgConstants.BorderThickness / 2f),
                new XAttribute("width", radioButton.ClientRectangle.Width - BlockSvgConstants.BorderThickness),
                new XAttribute("height", radioButton.ClientRectangle.Height - BlockSvgConstants.BorderThickness)));

        XElement radioButtonTextSvg = new(SvgNamespace + "text",
                new XAttribute("id", controlIdGenerator.GetNextId(radioButton.Name + "-text")),
                new XAttribute("x", radioButton.Width / 2f),
                new XAttribute("y", radioButton.Height / 2f + 2),
                new XAttribute("text-anchor", "middle"),
                new XAttribute("dominant-baseline", "middle"),
                new XAttribute("font-family", BlockSvgConstants.TextFont),
                new XAttribute("font-size", TextRenderer.MeasureText(radioButton.Text, new Font(BlockSvgConstants.TextFont, radioButton.Font.Size)).Height - 7),
                new XAttribute("fill", BlockSvgConstants.TextColor),
                radioButton.Text);

        return new XElement(SvgNamespace + "g", new XAttribute("transform", $"translate({radioButton.Location.X}, {radioButton.Location.Y})"), clipGroup, radioButtonSvg, radioButtonTextSvg);
    }
}
